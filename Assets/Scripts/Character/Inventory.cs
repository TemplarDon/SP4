using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class Inventory : MonoBehaviour{

    public int InventorySizeColumns;
    public int InventorySizeRows;

    private List<List<Items>> InventoryList = new List<List<Items>>();

    private Equippables EquippedArmour;
    private Equippables EquippedWeapon;

	// Use this for initialization
	public void Init (int c = 5, int r = 5) 
    {
        EquippedArmour = null;
        EquippedWeapon = null;

        InventorySizeColumns = InventorySizeRows = 5;

        // Fill list with null
        for (int y = 0; y < InventorySizeColumns; ++y)
        {
            for (int x = 0; x < InventorySizeRows; ++x)
            {
                InventoryList[y][x] = null;
            }

        }
	}
	
	// Update is called once per frame
	public void Update () {
	
	}

    // Add to item list
    // toAdd    - Item to add
    // c        - column index to add to, if -1; add to next empty spot
    // r        - row index to add to, if -1; add to next empty spot
    // returns true if could add, returns false otherwise
    public bool AddItem(Items toAdd,  int c = -1, int r = -1)
    {
        if (c < 0 || r < 0)
        {
            for (int y = 0; y < InventorySizeColumns; ++y)
            {
                for (int x = 0; x < InventorySizeRows; ++x)
                {
                    if (InventoryList[y][x] == null)
                    {
                        InventoryList[y][x] = toAdd;
                        return true;
                    }
                }
            }
        }
        else if (InventoryList[c][r] == null)
        {
            InventoryList[c][r] = toAdd;
            return true;
        }

        return false;
    }

    // Gets an item from list
    // c        - column index to get from
    // r        - row index to get from
    // returns true if could get, returns false otherwise
    public Items GetItem(int c, int r)
    {
        if (InventoryList[c][r] != null)
            return InventoryList[c][r];

        return null;
    }

}
