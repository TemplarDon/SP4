﻿using UnityEngine;
using System.Collections;

public class Ramen : Useables
{

    public int HealAmount = 1;

    // Use this for initialization
    void Start()
    {
        s_ItemName = "ramen";
        s_ItemDisp = "Cup Noodles";
        s_ItemDesc = "Instant noodles. Fill it with boiling water and it's ready in 3 seconds. Of course, it also goes bad in like 30...";
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
