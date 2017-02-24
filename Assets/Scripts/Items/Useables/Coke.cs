using UnityEngine;
using System.Collections;

public class Coke : Useables {

    public int BuffAmount = 2;

    // Use this for initialization
    void Start()
    {

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
