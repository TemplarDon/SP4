using UnityEngine;
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
	}
	
	// Update is called once per frame
	void Update () 
    {

	}
    public void tickImage()
    {
        if (tick[0].enabled == false)
            tick[0].enabled = true;
        else
            tick[0].enabled = false;
    }
    public void tickImage1()
    {
        if (tick[1].enabled == false)
            tick[1].enabled = true;
        else
            tick[1].enabled = false;
    }
    public void tickImage2()
    {
        if (tick[2].enabled == false)
            tick[2].enabled = true;
        else
            tick[2].enabled = false;
    }
    public void tickImage3()
    {
        if (tick[3].enabled == false)
            tick[3].enabled = true;
        else
            tick[3].enabled = false;
    }
    public void tickImage4()
    {
        if (tick[4].enabled == false)
            tick[4].enabled = true;
        else
            tick[4].enabled = false;
    }
    public void tickImage5()
    {
        if (tick[5].enabled == false)
            tick[5].enabled = true;
        else
            tick[5].enabled = false;
    }
    public void tickImage6()
    {
        if (tick[6].enabled == false)
            tick[6].enabled = true;
        else
            tick[6].enabled = false;
    }
    public void tickImage7()
    {
        if (tick[7].enabled == false)
            tick[7].enabled = true;
        else
            tick[7].enabled = false;
    }
    public void tickImage8()
    {
        if (tick[8].enabled == false)
            tick[8].enabled = true;
        else
            tick[8].enabled = false;
    }
}
