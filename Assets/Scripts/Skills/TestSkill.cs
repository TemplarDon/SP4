using UnityEngine;
using System.Collections;

public class TestSkill : BaseSkills {

    public GameObject Animation;

	// Use this for initialization
	void Start () {

        SkillName = "Test Skill";
        SkillType = BaseSkills.SKILL_TYPE.MAGICAL_OFFENSIVE_ONLY;
        SkillText = "A Skill's Text";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void DoEffect(BaseCharacter user)
    {
        LevelGenerate Map = GameObject.Find("MapGeneration").GetComponent<LevelGenerate>();

        Vector3 mapPos = new Vector3(user.pos.x, user.pos.y, 0);

        BaseCharacter check1 = user.theLevel.GetCharacterInTile(mapPos + new Vector3(0, 1, 0));
        if (check1 != null)
        {
            GameObject.Find(check1.name).GetComponent<BaseCharacter>().TakeDamage(3);
            Debug.Log("Damage Taken!");
        }

        BaseCharacter check2 = user.theLevel.GetCharacterInTile(mapPos + new Vector3(0, 2, 0));
        if (check2 != null)
        {
            GameObject.Find(check2.name).GetComponent<BaseCharacter>().TakeDamage(3);
            Debug.Log("Damage Taken!");
        }

        GameObject go = Instantiate(Animation, mapPos + new Vector3(0, 1, 0), Quaternion.identity) as GameObject;
        go.GetComponent<CleanUp>().enabled = true;

        go = Instantiate(Animation, mapPos + new Vector3(0, 2, 0), Quaternion.identity) as GameObject;
        go.GetComponent<CleanUp>().enabled = true;
    }
}
