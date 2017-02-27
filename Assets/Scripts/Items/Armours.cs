using UnityEngine;
using System.Collections;

public class Armours : Equippables {

    enum ARMOUR_TYPE
    {
        PHYSCIAL,
        MAGIC,
        BOTH
    }

    public int ArmourAmount = 1;

	// Use this for initialization
	void Start () {
        this.m_ItemType = Items.ITEM_TYPE.ARMOUR;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
