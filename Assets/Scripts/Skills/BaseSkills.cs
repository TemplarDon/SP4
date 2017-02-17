using UnityEngine;
using System.Collections;

public abstract class BaseSkills : MonoBehaviour {

    public enum SKILL_TYPE
    {
        PHYSICAL_OFFENSIVE_ONLY,
        MAGICAL_OFFENSIVE_ONLY,

        PHYSICAL_DEFENSIVE_ONLY,
        MAGICAL_DEFENSIVE_ONLY,

        BOTH,
    }

    public SKILL_TYPE SkillType;
    public string SkillName;
    public string SkillText;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public abstract void DoEffect(BaseCharacter user);
}   
