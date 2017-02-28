using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class JotaroSkill : BaseSkills
{

    // Refreshes turn
    public int Range = 1;

    public override void DoEffect(BaseCharacter user)
    {
        user.restrictActions[0] = false;
        user.restrictActions[1] = false;
        user.restrictActions[2] = false;
        user.restrictActions[3] = false;
        user.restrictActions[4] = false;
        GameObject.Find("TurnManager").GetComponent<turnManage>().actionSelection = 1;
    }
}
