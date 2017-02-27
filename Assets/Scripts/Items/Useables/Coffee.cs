using UnityEngine;
using System.Collections;

public class Coffee : Useables {

    public int BuffAmount = 1;

    // Use this for initialization
    void Start()
    {
        s_ItemName = "coffee";
        s_ItemDisp = "Coffee";
        s_ItemDesc = "Made from an extremely rare and expensive coffee bean collected from the dung of the Asian palm civet. It has a unique fragrance...";
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
