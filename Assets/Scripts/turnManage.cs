using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class turnManage : MonoBehaviour {

    public int turnNumber;
    public Text turnNum;
    public int actionSelection;
    public Text nameDisplay;
    public GameObject menuObject;
    public GameObject menuArrow;
    public TestingPlayer charac;
    public Camera camera2;
    public bool menuOpen;
    public GameObject shieldIcon;
    private Vector3 shieldSize;
    private bool shieldFade;

	// Use this for initialization
	void Start () {
        turnNum.text = 1.ToString();
        nameDisplay.text = charac.GetComponent<BaseCharacter>().Name;
        //camera = GetComponent<Camera>();
        actionSelection = 1;
        menuOpen = true;
        shieldSize = shieldIcon.transform.position;
        shieldFade = false;
    }
	
	// Update is called once per frame
	void Update () {
       turnNum.text = turnNumber.ToString();

        switch(actionSelection)
        {
            case 1:
                if(menuArrow.transform.localPosition.y > 135 + 4)
                {
                    menuArrow.transform.localPosition += new Vector3(0, -11, 0);
                }
                else if (menuArrow.transform.localPosition.y < 135 - 4)
                {
                    menuArrow.transform.localPosition += new Vector3(0, 11, 0);
                }
                else
                {
                    menuArrow.transform.localPosition = new Vector3(-267, 135, 0);
                }
                break;
            case 2:
                if (menuArrow.transform.localPosition.y > 60 + 4)
                {
                    menuArrow.transform.localPosition += new Vector3(0, -11, 0);
                }
                else if (menuArrow.transform.localPosition.y < 60 - 4)
                {
                    menuArrow.transform.localPosition += new Vector3(0, 11, 0);
                }
                else
                {
                    menuArrow.transform.localPosition = new Vector3(-267, 60, 0);
                }
                
                break;
            case 3:
                if (menuArrow.transform.localPosition.y > -19 + 4)
                {
                    menuArrow.transform.localPosition += new Vector3(0, -11, 0);
                }
                else if (menuArrow.transform.localPosition.y < -19 - 4)
                {
                    menuArrow.transform.localPosition += new Vector3(0, 11, 0);
                }
                else
                {
                    menuArrow.transform.localPosition = new Vector3(-267, -19, 0);
                }
                break;
            case 4:
                if (menuArrow.transform.localPosition.y > -96 + 4)
                {
                    menuArrow.transform.localPosition += new Vector3(0, -11, 0);
                }
                else if (menuArrow.transform.localPosition.y < -96 - 4)
                {
                    menuArrow.transform.localPosition += new Vector3(0, 11, 0);
                }
                else
                {
                    menuArrow.transform.localPosition = new Vector3(-267, -96, 0);
                }
                break;
            case 5:
                if (menuArrow.transform.localPosition.y > -173 + 4)
                {
                    menuArrow.transform.localPosition += new Vector3(0, -11, 0);
                }
                else if (menuArrow.transform.localPosition.y < -173 - 4)
                {
                    menuArrow.transform.localPosition += new Vector3(0, 11, 0);
                }
                else
                {
                    menuArrow.transform.localPosition = new Vector3(-267, -173, 0);
                }
                break;
        }

        if (menuOpen == true)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                switch (actionSelection)
                {
                    case 1:
                        menuObject.transform.position = new Vector3(-9999, -9999, 0);
                        menuOpen = false;
                        actionSelection = 1;
                        break;
                    case 2:
                        menuObject.transform.position = new Vector3(-9999, -9999, 0);
                        menuOpen = false;
                        actionSelection = 1;
                        break;
                    case 3:
                        menuObject.transform.position = new Vector3(-9999, -9999, 0);
                        menuOpen = false;
                        shieldFade = true;
                        actionSelection = 1;
                        break;
                    case 4:
                        //menuObject.transform.position = new Vector3(-9999, -9999, 0);
                        actionSelection = 1;
                        break;
                    case 5:
                        //menuObject.transform.position = new Vector3(-9999, -9999, 0);
                        actionSelection = 1;
                        break;
                }
            }

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
            {
                if (actionSelection > 1)
                    actionSelection--;
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
            {
                if (actionSelection < 5)
                    actionSelection++;
            }
        }

        if (shieldFade == true)
        {
            shieldIcon.transform.position = new Vector3(charac.xpos, charac.ypos, -6);
            //FadeTextToZeroAlpha(1f, shieldIcon.GetComponent<SpriteRenderer>());
            shieldIcon.transform.localScale += new Vector3(0.001f, 0.001f, 0);

            shieldIcon.GetComponent<SpriteRenderer>().color = new Color(shieldIcon.GetComponent<SpriteRenderer>().color.r, shieldIcon.GetComponent<SpriteRenderer>().color.g, shieldIcon.GetComponent<SpriteRenderer>().color.b, shieldIcon.GetComponent<SpriteRenderer>().color.a - Time.deltaTime);

            if (shieldIcon.GetComponent<SpriteRenderer>().color.a <= 0.0f)
            {
                shieldFade = false;
                shieldIcon.GetComponent<SpriteRenderer>().color = new Color(shieldIcon.GetComponent<SpriteRenderer>().color.r, shieldIcon.GetComponent<SpriteRenderer>().color.g, shieldIcon.GetComponent<SpriteRenderer>().color.b, 1.0f);
                shieldIcon.transform.position = new Vector3(-999, -999, -6);
                shieldIcon.transform.localScale = new Vector3(0.07090571f, 0.07090571f, 0.07090571f );
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            menuOpen = true;
            actionSelection = 1;
        }

        if (menuOpen == true)
        {
            menuObject.transform.position = camera2.WorldToScreenPoint(new Vector3(charac.xpos + 2, charac.ypos, 0));
        }
    }

    public IEnumerator FadeTextToFullAlpha(float t, SpriteRenderer i)
    {
        Debug.Log("FADING");
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
        if (i.color.a >= 1.0f)
        {
            //SceneManager.LoadScene("Story_MainMenu");
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, SpriteRenderer i)
    {
        
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
        if(i.color.a <= 0.0f)
        {
            shieldFade = false;
            //i.color = new Color(i.color.r, i.color.g, i.color.b, 1.0f);
        }
    }
}
