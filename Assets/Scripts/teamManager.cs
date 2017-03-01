using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class teamManager : MonoBehaviour {

    //public BaseCharacter member1;
    //public BaseCharacter member2;
    //public BaseCharacter member3;
    //public BaseCharacter member4;
    //public BaseCharacter member5;
    //public BaseCharacter member6;

    //public GameObject greenHighlight1;
    //public GameObject greenHighlight2;
    //public GameObject greenHighlight3;
    //public GameObject greenHighlight4;
    //public GameObject greenHighlight5;
    //public GameObject greenHighlight6;

    private GameObject controller;
    private GameObject turnManager;

    private bool teamDone;
    private bool callOnce = false;
    public bool resetStats = false;
    public bool running = false;

    public List<BaseCharacter> teamList = new List<BaseCharacter>();

    private int ActiveMemberIdx = 0;
    public bool IsEnemy;

    // Use this for initialization
    void Start () {
        controller = GameObject.Find("Controller");
        turnManager = GameObject.Find("TurnManager");
        teamDone = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (running == true)
        {
            if (resetStats == true)
            {
                callOnce = false;
                for (int i = 0; i < teamList.Count; i++)
                {
                    //teamList[i].BaseArmour = teamList[i].GetMaxArmour();
                    //teamList[i].BaseSpeed = teamList[i].GetMaxSpeed();
                    //teamList[i].BaseStrength = teamList[i].GetMaxStrength();
                    //teamList[i].BaseMagic = teamList[i].GetMaxMagic();
                    ActiveMemberIdx = 0;

                    if (teamList[i] != null)
                    {
                        //if (teamList[i].BaseMana > 0)
                        //{
                        //    teamList[i].BaseMana--;
                        //}

                        if (teamList[i].theSkill != null)
                        teamList[i].theSkill.UpdateCharge();

                        for (int j = 0; j < 5; j++)
                        {
                            teamList[i].restrictActions[j] = false;
                        }
                    }
                }
            }
            resetStats = false;

            teamDone = true;
            for (int i = 0; i < teamList.Count; i++)
            {
                if (teamList[i].theSkill != null)
                {
                    if (!teamList[i].theSkill.GetCanUse())
                    {
                        teamList[i].restrictActions[4] = true;
                    }
                }

                string greenHighlight = "PlayerLoc" + (i + 1);
                if (teamList[i] != null && teamList[i].restrictActions[1] == false && (controller.GetComponent<CharacterController>().CurrentMode != CharacterController.CONTROL_MODE.MOVING) && (controller.GetComponent<CharacterController>().CurrentMode != CharacterController.CONTROL_MODE.ATTACKING))
                {
                    GameObject.Find(greenHighlight).transform.position = new Vector3((int)teamList[i].pos.x, (int)teamList[i].pos.y, teamList[i].pos.z);
                    //greenHighlight1.transform.position = teamList[1.pos;
                }
                else
                {
                    GameObject.Find(greenHighlight).transform.position = new Vector3(-9999, -9999, 0);
                }

                if (teamDone == true && teamList[i] != null && teamList[i].restrictActions[1] == false)
                {
                    teamDone = false;
                }
            }

            if (teamDone == true & callOnce == false)
            {
                callOnce = true;
                turnManager.GetComponent<turnManage>().teamTurn++;
                turnManager.GetComponent<turnManage>().animDelay = true;
                ActiveMemberIdx = 0;
                //Debug.Log("TRIGGER");
                //for (int i = 0; i < teamList.Count; i++)
                //{
                //    if (teamList[i] != null)
                //    {
                //        if (teamList[i].BaseMana > 0)
                //        {
                //            teamList[i].BaseMana--;
                //        }
                //
                //        for (int j = 0; j < 5; j++)
                //        {
                //            teamList[i].restrictActions[j] = false;
                //        }
                //    }
                //}
            }

            if (IsEnemy)
                UpdateCurrentActiveMember();
        }


    }

    public void popPlayer(GameObject deadPlayer)
    {
        for (int i = 0; i < teamList.Count; i++)
        {
            if (teamList[i].gameObject.GetInstanceID() == deadPlayer.GetInstanceID())
            {
                teamList.Remove(deadPlayer.GetComponent<BaseCharacter>());
                ActiveMemberIdx = 0;
                //Debug.Log("Character Removed.");
            }
        }
    }

    // Specific functions for AI-controlled teams
    void UpdateCurrentActiveMember()
    {
        if (ActiveMemberIdx + 1 < teamList.Count)
        {
            if (teamList[ActiveMemberIdx].restrictActions[1] == true)
            {
                teamList[ActiveMemberIdx].b_EnemyActive = false;
                ++ActiveMemberIdx;
            }
        }
    }

    public BaseCharacter GetCurrentActiveMember()
    {
        return teamList[ActiveMemberIdx];
    }
}
