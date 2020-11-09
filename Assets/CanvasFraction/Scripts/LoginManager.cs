using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginManager : Singleton<LoginManager>
{
    // Start is called before the first frame update
    InputField name, phoneNumber,city,password,confirmPassword;//others
    Dropdown grade, grade2, userType;//grade1
    Transform messageBox;
    Button next, msgOk;
    public bool isStudent;
    string currentGrade = "Not Applicable";
    string currentUserType = "Student";

    public delegate void DemoRegistration();
    public static event DemoRegistration OnDemoRegistration;
    void Start()
    {
        Initilize();
    }

    void Initilize()
    {
        city = transform.GetChildFromName<InputField>("InputFieldCity");
        name  = transform.GetChildFromName<InputField>("InputFieldName");
        phoneNumber = transform.GetChildFromName<InputField>("InputFieldPnone");
        //others = transform.GetChildFromName<InputField>("InputFieldOther");
        password = transform.GetChildFromName<InputField>("InputFieldPassword");
        confirmPassword = transform.GetChildFromName<InputField>("InputFieldPasswordConfirm");
        //grade1 = transform.GetChildFromName<Dropdown>("DropdownGrade1");
        grade2 = transform.GetChildFromName<Dropdown>("DropdownGrade2");
        grade = grade2;//grade1;
        userType = transform.GetChildFromName<Dropdown>("DropdownUserType");
        messageBox = transform.GetChildFromName<Transform>("Dialog");
        next = transform.GetChildFromName<Button>("Submit");
        msgOk = transform.GetChildFromName<Button>("Button");
        next.onClick.AddListener(delegate { onSaveAndNext(); });
        msgOk.onClick.AddListener(delegate { CloseDialog(); });
        grade.onValueChanged.AddListener(delegate { onGradeChange(grade.value); });
        userType.onValueChanged.AddListener(delegate { onUserTypeChange(userType.value); });
        isStudent = false;
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
    void setGradeForStudent()
    {
        isStudent = true;
        grade2.gameObject.SetActive(true);
        //grade1.gameObject.SetActive(false);
        grade = grade2;
        
    }
    void setGradeForOther()
    {
        isStudent = false;
        //grade1.gameObject.SetActive(true);
        grade2.gameObject.SetActive(false);
        grade = grade2;// grade1;
    }

    void onGradeChange(int grade)
    {
        if(!isStudent)
        {
            switch (grade)
            {
                case 1:
                    currentGrade = "Not Applicable";
                    break;
                case 2:
                    currentGrade = "4";
                    break;
                case 3:
                    currentGrade = "5";
                    break;
                case 4:
                    currentGrade = "6";
                    break;
                case 5:
                    currentGrade = "7";
                    break;
                case 6:
                    currentGrade = "8";
                    break;
                case 7:
                    currentGrade = "9";
                    break;
                case 8:
                    currentGrade = "10";
                    break;
            }
            

        }
       else
        {
            switch (grade)
            {
                
                case 1:
                    currentGrade = "4";
                    break;
                case 2:
                    currentGrade = "5";
                    break;
                case 3:
                    currentGrade = "6";
                    break;
                case 4:
                    currentGrade = "7";
                    break;
                case 5:
                    currentGrade = "8";
                    break;
                case 6:
                    currentGrade = "9";
                    break;
                case 7:
                    currentGrade = "10";
                    break;
            }
        }
    }
    void onUserTypeChange(int userType)
    {
       // others.gameObject.SetActive(false);
        Debug.Log("user = " + userType);
        switch (userType)
        {
            case 0:
                currentUserType = "Student";
                
                setGradeForStudent();
                break;
            case 1:
                currentUserType = "Parent";
                
                setGradeForOther();
                break;
            case 2:
                currentUserType = "Teacher";
                
                setGradeForOther();
                break;
            case 3:
                //others.gameObject.SetActive(true);
                
                setGradeForOther();
                currentUserType = "Other";
                
                break;
            
        }
        Debug.Log("current user = " + currentUserType);
    }
    void onSaveAndNext()
    {
        if (name.text.Length > 0 && phoneNumber.text.Length == 10 && city.text.Length > 0 &&(string.Equals(password.text,confirmPassword.text)))
        {
            if(currentUserType=="Other")
            {
                //currentUserType = others.text;
                PlayerPrefs.SetString(UtilityArtifacts.UserName, name.text);
                PlayerPrefs.SetString(UtilityArtifacts.UserPhoneNumber, phoneNumber.text);
                PlayerPrefs.SetString(UtilityArtifacts.UserGrade, currentGrade);
                PlayerPrefs.SetString(UtilityArtifacts.UserType, currentUserType);
                PlayerPrefs.SetString(UtilityArtifacts.UserPassword, password.text);
                PlayerPrefs.SetString(UtilityArtifacts.UserCity, city.text);
                //LoadFeedBack();
                //UserLogin();
            }
            else
            {
                PlayerPrefs.SetString(UtilityArtifacts.UserName, name.text);
                PlayerPrefs.SetString(UtilityArtifacts.UserPhoneNumber, phoneNumber.text);
                PlayerPrefs.SetString(UtilityArtifacts.UserGrade, currentGrade);
                PlayerPrefs.SetString(UtilityArtifacts.UserType, currentUserType);
                PlayerPrefs.SetString(UtilityArtifacts.UserPassword, password.text);
                PlayerPrefs.SetString(UtilityArtifacts.UserCity, city.text);
                //UserLogin();
                //LoadFeedBack();
            }
            if (OnDemoRegistration != null)
                OnDemoRegistration();
            Debug.Log("user set = " + PlayerPrefs.GetString(UtilityArtifacts.UserType));
        }
        else
        {
            showDialogMsg();
        }
    }

    public void UserLogin()
    {
        SceneManager.LoadScene(2);
    }

    void LoadFeedBack()
    {
        //SceneManager.LoadScene(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Update is called once per frame

}
