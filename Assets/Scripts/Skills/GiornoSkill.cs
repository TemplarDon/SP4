﻿using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class GiornoSkill : BaseSkills {

    // Increase the health of nearby teammates
    public int Range = 2;

    public override void DoEffect(BaseCharacter user)
    {
        LevelGenerate Map = GameObject.Find("MapGeneration").GetComponent<LevelGenerate>();

        Vector3 mapPos = new Vector3(user.pos.x, user.pos.y, 0);

        List<BaseCharacter> AffectedCharacters = Map.GetCharactersInRange(mapPos, Range);

        foreach (BaseCharacter aCharacter in AffectedCharacters)
        {
            Modifier toAdd = new Modifier();
            toAdd.Init(Modifier.MODIFY_TYPE.HEALTH, 2, 1);
            GameObject.Find(aCharacter.name).GetComponent<BaseCharacter>().AddModifier(toAdd);
        }
    }
}
