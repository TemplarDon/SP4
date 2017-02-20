using UnityEngine;
using System.Collections;

public class Message {

    public enum MESSAGE_TYPE
    {
        ORDER_FRONTAL_ASSAULT,      // Order one enemy to move to attack one character each, DEFAULT ORDER
        ORDER_SURROUND_TARGET,      // Order at least 2 enemies to move to target an enemy
        ORDER_FALLBACK,             // Order the unit to fallback to safety
        ORDER_PROTECT_COMMMANDER,   // Order all units to fallback to protect commander

        UNIT_NEED_HELP,             // Unit calling for help, other units will try to respond 
    }

    public MESSAGE_TYPE theMessageType;
    public GameObject theSender, theReceiver, theTarget;
    public Vector3 theDestination;
}
