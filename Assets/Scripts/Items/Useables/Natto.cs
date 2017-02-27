using UnityEngine;
using System.Collections;

public class Natto : Useables {

    public int BuffAmount = 1;

    // Use this for initialization
    void Start()
    {
        s_ItemName = "natto";
        s_ItemDisp = "Crackers";
        s_ItemDesc = "A tough, long-lasting cracker used mainly as an emergency ration. Each piece contains a full seven different flavors.";
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
