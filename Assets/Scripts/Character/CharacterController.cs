using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

    public GameObject CurrentControlledCharacter;

    public enum CONTROL_MODE
    {
        FREE_ROAM,
        SELECTION,
        MOVING,
    }

    public CONTROL_MODE CurrentMode;

	// Use this for initialization
	void Start () {
        Debug.Log("Controller Launched.");
        CurrentMode = CONTROL_MODE.FREE_ROAM;
	}
	
	// Update is called once per frame
	void Update () {
	    
        if (Input.GetMouseButtonUp(0) && CurrentMode == CONTROL_MODE.MOVING)
        {
            Debug.Log("Telling " + CurrentControlledCharacter.name + " to move.");
            CurrentControlledCharacter.GetComponent<BaseCharacter>().SetToMove(true);
            CurrentControlledCharacter.GetComponent<BaseCharacter>().SetCharacterDestination(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

	}


}
