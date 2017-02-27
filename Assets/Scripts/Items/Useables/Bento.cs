using UnityEngine;
using System.Collections;

public class Bento : Useables {

    public int HealAmount = 1;

    // Use this for initialization
    void Start()
    {
        s_ItemName = "bento";
        s_ItemDisp = "Bento Box";
        s_ItemDesc = "Stuffed with rice, ginger, carrots, peppers, mushrooms, and more. It's meat-free, so you vegetarians out there are covered, too.";
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
