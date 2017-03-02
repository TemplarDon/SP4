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
            GameObject.Find("Main Camera").transform.position = new Vector3(GameObject.Find("EnemyTeamManager").GetComponent<teamManager>().GetCurrentActiveMember().transform.position.x, GameObject.Find("EnemyTeamManager").GetComponent<teamManager>().GetCurrentActiveMember().transform.position.y, -9);
            GameObject.Find("Main Camera").GetComponent<cameramove>().currentLoc = new Vector3(GameObject.Find("EnemyTeamManager").GetComponent<teamManager>().GetCurrentActiveMember().transform.position.x, GameObject.Find("EnemyTeamManager").GetComponent<teamManager>().GetCurrentActiveMember().transform.position.y, -9);
            GameObject.Find("EnemyTeamManager").GetComponent<teamManager>().GetCurrentActiveMember().b_EnemyActive = true;
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
        if (theBoard != null)
            return theBoard.GetMessage(this.gameObject.GetInstanceID());
        else
            return null;
    }
}
