using UnityEngine;
using System.Collections;

public class Donut : Useables {

    public int BuffAmount = 1;

    // Use this for initialization
    void Start()
    {
        s_ItemName = "donut";
        s_ItemDisp = "Donut";
        s_ItemDesc = "Donuts large enough to double as lifesavers. It goes without saying, you can float across the ocean and enjoy a delicious pastry simultaneously.";
        m_ItemType = Items.ITEM_TYPE.USEABLES;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void DoEffect(BaseCharacter user)
    {
        //GameObject.Find(user.name).GetComponent<BaseCharacter>().BaseAttackRange += BuffAmount;

        Modifier toAdd = new Modifier();
        toAdd.Init(Modifier.MODIFY_TYPE.RANGE, BuffAmount, 1);
        GameObject.Find(user.name).GetComponent<BaseCharacter>().AddModifier(toAdd);
    }
}
