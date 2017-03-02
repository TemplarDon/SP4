using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class EndGameManager : MonoBehaviour {

    bool b_GameEnded = false;
    bool b_PlayerWon = false;

    double d_timer = 0.0;
    
    public teamManager m_PlayerTeam;
    public teamManager m_EnemyTeam;

    public Image WinImage;
    public Image LoseImage;

	// Use this for initialization
	void Start () {
        WinImage.fillAmount = 0;
        LoseImage.fillAmount = 0;
	}
	
	// Update is called once per frame
	void Update () {

        if (!GameObject.Find("StartGameManager").GetComponent<StartGameManager>().b_AssignedCharacters)
            return;

        if (m_EnemyTeam.GetComponent<teamManager>().teamList.Count <= 0)
        {
            b_GameEnded = true;
            b_PlayerWon = true;
        }
        
        if (m_PlayerTeam.GetComponent<teamManager>().teamList.Count <= 0)
        {
            b_GameEnded = true;
            b_PlayerWon = false;
        }

        if (Input.GetKeyUp(KeyCode.P))
        {
            b_GameEnded = true;
            b_PlayerWon = true;
        }

        if (b_GameEnded)
            RunEndScreens();

	}

    void RunEndScreens()
    {
        if (b_PlayerWon)
        {
            WinImage.transform.localPosition = new Vector3(0, 0, 0);
            d_timer += Mathf.PI / 180;
            WinImage.fillAmount += 0.05f * Mathf.Cos((float)d_timer);

            if (d_timer >= Mathf.PI)
            {
                d_timer = 0.0f;
                WinImage.transform.localPosition = new Vector3(9999, 9999, 9999);

                GameObject.Find("SceneChanger").GetComponent<LoadScenes>().Loadscene("MainMenu");
                PersistentData.m_Instance.PlayerMoney += 50000;

                Debug.Log("Back to mainmenu");
            }
        }
        else
        {
            LoseImage.transform.localPosition = new Vector3(0, 0, 0);
            d_timer += Mathf.PI / 180;
            LoseImage.fillAmount += 0.05f * Mathf.Cos((float)d_timer);

            if (d_timer >= Mathf.PI)
            {
                d_timer = 0.0f;
                LoseImage.transform.localPosition = new Vector3(9999, 9999, 9999);

                GameObject.Find("SceneChanger").GetComponent<LoadScenes>().Loadscene("MainMenu");
                Debug.Log("Back to mainmenu");
            }
        }
    }

    public bool GetGameEnded()
    {
        return b_GameEnded;
    }

    public bool GetIfPlayerWon()
    {
        return b_PlayerWon;
    }
}
