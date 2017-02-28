using UnityEngine;
using System.Collections;

using System.Collections.Generic;

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

    public int ChargeCost;
    int OrigChargeCost;

	// Use this for initialization
	void Start () {
        OrigChargeCost = ChargeCost;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UpdateCharge()
    {
        ChargeCost -= 1;

        if (ChargeCost < 0)
            ChargeCost = 0;
    }

    public bool GetCanUse()
    {
        if (ChargeCost == 0)
            return true;
        else
            return false;
    }

    public void ResetCharge()
    {
        ChargeCost = OrigChargeCost;

        Debug.Log("Reset");
    }

    public abstract void DoEffect(BaseCharacter user);
}   
