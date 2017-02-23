using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PersistentData : MonoBehaviour {

    public static PersistentData m_Instance;

    // Stuff to store in persistent data
    public int PlayerMoney;
    public List<Items> ItemList = new List<Items>();

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
	   
	}

    void OnEnable()
    {
        LoadData();
    }

    void OnDisable()
    {
        SaveDate();
    }

    PlayerData CopyData()
    {
        PlayerData returnData = new PlayerData();

        returnData.PlayerMoney = this.PlayerMoney;
        returnData.ItemList = this.ItemList;

        return returnData;
    }

    void LoadData(PlayerData theData)
    {
        theData.PlayerMoney = this.PlayerMoney;
        theData.ItemList = this.ItemList;
    }

    public void SaveDate()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/PersistentData.dat");

        PlayerData data = CopyData();

        bf.Serialize(file, data);
        file.Close();
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
        }
    }
}

[System.Serializable]
class PlayerData
{
    public int PlayerMoney;
    public List<Items> ItemList = new List<Items>();
}