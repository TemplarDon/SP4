using UnityEngine;
using System.Collections;

public class MistaSkill : BaseSkills {

    // Longer range attack
    public int Range = 2;

    GameObject target;

    public override void DoEffect(BaseCharacter user)
    {
        if (GameObject.Find("Controller").GetComponent<CharacterController>().CurrentMode != CharacterController.CONTROL_MODE.TARGET)
        {
            GameObject.Find("Controller").GetComponent<CharacterController>().CurrentMode = CharacterController.CONTROL_MODE.TARGET;
            GameObject.Find("MapGeneration").GetComponent<LevelGenerate>().redGen = true;

            Modifier toAdd = new Modifier();
            toAdd.Init(Modifier.MODIFY_TYPE.RANGE, Range, 0);
            GameObject.Find(user.name).GetComponent<BaseCharacter>().AddModifier(toAdd);
        }
        else
        {
            target.GetComponent<BaseCharacter>().TakeDamage(user.GetAttackDamage());

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
