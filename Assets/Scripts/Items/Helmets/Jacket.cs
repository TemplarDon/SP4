using UnityEngine;
using System.Collections;

public class Jacket : Armours
{
    // Use this for initialization
    void Start()
    {
        this.s_ItemName = "jacket";
        this.s_ItemDisp = "Shinings Diamond";
        this.s_ItemDesc = "Has the name of the country's greatest biker gang leader embroidered on it.";
        ArmourAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
