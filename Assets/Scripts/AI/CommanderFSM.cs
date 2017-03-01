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
        CHASE,
        ATTACK,
    }

    STATES CurrentState;

    List<BaseCharacter> EnemyList = new List<BaseCharacter>();      // List to store the enemy (e.g the player's units)
    List<BaseCharacter> TeamList = new List<BaseCharacter>();       // List to store the team units
        
    bool b_PlanFound = false;                                       // Bool to trigger state switch from PLAN to SEND_ORDERS
    bool b_OrderSent = false;                                       // Bool to trigger state switch from SEND_ORDER to PLAN
    List<Message> MessageBuffer = new List<Message>();              // Message buffer to send to units
    int TurnCounter = 5;                                            // Counter for the amount of turns passed
    GameObject Target;                                              // Handle to message target, if needed

    bool b_NearEnemy = false;
    bool b_Attacked = false;
    GameObject m_TargetedEnemy = null;

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

        bool b_Change = false;
        float ClosestDist = 99999;
        foreach (GameObject go in goList)
        {
            if (!go.GetComponent<BaseCharacter>().enabled || go.GetComponent<BaseCharacter>() == this.GetComponent<BaseCharacter>())
                continue;

            if (!go.GetComponent<BaseCharacter>().IsEnemy)
            {
                if ((this.GetComponent<BaseCharacter>().pos - go.GetComponent<BaseCharacter>().pos).sqrMagnitude < AggroRange * AggroRange)
                {
                    if ((this.GetComponent<BaseCharacter>().pos - go.GetComponent<BaseCharacter>().pos).sqrMagnitude < ClosestDist)
                    {
                        ClosestDist = (this.GetComponent<BaseCharacter>().pos - go.GetComponent<BaseCharacter>().pos).sqrMagnitude;
                        b_Change = true;

                        b_NearEnemy = true;
                        m_TargetedEnemy = go.gameObject;

                        //Debug.Log("Enemy near!");
                    }
                }
            }
        }

        if (!b_Change)
        {
            b_NearEnemy = false;
        }

        if (m_TargetedEnemy != null)
        {
            if (!m_TargetedEnemy.GetComponent<BaseCharacter>().enabled)
            {
                b_NearEnemy = false;
                m_TargetedEnemy = null;
            }
        }
    }

    public override int Think()
    {
        switch (CurrentState)
        {
            case STATES.IDLE:

                if (TeamList.Count <= 0)
                {
                    if (b_NearEnemy)
                    {
                        Debug.Log("Going into CHASE.");
                        return (int)STATES.CHASE;
                    }
                }

                if (TurnCounter >= OrderFrequency)
                {
                    TurnCounter = 0;

                    //Debug.Log("Going into PLAN.");

                    return (int)STATES.PLAN;
                }

                return (int)STATES.IDLE;


            case STATES.PLAN:

                if (b_PlanFound)
                {
                    //Debug.Log("Going into SEND_ORDERS.");
                    return (int)STATES.SEND_ORDERS;
                }

                return (int)STATES.PLAN;

            case STATES.SEND_ORDERS:

                if (b_OrderSent)
                {
                    //Debug.Log("Going into IDLE.");
                    return (int)STATES.IDLE;
                }

                return (int)STATES.SEND_ORDERS;

            case STATES.CHASE:

                if (m_TargetedEnemy == null)
                    return (int)STATES.IDLE;

                if (CanAttack() && this.GetComponent<Pathfinder>().b_CompletedPath)
                {
                    //Debug.Log("Going into ATTACK.");

                    b_Attacked = false;
                    return (int)STATES.ATTACK;
                }

                if (this.GetComponent<BaseCharacter>().restrictActions[0] == true)
                    return (int)STATES.IDLE;

                return (int)STATES.CHASE;


            case STATES.ATTACK:

                if (!b_NearEnemy && m_TargetedEnemy != null)
                    return (int)STATES.CHASE;

                if (m_TargetedEnemy == null || b_Attacked)
                    return (int)STATES.IDLE;

                return (int)STATES.ATTACK;
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

            case (int)STATES.CHASE:
                CurrentState = STATES.CHASE;
                DoChase();
                break;

            case (int)STATES.ATTACK:
                CurrentState = STATES.ATTACK;
                DoAttack();
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
            //Debug.Log("Sending orders.");
        }

        b_PlanFound = false;
        b_OrderSent = true;
        MessageBuffer.Clear();
    }

    void DoChase()
    {
        //Debug.Log("Melee Chase");
        //Debug.Log(this.GetComponent<BaseCharacter>().restrictActions[0].ToString() + " " + this.GetComponent<BaseCharacter>().restrictActions[1].ToString());
        if (this.GetComponent<BaseCharacter>().restrictActions[0] == false && this.GetComponent<BaseCharacter>().restrictActions[1] == false)
        {
            if (!this.GetComponent<Pathfinder>().b_PathFound)
                this.GetComponent<BaseCharacter>().SetCharacterDestination(FindClosestSpot(m_TargetedEnemy.GetComponent<BaseCharacter>().pos));

            this.GetComponent<BaseCharacter>().SetToMove(true);
        }
    }

    void DoAttack()
    {
        //Debug.Log("Melee Attack");
        this.GetComponent<Pathfinder>().Reset();
        if (m_TargetedEnemy != null && !b_Attacked)
        {
            this.GetComponent<BaseCharacter>().restrictActions[1] = true;
            m_TargetedEnemy.GetComponent<BaseCharacter>().TakeDamage(this.GetComponent<BaseCharacter>().GetAttackDamage());

            GameObject.Find("EffectsSoundPlayer").GetComponent<SoundManager>().PlaySound("MeleeAttack");

            int attacker_x = Mathf.RoundToInt(this.GetComponent<BaseCharacter>().pos.x);
            int attacker_y = Mathf.RoundToInt(this.GetComponent<BaseCharacter>().pos.y);

            int reciever_x = Mathf.RoundToInt(m_TargetedEnemy.GetComponent<BaseCharacter>().pos.x);
            int reciever_y = Mathf.RoundToInt(m_TargetedEnemy.GetComponent<BaseCharacter>().pos.y);


            if (reciever_x < attacker_x && reciever_y == attacker_y)
            {
                //this.GetComponent<Animator>().Play("CharacterAnimationLeft");
                this.GetComponent<BaseCharacter>().CurrentAnimState = BaseCharacter.ANIM_STATE.ATTACK_LEFT;
            }
            else if (reciever_x > attacker_x && reciever_y == attacker_y)
            {
                //this.GetComponent<Animator>().Play("CharacterAnimationRight");
                this.GetComponent<BaseCharacter>().CurrentAnimState = BaseCharacter.ANIM_STATE.ATTACK_RIGHT;
            }
            else if (reciever_y < attacker_y && reciever_x == attacker_x)
            {
                //this.GetComponent<Animator>().Play("CharacterAnimationDown");
                this.GetComponent<BaseCharacter>().CurrentAnimState = BaseCharacter.ANIM_STATE.ATTACK_DOWN;
            }
            else if (reciever_y > attacker_y && reciever_x == attacker_x)
            {
                //this.GetComponent<Animator>().Play("CharacterAnimationUp");
                this.GetComponent<BaseCharacter>().CurrentAnimState = BaseCharacter.ANIM_STATE.ATTACK_UP;
            }

            b_Attacked = true;

            Debug.Log("Attacking!");
        }
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

    Vector3 FindClosestSpot(Vector3 target)
    {
        List<Vector3> PossibleLocations = new List<Vector3>();
        PossibleLocations.Add(target + new Vector3(0, 1, 0));
        PossibleLocations.Add(target + new Vector3(0, -1, 0));
        PossibleLocations.Add(target + new Vector3(1, 0, 0));
        PossibleLocations.Add(target + new Vector3(-1, 0, 0));

        // Check if possible locations are valid
        foreach (Vector3 check in PossibleLocations)
        {
            if (this.GetComponent<BaseCharacter>().theLevel.GetTileCost((int)check.x, (int)-check.y) == 4 || this.GetComponent<BaseCharacter>().theLevel.GetTileCost((int)check.x, (int)-check.y) == 5)
                PossibleLocations.Remove(check);
        }

        bool LocationFound = false;
        while (!LocationFound)
        {
            // Find closest pos
            float ClosestDist = 99999;
            int ClosestIdx = 0;
            for (int i = 0; i < PossibleLocations.Count; ++i)
            {
                if ((this.GetComponent<BaseCharacter>().pos - PossibleLocations[i]).magnitude < ClosestDist)
                {
                    ClosestDist = (this.GetComponent<BaseCharacter>().pos - PossibleLocations[i]).magnitude;
                    ClosestIdx = i;
                }
            }

            Vector3 CheckPos = PossibleLocations[ClosestIdx];
            PossibleLocations.RemoveAt(ClosestIdx);

            bool PosTaken = false;
            foreach (BaseCharacter aCharacter in TeamList)
            {
                if (aCharacter.name == "EnemyCommander")
                    continue;

                if (aCharacter.gameObject.GetComponent<FSMBase>().CurrentMessage.theMessageType == Message.MESSAGE_TYPE.ORDER_SURROUND_TARGET)
                {
                    if (aCharacter.GetCharacterDestination() == CheckPos)
                    {
                        PosTaken = true;
                    }
                }
            }

            if (!PosTaken)
            {
                LocationFound = true;
                return CheckPos;
            }
        }

        return new Vector3(0, 0, 0);
    }

    bool CanAttack()
    {
        BaseCharacter checkGo = this.GetComponent<BaseCharacter>().theLevel.GetCharacterInTile(this.GetComponent<BaseCharacter>().pos + new Vector3(0, -this.GetComponent<BaseCharacter>().BaseAttackRange, 0));
        if (checkGo == m_TargetedEnemy.GetComponent<BaseCharacter>())
        {
            return true;
        }

        checkGo = this.GetComponent<BaseCharacter>().theLevel.GetCharacterInTile(this.GetComponent<BaseCharacter>().pos + new Vector3(0, this.GetComponent<BaseCharacter>().BaseAttackRange, 0));
        if (checkGo == m_TargetedEnemy.GetComponent<BaseCharacter>())
        {
            return true;
        }

        checkGo = this.GetComponent<BaseCharacter>().theLevel.GetCharacterInTile(this.GetComponent<BaseCharacter>().pos + new Vector3(-this.GetComponent<BaseCharacter>().BaseAttackRange, 0, 0));
        if (checkGo == m_TargetedEnemy.GetComponent<BaseCharacter>())
        {
            return true;
        }

        checkGo = this.GetComponent<BaseCharacter>().theLevel.GetCharacterInTile(this.GetComponent<BaseCharacter>().pos + new Vector3(-this.GetComponent<BaseCharacter>().BaseAttackRange, 0, 0));
        if (checkGo == m_TargetedEnemy.GetComponent<BaseCharacter>())
        {
            return true;
        }

        return false;
    }
}
