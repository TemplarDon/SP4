﻿using UnityEngine;
using System.Collections;

public class Items : MonoBehaviour {

    public enum ITEM_TYPE
    {
        USEABLES,
        WEAPONS,
        ARMOUR,
    }

    public string s_ItemName;
    public string s_ItemDisp;
    public string s_ItemDesc;
    public ITEM_TYPE m_ItemType;
	
	// Update is called once per frame
	void Update () {
	
	}
}
