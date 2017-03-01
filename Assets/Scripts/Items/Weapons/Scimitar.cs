using UnityEngine;
using System.Collections;

public class Scimitar : Weapons
{
    // Use this for initialization
    void Start()
    {
        this.s_ItemName = "scimitar";
        this.s_ItemDisp = "Muramasa";
        this.s_ItemDesc = "The strongest weapon ever made. It's great for dungeon diving and lets you warp through walls. Of course, it doesn't actually exist in this reality, so...";

        this.m_ItemType = Items.ITEM_TYPE.WEAPONS;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
