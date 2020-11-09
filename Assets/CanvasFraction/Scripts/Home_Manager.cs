using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Home_Manager : MonoBehaviour
{
    public int scene_no,i;
    public GameObject[] skip_pnl;
    void Start()
    {
        foreach (GameObject g1 in skip_pnl)
            g1.SetActive(false);
        //skip_pnl[0].SetActive(true);
    }

   public void OnClick(GameObject g)
    {
        switch(g.name)
        {
            //case "Go":
            //    //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            //    //if (PlayerPrefs.GetInt("First") == 1)
            //    //{
            //    //    UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(2);
            //    //}
            //    //if (PlayerPrefs.GetInt("First") == 2)
            //    //{
            //    //    UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(8);
            //    //}
            //    UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene_no);

            //    break;
            case "Go":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


                break;


            case "SKIP":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);

                break;
            case "Proceed":
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                SceneManager.LoadSceneAsync(1);
                break;
            case "Quit":
                PlayerPrefs.SetInt("First", 2);
                Application.Quit();

                break;
            case "CROSS":
                Debug.Log("cross clikes");
                PlayerPrefs.SetInt("First", 2);
                Application.Quit();

                break;

            //case "SKIP":
            //    //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            //    scene_no = scene_no + 1;
            //    foreach (GameObject g1 in skip_pnl)
            //        g1.SetActive(false);
            //    skip_pnl[scene_no - 1].SetActive(true);
            //    break;

            //below code is for loading objective 2
            case "Go_obj2":
                if (scene_no == 12)
                {
                    //load scene visual qtype of obj 2
                    UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(9);
                }
                else
                {
                    //load akshay scene
                    //scene_no is local variable which is used for scene indexing from this panel
                    //if you want play it from your single scene theen check the scene no as per your need like
                    //i have set scene_no by default to 6 i.e. current scene no
                    //if scene_no is 8 then you will start loading your scene from Dignostic testing
                    //if scene_no is 9 then you will start loading your scene from Concrete experience
                    //if scene_no is 10 then you will start loading your scene from active experiment
                    //if scene_no is 11 then you will start loading your scene from activity
                    //if scene_no is 12 then i have loaded the visual qtypes of obj 2 in upper if statement
                    

                }

                break;

            case "SKIP_obj2":
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                scene_no = scene_no + 1;
                foreach (GameObject g1 in skip_pnl)
                    g1.SetActive(false);
                skip_pnl[scene_no - 8].SetActive(true);
                break;
        }
    }
}
