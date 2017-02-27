using UnityEngine;
using System.Collections;

public class BirdRice : Useables {

    public int BuffAmount = 2;

    // Use this for initialization
    void Start()
    {
        s_ItemName = "birdrice";
        s_ItemDisp = "Birdseed";
        s_ItemDesc = "Sprinkle this around outside and watch the birds come flocking. There's nothing stopping you from eating it too, I suppose...";
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
