using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class BaseCharacter : MonoBehaviour {

    // Handle to map
    public LevelGenerate theLevel;
    public Vector3 pos;

    // Managers
    public EquipmentManger theEquipmentManger = new EquipmentManger();
    public Equippables theWeapon;
    public Equippables theArmour;
    public Useables theItem;
    
    public SkillsManager theSkillsManager = new SkillsManager();
    public BaseSkills theSkill;

  	public bool[] restrictActions;

    // Character Stats
    public string Name = "Man";
    public int BaseSpeed = 1;          // The speed of the character, affects the number of tiles can be walked on
    public int BaseAttackRange = 1;    // The attack range of the character
    public int BaseStrength = 1;       // The attack strength of the character, affects the damage done by physical weapons
    public int BaseMagic = 1;          // The attack strength of the character, affects the damage done by magical weapons
    public int BaseMana = 1;           // The mana of the character, affects how many spells can be cast
    public int BaseHealth = 1;         // The health of the character
    public int BaseArmour = 1;         // The armour of the character, reduces the damage taken
    
    // Private Var
    private bool b_ShouldMove = false;
    private Vector3 m_Destination = new Vector3(0,1,0);

	// Use this for initialization
	void Start () {
        pos.x = (int)this.transform.position.x;
        pos.y = (int)this.transform.position.y;

        restrictActions = new bool[5];

        for(int i = 0; i < 5; i++)
        {
            restrictActions[i] = false;
        }

        this.GetComponent<Animator>().Play("CharacterAnimationIdle");
	}
	
	// Update is called once per frame
	void Update () {

        ConstrainToGrid();
        this.transform.position = pos;

        BaseCharacter theCharacter = GameObject.Find("Controller").GetComponent<CharacterController>().CurrentControlledCharacter.GetComponent<BaseCharacter>();
        if (theCharacter == this)
        {
            theLevel.mapposx = (int)pos.x - 1;
            theLevel.mapposy = -((int)pos.y) - 1;
        }

        if (b_ShouldMove)
        {
            //Debug.Log("Trying to follow path.");
            this.GetComponent<Pathfinder>().FollowPath();
        }    
	}

    void OnMouseDown()
    {
        // Switch current controlled character to this one
        GameObject.Find("Controller").GetComponent<CharacterController>().CurrentControlledCharacter = this.gameObject;
        GameObject.Find("TurnManager").GetComponent<turnManage>().characNEW = this.gameObject.GetComponent<BaseCharacter>();
        GameObject.Find("TurnManager").GetComponent<turnManage>().menuOpen = true;
    }

    public void SetCharacterDestination(Vector3 dest)
    {
        m_Destination = dest;
        this.GetComponent<Pathfinder>().FindPath(m_Destination);
    }

    public void SetToMove(bool status)
    {
        b_ShouldMove = status;
    }

    void ConstrainToGrid()
    {
        if (pos.x < 1)
        {
            pos.x = 1;
        }
        else if (pos.x > theLevel.xsize)
        {
            pos.x = theLevel.xsize;
        }

        if (pos.y > -1)
        {
            pos.y = -1;
        }
        else if (pos.y < -theLevel.ysize)
        {
            pos.y = -theLevel.ysize;
        }
    }

    public void UseSkill()
    {
        Debug.Log("Using skill!");
        theSkill.GetComponent<BaseSkills>().DoEffect(this);
    }

    public void UseItem()
    {
        Debug.Log("Using item!");
        theItem.GetComponent<Useables>().DoEffect(this);
    }
}
