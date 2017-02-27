using UnityEngine;
using System.Collections;

public class Biscuit : Useables
{

    public int HealAmount = 1;

    // Use this for initialization
    void Start()
    {
        s_ItemName = "biscuit";
        s_ItemDisp = "Royal Curry";
        s_ItemDesc = "It's made with expensive, high-quality ingredients you wouldn't expect from a kid's food.";
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
