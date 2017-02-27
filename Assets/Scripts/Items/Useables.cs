using UnityEngine;
using System.Collections;

public abstract class Useables : Items {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public abstract void DoEffect(BaseCharacter user);
}
