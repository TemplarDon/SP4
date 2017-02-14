using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

    public GameObject CurrentControlledCharacter;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
        if (Input.GetMouseButtonUp(0))
        {
            CurrentControlledCharacter.GetComponent<BaseCharacter>().SetToMove(true);
            CurrentControlledCharacter.GetComponent<BaseCharacter>().SetCharacterDestination(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            Debug.Log("MOUSE DOWN");
        }

	}


}
