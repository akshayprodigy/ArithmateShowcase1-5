using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    // Start is called before the first frame update
    float distance;
    private Vector3 screenPoint;
    private Vector3 offset;
    void Start()
    {
        distance = transform.position.y + 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
       
        screenPoint = Camera.main.WorldToScreenPoint(Input.mousePosition);
        //offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, screenPoint.y, Input.mousePosition.y));

        Debug.Log("Object selected: " + gameObject.name+" tranform: "+transform.position+ " offset: "+ offset);
    }

    private void OnMouseDrag()
    {
        //Debug.Log("Object OnMouseDrag: " + gameObject.name);
        //Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.localPosition.z );
        //Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        //transform.localPosition = curPosition;
        //Vector3 cursorScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorScreenPoint = new Vector3(Input.mousePosition.x, screenPoint.y, Input.mousePosition.y);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorScreenPoint) + offset;
        transform.localPosition = cursorPosition;
    }

    private void OnMouseUp()
    {
        Debug.Log("Object released: " + gameObject.name);
    }
}
