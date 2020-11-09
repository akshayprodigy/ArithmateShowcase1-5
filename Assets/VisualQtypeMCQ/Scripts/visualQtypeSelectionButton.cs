using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTypeSnapImageQuestion
{
    public bool textTypeQuestion = true;
    public string heading;
    public string question;
    public string SnapImageObjectName;
    public bool inputTypeAnswer = true;
    public int[] inputValues;//0 for nf,1 for numerator,2 for denominator if only fraction 0 for numerator,1 for denominator
    //public List<SetQtypeSelectionButton> qption;
}

public class QTypeMCQQuestion
{
    public bool textTypeQuestion = true;
    public string heading;
    public string question;
    public string imageName;
    public bool inputTypeAnswer = false;
    public int[] inputValues;//0 for nf,1 for numerator,2 for denominator if only fraction 0 for numerator,1 for denominator
    public List<SetQtypeSelectionButton> qption;
}

public class SetQtypeSelectionButton
{
   
    public string imageLocation;
    public string answerOption;
    public bool answer = false;
}

public class visualQtypeSelectionButton : MonoBehaviour
{
    // Start is called before the first frame update
    Text AnsText;
    Image btImage,highLightImage;
    public bool isSelected;
    Button button;
    public bool slice = true;

    void Start()
    {
        initilize();
    }

    void initilize()
    {
        btImage = GetComponent<Image>();
        highLightImage = transform.GetChildFromName<Image>("Checkmark");
        button = GetComponent<Button>();
        button.onClick.AddListener(() => OnAnswerClick());
        isSelected = false;
        highLightImage.gameObject.SetActive(false);
        AnsText = transform.GetChildFromName<Text>("TextValue");
        //AnsText.gameObject.SetActive(false);
    }


    void OnAnswerClick()
    {
        isSelected = !isSelected;
        if(isSelected)
            highLightImage.gameObject.SetActive(true);
        else
            highLightImage.gameObject.SetActive(false);
    }

    public void ResetButton()
    {
        isSelected = false;
        highLightImage.gameObject.SetActive(false);
    }
    public void setText(string msg)
    {
        if(AnsText == null)
            AnsText = transform.GetChildFromName<Text>("TextValue");
        AnsText.text = msg;
        AnsText.gameObject.SetActive(true);
    }

    public void setImage(string img)
    {
        if(AnsText!=null)
            AnsText.gameObject.SetActive(false);
        if (btImage==null)
            btImage = GetComponent<Image>();
        btImage.sprite = Resources.Load<Sprite>(img);
        if(slice)
            btImage.type = Image.Type.Sliced;
    }
        
    


}
