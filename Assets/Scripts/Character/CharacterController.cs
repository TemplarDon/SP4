using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

    public GameObject CurrentControlledCharacter;

	// Use this for initialization
	void Start () {
        Debug.Log("Test");
	}
	
	// Update is called once per frame
	void Update () {
	    
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Telling " + CurrentControlledCharacter.name + " to move.");
            CurrentControlledCharacter.GetComponent<BaseCharacter>().SetToMove(true);
            CurrentControlledCharacter.GetComponent<BaseCharacter>().SetCharacterDestination(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

	}


}
