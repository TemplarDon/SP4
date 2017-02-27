using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

using System.Collections.Generic;

public class InvManagerSelect : MonoBehaviour {

    private List<Sprite> itemSpriteList = new List<Sprite>();
    private List<Sprite> weaponSpriteList = new List<Sprite>();
    private List<Sprite> helmetSpriteList = new List<Sprite>();

    private Button currentHov;
    private Color currentCol;
    private bool currentHovState = false;

    public GameObject draggingEquip;
    private bool dragging;

    private Text slot1_helmetBuff;
    private Text slot1_weaponBuff;
    private Text slot1_itemBuff;
    private Text slot2_helmetBuff;
    private Text slot2_weaponBuff;
    private Text slot2_itemBuff;
    private Text slot3_helmetBuff;
    private Text slot3_weaponBuff;
    private Text slot3_itemBuff;

    private Text itemName;
    private Text itemDesc;

    private int currentTab = 3;

    public Sprite   helmetIcon;
    public Sprite   weaponIcon;
    public Sprite   itemIcon;

    // Use this for initialization
    void Start () {
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("ItemTemp");
        foreach (GameObject obj in allObjects)
        {
            itemSpriteList.Add(obj.GetComponent<Image>().sprite);
        }

        GameObject[] allObjects2 = GameObject.FindGameObjectsWithTag("WeaponTemp");
        foreach (GameObject obj in allObjects2)
        {
            weaponSpriteList.Add(obj.GetComponent<Image>().sprite);
        }

        GameObject[] allObjects3 = GameObject.FindGameObjectsWithTag("HelmetTemp");
        foreach (GameObject obj in allObjects3)
        {
            helmetSpriteList.Add(obj.GetComponent<Image>().sprite);
        }

        itemName = GameObject.Find("ItemName").GetComponent<Text>();
        itemDesc = GameObject.Find("ItemDesc").GetComponent<Text>();

        fetchHelmetSprites();
        //fetchWeaponSprites();
        //fetchItemSprites();

        slot1_helmetBuff = GameObject.Find("Slot1_helmetBuff").GetComponent<Text>();
        slot1_weaponBuff = GameObject.Find("Slot1_weaponBuff").GetComponent<Text>();
        slot1_itemBuff   = GameObject.Find("Slot1_itemBuff").GetComponent<Text>();
        slot2_helmetBuff = GameObject.Find("Slot2_helmetBuff").GetComponent<Text>();
        slot2_weaponBuff = GameObject.Find("Slot2_weaponBuff").GetComponent<Text>();
        slot2_itemBuff   = GameObject.Find("Slot2_itemBuff").GetComponent<Text>();
        slot3_helmetBuff = GameObject.Find("Slot3_helmetBuff").GetComponent<Text>();
        slot3_weaponBuff = GameObject.Find("Slot3_weaponBuff").GetComponent<Text>();
        slot3_itemBuff   = GameObject.Find("Slot3_itemBuff").GetComponent<Text>();

        slot1_helmetBuff.text   = "No Armour";
        slot1_weaponBuff.text   = "No Weapon";
        slot1_itemBuff.text     = "No Item";
        slot2_helmetBuff.text= "No Armour";
        slot2_weaponBuff.text= "No Weapon";
        slot2_itemBuff.text  = "No Item";
        slot3_helmetBuff.text = "No Armour";
        slot3_weaponBuff.text= "No Weapon";
        slot3_itemBuff.text  = "No Item";

        slot1_helmetBuff.color = new Color(1, 0, 0);
        slot1_weaponBuff.color = new Color(1, 0, 0);
        slot1_itemBuff.color = new Color(1, 0, 0);
        slot2_helmetBuff.color = new Color(1, 0, 0);
        slot2_weaponBuff.color = new Color(1, 0, 0);
        slot2_itemBuff.color = new Color(1, 0, 0);
        slot3_helmetBuff.color = new Color(1, 0, 0);
        slot3_weaponBuff.color = new Color(1, 0, 0);
        slot3_itemBuff.color = new Color(1, 0, 0);
    }

    // Update is called once per frame
    void Update () {
        if (dragging)
        {
            if (Input.GetMouseButton(0))
            {
                draggingEquip.transform.position = Input.mousePosition;
            }
            else if(currentHovState == false)
            {
                draggingEquip.transform.position = new Vector3(9999, 9999, 9999);
                dragging = false;
                string discardedItem = (draggingEquip.GetComponent<Image>().sprite.name.Replace("item_", "")).Replace("2", "");
                PersistentData.m_Instance.ItemList.Add(discardedItem);
                switch (currentTab)
                {
                    case 1:
                        fetchHelmetSprites();
                        break;
                    case 2:
                        fetchWeaponSprites();
                        break;
                    case 3:
                        fetchItemSprites();
                        break;
                }

                //Debug.Log("hovState = falseeee");
            }
            else if((currentHov.name[6] == 'h' && currentTab == 1) || (currentHov.name[6] == 'w' && currentTab == 2) || (currentHov.name[6] == 'i' && currentTab == 3))
            {
                if(currentHov.GetComponent<Image>().name.Contains("slot"))
                {
                    
                }
                else
                {
                    string discardedItem;
                    switch (currentTab)
                    {
                        case 1:
                            discardedItem = (currentHov.GetComponent<Image>().sprite.name.Replace("helmet_", "")).Replace("2", "");
                            PersistentData.m_Instance.ItemList.Add(discardedItem);
                            fetchHelmetSprites();
                            break;
                        case 2:
                            discardedItem = (currentHov.GetComponent<Image>().sprite.name.Replace("weapon_", "")).Replace("2", "");
                            PersistentData.m_Instance.ItemList.Add(discardedItem);
                            fetchWeaponSprites();
                            break;
                        case 3:
                            discardedItem = (currentHov.GetComponent<Image>().sprite.name.Replace("item_", "")).Replace("2", "");
                            PersistentData.m_Instance.ItemList.Add(discardedItem);
                            fetchItemSprites();
                            break;
                    }
                    //Debug.Log("RECYCLE");
                }

                currentHov.GetComponent<Image>().sprite = draggingEquip.GetComponent<Image>().sprite;

                if (currentHov.name.Contains("Slot1"))
                {
                    if (currentHov.name.Contains("helmet"))
                    {
                        slot1_helmetBuff.color = new Color(0, 0, 0);
                        slot1_helmetBuff.text = GameObject.Find(currentHov.GetComponent<Image>().sprite.name.Replace("2", "")).GetComponent<Items>().s_ItemDisp;
                    }
                    else if (currentHov.name.Contains("weapon"))
                    {
                        slot1_weaponBuff.color = new Color(0, 0, 0);
                        slot1_weaponBuff.text = GameObject.Find(currentHov.GetComponent<Image>().sprite.name.Replace("2", "")).GetComponent<Items>().s_ItemDisp;
                    }
                    else if (currentHov.name.Contains("item"))
                    {
                        slot1_itemBuff.color = new Color(0, 0, 0);
                        slot1_itemBuff.text = GameObject.Find(currentHov.GetComponent<Image>().sprite.name.Replace("2", "")).GetComponent<Items>().s_ItemDisp;
                    }
                }
                else if (currentHov.name.Contains("Slot2"))
                {
                    if (currentHov.name.Contains("helmet"))
                    {
                        slot2_helmetBuff.color = new Color(0, 0, 0);
                        slot2_helmetBuff.text = GameObject.Find(currentHov.GetComponent<Image>().sprite.name.Replace("2","")).GetComponent<Items>().s_ItemDisp;
                    }
                    else if (currentHov.name.Contains("weapon"))
                    {
                        slot2_weaponBuff.color = new Color(0, 0, 0);
                        slot2_weaponBuff.text = GameObject.Find(currentHov.GetComponent<Image>().sprite.name.Replace("2", "")).GetComponent<Items>().s_ItemDisp;
                    }
                    else if (currentHov.name.Contains("item"))
                    {
                        slot2_itemBuff.color = new Color(0, 0, 0);
                        slot2_itemBuff.text = GameObject.Find(currentHov.GetComponent<Image>().sprite.name.Replace("2", "")).GetComponent<Items>().s_ItemDisp;
                    }
                }
                else if (currentHov.name.Contains("Slot3"))
                {
                    if (currentHov.name.Contains("helmet"))
                    {
                        slot3_helmetBuff.color = new Color(0, 0, 0);
                        slot3_helmetBuff.text = GameObject.Find(currentHov.GetComponent<Image>().sprite.name.Replace("2", "")).GetComponent<Items>().s_ItemDisp;
                    }
                    else if (currentHov.name.Contains("weapon"))
                    {
                        slot3_weaponBuff.color = new Color(0, 0, 0);
                        slot3_weaponBuff.text = GameObject.Find(currentHov.GetComponent<Image>().sprite.name.Replace("2", "")).GetComponent<Items>().s_ItemDisp;
                    }
                    else if (currentHov.name.Contains("item"))
                    {
                        slot3_itemBuff.color = new Color(0, 0, 0);
                        slot3_itemBuff.text = GameObject.Find(currentHov.GetComponent<Image>().sprite.name.Replace("2", "")).GetComponent<Items>().s_ItemDisp;
                    }
                }

                
                draggingEquip.transform.position = new Vector3(9999, 9999, 9999);
                dragging = false;
            }
            else
            {
                draggingEquip.transform.position = new Vector3(9999, 9999, 9999);
                dragging = false;
                string discardedItem = (draggingEquip.GetComponent<Image>().sprite.name.Replace("item_", "")).Replace("2", "");
                PersistentData.m_Instance.ItemList.Add(discardedItem);
                Debug.Log(" ? else final ? ");

                switch (currentTab)
                {
                    case 1:
                        fetchHelmetSprites();
                        break;
                    case 2:
                        fetchWeaponSprites();
                        break;
                    case 3:
                        fetchItemSprites();
                        break;
                }

            }
        }

        //if (Input.GetKeyUp(KeyCode.Z))
        //{
        //    PersistentData.m_Instance.ItemList.Add("jacket");
        //    fetchHelmetSprites();
        //    Debug.Log("ADDED");
        //}
        //
        //if(Input.GetKeyUp(KeyCode.A))
        //{
        //    for(int i = 0; i < PersistentData.m_Instance.ItemList.Count; i++)
        //    {
        //        if(PersistentData.m_Instance.ItemList[i] == "Potion")
        //        {
        //            Debug.Log("POTION FOUND");
        //        }
        //    }
        //}

    }

    public void fetchHelmetSprites()
    {
        for (int i = 0; i < 15; i++)
        {
            string newName = "InvButton" + i.ToString();
            GameObject.Find(newName).GetComponent<Image>().sprite = GameObject.Find("helmet_unknown").GetComponent<Image>().sprite;
            GameObject.Find(newName).GetComponent<Image>().color = new Color(0.1f, 0.1f, 0.1f);
        }

        for (int j = 0; j < helmetSpriteList.Count; j++)
        {
            string newName = "InvButton" + j.ToString();
            GameObject.Find(newName).GetComponent<Image>().sprite = helmetSpriteList[j];
            GameObject.Find(newName).GetComponent<Image>().color = new Color(0.1f, 0.1f, 0.1f);
            for (int k = 0; k < PersistentData.m_Instance.ItemList.Count; k++)
            {
                if (helmetSpriteList[j].name.Contains(PersistentData.m_Instance.ItemList[k]))
                {
                    GameObject.Find(newName).GetComponent<Image>().color = new Color(1, 1, 1);
                    break;
                }
            }
        }

        currentTab = 1;
        GameObject.Find("InvTab1").GetComponent<Image>().color = new Color(0, 1, 0, 0.8f);
        GameObject.Find("InvTab2").GetComponent<Image>().color = new Color(1, 1, 1, 0.8f);
        GameObject.Find("InvTab3").GetComponent<Image>().color = new Color(1, 1, 1, 0.8f);
    }

    public void fetchWeaponSprites()
    {
        for (int i = 0; i < 15; i++)
        {
            string newName = "InvButton" + i.ToString();
            GameObject.Find(newName).GetComponent<Image>().sprite = GameObject.Find("weapon_unknown").GetComponent<Image>().sprite;
            GameObject.Find(newName).GetComponent<Image>().color = new Color(0.1f, 0.1f, 0.1f);
        }

        for (int i = 0; i < weaponSpriteList.Count; i++)
        {
            string newName = "InvButton" + i.ToString();
            GameObject.Find(newName).GetComponent<Image>().sprite = weaponSpriteList[i];
            GameObject.Find(newName).GetComponent<Image>().color = new Color(0.1f, 0.1f, 0.1f);
            for (int k = 0; k < PersistentData.m_Instance.ItemList.Count; k++)
            {
                if (weaponSpriteList[i].name.Contains(PersistentData.m_Instance.ItemList[k]))
                {
                    GameObject.Find(newName).GetComponent<Image>().color = new Color(1, 1, 1);
                    break;
                }
            }
        }

        currentTab = 2;
        GameObject.Find("InvTab1").GetComponent<Image>().color = new Color(1, 1, 1, 0.8f);
        GameObject.Find("InvTab2").GetComponent<Image>().color = new Color(0, 1, 0, 0.8f);
        GameObject.Find("InvTab3").GetComponent<Image>().color = new Color(1, 1, 1, 0.8f);
    }

    public void fetchItemSprites()
    {
        for (int i = 0; i < 15; i++)
        {
            string newName = "InvButton" + i.ToString();
            GameObject.Find(newName).GetComponent<Image>().sprite = GameObject.Find("item_unknown").GetComponent<Image>().sprite;
            GameObject.Find(newName).GetComponent<Image>().color = new Color(0.1f, 0.1f, 0.1f);
        }

        for (int j = 0; j < itemSpriteList.Count; j++)
        {
            string newName = "InvButton" + j.ToString();
            GameObject.Find(newName).GetComponent<Image>().sprite = itemSpriteList[j];

            GameObject.Find(newName).GetComponent<Image>().color = new Color(0.1f, 0.1f, 0.1f);
            for (int k = 0; k < PersistentData.m_Instance.ItemList.Count; k++)
            {
                if(itemSpriteList[j].name.Contains(PersistentData.m_Instance.ItemList[k]))
                {
                    GameObject.Find(newName).GetComponent<Image>().color = new Color(1, 1, 1);
                    break;
                }
            }
        }

        currentTab = 3;
        GameObject.Find("InvTab1").GetComponent<Image>().color = new Color(1, 1, 1, 0.8f);
        GameObject.Find("InvTab2").GetComponent<Image>().color = new Color(1, 1, 1, 0.8f);
        GameObject.Find("InvTab3").GetComponent<Image>().color = new Color(0, 1, 0, 0.8f);

        //PersistentData.m_Instance.ItemList
    }

    public void clickDrag(Button button)
    {
        for(int i = 0; i < 15; i++)
        {
            string newName = "InvButton" + i.ToString();
            if (button.name == newName && button.GetComponent<Image>().color.r != 0.1f)
            {
                for(int j = 0; j < PersistentData.m_Instance.ItemList.Count; j++)
                {
                    if((button.GetComponent<Image>().sprite.name).Contains(PersistentData.m_Instance.ItemList[j]))
                    {
                        PersistentData.m_Instance.ItemList.Remove(PersistentData.m_Instance.ItemList[j]);
                        switch(currentTab)
                        {
                            case 1:
                                fetchHelmetSprites();
                                break;
                            case 2:
                                fetchWeaponSprites();
                                break;
                            case 3:
                                fetchItemSprites();
                                break;
                        }
                        
                        //Debug.Log("--- removed --- ");
                        break;
                    }
                }

                dragging = true;
                draggingEquip.transform.position = Input.mousePosition;
                switch(currentTab)
                {
                    case 1:
                        string newerName = helmetSpriteList[i].name + 2.ToString();
                        draggingEquip.GetComponent<Image>().sprite = GameObject.Find(newerName).GetComponent<Image>().sprite;
                        //draggingEquip.GetComponent<Image>().sprite = helmetSpriteList[i];
                        break;
                    case 2:
                        string newerName2 = weaponSpriteList[i].name + 2.ToString();
                        draggingEquip.GetComponent<Image>().sprite = GameObject.Find(newerName2).GetComponent<Image>().sprite;
                        //draggingEquip.GetComponent<Image>().sprite = weaponSpriteList[i];
                        break;
                    case 3:
                        string newerName3 = itemSpriteList[i].name + 2.ToString();
                        draggingEquip.GetComponent<Image>().sprite = GameObject.Find(newerName3).GetComponent<Image>().sprite;
                        //draggingEquip.GetComponent<Image>().sprite = itemSpriteList[i];
                        break;
                }
            }
        }
    }

    public void slotHover(Button button)
    {
        currentHov = button;
        currentCol = button.GetComponent<Image>().color;
        currentHov.GetComponent<Image>().color = new Color(currentCol.r / 2.0f, currentCol.g / 2.0f, currentCol.b / 2.0f);
        draggingEquip.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        currentHovState = true;
    }

    public void slotLeave()
    {
        if(currentHov != null)
            currentHov.GetComponent<Image>().color = currentCol;
        draggingEquip.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        currentHovState = false;
    }

    public void clickItem(Button button)
    {
        string highlightName = draggingEquip.GetComponent<Image>().sprite.name.Replace("2", "");
        itemName.text = (GameObject.Find(highlightName).GetComponent<Items>().s_ItemDisp);
        itemDesc.text = (GameObject.Find(highlightName).GetComponent<Items>().s_ItemDesc);
    }

    public void clickSlot(Button button)
    {
        if (button.GetComponent<Image>().sprite.name.Contains("slot"))
        {

        }
        else
        {
            string discardedItem;
            if (button.name.Contains("helmet"))
            {
                discardedItem = (button.GetComponent<Image>().sprite.name.Replace("helmet_", "")).Replace("2", "");
                PersistentData.m_Instance.ItemList.Add(discardedItem);
                button.GetComponent<Image>().sprite = helmetIcon;
                fetchHelmetSprites();
                if (button.name.Contains("Slot1"))
                {
                    slot1_helmetBuff.text = "No Armour";
                    slot1_helmetBuff.color = new Color(1, 0, 0);
                }
                else if (button.name.Contains("Slot2"))
                {
                    slot2_helmetBuff.text = "No Armour";
                    slot2_helmetBuff.color = new Color(1, 0, 0);
                }
                else if (button.name.Contains("Slot3"))
                {
                    slot3_helmetBuff.text = "No Armour";
                    slot3_helmetBuff.color = new Color(1, 0, 0);
                }
            }
            else if (button.name.Contains("weapon"))
            {
                discardedItem = (button.GetComponent<Image>().sprite.name.Replace("weapon_", "")).Replace("2", "");
                PersistentData.m_Instance.ItemList.Add(discardedItem);
                button.GetComponent<Image>().sprite = weaponIcon;
                fetchWeaponSprites();
                if (button.name.Contains("Slot1"))
                {
                    slot1_weaponBuff.text = "No Armour";
                    slot1_weaponBuff.color = new Color(1, 0, 0);
                }
                else if (button.name.Contains("Slot2"))
                {
                    slot2_weaponBuff.text = "No Armour";
                    slot2_weaponBuff.color = new Color(1, 0, 0);
                }
                else if (button.name.Contains("Slot3"))
                {
                    slot3_weaponBuff.text = "No Armour";
                    slot3_weaponBuff.color = new Color(1, 0, 0);
                }
            }
            else if (button.name.Contains("item"))
            {
                discardedItem = (button.GetComponent<Image>().sprite.name.Replace("item_", "")).Replace("2", "");
                PersistentData.m_Instance.ItemList.Add(discardedItem);
                button.GetComponent<Image>().sprite = itemIcon;
                fetchItemSprites();
                if (button.name.Contains("Slot1"))
                {
                    slot1_itemBuff.text = "No Armour";
                    slot1_itemBuff.color = new Color(1, 0, 0);
                }
                else if (button.name.Contains("Slot2"))
                {
                    slot2_itemBuff.text = "No Armour";
                    slot2_itemBuff.color = new Color(1, 0, 0);
                }
                else if (button.name.Contains("Slot3"))
                {
                    slot3_itemBuff.text = "No Armour";
                    slot3_itemBuff.color = new Color(1, 0, 0);
                }
            }
        }
    }
}
