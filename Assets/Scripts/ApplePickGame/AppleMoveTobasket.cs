using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AppleMoveTobasket : MonoBehaviour
{
    //private float speed = 10.0f;
    //private Vector2 target;
    //public GameObject basket;
    //private Vector2 position;
    //private Camera cam;
    //Vector2 ab;
    //public GameObject  ClickedObject;
    //void Start()
    //{
    //    basket = GameObject.Find("Basket");
    //    target = new Vector2(0.0f, 0.0f);
    //    //target = basket.transform.position;
        

    //    cam = Camera.main;
    //}
    //private bool IsPointerOverUIObject()
    //{
    //    PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
    //    eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    //    List<RaycastResult> results = new List<RaycastResult>();
    //    EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
    //    return results.Count > 0;
    //}
    //void Update()
    //{
    //    float step = speed * Time.deltaTime;

    //    transform.position = Vector2.MoveTowards(ClickedObject.transform.position, target, step);
    //}
    //void FixedUpdate()
    //{
        
    //    ab = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    RaycastHit2D hit = Physics2D.Raycast(ab, Vector2.zero);
    //    if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
    //    {
    //        if (hit.collider != null)
    //        {

    //            ClickedObject = hit.collider.gameObject;
    //            Debug.Log(ClickedObject.tag);

    //            if (hit.collider.gameObject.tag.Equals(gameObject.tag))
    //            {
    //                target = basket.transform.position;
    //                position = gameObject.transform.position;
                    
    //            }

    //        }
    //    }
    //}
    ////                        void OnGUI()
    ////{
    ////    Event currentEvent = Event.current;
    ////    Vector2 mousePos = new Vector2();
    ////    Vector2 point = new Vector2();

    ////    // compute where the mouse is in world space
    ////    mousePos.x = currentEvent.mousePosition.x;
    ////    mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;
    ////    point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0.0f));

    ////    if (Input.GetMouseButtonDown(0))
    ////    {
    ////        // set the target to the mouse click location
    ////        target = basket.transform.position;
    ////    }
        
    ////}
}
