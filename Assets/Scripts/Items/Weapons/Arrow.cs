using UnityEngine;
using System.Collections;

public class Arrow : Weapons
{
    // Use this for initialization
    void Start()
    {
        this.s_ItemName = "arrow";
        WeaponDamage = 1;
        WeaponRange = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
