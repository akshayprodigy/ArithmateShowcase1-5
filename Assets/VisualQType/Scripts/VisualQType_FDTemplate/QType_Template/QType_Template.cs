using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


[System.Serializable]
public class QuestionAnd_Ans
{
    //public string question;
    //public Sprite[] btnSprite;
    //public List<string> rightAns;
    
}

public class QType_Template : MonoBehaviour
{
    //public List<string> questionList;
    Text questionText;
    Text qType_Text;

    Button[] btnOptions;
    GameObject btnOptionsContainer;
    Toggle[] toggleOptions;

    GameObject hintPopup,loading,objOver;
    Text hintMsg_Text;
    Button hintOkayBtn;
    Button submitBtn;
    int questionNum;
    int cont;
    int wrongAns_count;
    int Rcont;
    GameObject templateQType_Pnl;
    bool flagBtn_1, flagBtn_2, flagBtn_3, flagBtn_4;
    bool isRightOptionSelected, isWrongOptionSelected;
    int Right = 0;
    string spritePath;

    //public QuestionAnd_Ans[] questionAnd_Ans;
    


    private void Awake()
    {

        Init();
    }

    private void Init()
    {

        loading = transform.GetChildFromName<Transform>("Loading").gameObject;
        objOver = transform.GetChildFromName<Transform>("Hint_GameOver_Panel").gameObject;
        spritePath = "QTypeSprites/FractionItsPurpose/";

        questionNum = 0;
        btnOptionsContainer = GameObject.Find("QType_Template/TemplateQType/Image_Root/Options_Container");
        btnOptions = new Button [4];
        toggleOptions = new Toggle[4];

        for (int i = 0; i < btnOptions.Length; i++)
        {
            btnOptions[i] = btnOptionsContainer.transform.GetChild(i).GetComponent<Button>();
            toggleOptions[i] = btnOptionsContainer.transform.GetChild(i).GetComponent<Button>().transform.GetChild(0).GetComponent<Toggle>();

        }

        btnOptions[0].onClick.AddListener(() => OnCheckIdOfButton(0));
        btnOptions[1].onClick.AddListener(() => OnCheckIdOfButton(1));
        btnOptions[2].onClick.AddListener(() => OnCheckIdOfButton(2));
        btnOptions[3].onClick.AddListener(() => OnCheckIdOfButton(3));


        btnOptions[0].onClick.AddListener(() => OnOptionButton("a"));
        btnOptions[1].onClick.AddListener(() => OnOptionButton("b"));
        btnOptions[2].onClick.AddListener(() => OnOptionButton("c"));
        btnOptions[3].onClick.AddListener(() => OnOptionButton("d"));

        

        templateQType_Pnl = GameObject.Find("QType_Template");
        questionText = GameObject.Find("QType_Template/TemplateQType/Image_Root/QuestionText").GetComponent<Text>();
        qType_Text = GameObject.Find("QType_Template/TemplateQType/QType_Pnl/Text").GetComponent<Text>();
        hintPopup = GameObject.Find("QType_Template/TemplateQType/Image_Root/Hint_Popup_Panel");
        hintMsg_Text = GameObject.Find("QType_Template/TemplateQType/Image_Root/Hint_Popup_Panel/Dialougue Panel/Text_Hint").GetComponent<Text>();

        hintOkayBtn = GameObject.Find("QType_Template/TemplateQType/Image_Root/Hint_Popup_Panel/Dialougue Panel/Button").GetComponent<Button>();
        hintOkayBtn.onClick.AddListener(() => OnCloseHintPopup());

        submitBtn = GameObject.Find("QType_Template/TemplateQType/Image_Root/NextBtn").GetComponent<Button>();
        submitBtn.onClick.AddListener(() => NewNext());

        

    }

    private void Start()
    {
        Invoke("Initialize", 4);

    }

    // Start is called before the first frame update
    void Initialize()
    {
        templateQType_Pnl.SetActive(true);

        for (int j = 0; j < QType_File_Creator.Instance.stry_data.list[questionNum].questionSprite_list.Count; j++)
        {
            toggleOptions[j].isOn = false;
        }

        questionText.text = QType_File_Creator.Instance.stry_data.list[questionNum].question;
        hintPopup.SetActive(false);
        wrongAns_count = 1;
        Debug.Log("Initialize: questionNum" + questionNum);
        SetOptions();
        objOver.SetActive(false);
        loading.SetActive(false);
    }


    void SetOptions()
    {

        for (int j = 0; j < btnOptions.Length; j++)
        {
            btnOptions[j].gameObject.SetActive(false);
        }

        for (int i = 0; i < QType_File_Creator.Instance.stry_data.list[questionNum].questionSprite_list.Count; i++)
        {
            btnOptions[i].gameObject.SetActive(true);
            btnOptions[i].GetComponent<Image>().sprite = Resources.Load<Sprite>(spritePath + QType_File_Creator.Instance.stry_data.list[questionNum].questionSprite_list[i]); //load from resources
        }
        
    }

    void PrintQuestion(string question)
    {
        questionText.text = question;
    }

    public void OnOptionButton(string id)
    {

      
        for (int i = 0; i < (QType_File_Creator.Instance.stry_data.list[questionNum].rightAns.Count); i++)
        {
            if (id == QType_File_Creator.Instance.stry_data.list[questionNum].rightAns[i])
            {
            //    questionAnd_Ans[questionNum].rightAns.RemoveAt(i);
                //Debug.Log("Next  !!" + questionAnd_Ans[questionNum].rightAns.Count + " Value " + id);

            }
            else
            {
                //Debug.Log("Your Ans is wrong !!" + " Value " + id);
                wrongAns_count = 0;
            }

        }

        cont = cont - 1;

        //if (questionAnd_Ans[questionNum].rightAns.Count == 0)
        //{
        //    Debug.Log("Your Ans is right !! Next Question");
        //}
        //else if (cont == 0)
        //{
        //    Debug.Log("Hint!!!");
        //    if (Rcont == questionAnd_Ans[questionNum].rightAns.Count)
        //    {
        //        Debug.Log("Fail !!");
        //    }
        //}
        //}

        //else
        //{
        //    cont = -1;

        //    //questionAnd_Ans[questionNum].rightAns = new List<string>(questionAnd_Ans[questionNum].selectedRightAns);

        //}

        //}
        //}
        //}
    }
   
    public void OnNextBtn()
    {
        
        if ((QType_File_Creator.Instance.stry_data.list[questionNum].rightAns.Count == 0))
        {
            //if (wrongAns_count == 1)
            {
                if (questionNum < 2)
                {
                    //Debug.Log("Your Ans is right !! Next Question" + questionNum);
                    questionNum++;
                    questionText.text = QType_File_Creator.Instance.stry_data.list[questionNum].question;
                    for (int i = 0; i < btnOptions.Length; i++)
                    {
                        btnOptions[i].GetComponent<Image>().sprite = Resources.Load<Sprite>(spritePath + QType_File_Creator.Instance.stry_data.list[questionNum].questionSprite_list[i]);
                        flagBtn_1 = false;
                        flagBtn_2 = false;
                        flagBtn_3 = false;
                        flagBtn_4 = false;
                        toggleOptions[i].isOn = false;

                        SetOptions();

                    }
                }

                else
                {
                    templateQType_Pnl.SetActive(false);
                    //objOver.SetActive(true);
                    GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
                    Invoke("ExitNow", 3.0f);
                }
                //RestCont();
            }
            //else
            //{
            //    OnHintPopup("Fractions are used to represent objects which are less than a whole. Check if the figures you selected are all less than a whole."); // Hint 2
            //}
            
         

        }


        else if (cont == 0)
        {
            //Debug.Log("Hint!!!");

            OnHintPopup("Fractions are used to represent objects which are less than a whole. Check if the figures you selected are all less than a whole."); // Hint 2
            if (Rcont == QType_File_Creator.Instance.stry_data.list[questionNum].rightAns.Count)
            {
                //Debug.Log("Fail !!");
            }
            //RestCont();
        }
        else if (QType_File_Creator.Instance.stry_data.list[questionNum].rightAns.Count == 1)
        {
            OnHintPopup("Check if you missed any figure which can be represented as a fraction"); // Hint 1 

        }

    }
    void ExitNow()
    {
        templateQType_Pnl.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void NewNext()
    {
        List<Toggle> ActiveToggle = new List<Toggle>();

        for (int j = 0; j < toggleOptions.Length; j++)
        {
            if (toggleOptions[j].isOn)
            {
                ActiveToggle.Add(toggleOptions[j]);
            }
        }

        //Debug.Log(ActiveToggle.Count + "Active Toggle");
        //int Wroung = 0;
        for (int k = 0; k < ActiveToggle.Count; k++)
        {
            for (int i = 0; i < QType_File_Creator.Instance.stry_data.list[questionNum].rightAns.Count; i++)
            {
                if (ActiveToggle[k].name == QType_File_Creator.Instance.stry_data.list[questionNum].rightAns[i])
                {
                    Right++;
                    //Debug.Log("$$$ " + "Right " + Right + ActiveToggle[k].name);
                    OnHintPopup("Check if you missed any figure which can be represented as a fraction"); // Hint 1 
                    Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaa");
                    isRightOptionSelected = true;
                    
                    if (ActiveToggle.Count >= 2 && Right > 0 && isWrongOptionSelected == true)
                    {
                        OnHintPopup("Fractions are used to represent objects which are less than a whole. Check if the figures you selected are all less than a whole."); // Hint 2
                    }

                }
                else if(ActiveToggle[k].name != QType_File_Creator.Instance.stry_data.list[questionNum].rightAns[i])
                {
                    
                    //Debug.Log("isWrongOptionSelected");
                    isWrongOptionSelected = true;

                }
            }
        }
        if (Right == 1 && isWrongOptionSelected == false)
        {
            OnHintPopup("Check if you missed any figure which can be represented as a fraction"); // Hint 1 

        }
        else if ((Right == 0 && isWrongOptionSelected == true) || (Right == 2 && isWrongOptionSelected == true))
        {
            OnHintPopup("Fractions are used to represent objects which are less than a whole. Check if the figures you selected are all less than a whole."); // Hint 2

        }

        if (ActiveToggle.Count != QType_File_Creator.Instance.stry_data.list[questionNum].rightAns.Count) // more or less than 2 options
        {

            if(isRightOptionSelected == true && Right >= 2)
            {
                Debug.Log("Options are missed Right"+ Right);
                OnHintPopup("Check if you missed any figure which can be represented as a fraction"); // Hint 1 

            }


            return;
        }
        if (ActiveToggle.Count == QType_File_Creator.Instance.stry_data.list[questionNum].rightAns.Count && Right == QType_File_Creator.Instance.stry_data.list[questionNum].rightAns.Count)
        {
            Debug.Log("Perfectly Right " + Right);
            if (questionNum < 2)
            {
                //Debug.Log("Your Ans is right !! Next Question" + questionNum);
                questionNum++;
                questionText.text = QType_File_Creator.Instance.stry_data.list[questionNum].question;
                hintPopup.SetActive(false);
                for (int i = 0; i < btnOptions.Length; i++)
                {
                    btnOptions[i].GetComponent<Image>().sprite = Resources.Load<Sprite>(spritePath + QType_File_Creator.Instance.stry_data.list[questionNum].questionSprite_list[i]);
                    flagBtn_1 = false;
                    flagBtn_2 = false;
                    flagBtn_3 = false;
                    flagBtn_4 = false;
                    toggleOptions[i].isOn = false;

                    SetOptions();

                }

                Right = 0;
                isRightOptionSelected = false;
                isWrongOptionSelected = false;
            }

            else
            {
                
                GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
                Invoke("ExitNow", 3.0f);
            }

        }
        else if (Right <= 1)
        {
            //if(ActiveToggle.Count < 2)
            // {
            //     return;
            // }
            //if (Right == 1)
            //{
            //    Debug.Log("Hint 1 !!!" + Right);
            //    OnHintPopup("Check if you missed any figure which can be represented as a fraction"); // Hint 1
            //}
            //else if (Right == 0)
            {
                Debug.Log("Hint 2 !!!" + Right);
                OnHintPopup("Fractions are used to represent objects which are less than a whole. Check if the figures you selected are all less than a whole."); // Hint 2
            }
           
        }

    }

    public void OnCloseHintPopup()
    {
        hintPopup.SetActive(false);
        isRightOptionSelected = false;
        isRightOptionSelected = false;
        Right = 0;

        for (int i = 0; i < btnOptions.Length; i++)
        {
            flagBtn_1 = false;
            flagBtn_2 = false;
            flagBtn_3 = false;
            flagBtn_4 = false;
            toggleOptions[i].isOn = false;
        }
            
    }

    public void OnHintPopup(string hint)
    {
        hintPopup.SetActive(true);
        hintMsg_Text.text = hint;
    }

    void RestCont()
    {
        cont = -1;
        //questionAnd_Ans[questionNum].rightAns.Count = Rcont;
    }

    public void OnCheckIdOfButton(int btnNum)
    {
        if(btnNum == 0)
        {

            if (!flagBtn_1)
            {
                flagBtn_1 = true;
                toggleOptions[btnNum].isOn = true;

            }
            else
            {
                flagBtn_1 = false;
                toggleOptions[btnNum].isOn = false;

            }

        }

        else if (btnNum == 1)
        {
            if (!flagBtn_2)
            {
                flagBtn_2 = true;
                toggleOptions[btnNum].isOn = true;
            }
            else
            {
                flagBtn_2 = false;
                toggleOptions[btnNum].isOn = false;

            }
        }

        else if (btnNum == 2)
        {
            if (!flagBtn_3)
            {
                flagBtn_3 = true;
                toggleOptions[btnNum].isOn = true;
            }
            else
            {
                flagBtn_3 = false;
                toggleOptions[btnNum].isOn = false;

            }
        }

        else if (btnNum == 3)
        {
            if (!flagBtn_4)
            {
                flagBtn_4 = true;
                toggleOptions[btnNum].isOn = true;
            }
            else
            {
                flagBtn_4 = false;
                toggleOptions[btnNum].isOn = false;

            }
        }
        
    }

}
