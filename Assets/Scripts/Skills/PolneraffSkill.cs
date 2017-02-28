using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class PolneraffSkill :  BaseSkills {

    //Decrease armor, increase speed
    int Amount = 1;

    public override void DoEffect(BaseCharacter user)
    {
        LevelGenerate Map = GameObject.Find("MapGeneration").GetComponent<LevelGenerate>();

        Modifier toAdd = new Modifier();
        toAdd.Init(Modifier.MODIFY_TYPE.ARMOUR, -1, 1);
        GameObject.Find(user.name).GetComponent<BaseCharacter>().AddModifier(toAdd);

        toAdd = new Modifier();
        toAdd.Init(Modifier.MODIFY_TYPE.SPEED, 1, 1);
        GameObject.Find(user.name).GetComponent<BaseCharacter>().AddModifier(toAdd);

        
    }
}
