using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ReviewoverMamager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ButtonCollecter;

    private void Start()
    {
        string name = PlayerPrefs.GetString(UtilityArtifacts.UserName, "");
        Debug.Log("name: " + name);
        if (string.Equals(name, ""))
        {
            ButtonCollecter.SetActive(false);
            Invoke("LoadFeedBack", 5);
        }
        else
        {
            ButtonCollecter.SetActive(true);
        }
    }
    // Update is called once per frame
    public void next()
    {
        SceneManager.LoadScene(1);
        //SceneManager.LoadScene("obj 16 folding activity 1");
    }

    public void LoadFeedBack()
    {
        SceneManager.LoadScene(8);
    }
}
