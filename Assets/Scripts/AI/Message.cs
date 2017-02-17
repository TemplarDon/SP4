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

    //public Message CreateMessage(MESSAGE_TYPE aType, GameObject aSender, GameObject aReceiver, GameObject aTarget = null)
    //{
    //    Message returnMessage = new Message();
    //    returnMessage.theMessageType = aType;
    //    returnMessage.theSender = aSender;
    //    returnMessage.theReceiver = aReceiver;
    //    returnMessage.theTarget = aTarget;

    //    return returnMessage;
    //}

}
