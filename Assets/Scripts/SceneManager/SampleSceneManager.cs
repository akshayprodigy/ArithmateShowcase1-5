
using UnityEngine;

public class SampleSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    string jsonFileName = "OBJ_15_Cookies.json";
    void Start()
    {
        Initialised();
    }

    //private void OnEnable()
    //{
    //    timeline_new.OnEventCalled += EventToHandle;
    //}
    //private void OnDisable()
    //{
    //    timeline_new.OnEventCalled -= EventToHandle;
    //}
    // Update is called once per frame
    void Initialised()
    {
        EventManager.setNameOfJasonFile(jsonFileName);
        EventManager.Broadcast(EVENT.SetUpTimeLine);
       
    }
    //void EventToHandle(string EventName)
    //{
    //    switch (EventName)
    //    {
    //        case " ":
    //            break;
    //    }

    //}
}
