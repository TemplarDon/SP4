﻿using UnityEngine;
using System.Collections;

public class Water : Useables {

    public int HealAmount = 1;

    // Use this for initialization
    void Start()
    {
        s_ItemName = "water";
        s_ItemDisp = "Mineral Water";
        s_ItemDesc = "Drawn from the ocean depths and rigorously purified. Ideal for a modern on-the-go public unsatisfied with tap water.";
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
