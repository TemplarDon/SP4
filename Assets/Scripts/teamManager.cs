using UnityEngine;
using System.Collections;

public class teamManager : MonoBehaviour {

    public BaseCharacter member1;
    public BaseCharacter member2;
    public BaseCharacter member3;
    public BaseCharacter member4;
    public BaseCharacter member5;
    public BaseCharacter member6;

    public GameObject greenHighlight1;
    public GameObject greenHighlight2;
    public GameObject greenHighlight3;
    public GameObject greenHighlight4;
    public GameObject greenHighlight5;
    public GameObject greenHighlight6;

    private GameObject controller;

    // Use this for initialization
    void Start () {
        controller = GameObject.Find("Controller");
    }
	
	// Update is called once per frame
	void Update () {

        if (member1 != null && member1.restrictActions[1] == false && (controller.GetComponent<CharacterController>().CurrentMode != CharacterController.CONTROL_MODE.MOVING))
        {
            //greenHighlight1.transform.position = member1.pos;
            greenHighlight1.transform.position = new Vector3((int)member1.pos.x, (int)member1.pos.y, member1.pos.z);
        }
        else
        {
            greenHighlight1.transform.position = new Vector3(-9999, -9999, 0);
        }

        if (member2 != null && member2.restrictActions[1] == false && (controller.GetComponent<CharacterController>().CurrentMode != CharacterController.CONTROL_MODE.MOVING))
        {
            greenHighlight2.transform.position = member2.pos;
        }
        else
        {
            greenHighlight2.transform.position = new Vector3(-9999, -9999, 0);
        }

        if (member3 != null && member3.restrictActions[1] == false && (controller.GetComponent<CharacterController>().CurrentMode != CharacterController.CONTROL_MODE.MOVING))
        {
            greenHighlight3.transform.position = member3.pos;
        }
        else
        {
            greenHighlight3.transform.position = new Vector3(-9999, -9999, 0);
        }

        if (member4 != null && member4.restrictActions[1] == false && (controller.GetComponent<CharacterController>().CurrentMode != CharacterController.CONTROL_MODE.MOVING))
        {
            greenHighlight4.transform.position = member4.pos;
        }
        else
        {
            greenHighlight4.transform.position = new Vector3(-9999, -9999, 0);
        }

        if (member5 != null && member5.restrictActions[1] == false && (controller.GetComponent<CharacterController>().CurrentMode != CharacterController.CONTROL_MODE.MOVING))
        {
            greenHighlight5.transform.position = member5.pos;
        }
        else
        {
            greenHighlight5.transform.position = new Vector3(-9999, -9999, 0);
        }

        if (member6 != null && member6.restrictActions[1] == false && (controller.GetComponent<CharacterController>().CurrentMode != CharacterController.CONTROL_MODE.MOVING)                                                                                                                                                )
        {
            greenHighlight6.transform.position = member6.pos;
        }
        else
        {
            greenHighlight6.transform.position = new Vector3(-9999, -9999, 0);
        }

    }
}
