﻿using UnityEngine;
using System.Collections;

public class RedTea : Useables {

    public int BuffAmount = 1;

	// Use this for initialization
	void Start () {
        s_ItemName = "redtea";
        s_ItemDisp = "Rose Tea";
        s_ItemDesc = "An herbal tea said to promote beauty and wellness. You can somehow sense its essential elegance...";
        m_ItemType = Items.ITEM_TYPE.USEABLES;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void DoEffect(BaseCharacter user)
    {
        //GameObject.Find(user.name).GetComponent<BaseCharacter>().BaseStrength += BuffAmount;

        Modifier toAdd = new Modifier();
        toAdd.Init(Modifier.MODIFY_TYPE.ATTACK, BuffAmount, 1);
        GameObject.Find(user.name).GetComponent<BaseCharacter>().AddModifier(toAdd);
    }
}
