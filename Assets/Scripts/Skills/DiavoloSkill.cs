using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class DiavoloSkill : BaseSkills
{

    // Decreases everyone's stand charge
    public int Range = 9;

    public override void DoEffect(BaseCharacter user)
    {
        //LevelGenerate Map = GameObject.Find("MapGeneration").GetComponent<LevelGenerate>();
        //
        //Vector3 mapPos = new Vector3(user.pos.x, user.pos.y, 0);
        //
        //List<BaseCharacter> AffectedCharacters = Map.GetCharactersInRange(mapPos, Range);
        //
        //foreach (BaseCharacter aCharacter in AffectedCharacters)
        //{
        //    aCharacter.theSkill.UpdateCharge();
        //    if(aCharacter.restrictActions[2] == false)
        //    {
        //        aCharacter.restrictActions[4] = false;
        //    }
        //}
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Character");
        foreach (GameObject obj in allObjects)
        {
            if (obj.GetComponent<BaseCharacter>() != user && !obj.GetComponent<BaseCharacter>().IsEnemy && !obj.GetComponent<BaseCharacter>().IsDead)
            {
                obj.GetComponent<BaseCharacter>().theSkill.UpdateCharge();

                Vector3 spawn = obj.GetComponent<BaseCharacter>().pos + new Vector3(0, obj.transform.localScale.y / 4, 0);
                GameObject go = Instantiate(GameObject.Find("Charge Particle System"), spawn, GameObject.Find("Charge Particle System").transform.rotation) as GameObject;
                go.GetComponent<CleanUp>().enabled = true;
            }
        }
    }
}
