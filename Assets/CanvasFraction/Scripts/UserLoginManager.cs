using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UserLoginManager : MonoBehaviour
{
    // Start is called before the first frame update
    InputField  phoneNumber, password;
    Button next, msgOk;
    Transform messageBox;
    void Start()
    {
        phoneNumber = transform.GetChildFromName<InputField>("InputFieldPnone");
        password = transform.GetChildFromName<InputField>("InputFieldPassword");
        messageBox = transform.GetChildFromName<Transform>("Dialog");
        next = transform.GetChildFromName<Button>("Submit");
        msgOk = transform.GetChildFromName<Button>("Button");
        next.onClick.AddListener(delegate { onSaveAndNext(); });
        msgOk.onClick.AddListener(delegate { CloseDialog(); });
        CloseDialog();
    }
    void showDialogMsg()
    {
        messageBox.gameObject.SetActive(true);
    }

    void CloseDialog()
    {
        messageBox.gameObject.SetActive(false);
        //others.gameObject.SetActive(false);
    }

    void onSaveAndNext()
    {
        if(string.Equals(PlayerPrefs.GetString(UtilityArtifacts.UserPhoneNumber, ""), phoneNumber.text)&& string.Equals(PlayerPrefs.GetString(UtilityArtifacts.UserPassword, ""), password.text))
        {
            SceneManager.LoadScene(3);
        }
        else
        {
            showDialogMsg();
        }
    }
    // Update is called once per frame

}
