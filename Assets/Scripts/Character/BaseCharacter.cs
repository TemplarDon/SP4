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
    public bool b_EnemyActive = false;

    // Character Stats
    public string Name = "Man";
    public int BaseSpeed = 1;          // The speed of the character, affects the number of tiles can be walked on
    public int BaseAttackRange = 0;    // The attack range of the character
    public int BaseStrength = 1;       // The attack strength of the character, affects the damage done by physical weapons
    public int BaseMagic = 1;          // The attack strength of the character, affects the damage done by magical weapons
    //public int BaseMana = 1;           // The mana of the character, affects how many spells can be cast
    public int BaseHealth = 1;         // The health of the character
    public int BaseArmour = 1;         // The armour of the character, reduces the damage taken
    public bool IsEnemy;               // Whether or not this character is an enemy
    public bool IsDead;                // Whether or not this character is dead
    public Sprite profilePic;

    // Modified Values (Base Values + Modifier Values)
    private int ModifiedSpeed = 0;
    private int ModifiedAttackRange = 0;
    private int ModifiedStrength = 0;
    private int ModifiedMagic = 0;
    private int ModifiedArmour = 0;

    private int MaxSpeed =           0;
    private int MaxAttackRange =     0;
    private int MaxStrength =        0;
    private int MaxMagic =           0;
    private int MaxMana =            0;
    private int MaxHealth =          0;
    private int MaxArmour =          0;

    private List<Modifier> m_ModifierList = new List<Modifier>();

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
        TAKE_DAMAGE,
    }

    public ANIM_STATE CurrentAnimState;

    // Private Var
    private bool b_ShouldMove = false;
    private Vector3 m_Destination = new Vector3(0,1,0);
    private bool b_Restricted = false;

	// Use this for initialization
	void Start () {
        pos.x = (int)this.transform.position.x;
        pos.y = (int)this.transform.position.y;

        restrictActions = new bool[5];

        for(int i = 0; i < 5; i++)
        {
            restrictActions[i] = false;
        }

        ApplyEquipmentStats();

        MaxSpeed = BaseSpeed;
        MaxAttackRange = BaseAttackRange;
        MaxStrength = BaseStrength;
        MaxMagic = BaseMagic;
        //MaxMana = BaseMana;
        MaxHealth = BaseHealth;
        MaxArmour = BaseArmour;

        ModifiedSpeed = BaseSpeed;
        ModifiedAttackRange = BaseAttackRange;
        ModifiedStrength = BaseStrength;
        ModifiedMagic = BaseMagic;
        ModifiedArmour = BaseArmour;

    //this.GetComponent<Animator>().Play("CharacterAnimationIdle");
    CurrentAnimState = ANIM_STATE.IDLE;
    UpdateAnimState();
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

            if (b_Restricted)
            {
                restrictActions[0] = true;
                restrictActions[1] = true;
            }

            if (b_ShouldMove)
            {
                //Debug.Log("Trying to follow path.");
                this.GetComponent<Pathfinder>().FollowPath();
            }

            UpdateAnimState();
            CheckIfDead();
        }
        else
        {
            // Do Enemy Updates
            if (b_EnemyActive)
            {
                if (b_Restricted)
                {
                    restrictActions[0] = true;
                    restrictActions[1] = true;
                }

                if (b_ShouldMove)
                {
                    //Debug.Log("Trying to follow path.");
                    this.GetComponent<Pathfinder>().FollowPath();
                }

                UpdateAnimState();
                CheckIfDead();
            }
        }


        
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

            //Debug.Log("Clicked");
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

        this.CurrentAnimState = ANIM_STATE.STAND_DOWN;
    }

    public void UseItem()
    {
        Debug.Log("Using item!");
        theItem.GetComponent<Useables>().DoEffect(this);
    }

    void UpdateAnimState()
    {
        if (this.GetComponent<AnimationEnd>().b_AnimationEnded && CurrentAnimState != ANIM_STATE.IDLE)
        {
            if (this.CurrentAnimState == ANIM_STATE.DIE)
            {
                this.CurrentAnimState = ANIM_STATE.DEAD;
                if (IsEnemy)
                {
                    this.GetComponent<FSMBase>().enabled = false;
                    GameObject.Find("EnemyTeamManager").GetComponent<teamManager>().popPlayer(this.gameObject);
                }

                this.GetComponent<Pathfinder>().enabled = false;
                if (!IsEnemy)
                {
                    this.GetComponent<BoxCollider2D>().enabled = false;
                }
                this.enabled = false;
                GameObject.Find("friendlyTeamManager").GetComponent<teamManager>().popPlayer(this.gameObject);

                this.GetComponent<CharacterFadeOut>().StartFade = true;
            }
            else
                this.CurrentAnimState = ANIM_STATE.IDLE;
            //Debug.Log(this.name + ": Reset anim.");
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
                this.GetComponent<Animator>().Play("Stand");
                break;

            case ANIM_STATE.STAND_LEFT:
                this.GetComponent<Animator>().Play("Stand");
                break;

            case ANIM_STATE.STAND_RIGHT:
                this.GetComponent<Animator>().Play("Stand");
                break;

            case ANIM_STATE.STAND_UP:
                this.GetComponent<Animator>().Play("Stand");
                break;

            case ANIM_STATE.DIE:
                this.GetComponent<Animator>().Play("Die");
                break;

            case ANIM_STATE.DEAD:
                this.GetComponent<Animator>().Play("Dead");
                break;

            case ANIM_STATE.TAKE_DAMAGE:
                this.GetComponent<Animator>().Play("TakeDamage");
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

    public int GetSpeed()
    {
        return ModifiedSpeed;
    }

    public int GetAttackRange()
    {
        return ModifiedAttackRange;
    }

    public int GetAttackDamage()
    {
        if (theWeapon != null)
            return this.theWeapon.GetComponent<Weapons>().WeaponDamage + this.ModifiedStrength;
        else
            return this.ModifiedStrength;
    }

    public int GetMagicDamage(int SpellDmg)
    {
        return SpellDmg + this.ModifiedMagic;
    }

    public int GetArmour()
    {
        return ModifiedArmour;
    }

    public void TakeDamage(int damage)
    {
        int damageTaken = (int)Mathf.Clamp(damage - ModifiedArmour, 1.0f, 999.0f);
        BaseHealth -= damageTaken;
        GameObject.Find("DmgIndiManager").GetComponent<dmgDisp>().dispNum(dmgDisp.DISPLAY_TYPE.DAMAGE, damageTaken, transform.position);
        CurrentAnimState = ANIM_STATE.TAKE_DAMAGE;

        GameObject go = Instantiate(GameObject.Find("Damage Particle System"), pos, Quaternion.identity) as GameObject;
        go.GetComponent<CleanUp>().enabled = true;

        CheckIfDead();
    }

    void CheckIfDead()
    {
        if (BaseHealth <= 0 )
        {
            //Debug.Log("Died.");

            IsDead = true;
            CurrentAnimState = ANIM_STATE.DIE;
        }
    }

    void ApplyEquipmentStats()
    {
        if (theWeapon)
        {
            this.BaseStrength += ((Weapons)(theWeapon)).WeaponDamage;
            this.BaseAttackRange = ((Weapons)(theWeapon)).WeaponRange;
        }

        if (theArmour)
        {
            this.BaseArmour += ((Armours)(theArmour)).ArmourAmount;
        }
    }

    public void AddModifier(Modifier toAdd)
    {
        m_ModifierList.Add(toAdd);

        ApplyModifiers();
    }

    public void RunTurnIncrease()
    {
        UpdateModifiers();
    }

    public void UpdateModifiers()
    {
        // Runs only when turn increases
        List<bool> ToRemove = new List<bool>();
        for (int i = 0; i < m_ModifierList.Count; ++i)
        {
            ToRemove.Add(false);
        }

        for (int i = 0; i < m_ModifierList.Count; ++i)
        {
            Modifier aModifier = m_ModifierList[i];

            aModifier.Update();

            if (!aModifier.b_Active)
            {
                // Take away modifier effects if inactive then remove from list
                switch (aModifier.m_Type)
                {

                    case Modifier.MODIFY_TYPE.ATTACK:
                        this.ModifiedStrength -= aModifier.i_ModifierAmount;
                        break;

                    case Modifier.MODIFY_TYPE.MAGIC:
                        this.ModifiedMagic -= aModifier.i_ModifierAmount;
                        break;

                    case Modifier.MODIFY_TYPE.ARMOUR:
                        this.ModifiedArmour -= aModifier.i_ModifierAmount;
                        break;

                    case Modifier.MODIFY_TYPE.SPEED:
                        this.ModifiedSpeed -= aModifier.i_ModifierAmount;
                        break;

                    case Modifier.MODIFY_TYPE.RANGE:
                        this.ModifiedAttackRange -= aModifier.i_ModifierAmount;
                        break;

                    case Modifier.MODIFY_TYPE.RESTRICT:
                        b_Restricted = false;
                        break;
                }
                ToRemove[i] = true;
            }
        }
        
        for (int i = 0; i < ToRemove.Count; ++i)
        {
            if (ToRemove[i])
                m_ModifierList.RemoveAt(i);
        }

    }

    void ApplyModifiers()
    {
        foreach (Modifier aModifier in m_ModifierList)
        {
            if (aModifier.b_Active)
            {
                // Apply modifier effects if active
                switch (aModifier.m_Type)
                {
                    case Modifier.MODIFY_TYPE.HEALTH:

                        // Special case for health modifiers as they are immediate
                        this.BaseHealth += aModifier.i_ModifierAmount;
                        aModifier.b_Active = false;

                        GameObject.Find("DmgIndiManager").GetComponent<dmgDisp>().dispNum(dmgDisp.DISPLAY_TYPE.HEAL, aModifier.i_ModifierAmount, transform.position);

                        GameObject go = Instantiate(GameObject.Find("Heal Particle System"), pos, Quaternion.identity) as GameObject;
                        //GameObject go = Instantiate(GameObject.Find("Damage Particle System"), pos, Quaternion.identity) as GameObject;
                        go.GetComponent<CleanUp>().enabled = true;

                        GameObject.Find("EffectsSoundPlayer").GetComponent<SoundManager>().PlaySound("Heal");

                        break;

                    case Modifier.MODIFY_TYPE.ATTACK:
                        this.ModifiedStrength = this.BaseStrength + aModifier.i_ModifierAmount;

                        GameObject.Find("EffectsSoundPlayer").GetComponent<SoundManager>().PlaySound("PowerUp");
                        break;

                    case Modifier.MODIFY_TYPE.MAGIC:
                        this.ModifiedMagic = this.BaseMagic + aModifier.i_ModifierAmount;

                        GameObject.Find("EffectsSoundPlayer").GetComponent<SoundManager>().PlaySound("PowerUp");
                        break;

                    case Modifier.MODIFY_TYPE.ARMOUR:
                        this.ModifiedArmour = this.BaseArmour + aModifier.i_ModifierAmount;

                        GameObject.Find("EffectsSoundPlayer").GetComponent<SoundManager>().PlaySound("Defend");
                        break;

                    case Modifier.MODIFY_TYPE.SPEED:
                        this.ModifiedSpeed = this.BaseSpeed + aModifier.i_ModifierAmount;

                        GameObject.Find("EffectsSoundPlayer").GetComponent<SoundManager>().PlaySound("PowerUp");
                        break;

                    case Modifier.MODIFY_TYPE.RANGE:
                        this.ModifiedAttackRange = this.BaseAttackRange + aModifier.i_ModifierAmount;

                        GameObject.Find("EffectsSoundPlayer").GetComponent<SoundManager>().PlaySound("PowerUp");
                        break;

                    case Modifier.MODIFY_TYPE.RESTRICT:
                        Vector3 spawn = pos + new Vector3(0, this.transform.localScale.y / 2, 0);

                        go = Instantiate(GameObject.Find("Restrict Particle System"), spawn, GameObject.Find("Restrict Particle System").transform.rotation) as GameObject;
                        go.GetComponent<CleanUp>().enabled = true;
                        b_Restricted = true;
                        break;
                }
            }
        }
    }
}                   