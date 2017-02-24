using UnityEngine;
using System.Collections;

public class Crossaint : Useables {

    public int BuffAmount = 1;

    // Use this for initialization
    void Start()
    {
        s_ItemName = "Crossaint";
        m_ItemType = Items.ITEM_TYPE.USEABLES;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void DoEffect(BaseCharacter user)
    {
        GameObject.Find(user.name).GetComponent<BaseCharacter>().BaseArmour += BuffAmount;
    }
}
