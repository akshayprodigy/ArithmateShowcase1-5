using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class OBJ_19_Misconception : MonoBehaviour
{
    string jsonFileName = "OBJ_19_Misconception.json";
    public GameObject click_here_popup, notepad_panel,notepad_text, message_panel, message_text, what_do_you_question, plate1b4, plate4b16, can_you_tell_question, side_buttons, flashcard, Dialouge_panel, dialougue_text, Background,cookies_to_cut_1b4,cookies_to_cut4b16,background_2d, cookies_to_cut_1b4_post, cookies_to_cut4b16_post, background_2d_post;

    public Button customer1_button, customer2_button, both_button, cant_tell_button, notepad_ok_button;


    private void OnEnable()
    {
        timeline_new.OnEventCalled += EventToHandle;
    }
    private void OnDisable()
    {
        timeline_new.OnEventCalled -= EventToHandle;
    }

    void Start()
    {
        Initialization();
        Invoke("audio_invoke", 3.0f);
    }
    void audio_invoke()
    {
        Debug.Log(EVENT.SetUpTimeLine);
        EventManager.setNameOfJasonFile(jsonFileName);
        EventManager.Broadcast(EVENT.SetUpTimeLine);
    }

    void Initialization()
    {
        click_here_popup = GameObject.Find("click here for order popup");
        notepad_panel = GameObject.Find("notepad ui");
        notepad_text = GameObject.Find("notepad_text");
        message_panel = GameObject.Find("Message_panel");
        message_text = GameObject.Find("message_text");
        what_do_you_question = GameObject.Find("what do you question");
        plate1b4 = GameObject.Find("plate 1 by 4");
        plate4b16 = GameObject.Find("plate 4 by 16");
        can_you_tell_question = GameObject.Find("can_you_tell_question");
        side_buttons = GameObject.Find("side buttons");
        flashcard = GameObject.Find("Flash card");
        Dialouge_panel = GameObject.Find("Dialougue Panel");
        dialougue_text = GameObject.Find("Dialougue text");
        Background = GameObject.Find("Background");
        cookies_to_cut_1b4 = GameObject.Find("Cookie_to_cut 1by 4");
        cookies_to_cut4b16 = GameObject.Find("Cookie_to_cut  4 by 16");
        background_2d = GameObject.Find("Background 2d");
        cookies_to_cut4b16_post= GameObject.Find("Cookie_to_cut  4by16 post explain");
        cookies_to_cut_1b4_post = GameObject.Find("Cookie_to_cut 1by4 post explain");
        background_2d_post = GameObject.Find("Background 2d post explain");



        customer1_button = GameObject.Find("customer1_button").GetComponent<Button>();
        customer2_button = GameObject.Find("customer2_button").GetComponent<Button>();
        both_button = GameObject.Find("both_button").GetComponent<Button>(); 
        cant_tell_button= GameObject.Find("cant_tell_button").GetComponent<Button>();
        notepad_ok_button = GameObject.Find("notepad_ok_button").GetComponent<Button>();

       customer1_button.onClick.AddListener(() =>scaffold_for_wrong());
        customer2_button.onClick.AddListener(() => scaffold_for_wrong());
        both_button.onClick.AddListener(() => well_done_completed());
        cant_tell_button.onClick.AddListener(() => scaffold_for_wrong());
        notepad_ok_button.onClick.AddListener(()=>notepad_ok_button_method());

        cookies_to_cut_1b4.SetActive(false);
        cookies_to_cut4b16.SetActive(false);
        Background.SetActive(false);
        click_here_popup.SetActive(false);
        notepad_panel.SetActive(false);
        notepad_text.SetActive(false);
        message_panel.SetActive(false);
        background_2d.SetActive(false);
        what_do_you_question.SetActive(false);
        plate1b4.SetActive(false);
        plate4b16.SetActive(false);
        can_you_tell_question.SetActive(false);
        side_buttons.SetActive(false);
        flashcard.SetActive(false);
        Dialouge_panel.SetActive(false);

        background_2d_post.SetActive(false);
        cookies_to_cut_1b4_post.SetActive(false);
        cookies_to_cut4b16_post.SetActive(false);
     
        
    }

    void scaffold_for_wrong()
    {
        FindObjectOfType<timeline_new>().load_next();
    }

    public void well_done_completed()
    {
        disable_panel(what_do_you_question, what_do_you_question.GetComponent<Animator>(), 0.5f);
       enable_panel(Dialouge_panel, Dialouge_panel.GetComponent<Animator>());
       dialougue_text.GetComponent<Text>().text = "Well Done";
    }
    void EventToHandle(string EventName)
    {
        switch (EventName)
        {
            case "obj19_click_here":
                enable_panel(click_here_popup, click_here_popup.GetComponent<Animator>());

                break;
            case "obj19_there_two_order_of_cookies":

                disable_panel(click_here_popup, click_here_popup.GetComponent<Animator>(), 1.0f);
                enable_panel(notepad_panel, notepad_panel.GetComponent<Animator>());
                StartCoroutine(enable_notepad_text());
                break;
            case "obj19_one_customer_wants_one_fourth":
                  enable_panel(message_panel, message_panel.GetComponent<Animator>());
                  set_message("From 1 kg pack of cookie slab a customer wants \\frac{1}{4} of the cookie slab ");
                background_2d.SetActive(true);
                cookies_to_cut_1b4.SetActive(true);
                break;

            case "obj19_one customer_wants_four_sixteen":
                cookies_to_cut4b16.SetActive(true);
                set_message(" the other customer from the other 1 kg pack wants \\frac{4}{16} of the cookie slab.");
                break;


            case "obj19_what_do_you_think":
             
                disable_panel(message_panel, message_panel.GetComponent<Animator>(), .35f);
                enable_panel(what_do_you_question, what_do_you_question.GetComponent<Animator>());
                break;
            case "obj19_Here_are_the_one_kg":
                disable_panel(what_do_you_question, what_do_you_question.GetComponent<Animator>(),0.5f);
                disable_panel(plate1b4, plate1b4.GetComponent<Animator>(), .5f);
                disable_panel(plate4b16, plate4b16.GetComponent<Animator>(), .5f);

                background_2d_post.SetActive(true);
                enable_panel(cookies_to_cut_1b4_post,cookies_to_cut_1b4_post.GetComponent<Animator>());
                enable_panel(cookies_to_cut4b16_post, cookies_to_cut4b16_post.GetComponent<Animator>());
                enable_panel(message_panel, message_panel.GetComponent<Animator>());
              
                set_message("Here are the one kg packs for cookie slabs. One is cut into 4 parts and the other is cut into 16 parts");
                break;


            case "obj19_Why_dont_you_pack":
                set_message("Why don't you pack the orders so you can understand the difference better.");
               
                break;
            case "obj19_As_you_know_the_first":
                set_message("As you know the first cookie slab denotes \\frac{1}{4} and the second denotes \\frac{4}{16}");
                break;
            case "obj19_Now_can_you_tell_if":
                disable_panel(message_panel, message_panel.GetComponent<Animator>(), .35f);
                enable_panel(can_you_tell_question, can_you_tell_question.GetComponent<Animator>());
                break;


        }
    }

    public void notepad_ok_button_method()
    {
        disable_panel(notepad_panel, notepad_panel.GetComponent<Animator>(), 1f);
        notepad_text.SetActive(false);
        FindObjectOfType<timeline_new>().load_next();
    }


    IEnumerator enable_notepad_text()
    {
        yield return new WaitForSeconds(1f);
        notepad_text.SetActive(true);
    }









    public void enable_panel(GameObject object_to_enable, Animator animator_of_object)
    {
        object_to_enable.SetActive(true);
        animator_of_object.Play("enable", 0);
    }

    public void disable_panel(GameObject object_to_enable, Animator animator_of_object, float time)
    {
        Coroutine a = StartCoroutine(disable_after(object_to_enable, animator_of_object, time));
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


    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
        {
            GameObject hit_obj = getclicked_obj_2d();
            if (hit_obj != null)
            {
                hit_obj.GetComponent<Collider2D>().enabled = false;
                if (hit_obj.name == "NOTEPAD")
                {
                    FindObjectOfType<timeline_new>().load_next();
                    
                }

                if (hit_obj.name == "1b4cookies")
                {
                    hit_obj.transform.parent.gameObject.SetActive(false);
                    enable_panel(plate1b4, plate1b4.GetComponent<Animator>());
                    FindObjectOfType<timeline_new>().load_next();

                }
                if (hit_obj.name == "4b16cookies")
                {
                    hit_obj.transform.parent.gameObject.SetActive(false);
                    background_2d.SetActive(false);
                    enable_panel(plate4b16, plate4b16.GetComponent<Animator>());
                    FindObjectOfType<timeline_new>().load_next();
                }
                if (hit_obj.name == "1b4cookies post")
                {
                    hit_obj.transform.parent.gameObject.SetActive(false);
                  
                    enable_panel(plate1b4, plate1b4.GetComponent<Animator>());
                   

                }
                if (hit_obj.name == "4b16cookies post")
                {
                    hit_obj.transform.parent.gameObject.SetActive(false);
                    background_2d_post.SetActive(false);
                    enable_panel(plate4b16, plate4b16.GetComponent<Animator>());
                    FindObjectOfType<timeline_new>().load_next();
                }
            }
        }





    }





    public GameObject getclicked_obj_2d()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;

        Vector3 screenPos = Camera.main.ScreenToWorldPoint(mousePos);

        RaycastHit2D hit = Physics2D.Raycast(screenPos, Vector2.zero);

        if (hit)
        {
            return hit.collider.gameObject;
        }
        else
        {
            return null;
        }

    }
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
