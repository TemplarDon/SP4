using UnityEngine;
using System.Collections;

public class BaseCharacter : MonoBehaviour {

    // Handle to map
    public LevelGenerate theLevel;
    Vector3 pos;
    

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
	}
	
	// Update is called once per frame
	void Update () {
        
        // Test Code
        //if (b_ShouldMove)
        //{
        //    Vector3 dir = ((m_Destination - transform.position).normalized) * Time.deltaTime * 10;
        //    transform.Translate(dir.x, dir.y, 0);   
        //}
        
	}

    void OnMouseDown()
    {
        // Switch current controlled character to this one
        GameObject.Find("Controller").GetComponent<CharacterController>().CurrentControlledCharacter = this.gameObject;
        Debug.Log("Selected.");
    }

    public void SetCharacterDestination(Vector3 dest)
    {
        m_Destination = dest;
    }

    public void SetToMove(bool status)
    {
        b_ShouldMove = status;
    }

    void ConstrainToGrid()
    {
        //if (xpos < 1)
        //{
        //    mapposx++;
        //    xpos = 1;
        //}
        //else if (xpos > map.xsize)
        //{
        //    mapposx--;
        //    xpos = map.xsize;
        //}

        //if (ypos > -1)
        //{
        //    mapposy++;
        //    ypos = -1;
        //}
        //else if (ypos < -map.ysize)
        //{
        //    mapposy--;
        //    ypos = -map.ysize;
        //}
    }
}
