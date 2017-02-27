using UnityEngine;
using System.Collections;

public class Goldgun : Weapons
{
    // Use this for initialization
    void Start()
    {
        this.s_ItemName = "goldgun";
        WeaponDamage = 1;
        WeaponRange = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
