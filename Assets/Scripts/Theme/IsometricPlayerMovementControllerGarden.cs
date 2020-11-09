using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Linq;
using UnityEngine.UI;

public class IsometricPlayerMovementControllerGarden : MonoBehaviour
{

    public float movementSpeed = 4f;
    IsometricCharacterRenderer isoRenderer;
    public Vector2 input;
    Vector2 ab, temp;
    Rigidbody2D rbody;
    public GameObject tree,ClickedObject,clickedApple;
    public bool reached;
    public string prefabName;
    public GameManager gameManager;
    public ParticleSystem particleObject;

    private float speed = 10.0f;
    private Vector2 target;
    public GameObject basket;
    private Vector2 position;
    public Camera cam,cam1;
    public Ease easeType;

    public Transform targetCanvas;
    
    void testInit()
    {
        Invoke("testInit1", 1.0f);
    }

    void testInit1()
    {
        //GameObject g = Instantiate(Resources.Load("Prefabs/AppleObj1/ApplesToBeInitiated/AppleForUI"), targetCanvas, false) as GameObject;
        cam1 = GameObject.Find("GardenCamera").GetComponent<Camera>();
        GameObject g = Instantiate(Resources.Load("Prefabs/AppleObj1/AppleForUI/FullRedApple"), targetCanvas, false) as GameObject;
        RectTransform clone = g.GetComponent<RectTransform>();
        clone.anchorMin = cam1.WorldToViewportPoint(GameObject.FindGameObjectWithTag("Fallen Apple").transform.position);
        clone.anchorMax = clone.anchorMin;
        clone.anchoredPosition = clone.localPosition;
        clone.anchorMin = new Vector2(0.5f, 0.5f);
        clone.anchorMax = clone.anchorMin;
        g.transform.DOJump(GameObject.Find("BasketCanvas").transform.position, 1, 1, 1f)
            .SetEase(easeType)
            .OnComplete(() =>
            {
                Destroy(g);
            });
        testInit();
    }

    void cloneInist()
    {
        //GameObject g = Instantiate(Resources.Load("Prefabs/AppleObj1/ApplesToBeInitiated/AppleForUI"), targetCanvas, false) as GameObject;
        cam1 = GameObject.Find("GardenCamera").GetComponent<Camera>();
        GameObject g = Instantiate(Resources.Load("Prefabs/AppleObj1/AppleForUI/" + ClickedObject.tag), targetCanvas, false) as GameObject;
        RectTransform clone = g.GetComponent<RectTransform>();
        clone.anchorMin = cam1.WorldToViewportPoint(ClickedObject.transform.position);
        clone.anchorMax = clone.anchorMin;
        clone.anchoredPosition = clone.localPosition;
        clone.anchorMin = new Vector2(0.5f, 0.5f);
        clone.anchorMax = clone.anchorMin;
        g.transform.DOJump(GameObject.Find("BasketCanvas").transform.position, 1, 1, 1f)
            .SetEase(easeType)
            .OnComplete(() =>
            {
                Destroy(g);
            });

    }
    private void Awake()
    {
        Initialize();
    }
    public void Initialize()
    {
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();

        //AppleManager.resetData();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        particleObject = transform.GetChild(0).GetComponent<ParticleSystem>();
        particleObject.Stop();
        basket = GameObject.Find("Basket");
        //target = new Vector2(0.0f, 0.0f);
        //target = basket.transform.position;
        position = gameObject.transform.position;

        cam = Camera.main;
        AppleManager.falledApple = 0;
        targetCanvas = GameObject.Find("Canvas").transform;

        
    }
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;

    }
    void Update()
    {
        //float step = speed * Time.deltaTime;
        if(Input.GetKey(KeyCode.A))
        {
            testInit();
        }
        //clickedApple.position = Vector2.MoveTowards(transform.position, target, step);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameManager.IsGameOn)
        {
            Vector2 currentPos = rbody.position;
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
            inputVector = Vector2.ClampMagnitude(inputVector, 1);
            Vector2 movement = inputVector * movementSpeed;
            Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
            input = newPos;

            if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
            {
                reached = false;
                gameObject.GetComponentInChildren<Collider2D>().enabled = true;
               
                ab = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ab, Vector2.zero);
                if (hit.collider != null)
                {

                    ClickedObject = hit.collider.gameObject;
                    
                        Debug.Log(ClickedObject.tag);
                   
                    if (hit.collider.gameObject.tag.Equals("tree") )
                    {
                        movementSpeed = 1;
                        reached = false;

                        var d = hit.collider.gameObject.GetComponentsInChildren<Transform>();
                        if (d == null)
                        {
                            d = hit.collider.gameObject.transform.parent.GetComponentsInChildren<Transform>();
                            tree = hit.collider.gameObject.transform.parent.gameObject;
                        }
                        else
                        {
                            tree = hit.collider.gameObject;
                        }
                        Transform left = null, right = null;
                        float left_trans, right_trans;
                        foreach (Transform g in d)
                        {
                            if (g.name.Equals("left"))
                            {
                                left = g;

                            }
                            if (g.name.Equals("right"))
                            {
                                right = g;
                            }
                        }
                        if (left != null && right != null)
                        {
                            if (Vector3.Distance(gameObject.transform.position, left.transform.position) < Vector3.Distance(gameObject.transform.position, right.transform.position))
                            {
                                ab = left.transform.position;
                            }
                            else
                            {
                                ab = right.transform.position;
                            }
                        }
                    }
                    else
                    {
                        movementSpeed = 0;
                    }
                        if( ClickedObject.name.Equals("fallen Apple"))
                        {
                            movementSpeed = 0;
                        cloneInist();
                            //Animate();
                            CalculateApple();
                        
                        GameObject.FindObjectOfType<TimeManager>().clickOnApple = true;
                        CancelInvoke();
                        if (GameObject.FindObjectOfType<GameManager>().isFirstTime)
                            GameObject.FindObjectOfType<GameManager>().isFirstTime = false;
                        ClickedObject.SetActive(false);
                        //GameObject.FindObjectOfType<conversationManager>().DisableConversation();
                        
                        //highLightApplesForTut();
                    }
                    }
                
            }
            Vector2 direction = ab - currentPos;
            direction.Normalize();
            Vector2 velocity = direction * movementSpeed;
            newPos = currentPos + velocity * Time.deltaTime;

            //temp =
            // if (horizontalInput!=0||verticalInput!=0)  
            float dist = Vector2.Distance(currentPos, ab);
            //Debug.Log(dist);
            if (dist < 0.05f)
            {

                movementSpeed = 0;
                direction = currentPos;
                isoRenderer.SetDirection(Vector2.zero);

                if (reached == false)
                {
                    if (ClickedObject.tag.Equals("tree"))
                    {
                        trigger_tree();
                    }
                    else
                    {
                        TriggerApple();
                    }
                }

            }
            else if (movementSpeed == 1)
            {
                isoRenderer.SetDirection(direction);
                rbody.MovePosition(newPos);
                ///reached = false;
            }
            else
            {
                isoRenderer.SetDirection(Vector2.zero);
                rbody.velocity = Vector2.zero;
                //  reached = true;
            }


        }
    }

    public void trigger_tree()
    {
        //   if (reached == true && tree != null)
        // {
        GameObject.FindObjectOfType<TimeManager>().clickOnTree = true;
        reached = true;
        isoRenderer.SetDirection(Vector2.zero);
        movementSpeed = 0;
        
        if (tree != null)
        {
            tree.GetComponent<Animator>().SetTrigger("shake");
            tree.GetComponent<PolygonCollider2D>().enabled = false;
            ActiveAppleFall();
            Invoke("disableTreeAnimator", 2.0f);
            //tree.GetComponent<Animator>().SetTrigger("move");
            //StartCoroutine(disable_anim(tree));
        }  //GameObject.FindObjectOfType<IsometricPlayerMovementController>().movementSpeed = 0;
        //}
        tree = null;
        //GameObject.FindObjectOfType<conversationManager>().DisableConversation();
    }
    public void disableTreeAnimator()
    {
        
        gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y - 0.35f, gameObject.transform.position.z);
        //basket.transform.position = new Vector3(basket.transform.position.x, basket.transform.position.y + 0.35f, basket.transform.position.z);
        //tree.GetComponent<Animator>().enabled = false;

    }
   

public void TriggerApple()
    {
        if (ClickedObject != null)
        {
            Debug.Log("Hello");
           // CalculateApple();
            //ClickedObject.GetComponent<Animator>().SetTrigger("move");
            //StartCoroutine(disable_anim(tree));
        }
        ClickedObject = null;
    }
    void ActiveAppleFall()
    {
        foreach (Transform child in tree.transform)
        {
            if (child.GetComponent<AppleFall>())
            {
                child.GetComponent<AppleFall>().enabled = true;
            }
        }
        //AppleManager.falledApple = ;
    }
    void CalculateApple()
    {
        //target = basket.transform.position;

       
        switch (ClickedObject.tag)
        {
            
            case "HalfAppleRed":
                AppleManager.CollectedHalfRedApple = AppleManager.CollectedHalfRedApple+1;
                prefabName = "HalfAppleRed";
                break;
            case "HalfAppleGreen":
                AppleManager.CollectedHalfGreenApple = AppleManager.CollectedHalfGreenApple+1;
                prefabName = "HalfAppleGreen";
                break;
            case "HalfAppleYellow":
                AppleManager.CollectedHalfYellowApple = AppleManager.CollectedHalfYellowApple + 1;
                prefabName = "HalfAppleYellow";
                break;
            case "FourthAppleRed":
                AppleManager.CollectedQuarterRedApple = AppleManager.CollectedQuarterRedApple+1;
                prefabName = "FourthAppleRed";
                break;
            case "FourthAppleGreen":
                AppleManager.CollectedQuarterGreenApple = AppleManager.CollectedQuarterGreenApple+1;
                prefabName = "FourthAppleGreen";
                break;
            case "FourthAppleYellow":
                AppleManager.CollectedQuarterYellowApple = AppleManager.CollectedQuarterYellowApple + 1;
                prefabName = "FourthAppleYellow";
                break;
            case "ThirdAppleRed":
                AppleManager.CollectedThirdRedApple = AppleManager.CollectedThirdRedApple+1;
                prefabName = "ThirdAppleRed";
                break;
            case "ThirdAppleGreen":
                AppleManager.CollectedThirdGreenApple = AppleManager.CollectedThirdGreenApple+1;
                prefabName = "ThirdAppleGreen";
                break;
            case "ThirdAppleYellow":
                AppleManager.CollectedThirdYellowApple = AppleManager.CollectedThirdYellowApple + 1;
                prefabName = "ThirdAppleYellow";
                break;
            case "FullRedApple":
                AppleManager.CollectedFullRedApple = AppleManager.CollectedFullRedApple+1;
                prefabName = "FullRedApple";

                break;
            case "FullGreenApple":
                AppleManager.CollectedFullGreenApple = AppleManager.CollectedFullGreenApple+1;
                prefabName = "FullGreenApple";
                break;
            case "FullYellowApple":
                AppleManager.CollectedFullYellowApple = AppleManager.CollectedFullYellowApple + 1;
                prefabName = "FullYellowApple";
                break;

        }
       
        if(ClickedObject.tag!="playercollider")
        {
            //transform.GetChild(0).gameObject.SetActive(true);
            //particleObject.Play();

            AppleManager.DebugValue();
            AppleManager.collectedAppleName.Add(ClickedObject.tag);

           
            //Invoke("stopEmiting", 1.5f);
            //Destroy(ClickedObject);
        }
        
    }
    void Animate()
    {
        ClickedObject.transform.DOJump(basket.transform.position,1,1, 1f)
            .SetEase(easeType)
            .OnComplete(() =>
            {
                ClickedObject.SetActive(false);
            });
    }
    void stopEmiting()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
    IEnumerator disable_anim(GameObject ab)
    {
        yield return new WaitForSeconds(0.6f);
        ab.GetComponent<instatntiate_apples>().instantiate_particles();
        yield return new WaitForSeconds(0.4f);
        gameObject.GetComponentInChildren<Collider2D>().enabled = false;
        ab.GetComponent<instatntiate_apples>().instantiate_apple();
        //yield return new WaitForSeconds(0.2f);
        ab.GetComponent<Animator>().ResetTrigger("move");
        // ab.GetComponent<Animator>().enabled = false;
    }
}


