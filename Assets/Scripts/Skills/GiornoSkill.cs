using UnityEngine;
using System.Collections;

public class GiornoSkill : BaseSkills {

    // Increase the health of nearby teammates

	// Use this for initialization
	void Start () {
	
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
            GameObject.Find(check1.name).GetComponent<BaseCharacter>().TakeDamage(2);
            Debug.Log("Damage Taken!");
        }
        BaseCharacter check2 = user.theLevel.GetCharacterInTile(mapPos + new Vector3(0, -1, 0));
        if (check2 != null)
        {
            GameObject.Find(check2.name).GetComponent<BaseCharacter>().TakeDamage(2);
            Debug.Log("Damage Taken!");
        }
    }
}
