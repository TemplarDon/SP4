using UnityEngine;
using System.Collections;

public class Bat : Weapons
{
    // Use this for initialization
    void Start()
    {
        this.s_ItemName = "bat";
        this.s_ItemDisp = "Metal Bat";
        this.s_ItemDesc = "It's metal. It's hard.";
        WeaponDamage = 1;
        WeaponRange = 1;

        this.m_ItemType = Items.ITEM_TYPE.WEAPONS;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
