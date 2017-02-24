﻿using UnityEngine;
using System.Collections;

public class Seeds : Useables {

    public int HealAmount = 1;

    // Use this for initialization
    void Start()
    {
        s_ItemName = "Seeds";
        m_ItemType = Items.ITEM_TYPE.USEABLES;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void DoEffect(BaseCharacter user)
    {
        GameObject.Find(user.name).GetComponent<BaseCharacter>().BaseHealth += HealAmount;
        Debug.Log("Healed!");
    }
}