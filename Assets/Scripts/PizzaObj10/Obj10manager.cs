using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Obj10manager : MonoBehaviour
{
    string jsonFileName = "Obj10UnequallyPartitioned.json";
    public string ans;
    public int threePiecePizza, SixPiecePizza, eightPiecePizza;
    public bool enablePizza1Drag, enablePizza2Drag, enablePizza3Drag, correctPizzaDragged, rightcut;
    public int noOfPizzainTray1, numberOfHint, current_pizza;
    public GameObject right_prompt,equal_center_pos,unequal_center_pos, equal_pizza,unequal_pizza, Interaction_pizza_equa_unequal, translucent, none_of_the_slice, but_if_i_try_to_divide,what_we_did_is, unequal_pizza_reinforcement, this_applies,we_can_represent;
    public GameObject LoadingAudio;
    private void OnEnable()
    {
        timeline_new.OnEventCalled += EventToHandle;
        visualQtypesegmentImage.OnSnapPoint += OnSnapPointEnter;
    }

    private void OnDisable()
    {
        timeline_new.OnEventCalled -= EventToHandle;
        visualQtypesegmentImage.OnSnapPoint -= OnSnapPointEnter;
    }

    
       
   
       
   
    private void Awake()
    {
        Initialised();

    }

    void Start()
    {

        //GameObject.FindObjectOfType<conversationManager>().DisableDialouge();

    }

    void Initialised()
    {
        right_prompt = GameObject.Find("right_prompt");
        equal_center_pos = GameObject.Find("equal_center_pos");
        unequal_center_pos = GameObject.Find("unequal_center_pos");
        equal_pizza = GameObject.Find("equal_pizza");
        unequal_pizza = GameObject.Find("unequal_pizza");
        Interaction_pizza_equa_unequal = GameObject.Find("Interaction_pizza_equa_unequal");
        translucent = GameObject.Find("translucent");
        none_of_the_slice = GameObject.Find("none_of_the_slice");
        but_if_i_try_to_divide = GameObject.Find("but_if_i_try_to_divide");
        what_we_did_is = GameObject.Find("What we did is");
        unequal_pizza_reinforcement = GameObject.Find("unequal pizza reinforcement");
        this_applies = GameObject.Find("this applies");
        we_can_represent = GameObject.Find("we can represent");
        LoadingAudio = GameObject.Find("LoadAudio");
        //if (UtilityArtifacts.backTraversal)
        //{
        //    Text textLoadingText = LoadingAudio.transform.GetChild(1).GetComponent<Text>();
        //    textLoadingText.text = "Let us understand this better";
        //}
        this_applies.SetActive(false);
        we_can_represent.SetActive(false);
        none_of_the_slice.SetActive(false);
        but_if_i_try_to_divide.SetActive(false);
        what_we_did_is.SetActive(false);
        translucent.SetActive(false);
        equal_pizza.SetActive(false);
        unequal_pizza.SetActive(false);
        Interaction_pizza_equa_unequal.SetActive(false);
        right_prompt.SetActive(false);
        unequal_pizza_reinforcement.SetActive(false);
      //  StartCoroutine("enableDivision");
        Invoke("audio_invoke", 2.0f);
        //GameObject.Find("SubmitButton").transform.GetChild(0).GetComponent<Button>().onClick.AddListener(checkAnswer1);
        numberOfHint = 0;
        //GameObject.FindObjectOfType<QuestionManager>().GetOkButton();

    }

    IEnumerator enableDivision()
    {
        yield return new WaitForSeconds(1.0f);
        GameObject.Find("aa").GetComponent<TEXDraw>().text = "    3\n";
        yield return new WaitForSeconds(1.0f);
        GameObject.Find("aa").GetComponent<TEXDraw>().text = "    3\n2\\root7\n";
        yield return new WaitForSeconds(1.0f);
        GameObject.Find("aa").GetComponent<TEXDraw>().text = "    3\n2\\root7\n-  6\n";
        yield return new WaitForSeconds(1.0f);
        GameObject.Find("aa").GetComponent<TEXDraw>().text = "    3\n2\\root7\n-  6\n     \\border[03]1";

    }
    void reset_option()
    {
        foreach (GameObject b in GameObject.FindGameObjectsWithTag("Numbers"))
        {
            b.transform.GetChild(2).gameObject.SetActive(false);
        }
        ans = "";
    }


    void audio_invoke()
    {
        Debug.Log(EVENT.SetUpTimeLine);
        EventManager.setNameOfJasonFile(jsonFileName);
        EventManager.Broadcast(EVENT.SetUpTimeLine);
    }

    void HideLoadingAudio()
    {
        LoadingAudio.SetActive(false);
    }
    void EventToHandle(string EventName)
    {
        HideLoadingAudio();
        switch (EventName)
        {

            case "Obj10_You_are":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("You are doing a good job on packing the pizzas.");
                break;
            case "Obj10_Here_are":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Here are a few more pizzas.");
                enablePizzaFoprDisplay();
                break;
            case "Obj10_You_need":
                enableBoxes();
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("You need to arrange the pizzas on the counter.");
                break;
          
            case "Obj10_The_counter":
                enableFraction();
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("The counter is marked with fractions. These fractions indicate the kind of pizza the section can hold.");
                break;
            case "Obj10_place_the_pizza_in_reject":
                enableBoxes();
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Place the pizza in the reject section if it doesn't belong in any other sections");
                break;
            case "Obj10_Go_ahead":
                GameObject.FindObjectOfType<conversationManager>().DisableConversation();
                GameObject.FindObjectOfType<conversationManager>().EnableQuestion1("The size of each slice of the pizza is labelled on each section. Place the given pizza in the correct section based on the size of each slice");
               
                disablePizzaFoprDisplay();
                enableBoxes();
                enableFraction();
                enablePizza();
                if (UtilityArtifacts.coming_back_from == "to_Obj10_quest1")
                {
                    pizzaCannotBePlaced1();
                    UtilityArtifacts.coming_back_from = "";
                }
                    
                //   enabledontknowButton();
                //   GameObject.Find("I don't know").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(pizzaCannotBePlaced);
                break;
            case "Obj10_Here_is_a_pizza":
                disableAllPizzas();
                //enableInteractionPizza();
                equal_pizza.SetActive(true);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Here is a pizza where each slice is \\frac{1}{3}. All their sizes are equal.");
                break;
            case "Obj10_Even_though":
                equal_pizza.SetActive(false);
                unequal_pizza.SetActive(true);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation(" Observe this pizza. Even though it has 3 parts, we cannot say that all the slices are of the size \\frac{1}{3}");
                break;
            case "Obj10_Lets_compare_each":
                equal_pizza.SetActive(false);
                unequal_pizza.SetActive(false);
                Interaction_pizza_equa_unequal.SetActive(true);
                GameObject.FindObjectOfType<conversationManager>().DisableConversation();
                Invoke("highlight_unequal_interaction", 2);
                Invoke("unhighlight_interaction", 4);
                Invoke("highlight_equal_interaction", 4.05f);
                Invoke("unhighlight_interaction", 7);
                Invoke("enableInteractionQuestion",9);
               
                break;
            case "Obj10_None":
                translucent.SetActive(false);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("So, we cannot simply express each slice of this pizza as \\frac{1}{3} just because it has 3 parts in total.");
                none_of_the_slice.SetActive(true);
               // GameObject.Find("Interaction Pizza").transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                break;
            case "Obj10_But":
                none_of_the_slice.SetActive(false);
                but_if_i_try_to_divide.SetActive(true);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("But, now If I try to divide this pizza into equal parts, what we have is 4 equal parts.");
              //  GameObject.Find("Interaction Pizza").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
                break;
            case "Obj10_Hence_this":
                but_if_i_try_to_divide.GetComponent<Animator>().Play("move pizza in 1b4");
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Hence, this pizza can now be placed in the section of the counter that says \\frac{1}{4}. ");
                break;
            case "Obj10_What_we_did":
                but_if_i_try_to_divide.SetActive(false);
                what_we_did_is.SetActive(true);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("What we did is, we simply made an unequally cut pizza into a pizza that has equal parts.");
                break;
            case "Obj10_And_this":
                what_we_did_is.SetActive(false);
                this_applies.SetActive(true);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("And this applies to all unequally divided objects. When we have an unequally cut object");
                break;
            case "Obj10_we":
                this_applies.SetActive(false);
                we_can_represent.SetActive(true);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("we can represent the parts of the object in terms of fractions by trying to make the object have equal parts.");
                break;
            case "Obj10_Here_is_a":
                we_can_represent.SetActive(false);
                GameObject.FindObjectOfType<conversationManager>().DisableConversation();
                //GameObject.FindObjectOfType<conversationManager>().EnableConversation("Here is a circle.Find the fraction of the coloured part of the circle by making the unequal parts have equal parts.");
                GameObject.Find("Interaction Pizza").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                GameObject.Find("Interaction Pizza").transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                GameObject.Find("Interaction Pizza").transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
                GameObject.Find("Circle").transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("Circle").transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
                break;
            case "Obj10_Draw_a_line":
                GameObject.Find("Circle").transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
                GameObject.Find("Circle").transform.GetChild(0).GetChild(3).gameObject.GetComponent<Button>().onClick.AddListener(checkCircle);
                break;
            case "Obj10_RO1_quest1":
                if(UtilityArtifacts.coming_back_from == "to_Obj10_quest2")
                {
                    UtilityArtifacts.coming_back_from = "";
                    EnableRoPanel1();
                }
                //GameObject.FindObjectOfType<conversationManager>().EnableConversation("");
                break;
            case "Obj10_RO2_quest1":
                EnableRoPanel2();
                //GameObject.FindObjectOfType<conversationManager>().EnableConversation("");
                break;
            case "Obj10_RO3_quest1":
                rightcut = false;
                GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("Pizza Cutting").transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("Pizza Cutting").transform.GetChild(0).GetChild(3).gameObject.GetComponent<Button>().onClick.AddListener(checkPizza);
                //GameObject.FindObjectOfType<conversationManager>().EnableConversation("Draw a line so that the pizza gets cut into equal parts. You can click on a point and extend it to cut the pizza");
                break;
            case "Obj10_RO4_quest1":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Can you place the pizza on the correct shelf?");
                enableLastQuest();
                break;






        }

    }
    void enablePizzaFoprDisplay()
    {
        GameObject.Find("Pizza to display").transform.GetChild(0).gameObject.SetActive(true);
    }
    void disablePizzaFoprDisplay()
    {
        GameObject.Find("Pizza to display").transform.GetChild(0).gameObject.SetActive(false);
    }
    void enableFraction()
    {
        GameObject.Find("PizzaBoxes").transform.GetChild(0).GetChild(0).GetChild(3).gameObject.SetActive(true);
        GameObject.Find("PizzaBoxes").transform.GetChild(0).GetChild(1).GetChild(3).gameObject.SetActive(true);
        GameObject.Find("PizzaBoxes").transform.GetChild(0).GetChild(2).GetChild(3).gameObject.SetActive(true);
        GameObject.Find("PizzaBoxes").transform.GetChild(0).GetChild(3).GetChild(3).gameObject.SetActive(true);
        GameObject.Find("PizzaBoxes").transform.GetChild(0).GetChild(4).GetChild(3).gameObject.SetActive(true);
    }
    public void show_random()
    {
        numberOfHint = 0;
        if (GameObject.Find("Mushroom pizza").transform.GetChild(0).gameObject.activeSelf)
        {
            current_pizza = 2;
            GameObject.Find("Mushroom pizza").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("Mushroom pizza").transform.GetChild(1).gameObject.SetActive(true);

        }
        else if (GameObject.Find("Mushroom pizza").transform.GetChild(1).gameObject.activeSelf)
        {
            current_pizza = 3;
            GameObject.Find("Mushroom pizza").transform.GetChild(1).gameObject.SetActive(false);
            GameObject.Find("Mushroom pizza").transform.GetChild(2).gameObject.SetActive(true);
        }
        else if (GameObject.Find("Mushroom pizza").transform.GetChild(2).gameObject.activeSelf)
        {
            current_pizza = 4;
            GameObject.Find("Mushroom pizza").transform.GetChild(2).gameObject.SetActive(false);
            GameObject.Find("Mushroom pizza").transform.GetChild(3).gameObject.SetActive(true);
        }
        else if (GameObject.Find("Mushroom pizza").transform.GetChild(3).gameObject.activeSelf)
        {
            GameObject.Find("PizzaBoxes").transform.GetChild(0).GetChild(0).name = "4";
            GameObject.Find("PizzaBoxes").transform.GetChild(0).GetChild(1).name = "5";
            GameObject.Find("PizzaBoxes").transform.GetChild(0).GetChild(2).name = "2";
            GameObject.Find("PizzaBoxes").transform.GetChild(0).GetChild(3).name = "3";
            GameObject.Find("PizzaBoxes").transform.GetChild(0).GetChild(4).name = "unequal";
            current_pizza = 5;
            GameObject.Find("Mushroom pizza").transform.GetChild(3).gameObject.SetActive(false);
            GameObject.Find("Mushroom pizza").transform.GetChild(4).gameObject.SetActive(true);
        }
        GameObject.FindObjectOfType<GameManager>().isObj10On = true;
    }

    
    void enablePizza()
    {
        current_pizza = 1;
        GameObject.Find("Mushroom pizza").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.FindObjectOfType<GameManager>().isObj10On = true;
        // EnableSubmitButton();
        // GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(checkAnswer1);
    }
    void disablePizza()
    {
        GameObject.Find("Mushroom pizza").transform.GetChild(0).gameObject.SetActive(false);
        DisableSubmitButton();
    }
    void enabledontknowButton()
    {
        GameObject.Find("I don't know").transform.GetChild(0).gameObject.SetActive(true);

    }
    void disabledontknowButton()
    {
        GameObject.Find("I don't know").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("I don't know").transform.GetChild(0).gameObject.SetActive(false);

    }
    void pizzaCannotBePlaced()
    {
        reInforcementPizzaCannotbePlaced();

    }
    public void pizzaCannotBePlaced1()
    {
        EnableRoPanel();

    }
    public void EnableSubmitButton()
    {
        GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.SetActive(true);
    }
    public void DisableSubmitButton()
    {
        GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
    }
    void disableHint()
    {
        GameObject.FindObjectOfType<Obj10Drag>().resetAllPizza();
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
    }
    public void reInforcementPizzaCannotbePlaced()
    {
        GameObject.FindObjectOfType<GameManager>().isObj10On = false;
        GameObject.FindObjectOfType<conversationManager>().EnableConversation(" Place the pizza such that all the slices in the pizza matches the fraction mentioned on the section. ");
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj10_Quest1_Hint1.wav");
        Invoke("disableHint", 9f);
    }

    void enablePizza1()
    {

        GameObject.Find("Mushroom pizza").transform.GetChild(1).gameObject.SetActive(true);
        GameObject.FindObjectOfType<GameManager>().isObj10On = true;
        EnableSubmitButton();
        GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(checkAnswer2);

    }

    void disablePizza1()
    {

        GameObject.Find("Mushroom pizza").transform.GetChild(1).gameObject.SetActive(false);
        
        DisableSubmitButton();
    }
    void enablePizza2()
    {

        GameObject.Find("Mushroom pizza").transform.GetChild(2).gameObject.SetActive(true);
        GameObject.FindObjectOfType<GameManager>().isObj10On = true;
        EnableSubmitButton();
        GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(checkAnswer3);
        correctPizzaDragged = false;
    }

    void disablePizza2()
    {

        GameObject.Find("Mushroom pizza").transform.GetChild(2).gameObject.SetActive(false);
        DisableSubmitButton();
    }
    void enableBoxes()
    {
        GameObject.Find("PizzaBoxes").transform.GetChild(0).gameObject.SetActive(true);
    }
    void disableBoxes()
    {
        GameObject.Find("PizzaBoxes").transform.GetChild(0).gameObject.SetActive(false);
    }
    void calculateApplesInEachTray()
    {
        noOfPizzainTray1 = 0;

        foreach (GameObject t3a3 in GameObject.FindGameObjectsWithTag("biggest_slice"))
            noOfPizzainTray1 = noOfPizzainTray1 + 1;
    }

    void enable_right_prompt()
    {
        GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
        right_prompt.SetActive(true);
    }
    void disable_right_prompt()
    {
        right_prompt.SetActive(false);
    }
   public void checkAnswer1()
    {
        calculateApplesInEachTray();
        Debug.Log("Click on answer 1");
        if (correctPizzaDragged)
        {
            //GameObject.FindObjectOfType<Obj10Drag>().resetAllPizza();
           // disablePizza();
           // enablePizza1();
            disabledontknowButton();
            Invoke("enable_right_prompt", 0);
            Invoke("disable_right_prompt", 2);

            Invoke("show_random", 2);
            //enabledontknowButton();
           // GameObject.Find("I don't know").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(pizzaCannotBePlaced1);
          //  GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(Done);
        }
        else
        {
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
            if (numberOfHint < 1)
            {
                Obj10_ReinfoHint1();
                numberOfHint = numberOfHint + 1;
            }
            else
            {
                Obj10_ReinfoHint2();
                numberOfHint = 0;
            }
        }
    }
   public  void Obj10_ReinfoHint1()
    {
        GameObject.FindObjectOfType<GameManager>().isObj10On = false;
        GameObject.FindObjectOfType<conversationManager>().EnableConversation(" Place the pizza such that all the slices in the pizza matches the fraction mentioned on the section.");
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj10_Quest1_Hint1.wav");
        Invoke("disableHint",9.0f);
    }
    void Obj10_ReinfoHint2()
    {
        GameObject.FindObjectOfType<GameManager>().isObj10On = false;
        if (current_pizza == 1)
        {
            GameObject.FindObjectOfType<conversationManager>().EnableConversation("The fraction of each slice of the pizza is \\frac{1}{5}.");
            GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj10_Quest1_Hint1_1.wav");
        }
        else if (current_pizza == 2)
        {
            GameObject.FindObjectOfType<conversationManager>().EnableConversation(" The fraction of each slice of the pizza is \\frac{1}{4}.");
            GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj10_Quest1_Hint1_1_2.wav");
        }
        else if (current_pizza == 3)
        {
            GameObject.FindObjectOfType<conversationManager>().EnableConversation("The fraction of each slice of the pizza is \\frac{1}{2}.");
            GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj10_Quest1_Hint1_1_3.wav");

        }
        else if (current_pizza == 4)
        {
            GameObject.FindObjectOfType<conversationManager>().EnableConversation("The fraction of each slice of the pizza is \\frac{1}{3}.");
            GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj10_Quest1_Hint1_1_4.wav");
        }
        Invoke("disableHint", 7.0f);
    }
    void disableBox()
    {
        foreach (Transform child in GameObject.Find("PizzaBoxes").transform)
        {
            child.gameObject.SetActiveRecursively(false);
        }
    }
  public  void checkAnswer2()
    {
        calculateApplesInEachTray();
        Debug.Log("Click on answer 1");

        if (GameObject.Find("Placed in 2"))
        {
            GameObject.FindObjectOfType<GameManager>().isObj10On = false;
            GameObject.FindObjectOfType<conversationManager>().EnableConversation("\\frac{1}{2} means there are 2 parts. But as you can see, this pizza is cut into 3 slices. \nSo, it cannot be  placed in this shelf. Try again.");
            GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj10_Quest2_Hint1.wav");
            Invoke("disableHint", 13.0f);

        }
        else if (GameObject.Find("Placed in 3"))
        {
             GameObject.FindObjectOfType<GameManager>().isObj10On = false;
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Even if the pizza is cut into 3 parts, one slice is half the pizza. If one slice denotes half, we cannot place this pizza in \\frac{1}{3} section.");
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj10_Quest2_Hint2.wav");
                Invoke("placein_remaining_part", 12.0f);
               
            
           


        }
        else if (GameObject.Find("Placed in 4"))
        {
            GameObject.FindObjectOfType<GameManager>().isObj10On = false;
            GameObject.FindObjectOfType<conversationManager>().EnableConversation("\\frac{1}{4} means there should be 4 parts. This pizza clearly has only 3 parts and each part is of a different size. Try again.");
            GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj10_Quest2_Hint3.wav");
            Invoke("disableHint", 11.0f);

        }
        else
        {
            GameObject.FindObjectOfType<GameManager>().isObj10On = false;
            GameObject.FindObjectOfType<conversationManager>().EnableConversation("Place the pizza such that the all slices in the pizza matches the fraction mentioned on the section.");
            GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj10_Quest1_Hint1.wav");
            Invoke("disableHint", 8.0f);

        }


    }


    void placein_remaining_part()
    {
         
            GameObject.FindObjectOfType<GameManager>().isObj10On = false;
            GameObject.FindObjectOfType<conversationManager>().EnableConversation("All 3 slices in the pizza should be of the same size and shape, for the pizza to be placed in \\frac{1}{3} section.");
            GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj10_Quest2_Hint2_1.wav");
            Invoke("disableHint", 9.0f);
            

    }
    void enableInteractionPizza()
    {
        GameObject.Find("Interaction Pizza").transform.GetChild(0).gameObject.SetActive(true);
    }
    void enableInteractionPizza1()
    {
        GameObject.Find("Interaction Pizza").transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
    }

    void highlight_unequal_interaction()
    {
        Interaction_pizza_equa_unequal.transform.GetChild(0).GetComponent<AnimateColors>().enabled = true;
    }
    void highlight_equal_interaction()
    {
        Interaction_pizza_equa_unequal.transform.GetChild(1).GetComponent<AnimateColors>().enabled = true;
    }

    void unhighlight_interaction()
    {
        Interaction_pizza_equa_unequal.transform.GetChild(0).GetComponent<AnimateColors>().enabled = false; Interaction_pizza_equa_unequal.transform.GetChild(1).GetComponent<AnimateColors>().enabled = false;
        Interaction_pizza_equa_unequal.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white; Interaction_pizza_equa_unequal.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;

    }



    void pick1_st_pizza()
    {
        translucent.SetActive(true);
        Interaction_pizza_equa_unequal.transform.GetChild(0).GetChild(0).GetComponent<obj_10_move_to_center>().desired_pos = unequal_center_pos.transform.position;
        Interaction_pizza_equa_unequal.transform.GetChild(0).GetChild(0).GetComponent<obj_10_move_to_center>().move = true;
        Interaction_pizza_equa_unequal.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 5;


        Interaction_pizza_equa_unequal.transform.GetChild(1).GetChild(0).GetComponent<obj_10_move_to_center>().desired_pos = equal_center_pos.transform.position;
        Interaction_pizza_equa_unequal.transform.GetChild(1).GetChild(0).GetComponent<obj_10_move_to_center>().move = true;
        Interaction_pizza_equa_unequal.transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 5;



    }
    void pick2_nd_pizza()
    {
        translucent.SetActive(false);
        translucent.SetActive(true);
        Interaction_pizza_equa_unequal.transform.GetChild(0).GetChild(0).GetComponent<obj_10_move_to_center>().reset_piece();
        Interaction_pizza_equa_unequal.transform.GetChild(0).GetChild(0).GetComponent<obj_10_move_to_center>().move = false;
        Interaction_pizza_equa_unequal.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 3;

        Interaction_pizza_equa_unequal.transform.GetChild(1).GetChild(0).GetComponent<obj_10_move_to_center>().reset_piece();
        Interaction_pizza_equa_unequal.transform.GetChild(1).GetChild(0).GetComponent<obj_10_move_to_center>().move = false;
        Interaction_pizza_equa_unequal.transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 3;


        Interaction_pizza_equa_unequal.transform.GetChild(0).GetChild(1).GetComponent<obj_10_move_to_center>().desired_pos = unequal_center_pos.transform.position;
        Interaction_pizza_equa_unequal.transform.GetChild(0).GetChild(1).GetComponent<obj_10_move_to_center>().move = true;
        Interaction_pizza_equa_unequal.transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = 5;

        Interaction_pizza_equa_unequal.transform.GetChild(1).GetChild(1).GetComponent<obj_10_move_to_center>().desired_pos = equal_center_pos.transform.position;
        Interaction_pizza_equa_unequal.transform.GetChild(1).GetChild(1).GetComponent<obj_10_move_to_center>().move = true;
        Interaction_pizza_equa_unequal.transform.GetChild(1).GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = 5;
    }
    void pick3_rd_pizza()
    {
        translucent.SetActive(false);
        translucent.SetActive(true);
        Interaction_pizza_equa_unequal.transform.GetChild(0).GetChild(1).GetComponent<obj_10_move_to_center>().reset_piece();
        Interaction_pizza_equa_unequal.transform.GetChild(0).GetChild(1).GetComponent<obj_10_move_to_center>().move = false;
        Interaction_pizza_equa_unequal.transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = 3;

        Interaction_pizza_equa_unequal.transform.GetChild(1).GetChild(1).GetComponent<obj_10_move_to_center>().reset_piece();
        Interaction_pizza_equa_unequal.transform.GetChild(1).GetChild(1).GetComponent<obj_10_move_to_center>().move = false;
        Interaction_pizza_equa_unequal.transform.GetChild(1).GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = 3;




        Interaction_pizza_equa_unequal.transform.GetChild(0).GetChild(2).GetComponent<obj_10_move_to_center>().desired_pos = unequal_center_pos.transform.position;
        Interaction_pizza_equa_unequal.transform.GetChild(0).GetChild(2).GetComponent<obj_10_move_to_center>().move = true;
        Interaction_pizza_equa_unequal.transform.GetChild(0).GetChild(2).GetComponent<SpriteRenderer>().sortingOrder = 5;

        Interaction_pizza_equa_unequal.transform.GetChild(1).GetChild(2).GetComponent<obj_10_move_to_center>().desired_pos = equal_center_pos.transform.position;
        Interaction_pizza_equa_unequal.transform.GetChild(1).GetChild(2).GetComponent<obj_10_move_to_center>().move = true;
        Interaction_pizza_equa_unequal.transform.GetChild(1).GetChild(2).GetComponent<SpriteRenderer>().sortingOrder = 5;



    }


    void enableInteractionQuestion()
    {
        GameObject.Find("InteractionPanel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj10_Quest3.wav");
        pick1_st_pizza();
      //  GameObject.Find("InteractionPanel").transform.GetChild(0).GetChild(0).GetComponent<TEXDraw>().text = "Are they equal?";
      //   GameObject.Find("InteractionPanel").transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
      //GameObject.Find("InteractionPanel").transform.GetChild(0).GetChild(4).GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("InteractionPanel").transform.GetChild(0).GetChild(4).GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(no);
        //GameObject.Find("InteractionPanel").transform.GetChild(0).GetChild(4).GetChild(1).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("InteractionPanel").transform.GetChild(0).GetChild(4).GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(yes);
    }
    void enableInteractionQuestion1()
    {
        //GameObject.Find("InteractionPanel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("InteractionPanel").transform.GetChild(0).GetChild(4).gameObject.SetActive(true);
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj10_Quest3.wav");
        pick2_nd_pizza();
     //   GameObject.Find("InteractionPanel").transform.GetChild(0).GetChild(0).GetComponent<TEXDraw>().text = "Are they equal?";
     //  GameObject.Find("InteractionPanel").transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
     //  GameObject.Find("InteractionPanel").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        GameObject.Find("InteractionPanel").transform.GetChild(0).GetChild(4).GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("InteractionPanel").transform.GetChild(0).GetChild(4).GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(No1);
        GameObject.Find("InteractionPanel").transform.GetChild(0).GetChild(4).GetChild(1).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("InteractionPanel").transform.GetChild(0).GetChild(4).GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(yes1);

    }
    void enableInteractionQuestion2()
    {
        //GameObject.Find("InteractionPanel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("InteractionPanel").transform.GetChild(0).GetChild(4).gameObject.SetActive(true);
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj10_Quest3.wav");
        pick3_rd_pizza();
       // GameObject.Find("InteractionPanel").transform.GetChild(0).GetChild(0).GetComponent<TEXDraw>().text = "Are they equal?";
      //  GameObject.Find("InteractionPanel").transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
      //  GameObject.Find("InteractionPanel").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        GameObject.Find("InteractionPanel").transform.GetChild(0).GetChild(4).GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("InteractionPanel").transform.GetChild(0).GetChild(4).GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(No2);
        GameObject.Find("InteractionPanel").transform.GetChild(0).GetChild(4).GetChild(1).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("InteractionPanel").transform.GetChild(0).GetChild(4).GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(yes2);

    }

    void yes()
    {
        GameObject.Find("InteractionPanel").transform.GetChild(0).GetChild(4).gameObject.SetActive(false);
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj10_Quest3_correct.wav");
        Invoke("enableInteractionQuestion1",5.0f);
    }
    void no()
    {
        GameObject.Find("InteractionPanel").transform.GetChild(0).GetChild(4).gameObject.SetActive(false);
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj10_Quest3_wrong.wav");
        Invoke("enableInteractionQuestion1",8.0f);
    }
    void yes1()
    {
        GameObject.Find("InteractionPanel").transform.GetChild(0).GetChild(4).gameObject.SetActive(false);
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj10_Quest3_correct.wav");
        Invoke("enableInteractionQuestion2",5.0f);
    }
    void No1()
    {
        GameObject.Find("InteractionPanel").transform.GetChild(0).GetChild(4).gameObject.SetActive(false);
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj10_Quest3_wrong.wav");
        Invoke("enableInteractionQuestion2", 8.0f);
    }
    void yes2()
    {
        GameObject.Find("InteractionPanel").transform.GetChild(0).GetChild(4).gameObject.SetActive(false);
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj10_Quest3_correct1.wav");
        Invoke("disableInteractionPanel", 5.0f);
    }
    void No2()
    {
        GameObject.Find("InteractionPanel").transform.GetChild(0).GetChild(4).gameObject.SetActive(false);
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj10_Quest3_wrong.wav");
        Invoke("disableInteractionPanel", 8.0f);
    }
    void disableInteractionPanel()
    {
        GameObject.Find("InteractionPanel").transform.GetChild(0).gameObject.SetActive(false);
        Interaction_pizza_equa_unequal.SetActive(false);
        translucent.SetActive(false);
        GameObject.FindObjectOfType<timeline_new>().load_next();
    }

    void Done()
    {
        GameObject.FindObjectOfType<conversationManager>().DisableDialouge();

    }

    void OnSnapPointEnter(bool status)
    {
        rightcut = status;
    }
    void checkCircle()
    {
        if (rightcut)
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            GameObject.Find("Circle").transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
            enableFade2();
        }
        else
        {
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
            FindObjectOfType<timeline_new>().playAudioOnRelearn("draw_line_try_again.wav");
            GameObject.FindObjectOfType<conversationManager>().EnableHint("Draw a line so that the circle gets cut into equal parts. Try Again");

            Invoke("disablehint", 7.0f);
        }

    }

    void disablehint()
    {

        GameObject.FindObjectOfType<conversationManager>().DisableHint();
        GameObject.FindObjectOfType<GameManager>().isObj10On = true;
    }

    void checkPizza()
    {
        if (rightcut)
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            GameObject.Find("Pizza Cutting").transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
            GameObject.FindObjectOfType<timeline_new>().load_next();
        }
        else
        {
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
            GameObject.FindObjectOfType<conversationManager>().EnableHint("Try Again");

            Invoke("disablehint", 3.0f);
        }

    }

    void enableLastQuest()
    {
        GameObject.Find("Pizza Cutting").transform.GetChild(0).gameObject.SetActive(false);
        enablePizza2();
        enableBoxes();
        GameObject.Find("PizzaBoxes").transform.GetChild(0).GetChild(0).name = "4";
        GameObject.Find("PizzaBoxes").transform.GetChild(0).GetChild(1).name = "5";
        GameObject.Find("PizzaBoxes").transform.GetChild(0).GetChild(2).name = "2";
        GameObject.Find("PizzaBoxes").transform.GetChild(0).GetChild(3).name = "3";
        GameObject.Find("PizzaBoxes").transform.GetChild(0).GetChild(4).name = "unequal";
    }

    void checkAnswer3()
    {
        calculateApplesInEachTray();
        Debug.Log("Click on answer 1");
        if (correctPizzaDragged)
        {

            DisableSubmitButton();
            GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
            GameObject.FindObjectOfType<timeline_new>().stopAudio();
            Invoke("animStop", 2.5f);
        }
        else
        {

            {
                Obj10_ReinfoHintLast();

            }
        }
    }
    void Obj10_ReinfoHintLast()
    {
        GameObject.FindObjectOfType<GameManager>().isObj10On = false;
        GameObject.FindObjectOfType<conversationManager>().EnableConversation("This pizza has 4 slices which are all the same size. This might help you to place the pizza in the right shelf.");
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj10_RO3_Hint1.wav");
        Invoke("disableHint",9.0f);
    }

    void animStop()
    {

        GameObject.Find("ExitPanel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("ExitPanel").transform.GetChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(ExitNow);
    }
    void ExitNow()
    {
        Application.Quit();
    }

    void disableAllPizzas()
    {
        disablePizza();
        disablePizza1();
        disableBoxes();
        disabledontknowButton();
        DisableSubmitButton();
        GameObject.FindObjectOfType<conversationManager>().DisableQuestion1();
    }
    void highlightPizza()
    {
        GameObject.Find("Mushroom pizza").transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 1f); ;
        GameObject.Find("Mushroom pizza").transform.GetChild(2).GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 1f);
    }
    void unHighlightPizza()
    {
        GameObject.Find("Mushroom pizza").transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f); ;
        GameObject.Find("Mushroom pizza").transform.GetChild(2).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }

    void enablePizzaPart()
    {
        GameObject.Find("PizzaParts").transform.GetChild(0).gameObject.SetActive(true);
    }
    void enablePizzaText()
    {
        GameObject.Find("PizzaPartText").transform.GetChild(0).gameObject.SetActive(true);
    }
    void enableLo1Text()
    {
        GameObject.Find("LO1Text").transform.GetChild(0).gameObject.SetActive(true);
    }
    void enableLo3Text()
    {
        GameObject.Find("LO3Text").transform.GetChild(0).gameObject.SetActive(true);
    }
    void enableLo3Pizza()
    {
        GameObject.Find("LO3").transform.GetChild(0).gameObject.SetActive(true);
    }

    void enableLo4Text()
    {
        GameObject.Find("LO4Text").transform.GetChild(0).gameObject.SetActive(true);
    }
    void enableLo4Pizza()
    {
        GameObject.Find("LO4").transform.GetChild(0).gameObject.SetActive(true);
    }
    void enableLo4Pizza1()
    {
        GameObject.Find("LO4").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        GameObject.Find("LO4").transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
    }

    void enableLo5Text()
    {
        GameObject.Find("LO5Text").transform.GetChild(0).gameObject.SetActive(true);
    }
    void enableLo5Pizza()
    {
        GameObject.Find("LO5").transform.GetChild(0).gameObject.SetActive(true);
    }


    void enableFade1()
    {
        GameObject.Find("PizzaPartText").transform.GetChild(0).gameObject.SetActive(false);

        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("nextObjective1", 3.0f);
    }
    void nextObjective1()
    {

        //nextObjectiveVo2();
        Invoke("nextObjectiveVo1", 3.0f);
        GameObject.FindObjectOfType<conversationManager>().EnableConversation("");
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");

    }
    public void EnableRoPanel()
    {
        disableAllPizzas();
        GameObject.Find("Mushroom pizza").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("Mushroom pizza").transform.GetChild(1).gameObject.SetActive(false);
        GameObject.Find("Mushroom pizza").transform.GetChild(2).gameObject.SetActive(false);
        GameObject.Find("Mushroom pizza").transform.GetChild(3).gameObject.SetActive(false);
        GameObject.Find("Mushroom pizza").transform.GetChild(4).gameObject.SetActive(false);

        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        GameObject.FindObjectOfType<Obj10_RO_Manager>().Initiliaze();
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj10_Quest2.wav");

    }

    void enableFade2()
    {

        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("nextObjective2", 3.0f);
    }
    void nextObjective2()
    {
        GameObject.Find("Circle").transform.GetChild(0).gameObject.SetActive(false);
        EnableRoPanel1();
        Invoke("nextObjectiveVo1", 3.0f);
        GameObject.FindObjectOfType<conversationManager>().EnableConversation("");
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");

    }
    void EnableRoPanel1()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(1).gameObject.SetActive(true);

        GameObject.FindObjectOfType<Obj10_RO_Manager>().EnableSubmitButtonRO2_Obj10();
    }

    void enableFade3()
    {
        GameObject.Find("LO1Text").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("nextObjective3", 3.0f);
    }
    void nextObjective3()
    {
        EnableRoPanel2();
        Invoke("nextObjectiveVo1", 3.0f);
        GameObject.FindObjectOfType<conversationManager>().EnableConversation("");
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");

    }
    void EnableRoPanel2()
    {

        //GameObject.Find("LO3").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);


        GameObject.FindObjectOfType<Obj10_RO_Manager>().EnableSubmitButtonRO3_Obj10();
    }

    void enableFade4()
    {
        GameObject.Find("LO3Text").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("nextObjective4", 3.0f);
    }
    void nextObjective4()
    {
        EnableRoPanel3();
        Invoke("nextObjectiveVo1", 3.0f);
        GameObject.FindObjectOfType<conversationManager>().EnableConversation("");
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");

    }
    void EnableRoPanel3()
    {
        GameObject.Find("LO3").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(3).gameObject.SetActive(true);


        GameObject.FindObjectOfType<Obj11ROManager>().EnableSubmitButtonRO4();
    }

    void enableFade5()
    {
        GameObject.Find("LO4Text").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("nextObjective5", 3.0f);
    }
    void nextObjective5()
    {
        EnableRoPanel4();
        Invoke("nextObjectiveVo1", 3.0f);
        GameObject.FindObjectOfType<conversationManager>().EnableConversation("");
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");

    }
    void EnableRoPanel4()
    {
        GameObject.Find("LO4").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(4).gameObject.SetActive(true);


        GameObject.FindObjectOfType<Obj11ROManager>().EnableSubmitButtonRO5();
    }

    void enableFade6()
    {
        GameObject.Find("LO5Text").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("nextObjective6", 3.0f);
    }
    void nextObjective6()
    {
        EnableRoPanel5();
        Invoke("nextObjectiveVo1", 3.0f);
        GameObject.FindObjectOfType<conversationManager>().EnableConversation("");
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");

    }
    void EnableRoPanel5()
    {
        GameObject.Find("LO5").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(4).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(5).gameObject.SetActive(true);

        GameObject.FindObjectOfType<Obj11ROManager>().EnableSubmitButtonRO6();
    }

    void nextObjectiveVo1()
    {

        GameObject.FindObjectOfType<timeline_new>().load_next();

    }

    void StartQuestion1()
    {
        GameObject.FindObjectOfType<QuestionManager>().EnableForObj5Quest1();
    }

    void StartQuestion2()
    {
        GameObject.FindObjectOfType<QuestionManager>().EnableForObj5Quest2();
    }

    void StartQuestion3()
    {
        GameObject.FindObjectOfType<QuestionManager>().EnableForObj5Quest3();
    }

    void LO2_Quest2()
    {
        GameObject.FindObjectOfType<QuestionManager>().Obj5_LO2_Question2();
    }
    void LO2_Quest3()
    {
        GameObject.FindObjectOfType<QuestionManager>().Obj5_LO2_Question3();
    }

    void ActiveQuestion2Setup()
    {
        GameObject.Find("Canvas (1)").transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        GameObject.Find("Canvas (1)").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        GameObject.Find("Canvas (1)").transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
        GameObject.Find("GameManager").GetComponent<GameManager>().isObj5On = true;
    }

    public void enable_panel(GameObject object_to_enable)
    {
        //  Animator animator_of_object = object_to_enable.GetComponent<Animator>();
        object_to_enable.SetActive(true);
        //   animator_of_object.Play("enable", 0);
    }
}