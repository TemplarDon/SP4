﻿using UnityEngine;
using System.Collections;

public class Gloves : Weapons
{
    // Use this for initialization
    void Start()
    {
        this.s_ItemName = "gloves";
        this.s_ItemDisp = "Boxing Gloves";
        this.s_ItemDesc = "A pair of boxing gloves infused with a staggering amount of passion and effort. Wearing them makes you want to throw a thousand cross-counters.";

        this.m_ItemType = Items.ITEM_TYPE.WEAPONS;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
