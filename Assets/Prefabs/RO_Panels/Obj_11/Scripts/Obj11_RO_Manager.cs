using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Obj11_RO_Manager : MonoBehaviour
{
    public string ans;
    public List<string> ansList = new List<string>();
    public void Initiliaze()
    {
        EnableSubmitButtonRO1_Obj11();


        GetallNumberButtons();
        GetOkButton();


    }
    public void GetallNumberButtons()
    {
        foreach (GameObject b in GameObject.FindGameObjectsWithTag("Numbers"))
        {
            b.GetComponent<Button>().onClick.AddListener(InputNumbers);
        }
    }
    public void InputNumbers()
    {
        var currentEventSystem = EventSystem.current;
        var currentSelectedGameObject = currentEventSystem.currentSelectedGameObject;

        Debug.Log("numerator =" + currentSelectedGameObject.name);
        ans = currentSelectedGameObject.name;
        foreach (GameObject b in GameObject.FindGameObjectsWithTag("Numbers"))
        {
            b.transform.GetChild(2).gameObject.SetActive(false);
        }
        currentSelectedGameObject.transform.GetChild(2).gameObject.SetActive(true);


    }
    public void DisableSubmitButton()
    {

        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.SetActive(false);
    }
    public void GetOkButton()
    {
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(Done);
    }

    public void Done()
    {
        convesationDisable();

    }

    void convesationDisable()
    {
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
        GameObject.FindObjectOfType<conversationManager>().DisableDialouge();
        enableFade();
    }
    void enableFade()
    {
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("nextObjective1", 3.0f);
    }
    void nextObjective1()
    {
        GameObject enabledPanel = GameObject.FindGameObjectWithTag("RO Panel");
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");
        enabledPanel.transform.parent.gameObject.SetActive(false);
        enabledPanel.SetActive(false);
        GameObject.FindObjectOfType<timeline_new>().load_next();

    }

    public void EnableSubmitButtonRO1_Obj11()
    {
        Debug.Log("Enable for ro1");
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(Submit_RO_1);
    }
    public void Submit_RO_1()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_RO1_ans();

        }

    }
    void check_RO1_ans()
    {
        if (ans == "1")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            enableFade();


        }
        else if (ans == "2")
        {
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                ReInforse_RO_1();
            }
        }
        

    }
    void ReInforse_RO_1()
    {
        //GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("");
        //GameObject.FindObjectOfType<conversationManager>().EnableDialouge("");
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = Load_audio_n_Play_storyBoard from assets;
    }

    public void EnableSubmitButtonRO2_Obj11()
    {
        Debug.Log("Enable for ro1");
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(Submit_RO_2);
    }
    public void Submit_RO_2()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_RO2_ans();

        }

    }
    void check_RO2_ans()
    {
        if (ans == "3")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            enableFade();


        }
        else if (ans == "1")
        {
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                ReInforse_RO2_1();
            }
        }

        else if (ans == "2")
        {
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                ReInforse_RO2_2();
            }
        }

        else if (ans == "4")
        {
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                ReInforse_RO2_4();
            }
        }


    }
    void ReInforse_RO2_1()
    {
        //GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("");
        //GameObject.FindObjectOfType<conversationManager>().EnableDialouge("");
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = Load_audio_n_Play_storyBoard from assets;
    }
    void ReInforse_RO2_2()
    {
        //GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("");
        //GameObject.FindObjectOfType<conversationManager>().EnableDialouge("");
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = Load_audio_n_Play_storyBoard from assets;
    }
    void ReInforse_RO2_4()
    {
        //GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("");
        //GameObject.FindObjectOfType<conversationManager>().EnableDialouge("");
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = Load_audio_n_Play_storyBoard from assets;
    }

    public void EnableSubmitButtonRO3_Obj11()
    {
        Debug.Log("Enable for ro1");
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(Submit_RO_3);
    }
    public void Submit_RO_3()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_RO3_ans();

        }

    }
    void check_RO3_ans()
    {
        if (ans == "2")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            enableFade();


        }
        else if (ans == "1")
        {
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                ReInforse_RO3_1();
            }
        }

        else if (ans == "3")
        {
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                ReInforse_RO3_3();
            }
        }
    }
    void ReInforse_RO3_1()
    {
        //GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("");
        //GameObject.FindObjectOfType<conversationManager>().EnableDialouge("");
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = Load_audio_n_Play_storyBoard from assets;
    }
    void ReInforse_RO3_3()
    {
        //GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("");
        //GameObject.FindObjectOfType<conversationManager>().EnableDialouge("");
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = Load_audio_n_Play_storyBoard from assets;
    }

    public void EnableSubmitButtonRO4_Obj11()
    {
        Debug.Log("Enable for ro1");
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(Submit_RO_4);
    }
    public void Submit_RO_4()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_RO4_ans();

        }

    }
    void check_RO4_ans()
    {
        if (ans == "1")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            enableFade();


        }
        else if (ans == "2")
        {
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                ReInforse_RO4_2();
            }
        }

        else if (ans == "3")
        {
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                ReInforse_RO4_3();
            }
        }

        else if (ans == "4")
        {
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                ReInforse_RO4_4();
            }
        }


    }
    void ReInforse_RO4_2()
    {
        //GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("");
        //GameObject.FindObjectOfType<conversationManager>().EnableDialouge("");
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = Load_audio_n_Play_storyBoard from assets;
    }
    void ReInforse_RO4_3()
    {
        //GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("");
        //GameObject.FindObjectOfType<conversationManager>().EnableDialouge("");
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = Load_audio_n_Play_storyBoard from assets;
    }
    void ReInforse_RO4_4()
    {
        //GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("");
        //GameObject.FindObjectOfType<conversationManager>().EnableDialouge("");
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = Load_audio_n_Play_storyBoard from assets;
    }

    public void EnableSubmitButtonRO5_Obj11()
    {
        Debug.Log("Enable for ro1");
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(Submit_RO_5);
    }
    public void Submit_RO_5()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_RO5_ans();

        }

    }
    void check_RO5_ans()
    {
        if (ans == "1")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            enableFade();


        }
        else if (ans == "2")
        {
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                ReInforse_RO4_2();
            }
        }

        else if (ans == "3")
        {
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                ReInforse_RO4_3();
            }
        }

        else if (ans == "4")
        {
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                ReInforse_RO4_4();
            }
        }


    }
    void ReInforse_RO5_2()
    {
        //GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("");
        //GameObject.FindObjectOfType<conversationManager>().EnableDialouge("");
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = Load_audio_n_Play_storyBoard from assets;
    }
    void ReInforse_RO5_3()
    {
        //GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("");
        //GameObject.FindObjectOfType<conversationManager>().EnableDialouge("");
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = Load_audio_n_Play_storyBoard from assets;
    }
    void ReInforse_RO5_4()
    {
        //GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("");
        //GameObject.FindObjectOfType<conversationManager>().EnableDialouge("");
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = Load_audio_n_Play_storyBoard from assets;
    }

    public void EnableSubmitButtonRO6_Obj11()
    {
        Debug.Log("Enable for ro1");
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(Submit_RO_6);
    }
    public void Submit_RO_6()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_RO6_ans();

        }

    }
    void check_RO6_ans()
    {
        if (ans == "1")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            enableFade();


        }
        else if (ans == "2")
        {
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                ReInforse_RO6_2();
            }
        }

    }
    void ReInforse_RO6_2()
    {
        //GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("");
        //GameObject.FindObjectOfType<conversationManager>().EnableDialouge("");
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = Load_audio_n_Play_storyBoard from assets;
    }
    
}
