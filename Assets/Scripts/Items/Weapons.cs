using UnityEngine;
using System.Collections;

public class Weapons : Equippables {

    enum WEAPON_TYPE
    {
        PHYSICAL,
        MAGICAL,
    }

    public int WeaponDamage = 1;
    public int WeaponRange = 1;

	// Use this for initialization
	void Start () {
        this.m_ItemType = Items.ITEM_TYPE.WEAPONS;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
