using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class CommanderFSM : FSMBase {

    // Public Vars
    public int RecallHealthThreshold;   // The amount of health where the commander will call units to fallback and protect him
    public int TacticsRange;            // The range where the commander starts to use other orders besides ORDER_FRONTAL_ASSAULT
    public int OrderFrequency;          // Amount of turns before another order will be given

    // Private Vars
    enum STATES
    {
        IDLE,
        PLAN,
        SEND_ORDERS,
    }

    STATES CurrentState;

    List<BaseCharacter> EnemyList = new List<BaseCharacter>();      // List to store the enemy (e.g the player's units)
    List<BaseCharacter> TeamList = new List<BaseCharacter>();       // List to store the team units
        
    bool b_PlanFound = false;                                       // Bool to trigger state switch from PLAN to SEND_ORDERS
    bool b_OrderSent = false;                                       // Bool to trigger state switch from SEND_ORDER to PLAN
    Dictionary<Message, List<GameObject>> MessageBuffer;            // Message buffer to send to units
    int TurnCounter = 0;                                            // Counter for the amount of turns passed
    GameObject Target;                                              // Handle to message target, if needed

	// Use this for initialization
	void Start () {
        CurrentState = STATES.PLAN;
	}
	
    public override void Sense()     
    {
        // Get Enemy data
        GameObject[] goList = GameObject.FindGameObjectsWithTag("Character");

        foreach (GameObject go in goList)
        {
            if (go.GetComponent<BaseCharacter>().IsDead)
                continue;

            if (!go.GetComponent<BaseCharacter>().IsEnemy)
            {
                EnemyList.Add(go.GetComponent<BaseCharacter>());
            }
            else
            {
                TeamList.Add(go.GetComponent<BaseCharacter>());
            }
        }
    }

    public override int Think()
    {
        switch (CurrentState)
        {
            case STATES.IDLE:

                if (TurnCounter >= OrderFrequency)
                    return (int)STATES.PLAN;

                return (int)STATES.IDLE;


            case STATES.PLAN:

                if (b_PlanFound)
                    return (int)STATES.SEND_ORDERS;


                return (int)STATES.PLAN;

            case STATES.SEND_ORDERS:

                if (b_OrderSent)
                    return (int)STATES.PLAN;

                return (int)STATES.SEND_ORDERS;

        }

        return -1;
    }

    public override void Act(int value)
    {
        switch (CurrentState)
        {
            case STATES.PLAN:
                DoPlan();
                break;

            case STATES.SEND_ORDERS:
                DoSendOrders();
                break;
        }
    }

    void DoPlan()
    {
        float AverageDistToEnemy;

        float TotalDist = 0;
        foreach (BaseCharacter aCharacter in EnemyList)
        {
            TotalDist += (aCharacter.pos - this.GetComponent<BaseCharacter>().pos).magnitude;
        }
        AverageDistToEnemy = TotalDist / EnemyList.Count;

        GameObject[] TeamList = GameObject.FindGameObjectsWithTag("Characters");

        //// Check AverageDistToEnemy 
        //if (AverageDistToEnemy > TacticsRange)
        //{
        //    // Enemy further away than TacticsRange, send ORDER_FRONTAL_ASSAULT
        //    Message aMessage = new Message();
        //    aMessage.theMessageType = Message.MESSAGE_TYPE.ORDER_FRONTAL_ASSAULT;
        //    aMessage.theSender = this.gameObject;
        //    aMessage.theReceiver = aReceiver;
        //    aMessage.theTarget = null;

        //    MessageToSend = Message.MESSAGE_TYPE.ORDER_FRONTAL_ASSAULT;
        //    b_PlanFound = true;
        //}
        //else
        //{
        //    // Enemy within tactics range, send ORDER_SURROUND_TARGET
        //    MessageToSend = Message.MESSAGE_TYPE.ORDER_SURROUND_TARGET;
        //    b_PlanFound = true;
        //}
    }

    void DoSendOrders()
    {

    }
}
