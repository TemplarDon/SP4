using UnityEngine;
using System.Collections;

public class Gluegun : Weapons
{
    // Use this for initialization
    void Start()
    {
        this.s_ItemName = "gluegun";
        this.s_ItemDisp = "Raygun";
        this.s_ItemDesc = "Created with hi-tech future technology. A single shot can melt every molecule in a fully grown human.";
        WeaponDamage = 1;
        WeaponRange = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
