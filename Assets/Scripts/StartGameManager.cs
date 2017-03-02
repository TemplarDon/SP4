using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class StartGameManager : MonoBehaviour
{

    public GameObject SpawnPos_1;
    public GameObject SpawnPos_2;
    public GameObject SpawnPos_3;

    public bool b_AssignedCharacters = false;

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

            string tempWeapon;
            string tempArmour;
            string tempUseable;

            // Find Character(s)
            if (PersistentData.m_Instance.char1Char != "")
            {
                GameObject.Find(PersistentData.m_Instance.char1Char).transform.position = SpawnPos_1.transform.position;

                GameObject.Find(PersistentData.m_Instance.char1Char).GetComponent<BaseCharacter>().pos.x = (int)GameObject.Find(PersistentData.m_Instance.char1Char).transform.position.x;
                GameObject.Find(PersistentData.m_Instance.char1Char).GetComponent<BaseCharacter>().pos.y = (int)GameObject.Find(PersistentData.m_Instance.char1Char).transform.position.y;

                if ((tempWeapon = GetWeaponName(1)) != "")
                {
                    GameObject.Find(PersistentData.m_Instance.char1Char).GetComponent<BaseCharacter>().theWeapon = GameObject.Find(tempWeapon).GetComponent<Weapons>();
                }

                if ((tempArmour = GetArmourName(1)) != "")
                {
                    GameObject.Find(PersistentData.m_Instance.char1Char).GetComponent<BaseCharacter>().theArmour = GameObject.Find(tempArmour).GetComponent<Armours>();
                }

                if ((tempUseable = GetUseablesName(1)) != "")
                {
                    GameObject.Find(PersistentData.m_Instance.char1Char).GetComponent<BaseCharacter>().theItem = GameObject.Find(tempUseable).GetComponent<Useables>();
                }

                Char1Found = true;
            }

            if (PersistentData.m_Instance.char2Char != "")
            {
                GameObject.Find(PersistentData.m_Instance.char2Char).transform.position = SpawnPos_2.transform.position;

                GameObject.Find(PersistentData.m_Instance.char2Char).GetComponent<BaseCharacter>().pos.x = (int)GameObject.Find(PersistentData.m_Instance.char2Char).transform.position.x;
                GameObject.Find(PersistentData.m_Instance.char2Char).GetComponent<BaseCharacter>().pos.y = (int)GameObject.Find(PersistentData.m_Instance.char2Char).transform.position.y;

                if ((tempWeapon = GetWeaponName(2)) != "")
                {
                    GameObject.Find(PersistentData.m_Instance.char2Char).GetComponent<BaseCharacter>().theWeapon = GameObject.Find(tempWeapon).GetComponent<Weapons>();
                }

                if ((tempArmour = GetArmourName(2)) != "")
                {
                    GameObject.Find(PersistentData.m_Instance.char2Char).GetComponent<BaseCharacter>().theArmour = GameObject.Find(tempArmour).GetComponent<Armours>();
                }

                if ((tempUseable = GetUseablesName(2)) != "")
                {
                    GameObject.Find(PersistentData.m_Instance.char2Char).GetComponent<BaseCharacter>().theItem = GameObject.Find(tempUseable).GetComponent<Useables>();
                }

                Char2Found = true;
            }

            if (PersistentData.m_Instance.char3Char != "")
            {
                GameObject.Find(PersistentData.m_Instance.char3Char).transform.position = SpawnPos_3.transform.position;

                GameObject.Find(PersistentData.m_Instance.char3Char).GetComponent<BaseCharacter>().pos.x = (int)GameObject.Find(PersistentData.m_Instance.char3Char).transform.position.x;
                GameObject.Find(PersistentData.m_Instance.char3Char).GetComponent<BaseCharacter>().pos.y = (int)GameObject.Find(PersistentData.m_Instance.char3Char).transform.position.y;

                if ((tempWeapon = GetWeaponName(3)) != "")
                {
                    GameObject.Find(PersistentData.m_Instance.char3Char).GetComponent<BaseCharacter>().theWeapon = GameObject.Find(tempWeapon).GetComponent<Weapons>();
                }

                if ((tempArmour = GetArmourName(3)) != "")
                {
                    GameObject.Find(PersistentData.m_Instance.char3Char).GetComponent<BaseCharacter>().theArmour = GameObject.Find(tempArmour).GetComponent<Armours>();
                }

                if ((tempUseable = GetUseablesName(3)) != "")
                {
                    GameObject.Find(PersistentData.m_Instance.char3Char).GetComponent<BaseCharacter>().theItem = GameObject.Find(tempUseable).GetComponent<Useables>();
                }

                Char3Found = true;
            }

            if (GameObject.Find("friendlyTeamManager") != null)
            {
                if (Char1Found)
                {
                    GameObject.Find("friendlyTeamManager").GetComponent<teamManager>().teamList.Add(GameObject.Find(PersistentData.m_Instance.char1Char).GetComponent<BaseCharacter>());
                }

                if (Char2Found)
                {
                    GameObject.Find("friendlyTeamManager").GetComponent<teamManager>().teamList.Add(GameObject.Find(PersistentData.m_Instance.char2Char).GetComponent<BaseCharacter>());
                }

                if (Char3Found)
                {
                    GameObject.Find("friendlyTeamManager").GetComponent<teamManager>().teamList.Add(GameObject.Find(PersistentData.m_Instance.char3Char).GetComponent<BaseCharacter>());
                }

                GameObject.Find("TurnManager").GetComponent<turnManage>().characNEW = GameObject.Find("friendlyTeamManager").GetComponent<teamManager>().teamList[0];

                SpawnPos_1.SetActive(false);
                SpawnPos_2.SetActive(false);
                SpawnPos_3.SetActive(false);

                // Set all other characters to offscreen and disable them
                foreach (GameObject go in GameObject.FindGameObjectsWithTag("Character"))
                {
                    BaseCharacter aCharacter = go.GetComponent<BaseCharacter>();

                    if (!aCharacter.IsEnemy && (go.name != PersistentData.m_Instance.char1Char && go.name != PersistentData.m_Instance.char2Char && go.name != PersistentData.m_Instance.char3Char))
                    {
                        aCharacter.GetComponent<BaseCharacter>().enabled = false;
                        aCharacter.gameObject.SetActive(false);
                    }
                }

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
                return name;
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
                return name;
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
                return name;
            }
        }

        return "";
    }
}
