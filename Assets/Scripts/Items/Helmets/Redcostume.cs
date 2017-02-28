using UnityEngine;
using System.Collections;

public class Redcostume : Armours
{
    // Use this for initialization
    void Start()
    {
        this.s_ItemName = "redcostume";
        this.s_ItemDisp = "Red Costume";
        this.s_ItemDesc = "Jump into this, and you'll feel like you can support the world. It resembles some kind of yeti creature...";
        ArmourAmount = 1;

        this.m_ItemType = Items.ITEM_TYPE.ARMOUR;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
