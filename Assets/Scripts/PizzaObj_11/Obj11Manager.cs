using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Obj11Manager : MonoBehaviour
{
    string jsonFileName = "Obj11ProperAndFraction.json";
    public string ans;
    public GameObject LoadingAudio;
    public int threePiecePizza, SixPiecePizza, eightPiecePizza;
    public bool enablePizza1Drag, enablePizza2Drag, enablePizza3Drag;
    

    private void OnEnable()
    {
        timeline_new.OnEventCalled += EventToHandle;
    }

    private void OnDisable()
    {
        timeline_new.OnEventCalled -= EventToHandle;
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
        LoadingAudio = GameObject.Find("LoadAudio");
        if (UtilityArtifacts.backTraversal)
        {
            Text textLoadingText = LoadingAudio.transform.GetChild(2).GetComponent<Text>();
            textLoadingText.text = "Let us understand this better";
        }
        StartCoroutine("enableDivision");
        Invoke("audio_invoke", 2.0f);
        GameObject.Find("SubmitButton").transform.GetChild(0).GetComponent<Button>().onClick.AddListener(checkAnswer1);

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

            case "Obj11_Let_us_see":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Let's see what the next order says.");
                break;
            case "Obj11_A_group":
                GameObject.Find("Characters").transform.GetChild(0).gameObject.SetActive(true);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("A group of 3 customers have placed an order for mushroom pizza.");
                break;
            case "Obj11_First_Customer":
                GameObject.Find("Characters").transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Customer 1 wants \\frac{2}{8} of the mushroom pizza");
                break;
            case "Obj11_Second_Customer":
                GameObject.Find("Characters").transform.GetChild(0).GetChild(1).GetChild(0).gameObject.SetActive(true);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Customer 2 wants \\frac{4}{6} of the mushroom pizza");
                
                break;
            case "Obj11_Third_Customer":
                GameObject.Find("Characters").transform.GetChild(0).GetChild(2).GetChild(0).gameObject.SetActive(true);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Customer 3 wants \\frac{1}{3} of the mushroom pizza");
                break;
            case "Obj11_Here_are_the_mushroom":
                GameObject.Find("PizzaBoxes").transform.GetChild(0).gameObject.SetActive(true);
                enablePizza();
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Here are the mushroom pizzas. Now that you know how many slices each customer has asked for, go ahead and pack their pizza.");
                break;
            case "Obj11_pack_first_customer_pizza":
                GameObject.Find("PizzaBoxes").transform.GetChild(0).gameObject.SetActive(true);
                enablePizza();
                GameObject.FindObjectOfType<GameManager>().isObj11On = true;
                GameObject.Find("Characters").transform.GetChild(0).GetChild(1).GetChild(0).gameObject.SetActive(false);
                GameObject.Find("Characters").transform.GetChild(0).GetChild(2).GetChild(0).gameObject.SetActive(false);
                GameObject.Find("Characters").transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(false);
                GameObject.Find("Characters").transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                GameObject.Find("Characters").transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                GameObject.Find("Characters").transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Pack the pizza portion for customer 1");
                GameObject.Find("IntroQuestText").transform.GetChild(0).gameObject.SetActive(true); 
                GameObject.Find("IntroQuestText").transform.GetChild(0).GetChild(0).GetComponent<TEXDraw>().text = "Customer 1 wants \\frac{2}{8} of the pizza.";
                break;
            case "Obj11_pack_second_customer_pizza":
                
                enablePizza();
                GameObject.FindObjectOfType<GameManager>().isObj11On = true;
                GameObject.Find("Characters").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                GameObject.Find("Characters").transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
                GameObject.Find("Characters").transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                GameObject.Find("PizzaBoxes").transform.GetChild(1).gameObject.SetActive(true);
                GameObject.Find("IntroQuestText").transform.GetChild(0).GetChild(0).GetComponent<TEXDraw>().text = "Customer 2 wants \\frac{4}{6} of the pizza.";
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Now pack the pizza for customer 2");
                GameObject.Find("SubmitButton").transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
                GameObject.Find("SubmitButton").transform.GetChild(0).GetComponent<Button>().onClick.AddListener(checkAnswer2);
                break;
                
             case "Obj11_we_served":
                GameObject.Find("Characters").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                GameObject.Find("Characters").transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
                GameObject.Find("Characters").transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                GameObject.FindObjectOfType<GameManager>().isObj11On = false;
                highlightPizza();
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("We served 2 pizza orders for 2 customers and this is what is left over.");
                break;
            case "Obj11_If_you_observe":
                
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("If you observe , you will see that  a part or a portion has been taken from each pizza to serve the customers. That's why we have some slices left in each pizza.");
                break;
            case "Obj11_It_is_clear":
                unHighlightPizza();
                disablePizza();
                disableBox();
                enablePizzaPart();
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("It is clear that the portion each customer received is lesser than a full pizza.");
                break;
            case "Obj11_Therefore_we_can":

               
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Therefore, we can say that the fractions that denote the quantity each customer received are all less than 1");
                break;
            case "Obj11_two_by_four":
               
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("\\frac{2}{8} and \\frac{4}{6}");
                break;
            case "Obj11_In_these_fractions":
                GameObject.FindObjectOfType<conversationManager>().DisableConversation();
                //GameObject.FindObjectOfType<conversationManager>().EnableConversation("In these fractions, the numerators are lesser than the denominators.");
                GameObject.Find("PizzaParts").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("LO1_1Text").transform.GetChild(0).gameObject.SetActive(true);
                //GameObject.Find("PizzaPartText").transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                //GameObject.Find("PizzaPartText").transform.GetChild(0).GetChild(1).gameObject.SetActive(true);

                break;
            case "Obj11_Fractions_whose":
                //GameObject.FindObjectOfType<conversationManager>().DisableConversation();
                //GameObject.FindObjectOfType<conversationManager>().EnableConversation("Fractions whose value is less than 1 always have numerator less than their denominator. Such fractions are called Proper fractions");
                 //GameObject.Find("PizzaPartText").transform.GetChild(0).gameObject.SetActive(false);
                 GameObject.Find("PizzaParts").transform.GetChild(0).gameObject.SetActive(false);

                //GameObject.Find("LO1_1Text").transform.GetChild(0).gameObject.SetActive(true);
                break;
            case "Obj11_so_two_by_8":
                enablePizzaText();
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("So, \\frac{2}{8} and \\frac{4}{6} are proper fractions.");
                GameObject.Find("LO1_1Text").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("PizzaPartText").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
                GameObject.Find("PizzaPartText").transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                GameObject.Find("PizzaPartText").transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                //GameObject.Find("PizzaPartText").transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
                Invoke("enableFade1", 7.0f);
                break;
            case "Obj11_RO1_quest1":
                GameObject.Find("Stop").SetActive(false);
                break;
            case "Obj11_RO1_quest2":
                
                EnableRoPanel1();
                GameObject.Find("Stop").SetActive(false);
                break;
            case "Obj11_Now_you_have":
                GameObject.Find("RO Panel").transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Now you have learnt what are proper fractions. Let us continue with our pizza orders. Let’s pack the pizza for customer 3");
                break;
            case "Obj11_Customer_three_wants":
                GameObject.FindObjectOfType<GameManager>().isObj11On = true;

                foreach (GameObject g in GameObject.FindGameObjectsWithTag("tree"))
                    g.SetActive(false);
                GameObject.Find("PizzaBoxes").transform.GetChild(2).gameObject.SetActive(true);
                GameObject.Find("PizzaBoxes").transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
                GameObject.Find("PizzaBoxes").transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
                GameObject.Find("PizzaBoxes").transform.GetChild(2).GetChild(2).gameObject.SetActive(true);
                GameObject.Find("SubmitButton").transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
                GameObject.Find("SubmitButton").transform.GetChild(0).GetComponent<Button>().onClick.AddListener(checkAnswer3);
                GameObject.Find("Characters").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                GameObject.Find("Characters").transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                GameObject.Find("Characters").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
                GameObject.Find("IntroQuestText").transform.GetChild(0).GetChild(0).GetComponent<TEXDraw>().text = "Customer 3 wants \\frac{1}{3} of the pizza.";
                break;
            case "Obj11_If_you_observe_1_by_8":
                GameObject.FindObjectOfType<GameManager>().isObj11On = false;
                GameObject.Find("Characters").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                GameObject.Find("Characters").transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                GameObject.Find("Characters").transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
                disablePizza();
                disableBox();
                enableLo1Text();
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("If you observe this fraction \\frac{1}{8}, you will notice that it is a proper fraction too as the numerator is less than the denominator.");
                break;
            case "Obj11_But_this_fraction":
                GameObject.Find("LO1Text").transform.GetChild(0).GetChild(5).gameObject.SetActive(false);
                GameObject.Find("LO1Text").transform.GetChild(0).GetChild(4).gameObject.SetActive(false);
                GameObject.Find("LO1Text").transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("But this fraction is also called as a unit fraction. Lets see why.");
                break;
            case "Obj11_The_word_Unit":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("The word \\color[4]Unit} is used to denote a \\color[4]single object} or a thing.");
                break;
            case "Obj11_In_fractions":
                GameObject.FindObjectOfType<conversationManager>().DisableConversation();
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("In fractions, we call each part of an object as a unit. We can represent this unit as a fraction");
                enableLO2Pizza();
                break;
            case "Obj11_Such_fractions":
                enableLO2PizzaAnim();
                //GameObject.FindObjectOfType<conversationManager>().DisableConversation();
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Such fractions that represents a unit in an object is called a unit Fraction.");

                //GameObject.Find("LO1Text").transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                Invoke("enableLO2PizzaText",3.0f);
                break;
            case "Obj11_Since":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Since unit fractions represents one part from a whole,  it will always have \\color[4]1} as its Numerator.");
               
                Invoke("enableFade3", 8.0f);
                break;
            case "Obj11_RO2_quest1":
                GameObject.Find("Stop").SetActive(false);
                break;
            case "Obj11_A_whole":
                GameObject.Find("RO Panel").transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
                GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(false);
                enableLo3Pizza();
                GameObject.Find("LO3Text1_1").transform.GetChild(0).gameObject.SetActive(true);
                GameObject.FindObjectOfType<conversationManager>().DisableConversation();
                break;
            case "Obj11_Take":
                GameObject.Find("LO3Text1_1").transform.GetChild(0).gameObject.SetActive(false);
                enableLo3Text();
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Take a look at this pizza. Each slice is \\frac{1}{8} and \\frac{1}{8} is a unit fraction. It is made up of 8 unit fractions.");
                Invoke("enableFade4", 8.0f);
                break;
            case "Obj11_RO3_quest1":
                GameObject.Find("Stop").SetActive(false);
                break;
            case "Obj11_Look":
                GameObject.Find("RO Panel").transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
                GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Look at this pizza");
                enableLo4Pizza();
                break;
            case "Obj11_We_can_say":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("We can say that the unit fraction determines the size of each part in an object or a fraction.");
                break;
            case "Obj11_There_are_8_slices":
                GameObject.Find("LO4").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                GameObject.Find("LO4").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
                Invoke("enableLo4TestAfterAnim", 7.0f);
                //GameObject.FindObjectOfType<conversationManager>().EnableConversation("There are 8 slices and this is one part of the pizza. The size of each slice is \\frac{1}{8}.");
                GameObject.FindObjectOfType<conversationManager>().DisableConversation();

                break;
            case "Obj11_If_the_size":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("If you observe carefully, \\frac{3}{8} is made up of three unit fractions of \\frac{1}{8}");
                enableLo4Pizza1();
                enableLo4Text();

                Invoke("enableFade5", 8.0f);
                break;
            case "Obj11_RO4_quest":
                GameObject.Find("Stop").SetActive(false);
                break;
            case "Obj11_Do_you_know":
                GameObject.Find("RO Panel").transform.GetChild(0).GetChild(4).gameObject.SetActive(false);
                GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Do you know , If we add two or more unit fractions together , we get a new fraction.");
                break;
            case "Obj11_Let":
                enableLo5Pizza();
                enableLo5Text();
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Let's take these three unit fractions which are \\frac{1}{8}. We will get \\frac{3}{8}, which is a proper fraction.");
                Invoke("enableFade6", 12.0f);
                break;
            case "Obj11_RO5_quest1":
                GameObject.Find("Stop").SetActive(false);
                break;




        }

    }

    void enablePizza()
    {
        GameObject.Find("Mushroom pizza").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("Mushroom pizza").transform.GetChild(1).gameObject.SetActive(true);
        GameObject.Find("Mushroom pizza").transform.GetChild(2).gameObject.SetActive(true);
    }
    void disablePizza()
    {
        GameObject.Find("Mushroom pizza").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("Mushroom pizza").transform.GetChild(1).gameObject.SetActive(false);
        GameObject.Find("Mushroom pizza").transform.GetChild(2).gameObject.SetActive(false);
    }
    void checkAnswer1()
    {

        Debug.Log("Click on answer 1");
        GameObject.FindObjectOfType<QuestionManager>().checkObj11Ans1();
    }
    void disableBox()
    {
        foreach (Transform child in GameObject.Find("PizzaBoxes").transform)
        {
            child.gameObject.SetActiveRecursively(false);
        }
    }
    void checkAnswer2()
    {
        GameObject.FindObjectOfType<QuestionManager>().checkObj11Ans2();
    }
    void checkAnswer3()
    {
        GameObject.FindObjectOfType<QuestionManager>().checkObj11Ans3();
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
    void enableLO2Pizza()
    {
        GameObject.Find("LO1Text").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("LO1PizzaAnim").transform.GetChild(0).gameObject.SetActive(true);

    }
    void enableLO2PizzaAnim()
    {
        GameObject.Find("LO1Text").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("LO1PizzaAnim").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        //GameObject.Find("LO1PizzaAnim").transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        GameObject.Find("LO1PizzaAnim").transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
    }
    void enableLO2PizzaText()
    {
        GameObject.Find("LO1PizzaAnim").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
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
        GameObject.Find("LO4").transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        GameObject.Find("LO4").transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
    }
    void enableLo4TestAfterAnim()
    {
        GameObject.Find("LO4").transform.GetChild(0).GetChild(2).GetChild(2).gameObject.SetActive(true);
    }
    void enableLo5Text()
    {
        GameObject.Find("LO5Text").transform.GetChild(0).gameObject.SetActive(true);
    }
    void enableLo5Pizza()
    {
        //GameObject.Find("LO5").transform.GetChild(0).gameObject.SetActive(true);
        Debug.Log("Hello");
    }


    void enableFade1()
    {
        GameObject.Find("LO1_1Text").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("PizzaPartText").transform.GetChild(0).gameObject.SetActive(false);
         //GameObject.Find("PizzaPartText").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("nextObjective1", 3.0f);
    }
    void nextObjective1()
    {
        EnableRoPanel();
        //nextObjectiveVo2();
        Invoke("nextObjectiveVo1", 3.0f);
        GameObject.FindObjectOfType<conversationManager>().EnableConversation("");
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");

    }
    void EnableRoPanel()
    {
       
        enablePizza();
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        GameObject.FindObjectOfType<Obj11ROManager>().Initiliaze();
    }

    void enableFade2()
    {
        
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("nextObjective2", 3.0f);
    }
    void nextObjective2()
    {
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

        GameObject.FindObjectOfType<Obj11ROManager>().EnableSubmitButtonRO2();
    }

    void enableFade3()
    {
        GameObject.Find("LO1PizzaAnim").transform.GetChild(0).gameObject.SetActive(false);
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
        

        GameObject.FindObjectOfType<Obj11ROManager>().EnableSubmitButtonRO3();
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