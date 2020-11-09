using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineCreator : MonoBehaviour
{
    private LineRenderer lineRendrer;
    private Vector2 mousePos;
    private Vector2 startMousePos;

    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        lineRendrer = GetComponent<LineRenderer>();
        lineRendrer.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("pos = " + startMousePos);
        if(Input.GetMouseButtonDown(0))
        {  
            startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {

            mousePos= Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lineRendrer.SetPosition(0, new Vector3(startMousePos.x, startMousePos.y, 0f));
            lineRendrer.SetPosition(0, new Vector3(mousePos.x, mousePos.y, 0f));
        }
    }
}
