﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PersistentData : MonoBehaviour {

    public static PersistentData m_Instance;

    bool b_InitialLoad = false;

    // Stuff to store in persistent data
    public int PlayerMoney;
    public bool firstTimeAtModeSelect = true;
    public bool firstTimeAtShop = true;
    public bool firstTimeAtGacha = true;
    public List<string> ItemList = new List<string>();
    public List<string> CharacterList = new List<string>();

    public string char1Char = "";
    public string char2Char = "";
    public string char3Char = "";
    public List<string> char1Items = new List<string>();
    public List<string> char2Items = new List<string>();
    public List<string> char3Items = new List<string>();

    public enum GAME_MODE
    {
        STORY,
        FREE_BATTLE,
    }
    public GAME_MODE CurrentGameMode;

    // Use this for initialization
    void Awake () {

        DontDestroyOnLoad(gameObject);

        if (!m_Instance)
        {
            m_Instance = this;
        }
        else if (m_Instance != this)
        {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
	   
        if (!b_InitialLoad)
        {
            LoadData();
            b_InitialLoad = true;
        }

        //Debug.Log(firstTimeAtModeSelect.ToString());

        if (Input.GetKeyUp(KeyCode.X))
        {
            // Load whatever numbers here
            ItemList.Clear();
            CharacterList.Clear();
            firstTimeAtModeSelect = true;
            firstTimeAtShop = true;
            firstTimeAtGacha = true;
            Debug.Log("Data Cleared");
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            PlayerMoney += 10000;
        }

	}

    //void OnEnable()
    //{
    //    LoadData();
    //}

#if UNITY_ANDROID

    void OnApplicationQuit() {
        SaveDate();
    }

    void OnApplicationPause() {
        SaveDate();
    }

#endif

    void OnDisable()
    {
        SaveDate();
    }

    PlayerData CopyData()
    {
        PlayerData returnData = new PlayerData();

        returnData.PlayerMoney = this.PlayerMoney;
        returnData.firstTimeAtModeSelect = this.firstTimeAtModeSelect;
        returnData.firstTimeAtShop = this.firstTimeAtShop;
        returnData.firstTimeAtGacha = this.firstTimeAtGacha;
        returnData.ItemList = this.ItemList;
        returnData.CharacterList = this.CharacterList;

       // Debug.Log(firstTimeAtModeSelect.ToString());

        return returnData;
    }

    void LoadData(PlayerData theData)
    {
        this.PlayerMoney = theData.PlayerMoney;
        this.firstTimeAtModeSelect = theData.firstTimeAtModeSelect;
        this.firstTimeAtShop = theData.firstTimeAtShop;
        this.firstTimeAtGacha = theData.firstTimeAtGacha;
        this.ItemList = theData.ItemList;
        this.CharacterList = theData.CharacterList;

        //Debug.Log(firstTimeAtModeSelect.ToString());

    }

    public void SaveDate()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/PersistentData.dat");

        PlayerData data = CopyData();

        bf.Serialize(file, data);
        file.Close();

        Debug.Log("Saved Data.");
    }

    public void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/PersistentData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/PersistentData.dat", FileMode.Open);
            PlayerData data = (PlayerData)(bf.Deserialize(file));

            LoadData(data);
            file.Close();

            Debug.Log("Loaded Data.");
        }
    }
}

[System.Serializable]
class PlayerData
{
    public int PlayerMoney;
    public bool firstTimeAtModeSelect;
    public bool firstTimeAtShop;
    public bool firstTimeAtGacha;
    public List<string> ItemList = new List<string>();
    public List<string> CharacterList = new List<string>();
}