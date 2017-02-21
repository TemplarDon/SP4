using UnityEngine;
using System.Collections;

public class AnimationEnd : MonoBehaviour {

    public bool b_AnimationEnded = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetAnimStart()
    {
        b_AnimationEnded = false;
    }

    public void SetAnimEnd()
    {
        b_AnimationEnded = true; ;
    }
}
