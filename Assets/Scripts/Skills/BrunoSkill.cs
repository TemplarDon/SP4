using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class BrunoSkill : BaseSkills
{
    // Teleport
    public int Range = 2;

    Vector3 target;

    public override void DoEffect(BaseCharacter user)
    {
        if (GameObject.Find("Controller").GetComponent<CharacterController>().CurrentMode != CharacterController.CONTROL_MODE.TARGET)
        {
            GameObject.Find("Controller").GetComponent<CharacterController>().CurrentMode = CharacterController.CONTROL_MODE.TARGET;
            GameObject.Find("MapGeneration").GetComponent<LevelGenerate>().redGen = false;

            Modifier toAdd = new Modifier();
            toAdd.Init(Modifier.MODIFY_TYPE.SPEED, Range, 0);
            GameObject.Find(user.name).GetComponent<BaseCharacter>().AddModifier(toAdd);
        }
        else
        {
            Vector3 spawn = user.pos - new Vector3(0, this.transform.localScale.y / 2, 0);
            GameObject go = Instantiate(GameObject.Find("Teleport Particle System"), spawn, GameObject.Find("Teleport Particle System").transform.rotation) as GameObject;
            go.GetComponent<CleanUp>().enabled = true;

            GameObject.Find(user.name).transform.position = target;

            GameObject.Find(user.name).GetComponent<BaseCharacter>().pos.x = (int)GameObject.Find(PersistentData.m_Instance.char1Char).transform.position.x;
            GameObject.Find(user.name).GetComponent<BaseCharacter>().pos.y = (int)GameObject.Find(PersistentData.m_Instance.char1Char).transform.position.y;

            spawn = target - new Vector3(0, this.transform.localScale.y / 2, 0);
            go = Instantiate(GameObject.Find("Teleport Particle System"), spawn, GameObject.Find("Teleport Particle System").transform.rotation) as GameObject;
            go.GetComponent<CleanUp>().enabled = true;

            GameObject.Find("Controller").GetComponent<CharacterController>().CurrentMode = CharacterController.CONTROL_MODE.FREE_ROAM;

            user.restrictActions[0] = true;
            user.restrictActions[1] = true;
            user.restrictActions[2] = true;
            user.restrictActions[3] = true;
            user.restrictActions[4] = true;

        }
    }

    public void SetTargetedPosition(Vector3 target)
    {
        this.target = target;
    }
}
