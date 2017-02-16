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
        // Set Controller to allow character to move
        GameObject.Find("Controller").GetComponent<CharacterController>().SetCanMove(true);

        Debug.Log("Set CanMove to true");
    }
}
