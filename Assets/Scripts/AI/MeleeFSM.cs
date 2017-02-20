using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class MeleeFSM : FSMBase {

    public int AggroRange = 1;

    enum STATES
    {
        IDLE,
        FOLLOW_ORDER,
        ATTACK,
        HELP,
    }

    STATES CurrentState;

    bool b_NearEnemy = false;
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
            Debug.Log("Received a message.");
        }

        // Get Enemy data
        GameObject[] goList = GameObject.FindGameObjectsWithTag("Character");

        foreach (GameObject go in goList)
        {
            if (go.GetComponent<BaseCharacter>().IsDead || go.GetComponent<BaseCharacter>() == this.GetComponent<BaseCharacter>())
                continue;

            if (!go.GetComponent<BaseCharacter>().IsEnemy)
            {
                if ((this.GetComponent<BaseCharacter>().pos - go.GetComponent<BaseCharacter>().pos).sqrMagnitude < AggroRange * AggroRange)
                {
                    b_NearEnemy = true;
                }
            }
            else
            {
                AlliedUnits.Add(go);
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

                return (int)STATES.IDLE;


            case STATES.FOLLOW_ORDER:

                if (b_NearEnemy)
                    return (int)STATES.ATTACK;

                return (int)STATES.FOLLOW_ORDER;


            case STATES.ATTACK:

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
    { }

    void DoFollowOrder()
    {
        ProcessMessage();
    }

    void DoAttack()
    { }

    void DoHelp()
    { }

    public override void ProcessMessage()
    {
        if (CurrentMessage == null)
            return;

        switch (CurrentMessage.theMessageType)
        {
            case Message.MESSAGE_TYPE.ORDER_FRONTAL_ASSAULT:
                this.GetComponent<BaseCharacter>().SetToMove(true);
                this.GetComponent<BaseCharacter>().SetCharacterDestination(CurrentMessage.theDestination);

                Debug.Log("Following Order: Frontal Assualt");
                break;

            case Message.MESSAGE_TYPE.ORDER_SURROUND_TARGET:
                m_TargetedEnemy = CurrentMessage.theTarget;

                List<Vector3> PossibleLocations = new List<Vector3>();
                PossibleLocations.Add(m_TargetedEnemy.GetComponent<BaseCharacter>().pos + new Vector3(0, 1, 0));
                PossibleLocations.Add(m_TargetedEnemy.GetComponent<BaseCharacter>().pos + new Vector3(0, -1, 0));
                PossibleLocations.Add(m_TargetedEnemy.GetComponent<BaseCharacter>().pos + new Vector3(1, 0, 0));
                PossibleLocations.Add(m_TargetedEnemy.GetComponent<BaseCharacter>().pos + new Vector3(-1, 0, 0));

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

                    foreach (GameObject go in AlliedUnits)
                    {
                        if (go.GetComponent<FSMBase>().CurrentMessage.theMessageType == Message.MESSAGE_TYPE.ORDER_SURROUND_TARGET)
                        {
                            if (go.GetComponent<BaseCharacter>().GetCharacterDestination() == CheckPos)
                            {
                                break;
                            }
                        }

                        LocationFound = true;
                        this.GetComponent<BaseCharacter>().SetToMove(true);
                        this.GetComponent<BaseCharacter>().SetCharacterDestination(CheckPos);

                        Debug.Log("Following Order: Surround Target");
                    }
                }
                break;

            case Message.MESSAGE_TYPE.ORDER_FALLBACK:
                break;

            case Message.MESSAGE_TYPE.ORDER_PROTECT_COMMMANDER:
                break;
        }
        CurrentMessage = null;
    }

}
