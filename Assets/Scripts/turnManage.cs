﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class turnManage : MonoBehaviour {

    public int turnNumber;
    public Text turnNum;
    public int actionSelection;
    public Text nameDisplay;
    public Text chargeDisplay;
    public Text atkDisplay;
    public Text ranDisplay;
    public Text defDisplay;
    public Text heaDisplay;
    public Image profilePic;
    public GameObject menuObject;
    public GameObject menuArrow;
    public TestingPlayer charac;
    public BaseCharacter characNEW;
    public Camera camera2;
    public bool menuOpen;
    public GameObject shieldIcon;
    private bool shieldFade;

    public GameObject cancel1;
    public GameObject cancel2;
    public GameObject cancel3;
    public GameObject cancel4;
    public GameObject cancel5;

    public LevelGenerate map;

    public bool[] restrictions = new bool[5];

    private bool mouseOnMenu;
    public bool clickingNewChar;
    public bool cancelAction;

    public int teamTurn;
    public Image YourTurn;
    public Image EnemyTurn;
    private float timer;


    public teamManager playerTeam;
    public teamManager enemyTeam;

    public Sprite imgTmp;
    public bool animDelay = false;
    public int animTimer = 100;

    GameObject Commander;   // Handle to the enemy commander

	// Use this for initialization
	void Start () {
        turnNum.text = 1.ToString();
        //nameDisplay.text = charac.GetComponent<BaseCharacter>().Name;

        //characNEW = GameObject.Find("friendlyTeamManager").GetComponent<teamManager>().teamList[0];

        nameDisplay.text = characNEW.Name;
        chargeDisplay.text = characNEW.theSkill.ChargeCost.ToString();
        atkDisplay.text = characNEW.GetAttackDamage().ToString();
        ranDisplay.text = characNEW.GetAttackRange().ToString();
        defDisplay.text = characNEW.GetArmour().ToString();
        heaDisplay.text = characNEW.BaseHealth.ToString();
        profilePic.sprite = imgTmp;
        //camera = GetComponent<Camera>();
        actionSelection = 1;
        menuOpen = true;
        shieldFade = false;
        mouseOnMenu = false;
        clickingNewChar = false;
        cancelAction = true;
        teamTurn = 1;
        timer = 0.0f;

        for(int i = 0; i < 5; i++)
        {
            restrictions[i] = false;
        }

        YourTurn.transform.localPosition = new Vector3(0, 0, 0);
        YourTurn.fillAmount = 0.0f;
        //EnemyTurn.transform.localPosition = new Vector3(0, 0, 0);
        //EnemyTurn.fillAmount = 0.0f;

        Commander = GameObject.Find("EnemyCommander");
    }
	
	// Update is called once per frame
	void Update () {

        if (Commander == null)
        {
            Commander = GameObject.Find("EnemyCommander");
        }

        if(animDelay == true)
        {
            animTimer--;
            //Debug.Log(animTimer);
            if(animTimer <= 0)
            {
                animTimer = 100;
                animDelay = false;
            }
        }

        if(characNEW.profilePic != null)
        profilePic.sprite = characNEW.profilePic;

        if (!GameObject.Find("EndGameManager").GetComponent<EndGameManager>().GetGameEnded())
        {
            if (teamTurn >= 5)
            {
                teamTurn = 1;
            }
            if (teamTurn == 1 && animDelay == false)
            {
                YourTurn.transform.localPosition = new Vector3(0, 0, 0);
                timer += Mathf.PI / 180;
                YourTurn.fillAmount += 0.05f * Mathf.Cos(timer);
                enemyTeam.running = false;
                if (timer >= Mathf.PI)
                {
                    teamTurn = 2;
                    timer = 0.0f;
                    YourTurn.transform.localPosition = new Vector3(9999, 9999, 9999);
                    turnNumber++;
                    playerTeam.running = true;

                    if (turnNumber == 1)
                        playerTeam.resetStats = false;
                    else 
                        playerTeam.resetStats = true;
                        
                    foreach (BaseCharacter aCharacter in playerTeam.GetComponent<teamManager>().teamList)
                    {
                        aCharacter.UpdateModifiers();
                    }
                }
            }
            else if (teamTurn == 3 && animDelay == false)
            {
                EnemyTurn.transform.localPosition = new Vector3(0, 0, 0);
                timer += Mathf.PI / 180;
                EnemyTurn.fillAmount += 0.05f * Mathf.Cos(timer);
                playerTeam.running = false;
                if (timer >= Mathf.PI)
                {
                    teamTurn = 4;
                    timer = 0.0f;
                    EnemyTurn.transform.localPosition = new Vector3(9999, 9999, 9999);
                    turnNumber++;

                    enemyTeam.running = true;
                    enemyTeam.resetStats = true;

                    foreach (BaseCharacter aCharacter in enemyTeam.GetComponent<teamManager>().teamList)
                    {
                        aCharacter.UpdateModifiers();
                    }

                    Commander.GetComponent<CommanderFSM>().IncreaseTurnCount();

                }
            }
        }
        //Debug.Log(teamTurn);

       turnNum.text = turnNumber.ToString();
        nameDisplay.text = characNEW.Name;
        chargeDisplay.text = characNEW.theSkill.ChargeCost.ToString();
        atkDisplay.text = characNEW.GetAttackDamage().ToString();
        ranDisplay.text = characNEW.GetAttackRange().ToString();
        defDisplay.text = characNEW.GetArmour().ToString();
        heaDisplay.text = characNEW.BaseHealth.ToString();

        restrictions = characNEW.restrictActions;
        GameObject controller = GameObject.Find("Controller");

        switch (actionSelection)
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
            if(restrictions[0] == true)
            {
                cancel1.transform.localPosition = new Vector3(-3, 155, 0);
            }
            else
            {
                cancel1.transform.localPosition = new Vector3(9999, 155, 0);
            }

            if (restrictions[1] == true)
            {
                cancel2.transform.localPosition = new Vector3(-3, 77.5f, 0);
            }
            else
            {
                cancel2.transform.localPosition = new Vector3(9999, 77.5f, 0);
            }

            if (restrictions[2] == true)
            {
                cancel3.transform.localPosition = new Vector3(-3, 0, 0);
            }
            else
            {
                cancel3.transform.localPosition = new Vector3(9999, 0, 0);
            }

            if (restrictions[3] == true)
            {
                cancel4.transform.localPosition = new Vector3(-3, -77.5f, 0);
            }
            else
            {
                cancel4.transform.localPosition = new Vector3(9999, -77.5f, 0);
            }

            if (restrictions[4] == true)
            {
                cancel5.transform.localPosition = new Vector3(-3, -155, 0);
            }
            else
            {
                cancel5.transform.localPosition = new Vector3(9999, -155, 0);
            }

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                //characNEW.restrictActions[actionSelection - 1] = restrictions[actionSelection - 1] = true;
                if (restrictions[actionSelection - 1] == false)
                {
                    Debug.Log(actionSelection - 1);
                    switch (actionSelection)
                    {
                        case 1:
                            menuObject.transform.position = new Vector3(-9999, -9999, 0);
                            menuOpen = false;

                            // Change CONTROL_TYPE to SELECTION
                            controller.GetComponent<CharacterController>().CurrentMode = CharacterController.CONTROL_MODE.MOVING;
                            
                            break;
                        case 2:
                            menuObject.transform.position = new Vector3(-9999, -9999, 0);
                            menuOpen = false;
                            map.redGen = true;
                            controller.GetComponent<CharacterController>().CurrentMode = CharacterController.CONTROL_MODE.ATTACKING;
                            break;
                        case 3:
                            menuObject.transform.position = new Vector3(-9999, -9999, 0);
                            menuOpen = false;
                            shieldFade = true;
                            controller.GetComponent<CharacterController>().CurrentMode = CharacterController.CONTROL_MODE.FREE_ROAM;
                            GameObject.Find("Controller").GetComponent<CharacterController>().CurrentControlledCharacter.GetComponent<BaseCharacter>().BaseArmour++;
                            break;
                        case 4:
                            //menuObject.transform.position = new Vector3(-9999, -9999, 0);
                            controller.GetComponent<CharacterController>().CurrentMode = CharacterController.CONTROL_MODE.FREE_ROAM;
                            GameObject.Find("Controller").GetComponent<CharacterController>().CurrentControlledCharacter.GetComponent<BaseCharacter>().UseItem();
                            break;
                        case 5:
                            //menuObject.transform.position = new Vector3(-9999, -9999, 0);
                            controller.GetComponent<CharacterController>().CurrentMode = CharacterController.CONTROL_MODE.FREE_ROAM;
                            GameObject.Find("Controller").GetComponent<CharacterController>().CurrentControlledCharacter.GetComponent<BaseCharacter>().UseSkill();
                            //characNEW.BaseMana = characNEW.GetMaxMana();
                            characNEW.theSkill.ResetCharge();
                            break;
                    }

                    if (actionSelection > 2)
                    {
                        characNEW.restrictActions[0] = restrictions[0] = true;
                        characNEW.restrictActions[1] = restrictions[1] = true;
                        characNEW.restrictActions[2] = restrictions[2] = true;
                        characNEW.restrictActions[3] = restrictions[3] = true;
                        characNEW.restrictActions[4] = restrictions[4] = true;
                    }
                    else
                    {
                        //characNEW.restrictActions[0] = restrictions[0] = true;
                    }

                    actionSelection = 1;

                }
                else
                {
                    menuOpen = false;
                }

                
            }

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (actionSelection > 1)
                {
                    int nextOption = actionSelection;
                    while(nextOption >= 1)
                    {
                        nextOption--;
                        if(nextOption >= 1 && restrictions[nextOption - 1] == false)
                        {
                            actionSelection = nextOption;
                            break;
                        }
                    }
                }
                //actionSelection--;
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                //if (actionSelection < 5)
                //    actionSelection++;

                if (actionSelection < 5)
                {
                    int nextOption = actionSelection;
                    while (nextOption <= 5)
                    {
                        nextOption++;
                        if (nextOption <= 5 && restrictions[nextOption - 1] == false)
                        {
                            actionSelection = nextOption;
                            break;
                        }
                    }
                }
            }
        }

        if (shieldFade == true)
        {
            //shieldIcon.transform.position = new Vector3(charac.xpos, charac.ypos, -6);
            shieldIcon.transform.position = new Vector3(characNEW.pos.x, characNEW.pos.y, -6);
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

            // Change CONTROL_TYPE to SELECTION
            controller.GetComponent<CharacterController>().CurrentMode = CharacterController.CONTROL_MODE.SELECTION;
        }
        
        if (clickingNewChar == false && mouseOnMenu == false && menuOpen && Input.GetMouseButtonDown(0) && controller.GetComponent<CharacterController>().CurrentMode == CharacterController.CONTROL_MODE.SELECTION)
        {
            menuOpen = false;
        }

        if(controller.GetComponent<CharacterController>().CurrentMode == CharacterController.CONTROL_MODE.MOVING || controller.GetComponent<CharacterController>().CurrentMode == CharacterController.CONTROL_MODE.ATTACKING)
        {
            if(Input.GetMouseButtonDown(0) && cancelAction == true)
            {
                controller.GetComponent<CharacterController>().CurrentMode = CharacterController.CONTROL_MODE.FREE_ROAM;
            }
        }

        cancelAction = true;

        if (menuOpen == true)
        {
            controller.GetComponent<CharacterController>().CurrentMode = CharacterController.CONTROL_MODE.SELECTION;
            menuObject.transform.position = camera2.WorldToScreenPoint(new Vector3(characNEW.pos.x + 2, characNEW.pos.y, 0));
        }
        else
        {
            //controller.GetComponent<CharacterController>().CurrentMode = CharacterController.CONTROL_MODE.FREE_ROAM;
            menuObject.transform.position = new Vector3(-9999, -9999, 0);
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

    public void debugLogStuff()
    {
        Debug.Log("WOLOLOLO");
    }

    public void mouseHover(int hoveringLoc)
    {
        mouseOnMenu = true;
        actionSelection = hoveringLoc;
    }

    public void mouseLeave()
    {
        mouseOnMenu = false;
    }

    public void mouseClick(int clickLoc)
    {
        mouseOnMenu = true;
        actionSelection = clickLoc;

        if (restrictions[actionSelection - 1] == false)
        {
            GameObject controller = GameObject.Find("Controller");
            switch (actionSelection)
            {
                case 1:
                    menuObject.transform.position = new Vector3(-9999, -9999, 0);
                    menuOpen = false;

                    // Change CONTROL_TYPE to SELECTION

                    controller.GetComponent<CharacterController>().CurrentMode = CharacterController.CONTROL_MODE.MOVING;

                    break;
                case 2:
                    menuObject.transform.position = new Vector3(-9999, -9999, 0);
                    menuOpen = false;
                    map.redGen = true;
                    controller.GetComponent<CharacterController>().CurrentMode = CharacterController.CONTROL_MODE.ATTACKING;
                    break;
                case 3:
                    menuObject.transform.position = new Vector3(-9999, -9999, 0);
                    menuOpen = false;
                    shieldFade = true;
                    controller.GetComponent<CharacterController>().CurrentMode = CharacterController.CONTROL_MODE.FREE_ROAM;

                    Modifier toAdd = new Modifier();
                    toAdd.Init(Modifier.MODIFY_TYPE.ARMOUR, 1, 1);
                    GameObject.Find("Controller").GetComponent<CharacterController>().CurrentControlledCharacter.GetComponent<BaseCharacter>().AddModifier(toAdd);

                    //GameObject.Find("Controller").GetComponent<CharacterController>().CurrentControlledCharacter.GetComponent<BaseCharacter>().BaseArmour++;
                    break;
                case 4:
                    //menuObject.transform.position = new Vector3(-9999, -9999, 0);
                    controller.GetComponent<CharacterController>().CurrentMode = CharacterController.CONTROL_MODE.FREE_ROAM;
                    GameObject.Find("Controller").GetComponent<CharacterController>().CurrentControlledCharacter.GetComponent<BaseCharacter>().UseItem();
                    break;
                case 5:
                    //menuObject.transform.position = new Vector3(-9999, -9999, 0);
                    controller.GetComponent<CharacterController>().CurrentMode = CharacterController.CONTROL_MODE.FREE_ROAM;
                    GameObject.Find("Controller").GetComponent<CharacterController>().CurrentControlledCharacter.GetComponent<BaseCharacter>().UseSkill();
                    //characNEW.BaseMana = characNEW.GetMaxMana();
                    characNEW.theSkill.ResetCharge();
                    break;
            }

            if (actionSelection > 2)
            {
                characNEW.restrictActions[0] = restrictions[0] = true;
                characNEW.restrictActions[1] = restrictions[1] = true;
                characNEW.restrictActions[2] = restrictions[2] = true;
                characNEW.restrictActions[3] = restrictions[3] = true;
                characNEW.restrictActions[4] = restrictions[4] = true;
            }
            else
            {
                //characNEW.restrictActions[0] = restrictions[0] = true;
            }

            actionSelection = 1;


        }
        else
        {
            menuOpen = false;
        }

        
    }
}
