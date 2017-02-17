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

    public List<BaseCharacter> teamList = new List<BaseCharacter>();

    // Use this for initialization
    void Start () {
        controller = GameObject.Find("Controller");
    }
	
	// Update is called once per frame
	void Update () {

        for(int i = 0; i < teamList.Count; i++)
        {
            string greenHighlight = "PlayerLoc" + (i + 1);
            if (teamList[i] != null && teamList[i].restrictActions[1] == false && (controller.GetComponent<CharacterController>().CurrentMode != CharacterController.CONTROL_MODE.MOVING) && (controller.GetComponent<CharacterController>().CurrentMode != CharacterController.CONTROL_MODE.ATTACKING))
            {
                //greenHighlight1.transform.position = teamList[1.pos;
                GameObject.Find(greenHighlight).transform.position = new Vector3((int)teamList[i].pos.x, (int)teamList[i].pos.y, teamList[i].pos.z);
            }
            else
            {
                GameObject.Find(greenHighlight).transform.position = new Vector3(-9999, -9999, 0);
            }
        }
    }
}
