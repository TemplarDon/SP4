﻿using UnityEngine;
using System.Collections;

public abstract class FSMBase : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        RunFSM();

	}

   public void RunFSM()
    {
        Sense();

        int actValue = Think();
        if (actValue != -1)
        {
            Act(actValue);
        }
    }

    public abstract void Sense();          // get/receive updates from the world
    public abstract int Think();           // process the updates
    public abstract void Act(int value);   // act upon any change in behaviour
}
