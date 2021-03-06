﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterSelect : MonoBehaviour 
{
    public Image AeroSmith;
    public Image SixBullets;
    public Image Zipperman;
    public Image SilverChariot;
    public Image StarPlatinum;
    public Image GoldenWind;
    public Image PurpleSmoke;
    public Image Reverb;
    public Image EmperorCrimson;

    public Image[] tick;
    string char1;

    int tickCounter = 0;

    public Image infoPic;

    bool b_RunOnce = true;
	// Use this for initialization
	void Start () 
    {
        //AeroSmith.enabled = PlayerPrefs.HasKey("BoughtChar1");
        //SixBullets.enabled = PlayerPrefs.HasKey("BoughtChar2");
        //Zipperman.enabled = PlayerPrefs.HasKey("BoughtChar3");
        //SilverChariot.enabled = PlayerPrefs.HasKey("BoughtChar4");
        //StarPlatinum.enabled = PlayerPrefs.HasKey("BoughtChar5");
        //GoldenWind.enabled = PlayerPrefs.HasKey("BoughtChar6");
        //PurpleSmoke.enabled = PlayerPrefs.HasKey("BoughtChar7");
        //Reverb.enabled = PlayerPrefs.HasKey("BoughtChar8");
        //EmperorCrimson.enabled = PlayerPrefs.HasKey("BoughtChar9");
        Debug.Log("Start");


	}
	
	// Update is called once per frame
    void Update()
    {
        if (b_RunOnce)
        {
            for (int i = 0; i < PersistentData.m_Instance.CharacterList.Count; ++i)
            {
                if (PersistentData.m_Instance.CharacterList[i].Equals("AeroSmith"))
                {
                    AeroSmith.enabled = true;
                }
                if (PersistentData.m_Instance.CharacterList[i].Equals("SixBullets"))
                {
                    SixBullets.enabled = true;

                }
                if (PersistentData.m_Instance.CharacterList[i].Equals("Zipperman"))
                {
                    Zipperman.enabled = true;

                }
                if (PersistentData.m_Instance.CharacterList[i].Equals("SilverChariot"))
                {
                    SilverChariot.enabled = true;

                }
                if (PersistentData.m_Instance.CharacterList[i].Equals("StarPlatinum"))
                {
                    StarPlatinum.enabled = true;

                }
                if (PersistentData.m_Instance.CharacterList[i].Equals("GoldenWind"))
                {
                    GoldenWind.enabled = true;

                }
                if (PersistentData.m_Instance.CharacterList[i].Equals("PurpleSmoke"))
                {
                    PurpleSmoke.enabled = true;

                }
                if (PersistentData.m_Instance.CharacterList[i].Equals("Reverb"))
                {
                    Reverb.enabled = true;

                }
                if (PersistentData.m_Instance.CharacterList[i].Equals("EmperorCrimson"))
                {
                    EmperorCrimson.enabled = true;

                }
            }
            b_RunOnce = false;
        }
    }
    public void tickImageI(int i)
    {
        if (tick[i].enabled == false)
        {
            if (tickCounter < 3)
            {
                tick[i].enabled = true;
                tickCounter++;
                switchAdd(i);
            }
        }
        else
        {
            tick[i].enabled = false;
            tickCounter--;
            switchRem(i);
        }
    }

    public void switchAdd(int i)
    {
        switch(i)
        {
            case 0:
                GameObject.Find("InventoryManager").GetComponent<InvManagerSelect>().addChar(AeroSmith);
                break;
            case 1:
                GameObject.Find("InventoryManager").GetComponent<InvManagerSelect>().addChar(SixBullets);
                break;
            case 2:
                GameObject.Find("InventoryManager").GetComponent<InvManagerSelect>().addChar(Zipperman);
                break;
            case 3:
                GameObject.Find("InventoryManager").GetComponent<InvManagerSelect>().addChar(SilverChariot);
                break;
            case 4:
                GameObject.Find("InventoryManager").GetComponent<InvManagerSelect>().addChar(StarPlatinum);
                break;
            case 5:
                GameObject.Find("InventoryManager").GetComponent<InvManagerSelect>().addChar(GoldenWind);
                break;
            case 6:
                GameObject.Find("InventoryManager").GetComponent<InvManagerSelect>().addChar(PurpleSmoke);
                break;
            case 7:
                GameObject.Find("InventoryManager").GetComponent<InvManagerSelect>().addChar(Reverb);
                break;
            case 8:
                GameObject.Find("InventoryManager").GetComponent<InvManagerSelect>().addChar(EmperorCrimson);
                break;
        }
    }

    public void switchRem(int i)
    {
        switch (i)
        {
            case 0:
                GameObject.Find("InventoryManager").GetComponent<InvManagerSelect>().removeChar(AeroSmith);
                break;
            case 1:
                GameObject.Find("InventoryManager").GetComponent<InvManagerSelect>().removeChar(SixBullets);
                break;
            case 2:
                GameObject.Find("InventoryManager").GetComponent<InvManagerSelect>().removeChar(Zipperman);
                break;
            case 3:
                GameObject.Find("InventoryManager").GetComponent<InvManagerSelect>().removeChar(SilverChariot);
                break;
            case 4:
                GameObject.Find("InventoryManager").GetComponent<InvManagerSelect>().removeChar(StarPlatinum);
                break;
            case 5:
                GameObject.Find("InventoryManager").GetComponent<InvManagerSelect>().removeChar(GoldenWind);
                break;
            case 6:
                GameObject.Find("InventoryManager").GetComponent<InvManagerSelect>().removeChar(PurpleSmoke);
                break;
            case 7:
                GameObject.Find("InventoryManager").GetComponent<InvManagerSelect>().removeChar(Reverb);
                break;
            case 8:
                GameObject.Find("InventoryManager").GetComponent<InvManagerSelect>().removeChar(EmperorCrimson);
                break;
        }
    }
    public void closeInfo()
    {
        infoPic.enabled = false;
    }
}
