using UnityEngine;
using System.Collections;

public class Arrow : Weapons
{
    // Use this for initialization
    void Start()
    {
        this.s_ItemName = "arrow";
        this.s_ItemDisp = "Stand Arrow";
        this.s_ItemDesc = "An arrowhead discovered in some ancient ruins. Fashioned from a meteorite, they say that getting pierced by it will give you the power to see demons.";
        WeaponDamage = 1;
        WeaponRange = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
