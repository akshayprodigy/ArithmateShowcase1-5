using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class intiate_obj_15 : MonoBehaviour
{
    // Start is called before the first frame update
    string jsonFileName = "OBJ_15_Cookies.json";
    void Start()
    {
        Initialised();
    }

   
   
    void Initialised()
    {
        EventManager.setNameOfJasonFile(jsonFileName);
        EventManager.Broadcast(EVENT.SetUpTimeLine);

    }
}
