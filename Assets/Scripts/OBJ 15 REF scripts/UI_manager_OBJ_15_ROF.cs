using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_manager_OBJ_15_ROF : MonoBehaviour
{
   public  GameObject side_buttons, flashcard, Question_Panel, message_panel,message_text,Dialouge_panel,dialougue_text,who_do_you_question, who_do_you_question_text,is_it_possible_question;
   public  Animator sideButton_anim, flashcard_anim, questionPanel_anim,messagePanel_anim;
    public Button done_button, delete_button, tutorial_yes, tutorial_no, mina, shruti, rohan, everyone;

    public objectiveManager_15_REF ObjectiveManager_15_REF;
    private void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        side_buttons = GameObject.Find("side buttons");
        flashcard = GameObject.Find("Flash card");
        Question_Panel = GameObject.Find("Question Panel");
        message_panel = GameObject.Find("Message_panel");
        message_text = GameObject.Find("message_text");
        Dialouge_panel = GameObject.Find("Dialougue Panel");
        dialougue_text = GameObject.Find("Dialougue text");
        who_do_you_question = GameObject.Find("who do you question");
        is_it_possible_question = GameObject.Find("is it possible question");
        ObjectiveManager_15_REF = FindObjectOfType<objectiveManager_15_REF>();



        done_button = GameObject.Find("done_button").GetComponent<Button>();
        delete_button = GameObject.Find("delete_button").GetComponent<Button>();
        tutorial_yes = GameObject.Find("tutorial yes").GetComponent<Button>();
        tutorial_no = GameObject.Find("tutorial no").GetComponent<Button>();
        mina = GameObject.Find("Mina_button").GetComponent<Button>();
        shruti = GameObject.Find("Shruti_button").GetComponent<Button>();
        rohan = GameObject.Find("Rohan_button").GetComponent<Button>();
        everyone = GameObject.Find("Rohan_button").GetComponent<Button>();

        delete_button.onClick.AddListener(() => ObjectiveManager_15_REF.Delete_line());
        done_button.onClick.AddListener(() => ObjectiveManager_15_REF. cut_chocolates());
        tutorial_yes.onClick.AddListener(() => ObjectiveManager_15_REF.play_tutorial());
        tutorial_no.onClick.AddListener(() => ObjectiveManager_15_REF.not_play_tutorial());
        mina.onClick.AddListener(() => ObjectiveManager_15_REF.scaffold_forwrong());
        shruti.onClick.AddListener(() => ObjectiveManager_15_REF.scaffold_forwrong());
        rohan.onClick.AddListener(() => ObjectiveManager_15_REF.scaffold_forwrong());
        everyone.onClick.AddListener(() => ObjectiveManager_15_REF. everyone_answer());



        if (side_buttons != null)
        {
            sideButton_anim = side_buttons.GetComponent<Animator>();
            side_buttons.SetActive(false);
        }

        if (flashcard != null)
        {
            flashcard_anim = flashcard.GetComponent<Animator>();
            flashcard.SetActive(false);
        }

        if (Question_Panel != null)
        {
            questionPanel_anim = Question_Panel.GetComponent<Animator>();
            Question_Panel.SetActive(false);
        }


        if (message_panel != null)
        {
            messagePanel_anim = message_panel.GetComponent<Animator>();
            message_panel.SetActive(false);
        }


        if (Dialouge_panel != null)
        {
            Dialouge_panel.SetActive(false);
        }

        who_do_you_question.SetActive(false);
        is_it_possible_question.SetActive(false);


    }

    public  void enable_panel(GameObject object_to_enable,Animator animator_of_object)
    {
        object_to_enable.SetActive(true);
        animator_of_object.Play("enable", 0);
    }

    public  void disable_panel(GameObject object_to_enable, Animator animator_of_object,float time)
    {
        Coroutine a= StartCoroutine(disable_after(object_to_enable, animator_of_object, time));
    }

    IEnumerator disable_after(GameObject object_to_enable, Animator animator_of_object, float time)
    {
        animator_of_object.Play("disable", 0);
        yield return new WaitForSeconds(time);
        object_to_enable.SetActive(false);
        
    }



    public void set_message(string message)
    {
        if (message_text != null)
        {
            message_text.GetComponent<TEXDraw>().text = message;
        }
    }
   

}
