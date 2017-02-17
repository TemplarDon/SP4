using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class EquipmentManger {

    private Equippables EquippedArmour;
    private Equippables EquippedWeapon;

	// Use this for initialization
	public void Init (Equippables armour, Equippables weapon) 
    {
        EquippedArmour = armour;
        EquippedWeapon = weapon;
	}
	
	// Update is called once per frame
	public void Update () {
	
	}



}
