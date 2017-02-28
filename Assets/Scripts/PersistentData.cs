using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PersistentData : MonoBehaviour {

    public static PersistentData m_Instance;

    bool b_InitialLoad = false;

    // Stuff to store in persistent data
    public int PlayerMoney;
    public List<string> ItemList = new List<string>();
    public List<string> CharacterList = new List<string>();

    public List<BaseCharacter> IngameCharacterList = new List<BaseCharacter>(); // List to transfer characters from character select to ingame

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

        if (Input.GetKeyUp(KeyCode.X))
        {
            // Load whatever numbers here
            PlayerMoney = 100000;
            ItemList.Clear();
            CharacterList.Clear();
            Debug.Log("Data Cleared");
        }

	}

    //void OnEnable()
    //{
    //    LoadData();
    //}

    void OnDisable()
    {
        SaveDate();
    }

    PlayerData CopyData()
    {
        PlayerData returnData = new PlayerData();

        returnData.PlayerMoney = this.PlayerMoney;
        returnData.ItemList = this.ItemList;
        returnData.CharacterList = this.CharacterList;

        return returnData;
    }

    void LoadData(PlayerData theData)
    {
        this.PlayerMoney = theData.PlayerMoney;
        this.ItemList = theData.ItemList;
        this.CharacterList = theData.CharacterList;
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
    public List<string> ItemList = new List<string>();
    public List<string> CharacterList = new List<string>();
}