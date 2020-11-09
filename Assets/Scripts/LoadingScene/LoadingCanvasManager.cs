using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingCanvasManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject btController;

    // Update is called once per frame
   public void SendMessageToReactnative()
    {
        CloseAllButton();
        LoadingSceneManager.instance.loadScene("https://arithmate.com/downloads/AssetBundles/obj1", 1);//   https://arithmate.com/downloads/AssetBundles/newobj7spin
        //LoadingSceneManager.instance.sendDatatoRN();
    }

    public void LoadSecondScen()
    {
        CloseAllButton();
        LoadingSceneManager.instance.loadScene("https://arithmate.com/downloads/AssetBundles/obj2scene", 1);//   https://arithmate.com/downloads/AssetBundles/newfairscene
        //LoadingSceneManager.instance.sendDatatoRN();
    }

    public void LoadScenefromserver(string _url)
    {
        CloseAllButton();
        LoadingSceneManager.instance.loadScene(_url, 1);
    }

    public void CloseAllButton()
    {
        btController.SetActive(false);
    }

    private void Start()
    {
        if (UtilityArtifacts.GameQuit)
        {
            Screen.orientation = ScreenOrientation.Portrait;
           
        }
        else
        {
            Screen.orientation = ScreenOrientation.Landscape;
        }
        if (UtilityArtifacts.UnityMsg != null)
        {
            LoadingSceneManager.instance.sendDatatoRN();
        }
        else
        {
            LoadingSceneManager.instance.sendDatatoRNonStart();
        }
    }
}
