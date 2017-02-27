using UnityEngine;
using System.Collections;

public abstract class Useables : Items {

    // Use this for initialization
    void Start () {
        this.m_ItemType = Items.ITEM_TYPE.USEABLES;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public abstract void DoEffect(BaseCharacter user);
}
