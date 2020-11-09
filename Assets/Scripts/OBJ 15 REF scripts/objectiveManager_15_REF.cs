using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class objectiveManager_15_REF : MonoBehaviour
{

    string jsonFileName = "OBJ_15_Cookies.json";
   

    public GameObject MrXIntro, TableAndAssets, Customers, Plate1b2, Plate2b4, Plate4b8,explainationhorizontal,explaination_vertical,notepad_on_table_anim,gray_character,play_tutorial_popup,tutorial;

   
    public Animator MrXAnim;
    public UI_manager_OBJ_15_ROF UI_manager_OBJ_15_ROF;
    public List<Slice2D> result;
    public List<GameObject> Drawn_line;
    public GameObject prefab_Background, prefab_cookie, prefab_movable_line, prefab_horizontal_scale, prefab_vertical_scale, prefab_cookies_horizontal, prefab_cookies_vertical;
    public bool select_pate, instantiale_line, is_area_equal, select_parts_of_cookies;
    public List<GameObject> sliced_obj;
    public string current_cookie, initial_cut;

    public int wrong_cut_count;



    public int horizontal_line, vertical_line;




    private void OnEnable()
    {
        timeline_new.OnEventCalled += EventToHandle;
    }
    private void OnDisable()
    {
        timeline_new.OnEventCalled -= EventToHandle;
    }


    private void Start()
    {
        jsonFileName = "OBJ_15_Cookies.json";
        result = new List<Slice2D>();
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

       


        MrXIntro = GameObject.Find("Chef conversation");
        TableAndAssets = GameObject.Find("Table and assets");
        Customers = GameObject.Find("customers");
        Plate1b2 = GameObject.Find("plate 1 by 2");
        Plate2b4 = GameObject.Find("plate 2 by 4");
        Plate4b8 = GameObject.Find("playe 4 by 8");
        explainationhorizontal = GameObject.Find("Explaination horizontal");
        explaination_vertical = GameObject.Find("Explaination vertical");
        notepad_on_table_anim = GameObject.Find("notepad ui");
        gray_character = GameObject.Find("gray character");
        play_tutorial_popup = GameObject.Find("play tutorial popup");
        tutorial = GameObject.Find("tutorial_panel");
        UI_manager_OBJ_15_ROF = FindObjectOfType<UI_manager_OBJ_15_ROF>();

       



        if (MrXIntro != null)
        {
            MrXAnim = MrXIntro.GetComponent<Animator>();
        }
        MrXIntro.SetActive(false);
        TableAndAssets.SetActive(false);
        Customers.SetActive(false);
        Plate1b2.GetComponentInChildren<Collider2D>().enabled = false;
        Plate2b4.GetComponentInChildren<Collider2D>().enabled = false;
        Plate4b8.GetComponentInChildren<Collider2D>().enabled = false;
        Plate1b2.SetActive(false);
        Plate2b4.SetActive(false);
        Plate4b8.SetActive(false);
        explainationhorizontal.SetActive(false);
        explaination_vertical.SetActive(false);
        notepad_on_table_anim.SetActive(false);
        gray_character.SetActive(false);
        play_tutorial_popup.SetActive(false);
        tutorial.SetActive(false);

        current_cookie = "";
        initial_cut = "";
        TableAndAssets.SetActive(true);

        horizontal_line = 0;
        vertical_line = 0;

        select_pate = false;
       // StartCoroutine(play_scene());
        is_area_equal = false;

        wrong_cut_count = 0;

       
    }
    public IEnumerator play_scene()
    {
        yield return new WaitForSeconds(1);
        UI_manager_OBJ_15_ROF.enable_panel(MrXIntro, MrXAnim);
        yield return new WaitForSeconds(5);
        UI_manager_OBJ_15_ROF.disable_panel(MrXIntro, MrXAnim, .5f);
        yield return new WaitForSeconds(0.5f);

        UI_manager_OBJ_15_ROF.enable_panel(Plate1b2, Plate1b2.GetComponent<Animator>());
        yield return new WaitForSeconds(0.35f);
        UI_manager_OBJ_15_ROF.enable_panel(Plate2b4, Plate2b4.GetComponent<Animator>());
        yield return new WaitForSeconds(0.35f);
        UI_manager_OBJ_15_ROF.enable_panel(Plate4b8, Plate4b8.GetComponent<Animator>());
        yield return new WaitForSeconds(0.35f);

        UI_manager_OBJ_15_ROF.set_message("you have distribute equal choclates to mina,shruti and rohan. ");
        UI_manager_OBJ_15_ROF.enable_panel(UI_manager_OBJ_15_ROF.message_panel, UI_manager_OBJ_15_ROF.messagePanel_anim);
        yield return new WaitForSeconds(2);
        UI_manager_OBJ_15_ROF.enable_panel(Customers, Customers.GetComponent<Animator>());
        yield return new WaitForSeconds(3);
        UI_manager_OBJ_15_ROF.set_message(" Mina Wants half of choclate, shruti wants 2 4th of cho and rohan want 4 8th of choclate");
        yield return new WaitForSeconds(4);
        UI_manager_OBJ_15_ROF.set_message(" So go ahead and cut the chocolates by clickng on it");
        yield return new WaitForSeconds(4);
        UI_manager_OBJ_15_ROF.disable_panel(UI_manager_OBJ_15_ROF.message_panel, UI_manager_OBJ_15_ROF.messagePanel_anim, 0.5f);

        Plate1b2.GetComponentInChildren<Collider2D>().enabled = true;
        Plate2b4.GetComponentInChildren<Collider2D>().enabled = false;
        Plate4b8.GetComponentInChildren<Collider2D>().enabled = false;
        select_pate = true;

    }






    //public void select_part_of_slice()
    //{

    //}




    public void Delete_line()
    {
        if (Drawn_line.Count > 0)
        {
            Destroy(Drawn_line[Drawn_line.Count - 1]);
            Drawn_line.RemoveAt(Drawn_line.Count - 1);
        }
    }



    private void Update()
    { if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
        {
            if (instantiale_line)
            {
                GameObject hit_obj = getclicked_obj_2d();
                if (hit_obj != null)
                {
                    Debug.Log(hit_obj.name);
                    if (hit_obj.name.Equals("Moveable line"))
                    {
                        hit_obj.GetComponent<Move_line>().moving = true;
                    }
                    else
                    {
                        GameObject line = Instantiate(prefab_movable_line) as GameObject;
                        line.name = "Moveable line";

                        line.GetComponent<Move_line>().moving = true;
                        Drawn_line.Add(line);
                    }



                }
                else
                {
                    GameObject line = Instantiate(prefab_movable_line) as GameObject;
                    line.name = "Moveable line";
                    line.GetComponent<Move_line>().moving = true;
                    Drawn_line.Add(line);
                }
            }

            if (select_pate)
            {
                GameObject hit_obj = getclicked_obj_2d();
                if (hit_obj != null)
                {
                    UI_manager_OBJ_15_ROF.disable_panel(UI_manager_OBJ_15_ROF.message_panel, UI_manager_OBJ_15_ROF.messagePanel_anim,0.5f);


                    hit_obj.GetComponent<Collider2D>().enabled = false;
                    Debug.Log(hit_obj.name);
                    if (hit_obj.name.Equals("1b2"))
                    {
                        play_tutorial_popup.SetActive(true);

                        hit_obj.SetActive(false);
                        start_cut_actvity(hit_obj.transform.position);
                        select_pate = false;
                        instantiale_line = true;
                        current_cookie = "1b2";
                        horizontal_line = 0;
                        vertical_line = 0;

                    }
                    else if (hit_obj.name.Equals("2b4"))
                    {
                        hit_obj.SetActive(false);
                        start_cut_actvity(hit_obj.transform.position);
                        select_pate = false;
                        instantiale_line = true;
                        current_cookie = "2b4";
                        horizontal_line = 0;
                        vertical_line = 0;



                    }
                    else if (hit_obj.name.Equals("4b8"))
                    {
                        hit_obj.SetActive(false);
                        start_cut_actvity(hit_obj.transform.position);
                        select_pate = false;
                        instantiale_line = true;
                        current_cookie = "4b8";
                        horizontal_line = 0;
                        vertical_line = 0;


                    }
                }
            }


            if (select_parts_of_cookies)
            {
                GameObject hit_obj = getclicked_obj_2d();
                if (hit_obj != null)
                {
                    Debug.Log(hit_obj.name);
                    if (hit_obj.name.Equals("top"))
                    {
                        set_cookies_after_cut(hit_obj.name);
                    }
                    else if (hit_obj.name.Equals("bottom"))
                    {
                        set_cookies_after_cut(hit_obj.name);
                    }
                    else if (hit_obj.name.Equals("left"))
                    {
                        set_cookies_after_cut(hit_obj.name);
                    }
                    else if (hit_obj.name.Equals("right"))
                    {
                        set_cookies_after_cut(hit_obj.name);
                    }
                    hit_obj.SetActive(false);
                }
            }
        }
    }


    void start_cut_actvity(Vector3 pos)
    {
        wrong_cut_count = 0;
        UI_manager_OBJ_15_ROF.enable_panel(UI_manager_OBJ_15_ROF.side_buttons, UI_manager_OBJ_15_ROF.sideButton_anim);

        GameObject bg = Instantiate(prefab_Background) as GameObject;
        bg.name = "Background_panel";
        GameObject cookie = Instantiate(prefab_cookie) as GameObject;
        cookie.name = "Cookies";
        cookie.transform.position = pos;
        cookie.GetComponent<object_to_move>().move = true;



    }


    public void cut_chocolates()
    {
        if (initial_cut == "" && current_cookie == "1b2")
        {
            if (horizontal_line == 1 && vertical_line == 0)
            {
                initial_cut = "horizontal";
                slice_and_checkafter_count();
            }
            else if (vertical_line == 1 && horizontal_line == 0)
            {
                initial_cut = "vertical";
                slice_and_checkafter_count();
            }
            else
            {
                StartCoroutine(Validation_dialougue(false));
            }

        }
        else if (current_cookie == "2b4")
        {
            if (initial_cut == "horizontal" && horizontal_line == 1 && vertical_line == 1)
            {
                slice_and_checkafter_count();
            }
            else if (initial_cut == "horizontal" && horizontal_line == 3 && vertical_line == 0)
            {
                slice_and_checkafter_count();
            }
            else if (initial_cut == "vertical" && horizontal_line == 1 && vertical_line == 1)
            {
                slice_and_checkafter_count();
            }
            else if (initial_cut == "vertical" && horizontal_line == 0 && vertical_line == 3)
            {
                slice_and_checkafter_count();
            }
            else
            {
                StartCoroutine(Validation_dialougue(false));
            }
        }
        else if (current_cookie == "4b8")
        {
            if (initial_cut == "horizontal" && horizontal_line == 3 && vertical_line == 1)
            {
                slice_and_checkafter_count();
            }
            else if (initial_cut == "horizontal" && horizontal_line == 7 && vertical_line == 0)
            {
                slice_and_checkafter_count();
            }
            else if (initial_cut == "vertical" && horizontal_line == 1 && vertical_line == 3)
            {
                slice_and_checkafter_count();
            }
            else if (initial_cut == "vertical" && horizontal_line == 3 && vertical_line == 1)
            {
                slice_and_checkafter_count();
            }
            else
            {
                StartCoroutine(Validation_dialougue(false));
            }
        }


    }

    void slice_and_checkafter_count()
    {
        var d = GameObject.FindObjectsOfType<Move_line>();
        foreach (Move_line f in d)
        {
            Debug.Log("cutting");

            Pair2D temp = new Pair2D(f.gameObject.transform.GetChild(0).position, f.gameObject.transform.GetChild(1).position);

            result = Slicer2D.LinearSliceAll(temp);

        }



        print_volumes();
    }

    void print_volumes()
    {

        List<double> parts = new List<double>();
        foreach (Slicer2D slicer in Slicer2D.GetList())
        {
            Polygon2D poly = slicer.GetPolygon().ToWorldSpace(slicer.transform);
            parts.Add(Math.Round(poly.GetArea(), 1));

            Debug.Log(Math.Round(poly.GetArea(), 1));

        }

        double[] volumes = parts.ToArray();


        for (int i = 0; i < volumes.Length; i++)
        {
            for (int j = i; j < volumes.Length; j++)
            {
                double temp = volumes[i] - volumes[j];

                if (temp < 0)
                {
                    temp = temp * -1;
                }
                Debug.Log(temp);
                if (current_cookie.Equals("4b8"))
                {
                    if (temp > 0.2f)
                    {
                        is_area_equal = false;
                        Debug.Log(is_area_equal);
                        break;
                    }
                    else
                    {

                        is_area_equal = true;
                        Debug.Log(is_area_equal);
                    }
                }
                else
                {
                    if (temp > 0.2f)
                    {
                        is_area_equal = false;
                        Debug.Log(is_area_equal);
                        break;
                    }
                    else
                    {

                        is_area_equal = true;
                        Debug.Log(is_area_equal);
                    }
                }
            }
            if (is_area_equal == false)
            {
                break;
            }
        }



        Debug.Log(Drawn_line.Count);
        instantiale_line = false;
        if (is_area_equal == true)
        {
            select_parts_of_cookies = true;
            foreach (Slicer2D slicer in Slicer2D.GetList())
            {
                Destroy(slicer.gameObject);
            }

            Debug.Log("area _is equal");
            instantiate_cookies_for_given_type(initial_cut);


        }
        else
        {
            select_parts_of_cookies = false;

            Debug.Log("not equal");
            StartCoroutine(Validation_dialougue(false));
        }


    }

    void instantiate_cookies_for_given_type(string type_of_cookies)
    {
        if (type_of_cookies.Equals("horizontal"))
        {
            GameObject cookie = Instantiate(prefab_cookies_horizontal, GameObject.Find("Cookies").transform);
        }
        else if (type_of_cookies.Equals("vertical"))
        {
            GameObject cookie = Instantiate(prefab_cookies_vertical, GameObject.Find("Cookies").transform);
        }
    }


    IEnumerator Validation_dialougue(bool eql)
    {
        Debug.Log(eql);

        UI_manager_OBJ_15_ROF.enable_panel(UI_manager_OBJ_15_ROF.Dialouge_panel, UI_manager_OBJ_15_ROF.Dialouge_panel.GetComponent<Animator>());
        if (eql)
        {

            UI_manager_OBJ_15_ROF.dialougue_text.GetComponent<Text>().text = "Well Done";
            Debug.Log("well");
            instantiale_line = false;
            is_area_equal = false;

        }
        else
        {
            wrong_cut_count = wrong_cut_count + 1;
            if (wrong_cut_count == 1)
            {
                UI_manager_OBJ_15_ROF.dialougue_text.GetComponent<Text>().text = "Why Dont you try One more time";
            }
            else if (wrong_cut_count == 2)
            {
                UI_manager_OBJ_15_ROF.dialougue_text.GetComponent<Text>().text = "In Fraction all parts should be equal. Make sure you cut the chocolate such that all parts are equal";
            }
            else if (wrong_cut_count == 3)
            {
                UI_manager_OBJ_15_ROF.dialougue_text.GetComponent<Text>().text = "Show flashcard";
                wrong_cut_count = 0;
            }



            Debug.Log("sorry");
            is_area_equal = false;
        }

        yield return new WaitForSeconds(4);
        UI_manager_OBJ_15_ROF.disable_panel(UI_manager_OBJ_15_ROF.Dialouge_panel, UI_manager_OBJ_15_ROF.Dialouge_panel.GetComponent<Animator>(), .5f);

        if (eql)
        {

            Destroy(GameObject.Find("Background_panel"));
            Destroy(GameObject.Find("Cookies"));
            for (int i = 0; i < Drawn_line.Count; i++)
            {
                Destroy(Drawn_line[i]);

            }
            Drawn_line = new List<GameObject>();
            if (current_cookie.Equals("1b2"))
            {
                Plate1b2.transform.GetChild(0).gameObject.SetActive(true);
                Plate1b2.transform.GetChild(0).gameObject.GetComponent<Collider2D>().enabled = false;
                Plate2b4.transform.GetChild(0).gameObject.GetComponent<Collider2D>().enabled = true;
            }
            if (current_cookie.Equals("2b4"))
            {
                Plate2b4.transform.GetChild(0).gameObject.SetActive(true);
                Plate2b4.transform.GetChild(0).gameObject.GetComponent<Collider2D>().enabled = false;
                Plate4b8.transform.GetChild(0).gameObject.GetComponent<Collider2D>().enabled = true;
            }
            if (current_cookie.Equals("4b8"))
            {
                Plate4b8.transform.GetChild(0).gameObject.SetActive(true);
                Plate4b8.transform.GetChild(0).gameObject.GetComponent<Collider2D>().enabled = false;
            }
            select_pate = true;

            UI_manager_OBJ_15_ROF.disable_panel(UI_manager_OBJ_15_ROF.side_buttons, UI_manager_OBJ_15_ROF.sideButton_anim, .5f);
        }
        else
        {
            Debug.Log("wrong");
            for (int i = 0; i < Drawn_line.Count; i++)
            {
                Destroy(Drawn_line[i]);

            }
            Drawn_line = new List<GameObject>();
            Destroy(GameObject.Find("Cookies"));
            instantiale_line = true;
            GameObject cookie = Instantiate(prefab_cookie) as GameObject;
            cookie.name = "Cookies";
            cookie.transform.position = cookie.GetComponent<object_to_move>().desired_pos_center;
            cookie.transform.localScale = cookie.GetComponent<object_to_move>().desired_scale;
            cookie.GetComponent<object_to_move>().enabled = false;
            horizontal_line = 0;
            vertical_line = 0;
        }

    }


    void set_cookies_after_cut(string hit_part_name)
    {

        //for top bottom
        if (current_cookie.Equals("1b2") && hit_part_name.Equals("top"))
        {
            enable_pieces_ontable(Plate1b2, "top");
        }
        if (current_cookie.Equals("1b2") && hit_part_name.Equals("bottom"))
        {
            enable_pieces_ontable(Plate1b2, "bottom");
        }
        if (current_cookie.Equals("2b4") && hit_part_name.Equals("top"))
        {
            enable_pieces_ontable(Plate2b4, "top");
        }
        if (current_cookie.Equals("2b4") && hit_part_name.Equals("bottom"))
        {
            enable_pieces_ontable(Plate2b4, "bottom");
        }
        if (current_cookie.Equals("4b8") && hit_part_name.Equals("top"))
        {
            enable_pieces_ontable(Plate4b8, "top");
        }
        if (current_cookie.Equals("4b8") && hit_part_name.Equals("bottom"))
        {
            enable_pieces_ontable(Plate4b8, "bottom");
        }

        //for left right

        if (current_cookie.Equals("1b2") && hit_part_name.Equals("left"))
        {
            enable_pieces_ontable(Plate1b2, "left");
        }
        if (current_cookie.Equals("1b2") && hit_part_name.Equals("right"))
        {
            enable_pieces_ontable(Plate1b2, "right");
        }
        if (current_cookie.Equals("2b4") && hit_part_name.Equals("left"))
        {
            enable_pieces_ontable(Plate2b4, "left");
        }
        if (current_cookie.Equals("2b4") && hit_part_name.Equals("right"))
        {
            enable_pieces_ontable(Plate2b4, "right");
        }
        if (current_cookie.Equals("4b8") && hit_part_name.Equals("left"))
        {
            enable_pieces_ontable(Plate4b8, "left");
        }
        if (current_cookie.Equals("4b8") && hit_part_name.Equals("right"))
        {
            enable_pieces_ontable(Plate4b8, "right");
        }



        hit_part_name = "";

        Destroy(GameObject.Find("Background_panel"));
        Destroy(GameObject.Find("Cookies"));
        for (int i = 0; i < Drawn_line.Count; i++)
        {
            Destroy(Drawn_line[i]);

        }
        Drawn_line = new List<GameObject>();
        if (current_cookie.Equals("1b2"))
        {

            Plate1b2.transform.GetChild(0).gameObject.GetComponent<Collider2D>().enabled = false;
            Plate2b4.transform.GetChild(0).gameObject.GetComponent<Collider2D>().enabled = true;
        }
        if (current_cookie.Equals("2b4"))
        {

            Plate2b4.transform.GetChild(0).gameObject.GetComponent<Collider2D>().enabled = false;
            Plate4b8.transform.GetChild(0).gameObject.GetComponent<Collider2D>().enabled = true;
        }
        if (current_cookie.Equals("4b8"))
        {

            Plate4b8.transform.GetChild(0).gameObject.GetComponent<Collider2D>().enabled = false;
        }
        select_pate = true;
        is_area_equal = false;
        select_parts_of_cookies = false;

        UI_manager_OBJ_15_ROF.disable_panel(UI_manager_OBJ_15_ROF.side_buttons, UI_manager_OBJ_15_ROF.sideButton_anim, 0.5f);

        GameObject.FindObjectOfType<timeline_new>().load_next();
    }


    void enable_pieces_ontable(GameObject plate, string part)
    {

        int child_count = plate.transform.childCount;
        for (int i = 0; i < child_count; i++)
        {
            if (plate.transform.GetChild(i).name.Equals(part))
            {
                plate.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
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





    void EventToHandle(string EventName)
    {
        switch (EventName)
        {
            case "mrx_intro":
                UI_manager_OBJ_15_ROF.enable_panel(MrXIntro, MrXAnim);

                MrXIntro.GetComponentInChildren<Text>().text = "I am done making the desserts for the day. I can see that even you are done packing the pizzas. ";
               
                break;

            case "mrx_list_of_deserts":
                MrXIntro.GetComponentInChildren<Text>().text = "Here is a list of the desserts which you will have to pack.";
                UI_manager_OBJ_15_ROF.enable_panel(notepad_on_table_anim, notepad_on_table_anim.GetComponent<Animator>());
                break;

            case "mrx_intro_get_going":
                UI_manager_OBJ_15_ROF.disable_panel(notepad_on_table_anim, notepad_on_table_anim.GetComponent<Animator>(),1f);
                MrXIntro.GetComponentInChildren<Text>().text = "Get going ,we will open the store for the customers to come soon. While You do that I will make the apple juice.";
                break;
            case "chocolate_bars_distributed":
                StartCoroutine(chocolate_bars_distributed());
               break;

            case "between_shruti":
                UI_manager_OBJ_15_ROF.set_message("Distribute between Shruti, Mina and Rohan ");
                UI_manager_OBJ_15_ROF.enable_panel(UI_manager_OBJ_15_ROF.message_panel, UI_manager_OBJ_15_ROF.messagePanel_anim);
                UI_manager_OBJ_15_ROF.enable_panel(Customers, Customers.GetComponent<Animator>());
                break;


          
            case "breakeachchocolatebar":
                UI_manager_OBJ_15_ROF.set_message("You have to break each cookie bar such that each person gets the amount of cookie they want.");
                break;

         

            case "Give_shruti_half":
                UI_manager_OBJ_15_ROF.set_message("You have to Click on cookie to cut it and Give shruti \\frac{1}{2} of cookies");
                Plate1b2.GetComponentInChildren<Collider2D>().enabled = true;
                Plate2b4.GetComponentInChildren<Collider2D>().enabled = false;
                Plate4b8.GetComponentInChildren<Collider2D>().enabled = false;
                select_pate = true;
                break;


            case "Minahalf":
                UI_manager_OBJ_15_ROF.enable_panel(UI_manager_OBJ_15_ROF.message_panel, UI_manager_OBJ_15_ROF.messagePanel_anim);
                UI_manager_OBJ_15_ROF.set_message("now give Mina \\frac{2}{4}  of the cookies");
                break;

            case "Rohanhalf":
                UI_manager_OBJ_15_ROF.enable_panel(UI_manager_OBJ_15_ROF.message_panel, UI_manager_OBJ_15_ROF.messagePanel_anim);
                UI_manager_OBJ_15_ROF.set_message("now give Rohan \\frac{4}{8} of the cookies");
                break;

           
            case "Observechocloate":
                UI_manager_OBJ_15_ROF.enable_panel(UI_manager_OBJ_15_ROF.message_panel, UI_manager_OBJ_15_ROF.messagePanel_anim);
                UI_manager_OBJ_15_ROF.set_message("Now go ahead and Observe the amount of cookies each of them have received.");
                break;

            case "portionchocolate":

                UI_manager_OBJ_15_ROF.disable_panel(UI_manager_OBJ_15_ROF.message_panel, UI_manager_OBJ_15_ROF.messagePanel_anim,0.35f);

                gray_character.SetActive(true);
                UI_manager_OBJ_15_ROF.enable_panel(UI_manager_OBJ_15_ROF.who_do_you_question, UI_manager_OBJ_15_ROF.who_do_you_question.GetComponent<Animator>());

                break;

           
            case "fractionschocolate":
                UI_manager_OBJ_15_ROF.disable_panel(UI_manager_OBJ_15_ROF.who_do_you_question, UI_manager_OBJ_15_ROF.who_do_you_question.GetComponent<Animator>(), 0.35f);
                gray_character.SetActive(true);
                UI_manager_OBJ_15_ROF.enable_panel(UI_manager_OBJ_15_ROF.is_it_possible_question, UI_manager_OBJ_15_ROF.is_it_possible_question.GetComponent<Animator>());
                break;

           
        }


    }
    public IEnumerator chocolate_bars_distributed()
    {
        UI_manager_OBJ_15_ROF.disable_panel(MrXIntro, MrXAnim, .5f);
        yield return new WaitForSeconds(0.5f);
        UI_manager_OBJ_15_ROF.enable_panel(Plate1b2, Plate1b2.GetComponent<Animator>());
        yield return new WaitForSeconds(0.35f);
        UI_manager_OBJ_15_ROF.enable_panel(Plate2b4, Plate2b4.GetComponent<Animator>());
        yield return new WaitForSeconds(0.35f);
        UI_manager_OBJ_15_ROF.enable_panel(Plate4b8, Plate4b8.GetComponent<Animator>());
        yield return new WaitForSeconds(0.35f);
    }


    public void scaffold_forwrong()
    {
        UI_manager_OBJ_15_ROF.disable_panel(UI_manager_OBJ_15_ROF.who_do_you_question, UI_manager_OBJ_15_ROF.who_do_you_question.GetComponent<Animator>(), 0.35f);


        UI_manager_OBJ_15_ROF.enable_panel(UI_manager_OBJ_15_ROF.message_panel, UI_manager_OBJ_15_ROF.messagePanel_anim);
        UI_manager_OBJ_15_ROF.set_message("We will  now put the cookies beside each other to verify, if all the chocolates are of same size");


        if (initial_cut.Equals("horizontal"))
        {
            explainationhorizontal.SetActive(true);
        }
        if (initial_cut.Equals("vertical"))
        {
            explaination_vertical.SetActive(true);
        }
        StartCoroutine(scaffold_forwrong_coroutine());
    }
    public IEnumerator scaffold_forwrong_coroutine()
    {
        gray_character.SetActive(false);
        yield return new WaitForSeconds(10);
        explainationhorizontal.SetActive(false);
        explaination_vertical.SetActive(false);
        UI_manager_OBJ_15_ROF.disable_panel(UI_manager_OBJ_15_ROF.message_panel, UI_manager_OBJ_15_ROF.messagePanel_anim,0.35f);
        gray_character.SetActive(true);
        UI_manager_OBJ_15_ROF.enable_panel(UI_manager_OBJ_15_ROF.who_do_you_question, UI_manager_OBJ_15_ROF.who_do_you_question.GetComponent<Animator>());

    }

    public void everyone_answer()
    {
        gray_character.SetActive(false);
        GameObject.FindObjectOfType<timeline_new>().load_next();
    }

    public void play_tutorial()
    {
        tutorial.SetActive(true);
        play_tutorial_popup.SetActive(false);
        StartCoroutine(wait_for_tutorial());
    }

    public void not_play_tutorial()
    {
        play_tutorial_popup.SetActive(false);
    }

    IEnumerator wait_for_tutorial()
    {
        yield return new WaitForSeconds(20);
        tutorial.SetActive(false);
    }

    }
