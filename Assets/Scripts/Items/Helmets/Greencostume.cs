using UnityEngine;
using System.Collections;

public class Greencostume : Armours
{
    // Use this for initialization
    void Start()
    {
        this.s_ItemName = "greencostume";
        this.s_ItemDisp = "Green Costume";
        this.s_ItemDesc = "As soon as you put this on, you'll feel like you can take on any challenge. It resembles a stereotypical dinosaur.";
        ArmourAmount = 1;

        this.m_ItemType = Items.ITEM_TYPE.ARMOUR;
}

    // Update is called once per frame
    void Update()
    {

    }
}
