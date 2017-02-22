using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using System.Collections.Generic;

public class gachaManager : MonoBehaviour {

    public Image prize;
    public List<Sprite> spriteList = new List<Sprite>();

    private Image prizeRays;
    private Image prizeWhite;

    private bool prizeDisplay = true;
    public float fadeSpeed = 0.1f;

    // Use this for initialization
    void Start () {
        prizeRays = GameObject.Find("gacha_prizeRays").GetComponent<Image>();
        prizeRays.GetComponent<Animator>().speed = 0.01f;

        prizeWhite = GameObject.Find("gacha_prizeWhite").GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {

        if (prizeDisplay == true)
        {
            if(prizeWhite.color.a < 1.0f)
            {
                prizeWhite.color = new Color(1, 1, 1, prizeWhite.color.a + fadeSpeed);
            }

            if(prizeRays.color.a < 1.0f)
            {
                prizeWhite.color = new Color(1, 1, 1, prizeWhite.color.a + fadeSpeed);
                if (prizeRays.transform.localScale.x > 0.1f)
                {
                    prizeRays.transform.localScale *= 1.1f;
                }
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
                prizeWhite.color = new Color(1, 1, 1, prizeWhite.color.a - fadeSpeed);
                if (prizeRays.transform.localScale.x > 0.1f)
                {
                    prizeRays.transform.localScale *= 0.9f;
                }
            }
        }

        if (prizeWhite.color.a <= 0.0f)
        {
            prizeWhite.transform.localPosition = new Vector3(999, 999, 999);
        }
        else
        {
            prizeWhite.transform.localPosition = new Vector3(0, 0, 0);
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
}
