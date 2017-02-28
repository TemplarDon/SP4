using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class StartGameManager : MonoBehaviour
{

    public GameObject SpawnPos_1;
    public GameObject SpawnPos_2;
    public GameObject SpawnPos_3;

    bool b_AssignedCharacters = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!b_AssignedCharacters)
        {
            bool Char1Found = false;
            bool Char2Found = false;
            bool Char3Found = false;


            // Find Character(s)
            if (PersistentData.m_Instance.char1Char != "")
            {
                GameObject.Find(PersistentData.m_Instance.char1Char).transform.position = SpawnPos_1.transform.position;
                GameObject.Find(PersistentData.m_Instance.char1Char).GetComponent<BaseCharacter>().theWeapon = GameObject.Find(GetWeaponName(1)).GetComponent<Weapons>();
                GameObject.Find(PersistentData.m_Instance.char1Char).GetComponent<BaseCharacter>().theArmour = GameObject.Find(GetArmourName(1)).GetComponent<Armours>();
                GameObject.Find(PersistentData.m_Instance.char1Char).GetComponent<BaseCharacter>().theItem = GameObject.Find(GetUseablesName(1)).GetComponent<Useables>();

                Char1Found = true;
            }

            if (PersistentData.m_Instance.char2Char != "")
            {
                GameObject.Find(PersistentData.m_Instance.char2Char).transform.position = SpawnPos_2.transform.position;
                GameObject.Find(PersistentData.m_Instance.char2Char).GetComponent<BaseCharacter>().theWeapon = GameObject.Find(GetWeaponName(2)).GetComponent<Weapons>();
                GameObject.Find(PersistentData.m_Instance.char2Char).GetComponent<BaseCharacter>().theArmour = GameObject.Find(GetArmourName(2)).GetComponent<Armours>();
                GameObject.Find(PersistentData.m_Instance.char2Char).GetComponent<BaseCharacter>().theItem = GameObject.Find(GetUseablesName(2)).GetComponent<Useables>();

                Char2Found = true;
            }

            if (PersistentData.m_Instance.char3Char != "")
            {

                GameObject.Find(PersistentData.m_Instance.char3Char).transform.position = SpawnPos_3.transform.position;
                GameObject.Find(PersistentData.m_Instance.char3Char).GetComponent<BaseCharacter>().theWeapon = GameObject.Find(GetWeaponName(3)).GetComponent<Weapons>();
                GameObject.Find(PersistentData.m_Instance.char3Char).GetComponent<BaseCharacter>().theArmour = GameObject.Find(GetArmourName(3)).GetComponent<Armours>();
                GameObject.Find(PersistentData.m_Instance.char3Char).GetComponent<BaseCharacter>().theItem = GameObject.Find(GetUseablesName(3)).GetComponent<Useables>();

                Char3Found = true;
            }

            if (GameObject.Find("friendlyTeamManager") != null)
            {
                if (Char1Found)
                GameObject.Find("friendlyTeamManager").GetComponent<teamManager>().teamList.Add(GameObject.Find(PersistentData.m_Instance.char1Char).GetComponent<BaseCharacter>());
                
                if (Char2Found)
                GameObject.Find("friendlyTeamManager").GetComponent<teamManager>().teamList.Add(GameObject.Find(PersistentData.m_Instance.char2Char).GetComponent<BaseCharacter>());
                
                if (Char3Found)
                GameObject.Find("friendlyTeamManager").GetComponent<teamManager>().teamList.Add(GameObject.Find(PersistentData.m_Instance.char3Char).GetComponent<BaseCharacter>());

                b_AssignedCharacters = true;
            }
        }
    }

    string GetWeaponName(int charNum)
    {
        List<string> checkList = new List<string>();

        switch (charNum)
        {
            case 1:
                checkList = PersistentData.m_Instance.char1Items;
                break;

            case 2:
                checkList = PersistentData.m_Instance.char2Items;
            break;

            case 3:
                checkList = PersistentData.m_Instance.char3Items;
            break;

        }

        foreach (string name in checkList)
        {
            Items anItem = GameObject.Find(name).GetComponent<Items>();

            if (anItem.m_ItemType == Items.ITEM_TYPE.WEAPONS)
            {
                return anItem.s_ItemName;
            }
        }

        return "";
    }

    string GetArmourName(int charNum)
    {
        List<string> checkList = new List<string>();

        switch (charNum)
        {
            case 1:
                checkList = PersistentData.m_Instance.char1Items;
                break;

            case 2:
                checkList = PersistentData.m_Instance.char2Items;
                break;

            case 3:
                checkList = PersistentData.m_Instance.char3Items;
                break;

        }

        foreach (string name in checkList)
        {
            Items anItem = GameObject.Find(name).GetComponent<Items>();

            if (anItem.m_ItemType == Items.ITEM_TYPE.ARMOUR)
            {
                return anItem.s_ItemName;
            }
        }

        return "";
    }

    string GetUseablesName(int charNum)
    {
        List<string> checkList = new List<string>();

        switch (charNum)
        {
            case 1:
                checkList = PersistentData.m_Instance.char1Items;
                break;

            case 2:
                checkList = PersistentData.m_Instance.char2Items;
                break;

            case 3:
                checkList = PersistentData.m_Instance.char3Items;
                break;

        }

        foreach (string name in checkList)
        {
            Items anItem = GameObject.Find(name).GetComponent<Items>();

            if (anItem.m_ItemType == Items.ITEM_TYPE.USEABLES)
            {
                return anItem.s_ItemName;
            }
        }

        return "";
    }
}
