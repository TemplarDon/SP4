using UnityEngine;
using System.Collections;

public class Gluegun : Weapons
{
    // Use this for initialization
    void Start()
    {
        this.s_ItemName = "gluegun";
        this.s_ItemDisp = "Raygun";
        this.s_ItemDesc = "Created with hi-tech future technology. A single shot can melt every molecule in a fully grown human.";

        this.m_ItemType = Items.ITEM_TYPE.WEAPONS;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
