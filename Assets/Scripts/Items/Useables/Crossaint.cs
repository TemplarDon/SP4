using UnityEngine;
using System.Collections;

public class Crossaint : Useables {

    public int BuffAmount = 1;

    // Use this for initialization
    void Start()
    {
        s_ItemName = "blackcrossaint";
        s_ItemDisp = "Black Crossaint";
        s_ItemDesc = "A baked good made from black ingredients. It looks burnt, but it's actually pretty good.";
        m_ItemType = Items.ITEM_TYPE.USEABLES;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void DoEffect(BaseCharacter user)
    {
        //GameObject.Find(user.name).GetComponent<BaseCharacter>().BaseArmour += BuffAmount;

        Modifier toAdd = new Modifier();
        toAdd.Init(Modifier.MODIFY_TYPE.ARMOUR, BuffAmount, 1);
        GameObject.Find(user.name).GetComponent<BaseCharacter>().AddModifier(toAdd);
    }
}
