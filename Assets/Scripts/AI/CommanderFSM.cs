using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class CommanderFSM : FSMBase {

    // Public Vars
    public int RecallHealthThreshold = 3;   // The amount of health where the commander will call units to fallback and protect him
    public int ActivaRange = 8;             // The range where the commander will start moving his units
    public int TacticsRange = 5;            // The range where the commander starts to use other orders besides ORDER_FRONTAL_ASSAULT
    public int OrderFrequency = 2;          // Amount of turns before another order will be given

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
    List<Message> MessageBuffer = new List<Message>();              // Message buffer to send to units
    int TurnCounter = 5;                                            // Counter for the amount of turns passed
    GameObject Target;                                              // Handle to message target, if needed

	// Use this for initialization
	void Start () {
        CurrentState = STATES.IDLE;
	}
	
    public override void Sense()     
    {
        // Get Enemy data
        GameObject[] goList = GameObject.FindGameObjectsWithTag("Character");

        foreach (GameObject go in goList)
        {
            if (!go.GetComponent<BaseCharacter>().enabled || go.GetComponent<BaseCharacter>() == this.GetComponent<BaseCharacter>())
            {
                if (!go.GetComponent<BaseCharacter>().IsEnemy)
                {
                    if (EnemyList.Contains(go.GetComponent<BaseCharacter>()))
                        EnemyList.Remove(go.GetComponent<BaseCharacter>());
                }
                else
                {
                    if (TeamList.Contains(go.GetComponent<BaseCharacter>()))
                        TeamList.Remove(go.GetComponent<BaseCharacter>());
                }

                continue;
            }

            if (EnemyList.Contains(go.GetComponent<BaseCharacter>()) || TeamList.Contains(go.GetComponent<BaseCharacter>()))
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
                {
                    TurnCounter = 0;

                    Debug.Log("Going into PLAN.");

                    return (int)STATES.PLAN;
                }

                return (int)STATES.IDLE;


            case STATES.PLAN:

                if (b_PlanFound)
                {
                    Debug.Log("Going into SEND_ORDERS.");
                    return (int)STATES.SEND_ORDERS;
                }

                return (int)STATES.PLAN;

            case STATES.SEND_ORDERS:

                if (b_OrderSent)
                {
                    Debug.Log("Going into IDLE.");
                    return (int)STATES.IDLE;
                }

                return (int)STATES.SEND_ORDERS;

        }

        return -1;
    }

    public override void Act(int value)
    {
        switch (value)
        {
            case (int)STATES.IDLE:
                CurrentState = STATES.IDLE;
                DoIdle();
                break;

            case (int)STATES.PLAN:
                CurrentState = STATES.PLAN;
                DoPlan();
                break;

            case (int)STATES.SEND_ORDERS:
                CurrentState = STATES.SEND_ORDERS;
                DoSendOrders();
                break;
        }
    }

    void DoIdle()
    {
        this.GetComponent<BaseCharacter>().restrictActions[1] = true;
        Debug.Log("Commander done.");
    }

    void DoPlan()
    {
        float ClosestDistToEnemy = 99999;
        foreach (BaseCharacter AICharacter in EnemyList)
        {
            foreach (BaseCharacter PlayerCharacter in TeamList)
            {
                if ((AICharacter.pos - PlayerCharacter.pos).magnitude < ClosestDistToEnemy)
                {
                    ClosestDistToEnemy = (AICharacter.pos - PlayerCharacter.pos).magnitude;
                }
            }
        }

        Vector3 AverageTeamPosition = new Vector3(0, 0, 0);
        foreach (BaseCharacter Character in TeamList)
        {
            AverageTeamPosition += Character.pos;
        }
        AverageTeamPosition /= TeamList.Count;

        Vector3 AverageEnemyPosition = new Vector3(0, 0, 0);
        foreach (BaseCharacter Character in EnemyList)
        {
            AverageEnemyPosition += Character.pos;
        }
        AverageEnemyPosition /= EnemyList.Count;

        //float AverageDistToEnemy = (AverageTeamPosition - AverageEnemyPosition).magnitude;

        // Check AverageDistToEnemy 
        if (ClosestDistToEnemy > ActivaRange)
        {
            b_PlanFound = true;
            return;
        }

        if (ClosestDistToEnemy > TacticsRange)
        {
            // Enemy further away than TacticsRange, send ORDER_FRONTAL_ASSAULT
            foreach (BaseCharacter Character in TeamList)
            {
                Message aMessage = new Message();
                aMessage.theMessageType = Message.MESSAGE_TYPE.ORDER_FRONTAL_ASSAULT;
                aMessage.theSender = this.gameObject;
                aMessage.theReceiver = Character.gameObject;
                aMessage.theTarget = null;
                aMessage.theDestination = AverageEnemyPosition;

                MessageBuffer.Add(aMessage);
            }

            b_PlanFound = true;
            b_OrderSent = false;
        }
        else
        {
            // Enemy within tactics range, send ORDER_SURROUND_TARGET

            // Find target to surround
            float ClosestDist = 99999;
            int ClosestIdx = 0;
            for (int i = 0; i < EnemyList.Count; ++i)
            {
                BaseCharacter Enemy = EnemyList[i];
                if ((AverageTeamPosition - Enemy.pos).magnitude < ClosestDist)
                {
                    ClosestDist = (AverageTeamPosition - Enemy.pos).magnitude;
                    ClosestIdx = i;
                }
            }

            foreach (BaseCharacter Character in TeamList)
            {
                Message aMessage = new Message();
                aMessage.theMessageType = Message.MESSAGE_TYPE.ORDER_SURROUND_TARGET;
                aMessage.theSender = this.gameObject;
                aMessage.theReceiver = Character.gameObject;
                aMessage.theTarget = EnemyList[ClosestIdx].gameObject;

                MessageBuffer.Add(aMessage);
            }

            b_PlanFound = true;
            b_OrderSent = false;
        }
    }

    void DoSendOrders()
    {
        foreach (Message aMessage in MessageBuffer)
        {
            theBoard.AddMessage(aMessage);
            Debug.Log("Sending orders.");
        }

        b_PlanFound = false;
        b_OrderSent = true;
        MessageBuffer.Clear();
    }

    public override void ProcessMessage()
    {
    }

    public override void TurnReset()
    {
    }

    public void IncreaseTurnCount()
    {
        TurnCounter++;
        Debug.Log("Commander increase turn counter.");

        foreach (BaseCharacter aCharacter in TeamList)
        {
            aCharacter.GetComponent<FSMBase>().TurnReset();
        }
    }
}
