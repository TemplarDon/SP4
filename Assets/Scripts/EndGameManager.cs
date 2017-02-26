using UnityEngine;
using System.Collections;

public class EndGameManager : MonoBehaviour {

    bool b_GameEnded = false;
    
    public teamManager m_PlayerTeam;
    public teamManager m_EnemyTeam;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        if (m_EnemyTeam.GetComponent<teamManager>().teamList.Count <= 0)
        {
            b_GameEnded = true;
        }
        
        if (m_PlayerTeam.GetComponent<teamManager>().teamList.Count <= 0)
        {
            b_GameEnded = true;
        }

	}

    public bool GetGameStatus()
    {
        return b_GameEnded;
    }
}
