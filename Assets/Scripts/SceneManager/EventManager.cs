using System;
using System.Collections.Generic;


public enum EVENT { SetUpTimeLine, MyEvent2 }; // ... Other events
public static class EventManager
{
    // Stores the delegates that get called when an event is fired
    private static Dictionary<EVENT, Action> eventTable
                 = new Dictionary<EVENT, Action>();

    private static string NameOfJasonFile = "";
    public static void setNameOfJasonFile(string msg)
    {
        NameOfJasonFile = msg;
    }
    public static string getNameOfJasonFile()
    {
        return NameOfJasonFile;
    }
    // Adds a delegate to get called for a specific event
    public static void AddHandler(EVENT evnt, Action action)
    {
        if (!eventTable.ContainsKey(evnt)) eventTable[evnt] = action;
        else eventTable[evnt] += action;
    }

    // Fires the event
    public static void Broadcast(EVENT evnt)
    {
        if (eventTable[evnt] != null) eventTable[evnt]();
    }
    //Remove Handler
    public static void RemoveHandler(EVENT evnt, Action action)
    {
        if (eventTable[evnt] != null)
            eventTable[evnt] -= action;
        if (eventTable[evnt] == null)
            eventTable.Remove(evnt);
    }
}
