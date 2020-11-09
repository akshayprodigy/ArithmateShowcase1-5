using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switch_cam : MonoBehaviour
{



    public GameObject main_cam; 
    
    public float transition_speed=1.0f;
    
   
    
    // Use this for initialization
    void Start () {
        main_cam = Camera.main.gameObject;
       
        
	}
	
	

    public void LateUpdate()
    {
        float viewSize = Mathf.Lerp(main_cam.GetComponent<Camera>().orthographicSize, 2.3f, Time.deltaTime * transition_speed);
        main_cam.GetComponent<Camera>().orthographicSize = viewSize;
        
       
       
    }


   

}
