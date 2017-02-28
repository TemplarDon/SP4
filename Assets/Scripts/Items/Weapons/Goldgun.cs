using UnityEngine;
using System.Collections;

public class Goldgun : Weapons
{
    // Use this for initialization
    void Start()
    {
        this.s_ItemName = "goldgun";
        this.s_ItemDisp = "Golden Gun";
        this.s_ItemDesc = "A replica of the gun preferred by a famous assassin. It's not really much good by itself. You can't even cock it...";
        WeaponDamage = 1;
        WeaponRange = 1;

        this.m_ItemType = Items.ITEM_TYPE.WEAPONS;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
