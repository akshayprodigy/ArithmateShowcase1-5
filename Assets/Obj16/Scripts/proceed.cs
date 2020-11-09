using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class proceed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        Invoke("next", 10f);
    }

    // Update is called once per frame
    public void next()
    {
        if(string.Equals(  PlayerPrefs.GetString(UtilityArtifacts.UserPhoneNumber,""),""))
            SceneManager.LoadScene(1);
        else
            SceneManager.LoadScene(2);
        //SceneManager.LoadScene("obj 16 folding activity 1");
    }
    public void OnClick(GameObject g)
    {
        switch (g.name)
        {
            case "1":
                SceneManager.LoadScene("Obj1AppleSorting");
                break;
            case "2":
                SceneManager.LoadScene("Obj2FractionasDivision");
                break;
            case "3":
                SceneManager.LoadScene("Obj3AppleNumDenum");
                break;
            case "4":
                SceneManager.LoadScene("Obj4AreaModule");
                break;
            case "6":
                SceneManager.LoadScene("Obj6NumberLine");
                break;
            case "7":
                SceneManager.LoadScene("Obj7SpinWheel");
                break;
            case "8":
                SceneManager.LoadScene("OBJ_8_N_subscenario_2");
                break;
            case "9":
                SceneManager.LoadScene("OBJ_9_more_thenumber_of_parts");
                break;
            case "10":
                SceneManager.LoadScene("Obj10");
                break;
            case "11":
                SceneManager.LoadScene("Obj11");
                break;
            case "5":
                SceneManager.LoadScene("Obj5AppleGroup");
                break;
            case "12":
                SceneManager.LoadScene("obj_12_improper_and_mixed");
                break;
            case "13":
                SceneManager.LoadScene("obj_13_conversion_improper_to_mixed");
                break;
            case "14":
                SceneManager.LoadScene("Obj14");
                break;
            case "15":
                SceneManager.LoadScene("obj_15_new_story");
                break;
        }
    }
}
