using UnityEngine;
using System.Collections;

public class Coke : Useables {

    public int BuffAmount = 2;

    // Use this for initialization
    void Start()
    {
        s_ItemName = "Coke";
        m_ItemType = Items.ITEM_TYPE.USEABLES;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void DoEffect(BaseCharacter user)
    {
        GameObject.Find(user.name).GetComponent<BaseCharacter>().BaseSpeed += BuffAmount;
    }
}
