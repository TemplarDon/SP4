using UnityEngine;
using System.Collections;

public class Katana : Weapons
{
    // Use this for initialization
    void Start()
    {
        this.s_ItemName = "katana";
        this.s_ItemDisp = "Zantetsuken";
        this.s_ItemDesc = "A sword that can't even cut through iron. Or flesh. Or anything, really. In other words, totally useless...";
        WeaponDamage = 1;
        WeaponRange = 1;

        this.m_ItemType = Items.ITEM_TYPE.WEAPONS;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
