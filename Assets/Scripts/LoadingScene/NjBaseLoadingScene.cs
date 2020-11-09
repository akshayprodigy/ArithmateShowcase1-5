using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NjBaseLoadingScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("loadBaseScene", 2);
    }

    public void loadBaseScene()
    {
        SceneManager.LoadScene(UtilityArtifacts.NJLoadScene);
    }
}
