using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

using System.Collections.Generic;

public class gachaManager : MonoBehaviour {

    public Image prize;
    //public List<Sprite> spriteList = new List<Sprite>();
    private List<Sprite> spriteList2 = new List<Sprite>();

    private Image gachaHandle;
    private Image gachaCapsules;
    private Image gachaPrize;

    private Image prizeRays;
    private Image prizeWhite;
    private Image prizeImage;
    private int prizeNum = 0;

    private Image background1;
    private Image background2;

    private bool prizeDisplay = true;
    public float fadeSpeed = 0.1f;

    private int frameNum = 0;
    private bool playAnim = false;

    public int currentMoney;
    public Text moneyText;

        // Use this for initialization
        void Start () {
        gachaHandle = GameObject.Find("gacha_machineHandle").GetComponent<Image>();
        gachaHandle.GetComponent<Animator>().speed = 1.0f;

        gachaCapsules = GameObject.Find("gacha_machineCapsule").GetComponent<Image>();
        gachaCapsules.GetComponent<Animator>().speed = 1.0f;

        gachaPrize = GameObject.Find("gacha_machinePrize").GetComponent<Image>();
        gachaPrize.GetComponent<Animator>().speed = 1.0f;

        prizeRays = GameObject.Find("gacha_prizeRays").GetComponent<Image>();
        prizeRays.GetComponent<Animator>().speed = 0.01f;

        prizeWhite = GameObject.Find("gacha_prizeWhite").GetComponent<Image>();

        prizeImage = GameObject.Find("gacha_prizeItem").GetComponent<Image>();

        background1 = GameObject.Find("gacha_background (1)").GetComponent<Image>();
        background2 = GameObject.Find("gacha_background (2)").GetComponent<Image>();
        background1.GetComponent<Animator>().speed = 0.01f;
        background2.GetComponent<Animator>().speed = 0.01f;

        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("ItemTemp");
        foreach (GameObject obj in allObjects)
        {
            spriteList2.Add(obj.GetComponent<Image>().sprite);
        }

        GameObject[] allObjects2 = GameObject.FindGameObjectsWithTag("WeaponTemp");
        foreach (GameObject obj in allObjects2)
        {
            spriteList2.Add(obj.GetComponent<Image>().sprite);
        }

        GameObject[] allObjects3 = GameObject.FindGameObjectsWithTag("HelmetTemp");
        foreach (GameObject obj in allObjects3)
        {
            spriteList2.Add(obj.GetComponent<Image>().sprite);
        }

        //PlayerPrefs.SetInt("CurrentMoney", 50000);
        if (PlayerPrefs.HasKey("CurrentMoney"))
        {
            currentMoney = PlayerPrefs.GetInt("CurrentMoney");
        }
        else
        {
            currentMoney = 50000;
            PlayerPrefs.SetInt("CurrentMoney", 50000);

        }
        moneyText.text = "\u00A5" + currentMoney;
   }
	
	// Update is called once per frame
	void Update () {

        if (playAnim == true)
        {
            frameNum++;
            if(frameNum >= 60)
            {
                gachaHandle.GetComponent<Animator>().SetTrigger("StopGacha");
                gachaCapsules.GetComponent<Animator>().SetTrigger("StopGacha");
                gachaPrize.GetComponent<Animator>().SetTrigger("StopGacha");
                prizeNum = (int)Random.Range(0.0f, spriteList2.Count);
                //Debug.Log(prizeNum);
                //prizeImage.sprite = spriteList[prizeNum];
                prizeImage.sprite = spriteList2[prizeNum];
                string discardedItem = prizeImage.GetComponent<Image>().sprite.name.Replace("item_", "");
                PersistentData.m_Instance.ItemList.Add(discardedItem);
                frameNum = 0;
                playAnim = false;
                prizeDisplay = true;
            }
        }

        if(Input.GetKeyDown(KeyCode.Space) && playAnim == false && prizeDisplay == false)
        {
            gachaHandle.GetComponent<Animator>().SetTrigger("PlayGacha");
            gachaCapsules.GetComponent<Animator>().SetTrigger("PlayGacha");
            gachaPrize.GetComponent<Animator>().SetTrigger("PlayGacha");
            playAnim = true;
            insertCoin();
        }

        if (prizeDisplay == true)
        {
            if(prizeWhite.color.a < 1.0f)
            {
                prizeWhite.color = new Color(1, 1, 1, prizeWhite.color.a + fadeSpeed);
            }

            if(prizeRays.color.a < 1.0f)
            {
                prizeRays.color = new Color(1, 1, 1, prizeRays.color.a + fadeSpeed);
            }
            if (prizeRays.transform.localScale.x < 16.12046f)
            {
                prizeRays.transform.localScale *= 1.1f;
            }

            if (prizeImage.color.a < 1.0f)
            {
                prizeImage.color = new Color(1, 1, 1, prizeImage.color.a + fadeSpeed);
            }
            if (prizeImage.transform.localScale.x < 5.355362f)
            {
                prizeImage.transform.localScale *= 1.1f;
            }
        }
        else if (prizeDisplay == false)
        {
            if (prizeWhite.color.a > 0.0f)
            {
                prizeWhite.color = new Color(1, 1, 1, prizeWhite.color.a - fadeSpeed);
            }

            if (prizeRays.color.a > 0.0f)
            {
                prizeRays.color = new Color(1, 1, 1, prizeRays.color.a - fadeSpeed);
                if (prizeRays.transform.localScale.x > 0.1f)
                {
                    prizeRays.transform.localScale *= 0.9f;
                }
            }

            if (prizeImage.color.a > 0.0f)
            {
                prizeImage.color = new Color(1, 1, 1, prizeImage.color.a - (fadeSpeed * 0.9f));
                if (prizeImage.transform.localScale.x > 0.1f)
                {
                    prizeImage.transform.localScale *= 0.95f;
                }
            }
        }

        if (prizeWhite.color.a <= 0.0f)
        {
            prizeWhite.transform.localPosition = new Vector3(9999, 9999, 999);
        }
        else
        {
            prizeWhite.transform.localPosition = new Vector3(0, 0, 0);
        }

        if (prizeRays.color.a <= 0.0f)
        {
            prizeRays.transform.localPosition = new Vector3(9999, 9999, 999);
        }
        else
        {
            prizeRays.transform.localPosition = new Vector3(0, 0, 0);
        }

        if (prizeImage.color.a <= 0.0f)
        {
            prizeImage.transform.localPosition = new Vector3(9999, 9999, 999);
        }
        else
        {
            prizeImage.transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    public void generatePrize()
    {
        prizeDisplay = true;
    }

    public void closePrize()
    {
        prizeDisplay = false;
    }

    public void insertCoin()
    {
        if (playAnim == false && prizeDisplay == false)
        {
            if (currentMoney - 200 < 0)
            {
                Debug.Log("Dont have enough money");
            }
            else
            {
                Debug.Log("Have enough money");

                currentMoney -= 200;
                PlayerPrefs.SetInt("CurrentMoney", currentMoney);
                moneyText.text = "\u00A5" + currentMoney;

                gachaHandle.GetComponent<Animator>().SetTrigger("PlayGacha");
                gachaCapsules.GetComponent<Animator>().SetTrigger("PlayGacha");
                gachaPrize.GetComponent<Animator>().SetTrigger("PlayGacha");
                playAnim = true;
            }
        }
    }

    public void backBtn(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
