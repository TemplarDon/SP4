using UnityEngine;
using System.Collections;

public class Rabbit : Armours
{
    // Use this for initialization
    void Start()
    {
        this.s_ItemName = "rabbit";
        this.s_ItemDisp = "Bunny Earmuffs";
        this.s_ItemDesc = "One of the most popular items from gothic lolita designer Ina Bauer.";
        ArmourAmount = 10;

        this.m_ItemType = Items.ITEM_TYPE.ARMOUR;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
