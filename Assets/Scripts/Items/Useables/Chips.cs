using UnityEngine;
using System.Collections;

public class Chips : Useables
{

    public int HealAmount = 1;

    // Use this for initialization
    void Start()
    {
        s_ItemName = "chips";
        s_ItemDisp = "Potato Chips";
        s_ItemDesc = "A staple snack food made by frying thick potato slices in oil. Beware its dangerously high calorie count.";
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
