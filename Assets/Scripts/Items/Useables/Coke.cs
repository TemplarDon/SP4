using UnityEngine;
using System.Collections;

public class Coke : Useables {

    public int BuffAmount = 2;

    // Use this for initialization
    void Start()
    {
        s_ItemName = "coke";
        s_ItemDisp = "Cola Cola";
        s_ItemDesc = "Contains a highly stimulating almost addictive sweetness. Pair it with some nice junk food for a can't-miss combo.";
        m_ItemType = Items.ITEM_TYPE.USEABLES;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void DoEffect(BaseCharacter user)
    {
        //GameObject.Find(user.name).GetComponent<BaseCharacter>().BaseSpeed += BuffAmount;

        Modifier toAdd = new Modifier();
        toAdd.Init(Modifier.MODIFY_TYPE.SPEED, BuffAmount, 1);
        GameObject.Find(user.name).GetComponent<BaseCharacter>().AddModifier(toAdd);
    }
}
