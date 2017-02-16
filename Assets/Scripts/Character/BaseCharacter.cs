using UnityEngine;
using System.Collections;

public class BaseCharacter : MonoBehaviour {

    // Handle to map
    public LevelGenerate theLevel;
    public Vector3 pos;

    // Managers
    public Inventory theInventory = new Inventory();
    public int InventorySizeColumns = 5;
    public int InventorySizeRows = 5;

    public SkillsManager theSkillsManager = new SkillsManager();

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
        //theInventory.Init(InventorySizeColumns, InventorySizeRows);

        pos.x = (int)this.transform.position.x;
        pos.y = (int)this.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {

        ConstrainToGrid();
        this.transform.position = pos;
        //theLevel.checkPossibleLoc((int)pos.x - 1, -((int)pos.y) - 1, 7, 4);
        theLevel.mapposx = (int)pos.x - 1;
        theLevel.mapposy = -((int)pos.y) - 1;

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
}
