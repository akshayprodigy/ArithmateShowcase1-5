using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IsometricPlayerMovementControllerFair : MonoBehaviour
{

    public float movementSpeed, playermovementSpeed;
    IsometricCharacterRenderer isoRenderer;
    public Vector2 input;
    public Vector2 ab, temp;
    Rigidbody2D rbody;
    public GameObject tree;
    public bool reached, finished;
    public string destination;


    private void Start()
    {

        Initialization();
    }
    void Initialization()
    {
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
        destination = "Park_Gate";
    }
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 currentPos = rbody.position;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        Vector2 movement = inputVector * movementSpeed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;


        input = newPos;
        playerWalk();

        Vector2 direction = ab - currentPos;
        direction.Normalize();
        Vector2 velocity = direction * movementSpeed;
        newPos = currentPos + velocity * Time.deltaTime;


        float dist = Vector2.Distance(currentPos, ab);
        //Debug.Log(dist);
        if (dist < 0.05f)
        {

            movementSpeed = 0;
            direction = currentPos;
            isoRenderer.SetDirection(Vector2.zero);

            if (reached == false)
            {
                if (finished == false)
                {

                    GameObject.Find("WitchRender").GetComponent<Animator>().enabled = false;
                    GameObject.Find("Guide").GetComponent<IsometricPlayerMovementControllerFair>().enabled = false;
                    //StartCoroutine(this.gameObject.GetComponent<conversationManager>().sentanceChange());
                    reached = true;


                    trigger_tree();
                }
            }
            if (finished == true)
            {
                destination = "Park_Gate_2";
                reached = true;
            }

        }
        else if (movementSpeed == playermovementSpeed)
        {
            isoRenderer.SetDirection(direction);
            rbody.MovePosition(newPos);

        }
        else
        {
            isoRenderer.SetDirection(Vector2.zero);
            rbody.velocity = Vector2.zero;

        }



    }

    public void trigger_tree()
    {

        reached = true;
        isoRenderer.SetDirection(Vector2.zero);
        movementSpeed = 0;
        if (tree != null)
        {

            tree.GetComponent<Animator>().SetTrigger("move");
            StartCoroutine(disable_anim(tree));
        }
        tree = null;
    }

    public void playerWalk()
    {

        reached = false;
        gameObject.GetComponentInChildren<Collider2D>().enabled = true;
        movementSpeed = playermovementSpeed;
        ab = GameObject.Find(destination).transform.position;
        RaycastHit2D hit = Physics2D.Raycast(ab, Vector2.zero);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.name);

        }

    }
    IEnumerator disable_anim(GameObject ab)
    {
        yield return new WaitForSeconds(0.6f);
        ab.GetComponent<instatntiate_apples>().instantiate_particles();
        yield return new WaitForSeconds(0.4f);
        gameObject.GetComponentInChildren<Collider2D>().enabled = false;
        ab.GetComponent<instatntiate_apples>().instantiate_apple();

        ab.GetComponent<Animator>().ResetTrigger("move");

    }
}