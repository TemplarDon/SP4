using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class MeleeFSM : FSMBase {

    enum STATES
    {
        IDLE,
        FOLLOW_ORDER,
        CHASE,
        ATTACK,
        HELP,
    }

    STATES CurrentState;

    bool b_NearEnemy = false;
    bool b_Attacked = false;
    GameObject m_TargetedEnemy = null;

    List<GameObject> AlliedUnits = new List<GameObject>();

	// Use this for initialization
	void Start () {
        CurrentState = STATES.IDLE;
	}

    public override void Sense()
    {
        Message temp =  ReadFromMessageBoard();
        if (temp != null)
        {
            CurrentMessage = temp;
            //Debug.Log("Received a message.");
        }


        // Get Enemy data
        GameObject[] goList = GameObject.FindGameObjectsWithTag("Character");

        foreach (GameObject go in goList)
        {
            if (!go.GetComponent<BaseCharacter>().enabled || go.GetComponent<BaseCharacter>() == this.GetComponent<BaseCharacter>())
            {
                if (AlliedUnits.Contains(go))
                    AlliedUnits.Remove(go);

                continue;
            }

            if (AlliedUnits.Contains(go))
                continue;

            if (go.GetComponent<BaseCharacter>().IsEnemy)
            {
                AlliedUnits.Add(go);
            }

        }

        //foreach (BaseCharacter aCharacter in GameObject.Find("EnemyTeamManager").GetComponent<teamManager>().teamList)
        //{
        //    if (!aCharacter.enabled || aCharacter == this.GetComponent<BaseCharacter>())
        //    {
        //        if (AlliedUnits.Contains(aCharacter.gameObject))
        //            AlliedUnits.Remove(aCharacter.gameObject);

        //        continue;
        //    }

        //    if (AlliedUnits.Contains(aCharacter.gameObject))
        //        continue;

        //    if (aCharacter.IsEnemy)
        //    {
        //        AlliedUnits.Add(aCharacter.gameObject);
        //    }
        //}

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

        //foreach (BaseCharacter aCharacter in GameObject.Find("EnemyTeamManager").GetComponent<teamManager>().teamList)
        //{
        //    if (!aCharacter.enabled || aCharacter == this.GetComponent<BaseCharacter>())
        //        continue;

        //    if (!aCharacter.IsEnemy)
        //    {
        //        if ((this.GetComponent<BaseCharacter>().pos - aCharacter.pos).sqrMagnitude < AggroRange * AggroRange)
        //        {
        //            if ((this.GetComponent<BaseCharacter>().pos - aCharacter.pos).sqrMagnitude < ClosestDist)
        //            {
        //                ClosestDist = (this.GetComponent<BaseCharacter>().pos - aCharacter.pos).sqrMagnitude;
        //                b_Change = true;

        //                b_NearEnemy = true;
        //                m_TargetedEnemy = aCharacter.gameObject;

        //                //Debug.Log("Enemy near!");
        //            }
        //        }
        //    }
        //}

        if (!b_Change)
        {
            b_NearEnemy = false;
        }
        
        //int EmptyPositisions = 0;
        //BaseCharacter checkGo = this.GetComponent<BaseCharacter>().theLevel.GetCharacterInTile(this.GetComponent<BaseCharacter>().pos + new Vector3(0, AggroRange, 0));
        //if (checkGo != null)
        //{
        //    if (!checkGo.IsEnemy)
        //    {
        //        b_NearEnemy = true;
        //        m_TargetedEnemy = checkGo.gameObject;

        //        Debug.Log("Enemy near!");
        //    }
        //}
        //else
        //{
        //    EmptyPositisions++;
        //}

        //checkGo = this.GetComponent<BaseCharacter>().theLevel.GetCharacterInTile(this.GetComponent<BaseCharacter>().pos + new Vector3(0, -AggroRange, 0));
        //if (checkGo != null)
        //{
        //    if (!checkGo.IsEnemy)
        //    {
        //        b_NearEnemy = true;
        //        m_TargetedEnemy = checkGo.gameObject;

        //        Debug.Log("Enemy near!");
        //    }
        //}
        //else
        //{
        //    EmptyPositisions++;
        //}

        //checkGo = this.GetComponent<BaseCharacter>().theLevel.GetCharacterInTile(this.GetComponent<BaseCharacter>().pos + new Vector3(AggroRange, 0, 0));
        //if (checkGo != null)
        //{
        //    if (!checkGo.IsEnemy)
        //    {
        //        b_NearEnemy = true;
        //        m_TargetedEnemy = checkGo.gameObject;

        //        Debug.Log("Enemy near!");
        //    }
        //}
        //else
        //{
        //    EmptyPositisions++;
        //}

        //checkGo = this.GetComponent<BaseCharacter>().theLevel.GetCharacterInTile(this.GetComponent<BaseCharacter>().pos + new Vector3(-AggroRange, 0, 0));
        //if (checkGo != null)
        //{
        //    if (!checkGo.IsEnemy)
        //    {
        //        b_NearEnemy = true;
        //        m_TargetedEnemy = checkGo.gameObject;

        //        Debug.Log("Enemy near!");
        //    }
        //}
        //else
        //{
        //    EmptyPositisions++;
        //}


        //if (EmptyPositisions == 4)
        //{
        //    b_NearEnemy = false;
        //}

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

                if (CurrentMessage != null)
                    return (int)STATES.FOLLOW_ORDER;

                if (b_NearEnemy && (this.GetComponent<BaseCharacter>().restrictActions[0] == false && this.GetComponent<BaseCharacter>().restrictActions[1] == false))
                {
                    return (int)STATES.CHASE;
                }

                return (int)STATES.IDLE;


            case STATES.FOLLOW_ORDER:

                //if (b_NearEnemy && this.GetComponent<Pathfinder>().b_CompletedPath)
                //{
                //    return (int)STATES.CHASE;
                //}

                if (this.GetComponent<Pathfinder>().b_CompletedPath)
                    return (int)STATES.IDLE;

                if (b_NearEnemy)
                    return (int)STATES.CHASE;
                

                return (int)STATES.FOLLOW_ORDER;


            case STATES.CHASE:

                if (m_TargetedEnemy == null)
                    return (int)STATES.IDLE;

                if (CanAttack() && this.GetComponent<Pathfinder>().b_CompletedPath)
                {
                    b_Attacked = false;
                    return (int)STATES.ATTACK;
                }

                if (this.GetComponent<BaseCharacter>().restrictActions[0] == true || this.GetComponent<BaseCharacter>().restrictActions[1] == true)
                    return (int)STATES.IDLE;

                return (int)STATES.CHASE;


            case STATES.ATTACK:

                if (m_TargetedEnemy == null || b_Attacked)
                    return (int)STATES.IDLE;

                if (!b_NearEnemy && m_TargetedEnemy != null)
                    return (int)STATES.CHASE;

                return (int)STATES.ATTACK;


            case STATES.HELP:

                return (int)STATES.HELP;

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

            case (int)STATES.FOLLOW_ORDER:
                CurrentState = STATES.FOLLOW_ORDER;
                DoFollowOrder();
                break;

            case (int)STATES.CHASE:
                CurrentState = STATES.CHASE;
                DoChase();
                break;

            case (int)STATES.ATTACK:
                CurrentState = STATES.ATTACK;
                DoAttack();
                break;

            case (int)STATES.HELP:
                CurrentState = STATES.HELP;
                DoHelp();
                break;
        }
    }
    
    void DoIdle()
    {
        Debug.Log( this.gameObject.name + " Idling");
        this.GetComponent<BaseCharacter>().restrictActions[0] = true;
        this.GetComponent<BaseCharacter>().restrictActions[1] = true;
    }

    void DoFollowOrder()
    {
        Debug.Log(this.gameObject.name + " Follow");
        ProcessMessage();
    }


    void DoChase()
    {
        Debug.Log(this.gameObject.name + " Chase");
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
        Debug.Log(this.gameObject.name + " Attack");
        this.GetComponent<Pathfinder>().Reset();
        if (m_TargetedEnemy != null && !b_Attacked)
        {
            this.GetComponent<BaseCharacter>().restrictActions[1] = true;
            m_TargetedEnemy.GetComponent<BaseCharacter>().TakeDamage(this.GetComponent<BaseCharacter>().GetAttackDamage());

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

            //Debug.Log("Attacking!");
        }
    }

    void DoHelp()
    { }

    public override void ProcessMessage()
    {
        if (CurrentMessage == null)
        {
            if (this.GetComponent<Pathfinder>().b_CompletedPath)
            {
                this.GetComponent<BaseCharacter>().restrictActions[1] = true;
                //this.GetComponent<BaseCharacter>().SetCharacterDestination(this.GetComponent<BaseCharacter>().GetCharacterDestination());
                this.GetComponent<BaseCharacter>().SetToMove(false);
                return;
            }

            if (!this.GetComponent<Pathfinder>().b_PathFound)
                this.GetComponent<BaseCharacter>().SetCharacterDestination(this.GetComponent<BaseCharacter>().GetCharacterDestination());

            this.GetComponent<BaseCharacter>().SetToMove(true);
            return;

        }

        switch (CurrentMessage.theMessageType)
        {
            case Message.MESSAGE_TYPE.ORDER_FRONTAL_ASSAULT:

                //Vector3 EnemyAvgPos = CurrentMessage.theDestination;

                //float ClosestDist = 99999;
                //int ClosestIdx = 0;

                //GameObject[] goList = GameObject.FindGameObjectsWithTag("Character");

                //for (int i = 0; i < goList.Length; ++i)
                //{
                //    GameObject go = goList[i];
                //    if (go.GetComponent<BaseCharacter>().IsEnemy || go.GetComponent<BaseCharacter>().IsDead)
                //        continue;
                    
                //    if ( (go.GetComponent<BaseCharacter>().pos - this.GetComponent<) )

                //}

                this.GetComponent<BaseCharacter>().SetCharacterDestination(CurrentMessage.theDestination);
                this.GetComponent<BaseCharacter>().SetToMove(true);

                Debug.Log("Following Order: Frontal Assualt");
                break;

            case Message.MESSAGE_TYPE.ORDER_SURROUND_TARGET:
                this.GetComponent<BaseCharacter>().SetCharacterDestination(FindClosestSpot(CurrentMessage.theTarget.GetComponent<BaseCharacter>().pos));

                Instantiate(GameObject.Find("PathTile"), CurrentMessage.theTarget.GetComponent<BaseCharacter>().pos, Quaternion.identity);

                this.GetComponent<BaseCharacter>().SetToMove(true);
                Debug.Log("Following Order: Surround Target");
           
                break;

            case Message.MESSAGE_TYPE.ORDER_FALLBACK:
                break;

            case Message.MESSAGE_TYPE.ORDER_PROTECT_COMMMANDER:
                break;
        }
        CurrentMessage = null;
    }

    Vector3 FindClosestSpot(Vector3 target)
    {
        List<Vector3> PossibleLocations = new List<Vector3>();
        PossibleLocations.Add(target + new Vector3(0, 1, 0));
        PossibleLocations.Add(target + new Vector3(0, -1, 0));
        PossibleLocations.Add(target + new Vector3(1, 0, 0));
        PossibleLocations.Add(target + new Vector3(-1, 0, 0));

        bool[] Marked = new bool[4]{ false, false, false, false };

        // Check if possible locations are valid
        for (int i = 0; i < PossibleLocations.Count; ++i)
        {
            Vector3 check = PossibleLocations[i];
            if (this.GetComponent<BaseCharacter>().theLevel.GetTileCost((int)check.x - 1, (int)-check.y - 1) == -1 || this.GetComponent<BaseCharacter>().theLevel.GetCharacterInTile(check) != null)
            {
                //Debug.Log("Found invalid closest spot.");
                Marked[i] = true;
            }
        }

        for (int i = 0; i < PossibleLocations.Count; ++i)
        {
            if (Marked[i])
                PossibleLocations.RemoveAt(i);
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
            foreach (GameObject go in AlliedUnits)
            {
                if (go.GetComponent<BaseCharacter>().name == "EnemyCommander")
                    continue;

                if (go.GetComponent<FSMBase>().CurrentMessage == null)
                    continue;

                if (go.GetComponent<FSMBase>().CurrentMessage.theMessageType == Message.MESSAGE_TYPE.ORDER_SURROUND_TARGET)
                {
                    if (go.GetComponent<BaseCharacter>().GetCharacterDestination() == CheckPos)
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

        return new Vector3(0,0,0);
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

    public override void TurnReset()
    {
        b_Attacked = false;
    }

}
