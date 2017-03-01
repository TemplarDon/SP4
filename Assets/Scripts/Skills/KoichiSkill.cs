using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class KoichiSkill : BaseSkills
{
    // Restrict an enemy movement
    public int Duration = 2;

    GameObject target;

    public override void DoEffect(BaseCharacter user)
    {
        if (GameObject.Find("Controller").GetComponent<CharacterController>().CurrentMode != CharacterController.CONTROL_MODE.TARGET)
        {
            GameObject.Find("Controller").GetComponent<CharacterController>().CurrentMode = CharacterController.CONTROL_MODE.TARGET;
            GameObject.Find("MapGeneration").GetComponent<LevelGenerate>().redGen = true;

            Debug.Log("Enter Target mode");
            Debug.Log(GameObject.Find("Controller").GetComponent<CharacterController>().CurrentControlledCharacter.name + " " + GameObject.Find("Controller").GetComponent<CharacterController>().CurrentMode.ToString());
        }
        else
        {
            Modifier toAdd = new Modifier();
            toAdd.Init(Modifier.MODIFY_TYPE.RESTRICT, 1, Duration);
            GameObject.Find(target.name).GetComponent<BaseCharacter>().AddModifier(toAdd);

            GameObject.Find("Controller").GetComponent<CharacterController>().CurrentMode = CharacterController.CONTROL_MODE.FREE_ROAM;

            Debug.Log("Exit Target mode");

            user.restrictActions[0] = true;
            user.restrictActions[1] = true;
            user.restrictActions[2] = true;
            user.restrictActions[3] = true;
            user.restrictActions[4] = true;

        }
    }

    public void SetTargetedObject(GameObject target)
    {
        this.target = target;
    }
}
