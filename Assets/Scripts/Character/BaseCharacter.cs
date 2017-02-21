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
    public bool IsEnemy;               // Whether or not this character is an enemy
    public bool IsDead;                // Whether or not this character is dead

    private int MaxSpeed =           0;
    private int MaxAttackRange =     0;
    private int MaxStrength =        0;
    private int MaxMagic =           0;
    private int MaxMana =            0;
    private int MaxHealth =          0;
    private int MaxArmour =          0;

    // Animation Enums
    public enum ANIM_STATE
    {
        IDLE,
        MOVE_LEFT,
        MOVE_RIGHT,
        MOVE_UP,
        MOVE_DOWN,
        ATTACK_LEFT,
        ATTACK_RIGHT,
        ATTACK_UP,
        ATTACK_DOWN,
        STAND_LEFT,
        STAND_RIGHT,
        STAND_UP,
        STAND_DOWN,
        DIE,
        DEAD,
    }

    public ANIM_STATE CurrentAnimState;

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

        MaxSpeed = BaseSpeed;
        MaxAttackRange = BaseAttackRange;
        MaxStrength = BaseStrength;
        MaxMagic = BaseMagic;
        MaxMana = BaseMana;
        MaxHealth = BaseHealth;
        MaxArmour = BaseArmour;

    //this.GetComponent<Animator>().Play("CharacterAnimationIdle");
    CurrentAnimState = ANIM_STATE.IDLE;
	}
	
	// Update is called once per frame
	void Update () {
        ConstrainToGrid();
        this.transform.position = pos;

        if (!IsEnemy)
        {
            BaseCharacter theCharacter = GameObject.Find("Controller").GetComponent<CharacterController>().CurrentControlledCharacter.GetComponent<BaseCharacter>();
            if (theCharacter == this)
            {
                theLevel.mapposx = (int)pos.x - 1;
                theLevel.mapposy = -((int)pos.y) - 1;
            }
        }
        else
        {
            // Do Enemy Updates

        }

        if (b_ShouldMove)
        {
            //Debug.Log("Trying to follow path.");
            this.GetComponent<Pathfinder>().FollowPath();
        }

        UpdateAnimState();
        CheckIfDead();
        
	}

    void OnMouseDown()
    {
        if (GameObject.Find("TurnManager").GetComponent<turnManage>().menuOpen == false && GameObject.Find("Controller").GetComponent<CharacterController>().CurrentMode != CharacterController.CONTROL_MODE.ATTACKING)
        {
            // Switch current controlled character to this one
            GameObject.Find("Controller").GetComponent<CharacterController>().CurrentControlledCharacter = this.gameObject;
            GameObject.Find("TurnManager").GetComponent<turnManage>().characNEW = this.gameObject.GetComponent<BaseCharacter>();
            GameObject.Find("TurnManager").GetComponent<turnManage>().menuOpen = true;
            GameObject.Find("TurnManager").GetComponent<turnManage>().clickingNewChar = true;
        }
    }

    void OnMouseExit()
    {
        GameObject.Find("TurnManager").GetComponent<turnManage>().clickingNewChar = false;
    }

    public void SetCharacterDestination(Vector3 dest)
    {
        m_Destination = dest;
        this.GetComponent<Pathfinder>().Reset();
        this.GetComponent<Pathfinder>().FindPath(m_Destination);
    }

    public Vector3 GetCharacterDestination()
    {
        return m_Destination;
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

    void UpdateAnimState()
    {
        if (this.GetComponent<AnimationEnd>().b_AnimationEnded)
        {
            if (this.CurrentAnimState == ANIM_STATE.DIE)
            {
                this.CurrentAnimState = ANIM_STATE.DEAD;
                if (IsEnemy)
                    this.GetComponent<FSMBase>().enabled = false;

                this.GetComponent<Pathfinder>().enabled = false;
                this.enabled = false;
            }
            else
                this.CurrentAnimState = ANIM_STATE.IDLE;
            Debug.Log("Reset anim.");
        }


        switch (CurrentAnimState)
        {
            case ANIM_STATE.IDLE:
                this.GetComponent<Animator>().Play("Idle");
                break;

            case ANIM_STATE.MOVE_DOWN:
                this.GetComponent<Animator>().Play("MoveDown");
                break;

            case ANIM_STATE.MOVE_LEFT:
                this.GetComponent<Animator>().Play("MoveLeft");
                break;

            case ANIM_STATE.MOVE_RIGHT:
                this.GetComponent<Animator>().Play("MoveRight");
                break;

            case ANIM_STATE.MOVE_UP:
                this.GetComponent<Animator>().Play("MoveUp");
                break;

            case ANIM_STATE.ATTACK_DOWN:
                this.GetComponent<Animator>().Play("AttackDown");
                break;

            case ANIM_STATE.ATTACK_LEFT:
                this.GetComponent<Animator>().Play("AttackLeft");
                break;

            case ANIM_STATE.ATTACK_RIGHT:
                this.GetComponent<Animator>().Play("AttackRight");
                break;

            case ANIM_STATE.ATTACK_UP:
                this.GetComponent<Animator>().Play("AttackUp");
                break;

            case ANIM_STATE.STAND_DOWN:
                this.GetComponent<Animator>().Play("StandDown");
                break;

            case ANIM_STATE.STAND_LEFT:
                this.GetComponent<Animator>().Play("StandLeft");
                break;

            case ANIM_STATE.STAND_RIGHT:
                this.GetComponent<Animator>().Play("StandRight");
                break;

            case ANIM_STATE.STAND_UP:
                this.GetComponent<Animator>().Play("StandUp");
                break;

            case ANIM_STATE.DIE:
                this.GetComponent<Animator>().Play("Die");
                break;

            case ANIM_STATE.DEAD:
                this.GetComponent<Animator>().Play("Dead");
                break;
        }
    }

    public int GetMaxHealth()
    {
        return MaxHealth;
    }

    public int GetMaxSpeed()
    {
        return MaxSpeed;
    }

    public int GetMaxAttackRange()
    {
        return MaxAttackRange;
    }

    public int GetMaxStrength()
    {
        return MaxStrength;
    }

    public int GetMaxMagic()
    {
        return MaxMagic;
    }

    public int GetMaxMana()
    {
        return MaxMana;
    }
    
    public int GetMaxArmour()
    {
        return MaxArmour;
    }

    public void TakeDamage(int damage)
    {
        BaseHealth -= damage;
        CheckIfDead();
    }

    void CheckIfDead()
    {
        if (BaseHealth <= 0 )
        {
            Debug.Log("Died.");

            CurrentAnimState = ANIM_STATE.DIE;
        }
    }
}                   