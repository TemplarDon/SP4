using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class DiavoloSkill : BaseSkills
{

    // Decreases everyone's stand charge
    public int Range = 9;

    public override void DoEffect(BaseCharacter user)
    {
        LevelGenerate Map = GameObject.Find("MapGeneration").GetComponent<LevelGenerate>();

        Vector3 mapPos = new Vector3(user.pos.x, user.pos.y, 0);

        List<BaseCharacter> AffectedCharacters = Map.GetCharactersInRange(mapPos, Range);

        foreach (BaseCharacter aCharacter in AffectedCharacters)
        {
            aCharacter.theSkill.UpdateCharge();
        }
        GameObject.Find("TurnManager").GetComponent<turnManage>().actionSelection = 1;
    }
}
