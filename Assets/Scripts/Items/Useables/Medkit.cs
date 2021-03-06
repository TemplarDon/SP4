﻿using UnityEngine;
using System.Collections;

public class Medkit : Useables {

    public int HealAmount = 1;

    // Use this for initialization
    void Start()
    {
        s_ItemName = "medkit";
        s_ItemDisp = "Ration";
        s_ItemDesc = "A set of canned and vacuum-sealed foodstuffs. The taste isn't bad, and certain snakes that enjoy hide-and-go-seek are just crazy about it.";
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