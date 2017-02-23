using UnityEngine;
using System.Collections;

public abstract class FSMBase : MonoBehaviour {

    public MessageBoard theBoard;
    public Message CurrentMessage = null;     // Handle to message

    public int AggroRange = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (GameObject.Find("TurnManager").GetComponent<turnManage>().teamTurn == 4)
        {
            GameObject.Find("EnemyTeamManager").GetComponent<teamManager>().GetCurrentActiveMember().GetComponent<FSMBase>().RunFSM();
        }
	}

    public void RunFSM()
    {
        Sense();

        int actValue = Think();
        if (actValue != -1)
        {
            Act(actValue);
        }
    }

    public abstract void Sense();           // get/receive updates from the world
    public abstract int Think();            // process the updates
    public abstract void Act(int value);    // act upon any change in behaviour
    public abstract void ProcessMessage();  // process message received
    public abstract void TurnReset();       // reset any variables when turn increases

    public Message ReadFromMessageBoard()
    {
        return theBoard.GetMessage(this.gameObject.GetInstanceID());
    }
}
