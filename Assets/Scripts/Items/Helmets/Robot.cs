﻿using UnityEngine;
using System.Collections;

public class Robot : Armours
{
    // Use this for initialization
    void Start()
    {
        this.s_ItemName = "robot";
        this.s_ItemDisp = "Robo Justice";
        this.s_ItemDesc = "The quality is so high, it's hard to imagine it was made with recycled materials";
        ArmourAmount = 10;

        this.m_ItemType = Items.ITEM_TYPE.ARMOUR;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
