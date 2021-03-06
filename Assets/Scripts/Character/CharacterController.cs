﻿using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

    public GameObject CurrentControlledCharacter;

    private Vector3 MousePos;
    private double d_Timer = 0.0;
    private bool b_CanMove = false;

    // Irrelevant
    private bool b_CommandSent = false;

    public enum CONTROL_MODE
    {
        FREE_ROAM,
        SELECTION,
        MOVING,
        ATTACKING,
        TARGET,     // FOR STANDS THAT NEED TO TARGET ANOTHER POSITION
    }

    public CONTROL_MODE CurrentMode;

	// Use this for initialization
	void Start () {
        //Debug.Log("Controller Launched.");
        CurrentMode = CONTROL_MODE.FREE_ROAM;
	}
	
	// Update is called once per frame
	void Update () {
        
        if (CurrentMode == CONTROL_MODE.MOVING)
        {
            d_Timer += Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0))
        {
            MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
#if UNITY_ANDROID
            Touch myTouch = Input.GetTouch(0);
            MousePos = Camera.main.ScreenToWorldPoint(new Vector3(myTouch.position.x, myTouch.position.y, 0));
#endif
        }

        if (Input.GetMouseButtonUp(0) && CurrentMode == CONTROL_MODE.MOVING && b_CanMove && d_Timer > 0.5 && !b_CommandSent)
        {
            //Debug.Log("Telling " + CurrentControlledCharacter.name + " to move.");
            CurrentControlledCharacter.GetComponent<BaseCharacter>().SetToMove(true);
            CurrentControlledCharacter.GetComponent<BaseCharacter>().SetCharacterDestination(MousePos);

            d_Timer = 0;
            b_CommandSent = true;
        }

        if (CurrentMode == CONTROL_MODE.MOVING || CurrentMode == CONTROL_MODE.ATTACKING)
        {
            GameObject.Find("Main Camera").transform.position = new Vector3(this.CurrentControlledCharacter.transform.position.x, this.CurrentControlledCharacter.transform.position.y, -9);
            GameObject.Find("Main Camera").GetComponent<cameramove>().currentLoc = new Vector3(this.CurrentControlledCharacter.transform.position.x, this.CurrentControlledCharacter.transform.position.y, -9);
        }


        //Debug.Log(CurrentControlledCharacter.name + " " + CurrentMode.ToString());

        // Temporary code
        //if (Input.GetKeyUp(KeyCode.T))
        //{
        //    Debug.Log("Telling " + CurrentControlledCharacter.name + " to use spell.");
        //    CurrentControlledCharacter.GetComponent<BaseCharacter>().UseSkill();
        //}

        //if (Input.GetKeyUp(KeyCode.Y))
        //{
        //    Debug.Log("Telling " + CurrentControlledCharacter.name + " to use item.");
        //    CurrentControlledCharacter.GetComponent<BaseCharacter>().UseItem();
        //}


        if (Input.GetKeyUp(KeyCode.O))
        {
            CurrentControlledCharacter.GetComponent<BaseCharacter>().theSkill.UpdateCharge();

            Vector3 spawn = CurrentControlledCharacter.GetComponent<BaseCharacter>().pos - new Vector3(0, CurrentControlledCharacter.transform.localScale.y / 4, 0);
            GameObject go = Instantiate(GameObject.Find("Charge Particle System"), spawn, GameObject.Find("Charge Particle System").transform.rotation) as GameObject;
            go.GetComponent<CleanUp>().enabled = true;
        }
	}

    public void SetCanMove(bool status)
    {
        b_CanMove = status;

        if (!status)
        {
            b_CommandSent = false;
        }
    }
}
