﻿using UnityEngine;
using System.Collections;

public class Seeds : Useables {

    public int HealAmount = 1;

    // Use this for initialization
    void Start()
    {
        s_ItemName = "sunflowerseeds";
        s_ItemDisp = "Sunflower Seeds";
        s_ItemDesc = "They have a flavor somewhat similar to peanuts. The flower itself represents the sun's watchful eyes";
        m_ItemType = Items.ITEM_TYPE.USEABLES;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void DoEffect(BaseCharacter user)
    {
        //GameObject.Find(user.name).GetComponent<BaseCharacter>().BaseHealth += HealAmount;
        Debug.Log("Healed!");

        Modifier toAdd = new Modifier();
        toAdd.Init(Modifier.MODIFY_TYPE.HEALTH, HealAmount, 1);
        GameObject.Find(user.name).GetComponent<BaseCharacter>().AddModifier(toAdd);
    }
}