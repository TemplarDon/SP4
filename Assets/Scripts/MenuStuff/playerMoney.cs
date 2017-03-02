using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerMoney : MonoBehaviour {
	public Image boughtImage;
	public Image boughtImage2;
	public Image boughtImage3;
	public Image boughtImage4;
	public Image boughtImage5;
	public Image boughtImage6;
	public Image boughtImage7;
	public Image boughtImage8;
	public Image boughtImage9;
	public Image buyButton;
	public Image buyButton2;
	public Image buyButton3;
	public Image buyButton4;
	public Image buyButton5;
	public Image buyButton6;
	public Image buyButton7;
	public Image buyButton8;
	public Image buyButton9;

    public int currentMoney;
    public Text moneyText;

    public Image tut2;
    public Image tut2ToGachaButton;
    public Text tut2ToGachaText;
	// Use this for initialization
	void Start () 
    {
        //PlayerPrefs.SetInt("CurrentMoney", 50000);
       // if (PlayerPrefs.HasKey("CurrentMoney"))
        //{
        //    currentMoney = PlayerPrefs.GetInt("CurrentMoney");
        //}
        //else
        //{
        //    currentMoney = 50000;
        //    PlayerPrefs.SetInt("CurrentMoney", 50000);

        //}
        //moneyText.text = "\u00A5" + currentMoney;
        moneyText.text = "\u00A5" + PersistentData.m_Instance.PlayerMoney;

        if (PersistentData.m_Instance.PlayerMoney < 50000)
        {
            buyButton.enabled = false;
            buyButton2.enabled = false;
            buyButton3.enabled = false;
            buyButton4.enabled = false;
            buyButton5.enabled = false;
            buyButton6.enabled = false;
            buyButton7.enabled = false;
            buyButton8.enabled = false;
            buyButton9.enabled = false;
        }
        if (PersistentData.m_Instance.PlayerMoney < 100000)
        {
            buyButton5.enabled = false;
            buyButton6.enabled = false;
            buyButton7.enabled = false;
            buyButton8.enabled = false;
            buyButton9.enabled = false;
        }
        if (PersistentData.m_Instance.PlayerMoney < 200000)
        {
            buyButton9.enabled = false;
        }
        for (int i = 0; i < PersistentData.m_Instance.CharacterList.Count; ++i)
        {
            if (PersistentData.m_Instance.CharacterList[i].Equals("AeroSmith"))
            {
                boughtImage.enabled = true;
                buyButton.enabled = false;

            }
            if (PersistentData.m_Instance.CharacterList[i].Equals("SixBullets"))
            {
                boughtImage2.enabled = true;
                buyButton2.enabled = false;
            }
            if (PersistentData.m_Instance.CharacterList[i].Equals("Zipperman"))
            {
                boughtImage3.enabled = true;
                buyButton3.enabled = false;
            }
            if (PersistentData.m_Instance.CharacterList[i].Equals("SilverChariot"))
            {
                boughtImage4.enabled = true;
                buyButton4.enabled = false;
            }
            if (PersistentData.m_Instance.CharacterList[i].Equals("StarPlatinum"))
            {
                boughtImage5.enabled = true;
                buyButton5.enabled = false;
            }
            if (PersistentData.m_Instance.CharacterList[i].Equals("GoldenWind"))
            {
                boughtImage6.enabled = true;
                buyButton6.enabled = false;
            }
            if (PersistentData.m_Instance.CharacterList[i].Equals("PurpleSmoke"))
            {
                boughtImage7.enabled = true;
                buyButton7.enabled = false;
            }
            if (PersistentData.m_Instance.CharacterList[i].Equals("Reverb"))
            {
                boughtImage8.enabled = true;
                buyButton8.enabled = false;
            }
            if (PersistentData.m_Instance.CharacterList[i].Equals("EmperorCrimson"))
            {
                boughtImage9.enabled = true;
                buyButton9.enabled = false;
            }
        }


        //if (boughtImage)
        //{
        //    boughtImage.enabled = PlayerPrefs.HasKey("BoughtChar1");
        //}
        //if (boughtImage2)
        //{
        //    boughtImage2.enabled = PlayerPrefs.HasKey("BoughtChar2");
        //}
        //if (boughtImage3)
        //{
        //    boughtImage3.enabled = PlayerPrefs.HasKey("BoughtChar3");
        //}
        //if (boughtImage4)
        //{
        //    boughtImage4.enabled = PlayerPrefs.HasKey("BoughtChar4");
        //}
        //if (boughtImage5)
        //{
        //    boughtImage5.enabled = PlayerPrefs.HasKey("BoughtChar5");
        //}
        //if (boughtImage6)
        //{
        //    boughtImage6.enabled = PlayerPrefs.HasKey("BoughtChar6");
        //}
        //if (boughtImage7)
        //{
        //    boughtImage7.enabled = PlayerPrefs.HasKey("BoughtChar7");
        //}
        //if (boughtImage8)
        //{
        //    boughtImage8.enabled = PlayerPrefs.HasKey("BoughtChar8");
        //}
        //if (boughtImage9)
        //{
        //    boughtImage9.enabled = PlayerPrefs.HasKey("BoughtChar9");
        //}
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyUp(KeyCode.X))
        {
            PlayerPrefs.DeleteAll();
        }
	}
    public void addMoney(int moneyToAdd)
    {
        PersistentData.m_Instance.PlayerMoney += moneyToAdd;
        //currentMoney += moneyToAdd;
        //PlayerPrefs.SetInt("CurrentMoney",currentMoney);
        moneyText.text = "\u00A5" + PersistentData.m_Instance.PlayerMoney;
    }
    public void subtractMoney(int moneyToSubtract)
    {
        if (PersistentData.m_Instance.PlayerMoney - moneyToSubtract < 0)
        {
            Debug.Log("Dont have enough money");
        }
        else
        {
            Debug.Log("Have enough money");

            //currentMoney -= moneyToSubtract;
            PersistentData.m_Instance.PlayerMoney -= moneyToSubtract;
            //PlayerPrefs.SetInt("CurrentMoney", currentMoney);
            moneyText.text = "\u00A5" + PersistentData.m_Instance.PlayerMoney;
        }
    }

    public void BoughtImage()
    {
        if (PersistentData.m_Instance.PlayerMoney >= 50000)
        {
            //PlayerPrefs.SetString("BoughtChar1", "AeroSmith");
            boughtImage.enabled = true;
            buyButton.enabled =false;

            subtractMoney(50000);
            PersistentData.m_Instance.CharacterList.Add("AeroSmith");
            PersistentData.m_Instance.firstTimeAtModeSelect = false;

            if (PersistentData.m_Instance.firstTimeAtShop == true)
            {
                tut2.enabled = true;
                tut2ToGachaButton.enabled = true;
                tut2ToGachaText.enabled = true;
            }
            if (PersistentData.m_Instance.firstTimeAtShop == false)
            {
                tut2.enabled = false;
                tut2ToGachaButton.enabled = false;
                tut2ToGachaText.enabled = false;
            }
        }
    }

	public void BoughtImage2()
	{
        if (PersistentData.m_Instance.PlayerMoney >= 50000)
        {
            //PlayerPrefs.SetString("BoughtChar2", "SixBullets");
            boughtImage2.enabled = true;
            buyButton2.enabled = false;

            subtractMoney(50000);
            PersistentData.m_Instance.CharacterList.Add("SixBullets");
            PersistentData.m_Instance.firstTimeAtModeSelect = false;

            if (PersistentData.m_Instance.firstTimeAtShop == true)
            {
                tut2.enabled = true;
                tut2ToGachaButton.enabled = true;
                tut2ToGachaText.enabled = true;
            }
            if (PersistentData.m_Instance.firstTimeAtShop == false)
            {
                tut2.enabled = false;
                tut2ToGachaButton.enabled = false;
                tut2ToGachaText.enabled = false;
            }
        }
	}
	public void BoughtImage3()
	{
        if (PersistentData.m_Instance.PlayerMoney >= 50000)
        {
            //PlayerPrefs.SetString("BoughtChar3", "Zipperman");
            boughtImage3.enabled = true;
            buyButton3.enabled = false;

            subtractMoney(50000);
            PersistentData.m_Instance.CharacterList.Add("Zipperman");
            PersistentData.m_Instance.firstTimeAtModeSelect = false;

            if (PersistentData.m_Instance.firstTimeAtShop == true)
            {
                tut2.enabled = true;
                tut2ToGachaButton.enabled = true;
                tut2ToGachaText.enabled = true;
            }
            if (PersistentData.m_Instance.firstTimeAtShop == false)
            {
                tut2.enabled = false;
                tut2ToGachaButton.enabled = false;
                tut2ToGachaText.enabled = false;
            }
        }
	}
	public void BoughtImage4()
	{
        if (PersistentData.m_Instance.PlayerMoney >= 50000)
        {
           // PlayerPrefs.SetString("BoughtChar4", "SliverChariot");
            boughtImage4.enabled = true;
            buyButton4.enabled = false;

            subtractMoney(50000);
            PersistentData.m_Instance.CharacterList.Add("SilverChariot");
            PersistentData.m_Instance.firstTimeAtModeSelect = false;

            if (PersistentData.m_Instance.firstTimeAtShop == true)
            {
                tut2.enabled = true;
                tut2ToGachaButton.enabled = true;
                tut2ToGachaText.enabled = true;
            }
            if (PersistentData.m_Instance.firstTimeAtShop == false)
            {
                tut2.enabled = false;
                tut2ToGachaButton.enabled = false;
                tut2ToGachaText.enabled = false;
            }
        }
	}
	public void BoughtImage5()
	{
        if (PersistentData.m_Instance.PlayerMoney >= 100000)
        {
            //PlayerPrefs.SetString("BoughtChar5", "StarPlatinum");
            boughtImage5.enabled = true;
            buyButton5.enabled = false;

            subtractMoney(100000);
            PersistentData.m_Instance.CharacterList.Add("StarPlatinum");
        }
	}
	public void BoughtImage6()
	{
        if (PersistentData.m_Instance.PlayerMoney >= 100000)
        {
            //PlayerPrefs.SetString("BoughtChar6", "GoldenWind");
            boughtImage6.enabled = true;
            buyButton6.enabled = false;

            subtractMoney(100000);
            PersistentData.m_Instance.CharacterList.Add("GoldenWind");
        }
	}
	public void BoughtImage7()
	{
        if (PersistentData.m_Instance.PlayerMoney >= 100000)
        {
            //PlayerPrefs.SetString("BoughtChar7", "PurpleSmoke");
            boughtImage7.enabled = true;
            buyButton7.enabled = false;

            subtractMoney(100000);
            PersistentData.m_Instance.CharacterList.Add("PurpleSmoke");
        }
	}
	public void BoughtImage8()
	{
        if (PersistentData.m_Instance.PlayerMoney >= 100000)
        {
            //PlayerPrefs.SetString("BoughtChar8", "Reverb");
            boughtImage8.enabled = true;
            buyButton8.enabled = false;

            subtractMoney(100000);
            PersistentData.m_Instance.CharacterList.Add("Reverb");
        }
		
	}
	public void BoughtImage9()
	{
        if (PersistentData.m_Instance.PlayerMoney >= 200000)
        {
            //PlayerPrefs.SetString("BoughtChar9", "EmperorCrimson");
            boughtImage9.enabled = true;
            buyButton9.enabled = false;

            subtractMoney(200000);
            PersistentData.m_Instance.CharacterList.Add("EmperorCrimson");
        }
	}
}
