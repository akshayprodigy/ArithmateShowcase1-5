using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    static LoadingSceneManager _instance;
    AssetBundle assetBundle;
    static List<AssetBundle> AssetList = new List<AssetBundle>();
    static List<string> AssetAddress = new List<string>();
    List<int> count = new List<int>();
    string[] scenes;
    NewIncomingMsg IncomingMsgFron;
    //SampleMsg sampleMsg;
    NewIncomingMsg incomingMsg;
    public static LoadingSceneManager instance
    {
        get
        {
            if (_instance == null)
            {
                Init();
            }
            return _instance;
        }
    }

    public static void Init()
    {
        if (_instance != null) return;
        GameObject obj = new GameObject();
        obj.name = "RnToUnityManager";
        _instance = obj.AddComponent<LoadingSceneManager>();
    }

    void Awake()
    {
        _instance = this;
        UnityMessageManager.Instance.OnRNMessage += onMessage;
        //Screen.orientation = ScreenOrientation.Landscape;
        // AssetList 
        // AssetAddress 

    }


    void onMessage(MessageHandler message)
    {
        var data = message.getData<string>();
        //Debug.Log("Unity onMessage:" + data);
        incomingMsg = JsonUtility.FromJson<NewIncomingMsg>(data);
        //sampleMsg = JsonUtility.FromJson<SampleMsg>(data);
        string assetBundelAddress = incomingMsg.user_data.asset_bundle_address + "/" + incomingMsg.user_data.objective_name;//incomingMsg.asset_bundle_address+ "/"+ incomingMsg.objective_name;
       // Debug.Log("sampleMsg: " + incomingMsg.user_data.asset_bundle_address + "  " + incomingMsg.user_data.objective_name);
        //Debug.Log("Unity assetBundelAddress: " + assetBundelAddress);
        message.send(new { CallbackTest = "Canvas Unity callback" });
        UtilityArtifacts.NJMsg = incomingMsg;
        loadScene(assetBundelAddress, 1);
    }


    public void loadScene(string sceneAddress,int _ver)
    {
        if (this != null)
        {
            ver = _ver;
            Debug.Log("Unity loadScene AssetAddress: " + AssetAddress.Count + "AssetAddress.Contains(sceneAddress): " + AssetAddress.Contains(sceneAddress) + "count: " + count.Count+"_url: "+ sceneAddress);
            StopAllCoroutines();
        
            StartCoroutine(LoadSceneFromServer(sceneAddress));
            UtilityArtifacts.GameQuit = false;
            Screen.orientation = ScreenOrientation.Landscape;
        }
       
    }
    int ver = 1;
    IEnumerator LoadSceneFromServer(string sceneAddress)
    {
        Debug.Log("LoadSceneFromServer: sceneAddress" + sceneAddress);
        string _uri = "file:///" + sceneAddress;//
        //Debug.Log("AssetAddress: " + AssetAddress.Count+ "AssetAddress.Contains(sceneAddress): "+ AssetAddress.Contains(sceneAddress)+ "count: "+ count.Count);
        Debug.Log("LoadSceneFromServer: _uri" + _uri);
        if (AssetAddress.Contains(sceneAddress))
        {
            int index = AssetAddress.FindIndex(a => a.Contains(sceneAddress));
            scenes = AssetList[index].GetAllScenePaths();
            LoadAssetBundleScene();
        }
        else
        {

            //Debug.Log("Unity _uri: " + _uri+" Straming assets "+ Application.streamingAssetsPath);
            while (!Caching.ready)
                yield return null;
            AssetAddress.Add(sceneAddress);
            //Debug.Log("AssetAddress: " + AssetAddress.Count + "AssetAddress.Contains(sceneAddress): " + AssetAddress.Contains(sceneAddress));
            using (WWW www = new WWW(_uri))
            {
                yield return www;
                if (!string.IsNullOrEmpty(www.error))
                {
                    Debug.Log(www.error);
                    yield break;
                }
                AssetList.Add(www.assetBundle);
                count.Add(1);

                scenes = AssetList[AssetList.Count-1].GetAllScenePaths();
                //obj = Instantiate(assetBundle.LoadAsset("Canvas (1)", typeof(GameObject))) as GameObject;
                LoadAssetBundleScene();
               // assetBundle.Unload(false);
                www.Dispose();
            }
        }

        //test
        ////if (assetBundle != null)
        ////{
        ////    Debug.Log("unloadAssetbundle");
        ////    assetBundle.Unload(false);
        ////    //assetBundle = null;
        ////}
        ////while (!Caching.ready)
        ////    yield return null;

        ////if (assetBundle == null)
        ////{
        ////    //var wwwNewAsset = WWW.LoadFromCacheOrDownload(_uri, 1);
        ////    using (WWW www = new WWW(_uri))
        ////    {
        ////        yield return www;
        ////        if (!string.IsNullOrEmpty(www.error))
        ////        {
        ////            Debug.Log(www.error);
        ////            yield break;
        ////        }
        ////        assetBundle = www.assetBundle;


        ////        scenes = assetBundle.GetAllScenePaths();
        ////        //obj = Instantiate(assetBundle.LoadAsset("Canvas (1)", typeof(GameObject))) as GameObject;
        ////        LoadAssetBundleScene();
        ////        assetBundle.Unload(false);
        ////        www.Dispose();


        ////    }
        ////    ////WWW www = WWW.LoadFromCacheOrDownload(_uri, ver);
        ////    ////yield return www;

        ////    ////if (!string.IsNullOrEmpty(www.error))
        ////    ////{
        ////    ////    Debug.Log(www.error);
        ////    ////    yield return null;
        ////    ////}
        ////    ////assetBundle = www.assetBundle;
        ////    ////scenes = assetBundle.GetAllScenePaths();
        ////    //////assetBundle.LoadAllAssets();
        ////    ////LoadAssetBundleScene();
        ////    ////assetBundle.Unload(false);
        ////    ////www.Dispose();
        ////}
        ////else
        ////{
        ////    Debug.Log("assetBundle not null");
        ////}
    }

    public void LoadAssetBundleScene()
    {
        string sceneNames = scenes[0];
        string sceneName = Path.GetFileNameWithoutExtension(sceneNames);
        SceneManager.LoadScene(sceneName);
    }

    public void sendDatatoRN()
    {
        UnityMessageManager.Instance.SendMessageToRN(new UnityMessage()
        {
            name = JsonUtility.ToJson( UtilityArtifacts.UnityMsg) ,
            callBack = (data) =>
            {
                Debug.Log("unityonClickCallBack:" + data);
            }
        });
    }

    public void sendDatatoRNonStart()
    {
        UnityMessageManager.Instance.SendMessageToRN(new UnityMessage()
        {
            name = "{\"sceneLoaded\": \"true\"}",
            callBack = (data) =>
            {
                Debug.Log("unityonClickCallBack:" + data);
            }
        });
    }

}
