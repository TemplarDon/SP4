using UnityEngine;
using System.Collections;

public class MoveSelection : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        switch(tag)
        {
            case "yellowSq":
                // Set Controller to allow character to move
                GameObject.Find("Controller").GetComponent<CharacterController>().SetCanMove(true);
                GameObject.Find("TurnManager").GetComponent<turnManage>().cancelAction = false;

                Debug.Log("Set CanMove to true");
                break;
            case "redSq":
                bool playerPresent = false;

                GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Character");
                foreach (GameObject obj in allObjects)
                {
                    if ((int)obj.transform.position.x == (int)transform.position.x + 1 && (int)obj.transform.position.y == (int)transform.position.y - 1)
                    {
                        obj.GetComponent<BaseCharacter>().BaseHealth -= GameObject.Find("Controller").GetComponent<CharacterController>().CurrentControlledCharacter.GetComponent<BaseCharacter>().BaseStrength;
                        playerPresent = true;
                    }
                }

                if(playerPresent == true)
                {
                    GameObject.Find("Controller").GetComponent<CharacterController>().CurrentControlledCharacter.GetComponent<BaseCharacter>().restrictActions[0] = true;
                    GameObject.Find("Controller").GetComponent<CharacterController>().CurrentControlledCharacter.GetComponent<BaseCharacter>().restrictActions[1] = true;
                    GameObject.Find("Controller").GetComponent<CharacterController>().CurrentControlledCharacter.GetComponent<BaseCharacter>().restrictActions[2] = true;
                    GameObject.Find("Controller").GetComponent<CharacterController>().CurrentControlledCharacter.GetComponent<BaseCharacter>().restrictActions[3] = true;
                    GameObject.Find("Controller").GetComponent<CharacterController>().CurrentControlledCharacter.GetComponent<BaseCharacter>().restrictActions[4] = true;
                }
                GameObject.Find("Controller").GetComponent<CharacterController>().CurrentMode = CharacterController.CONTROL_MODE.FREE_ROAM;
                GameObject.Find("TurnManager").GetComponent<turnManage>().menuOpen = false;
                GameObject.Find("TurnManager").GetComponent<turnManage>().cancelAction = true;
                break;
        }
    }

    void OnMouseOver()
    {
        GameObject.Find("TurnManager").GetComponent<turnManage>().cancelAction = false;
    }
}
