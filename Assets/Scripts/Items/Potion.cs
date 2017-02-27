using UnityEngine;
using System.Collections;

public class Potion : Useables {

    public int HealAmount = 1;

	// Use this for initialization
	void Start () {
        this.s_ItemName = "Potion";

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void DoEffect(BaseCharacter user)
    {
        GameObject.Find(user.name).GetComponent<BaseCharacter>().BaseHealth += HealAmount;
        Debug.Log("Healed!");
    }
}
