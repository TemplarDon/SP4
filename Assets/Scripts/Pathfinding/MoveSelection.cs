using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using System.Collections.Generic;

public class MoveSelection : MonoBehaviour {

    //public Text dmgIndicator;
    public List<Text> dmgList = new List<Text>();
    //private Camera camera;

    // Use this for initialization
    void Start () {
        //dmgIndicator = GameObject.Find("DmgIndicator").GetComponent<Text>();
        //camera = GameObject.Find("Main Camera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        //dmgIndicator.transform.Translate(0, 0.1f, 0);
        //
        //dmgIndicator.color = new Color(dmgIndicator.color.r, dmgIndicator.color.g, dmgIndicator.color.b, dmgIndicator.color.a - (Time.deltaTime * 0.2f));
        //
        //if (dmgIndicator.color.a <= 0.0f)
        //{
        //    dmgIndicator.transform.position = new Vector3(9999, 9999, 9999);
        //}

    }

    void OnMouseDown()
    {
        switch(tag)
        {
            case "yellowSq":
                if (GameObject.Find("Controller").GetComponent<CharacterController>().CurrentMode == CharacterController.CONTROL_MODE.TARGET)
                {
                    (GameObject.Find("Controller").GetComponent<CharacterController>().CurrentControlledCharacter.GetComponent<BaseCharacter>().theSkill as BrunoSkill).SetTargetedPosition(this.transform.position);
                    GameObject.Find("Controller").GetComponent<CharacterController>().CurrentControlledCharacter.GetComponent<BaseCharacter>().UseSkill();
                }
                else
                {
                    // Set Controller to allow character to move
                    GameObject.Find("Controller").GetComponent<CharacterController>().SetCanMove(true);
                    GameObject.Find("TurnManager").GetComponent<turnManage>().cancelAction = false;
                }
                //Debug.Log("Set CanMove to true");
                break;
            case "redSq":
                bool playerPresent = false;

                GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Character");
                foreach (GameObject obj in allObjects)
                {
                    if ((int)obj.transform.position.x == (int)transform.position.x && (int)obj.transform.position.y == (int)transform.position.y)
                    {
                        if (GameObject.Find("Controller").GetComponent<CharacterController>().CurrentMode == CharacterController.CONTROL_MODE.TARGET)
                        {
                            if (GameObject.Find("Controller").GetComponent<CharacterController>().CurrentControlledCharacter.name == "Koichi")
                                (GameObject.Find("Controller").GetComponent<CharacterController>().CurrentControlledCharacter.GetComponent<BaseCharacter>().theSkill as KoichiSkill).SetTargetedObject(obj);
                            else if (GameObject.Find("Controller").GetComponent<CharacterController>().CurrentControlledCharacter.name == "Mista")
                                (GameObject.Find("Controller").GetComponent<CharacterController>().CurrentControlledCharacter.GetComponent<BaseCharacter>().theSkill as MistaSkill).SetTargetedObject(obj);

                            GameObject.Find("Controller").GetComponent<CharacterController>().CurrentControlledCharacter.GetComponent<BaseCharacter>().UseSkill();
                        }
                        else
                        {
                            //int damageDealt = (int)(Mathf.Clamp(GameObject.Find("Controller").GetComponent<CharacterController>().CurrentControlledCharacter.GetComponent<BaseCharacter>().BaseStrength - obj.GetComponent<BaseCharacter>().BaseArmour, 1.0f, 999.0f));
                            obj.GetComponent<BaseCharacter>().TakeDamage(GameObject.Find("Controller").GetComponent<CharacterController>().CurrentControlledCharacter.GetComponent<BaseCharacter>().GetAttackDamage());

                            //GameObject.Find("EffectsSoundPlayer").GetComponent<SoundManager>().PlaySound("MeleeAttack");
                            PersistentSoundManager.m_Instance.PlaySoundEffect("MeleeAttack");

                            playerPresent = true;
                            //GameObject.Find("DmgIndicator").GetComponent<dmgDisp>().dispAtk(damageDealt, obj.transform.position);

                            int attacker_x = Mathf.RoundToInt(GameObject.Find("Controller").GetComponent<CharacterController>().CurrentControlledCharacter.GetComponent<BaseCharacter>().pos.x);
                            int attacker_y = Mathf.RoundToInt(GameObject.Find("Controller").GetComponent<CharacterController>().CurrentControlledCharacter.GetComponent<BaseCharacter>().pos.y);

                            int reciever_x = Mathf.RoundToInt(obj.GetComponent<BaseCharacter>().pos.x);
                            int reciever_y = Mathf.RoundToInt(obj.GetComponent<BaseCharacter>().pos.y);


                            if (reciever_x < attacker_x && reciever_y == attacker_y)
                            {
                                //this.GetComponent<Animator>().Play("CharacterAnimationLeft");
                                GameObject.Find("Controller").GetComponent<CharacterController>().CurrentControlledCharacter.GetComponent<BaseCharacter>().CurrentAnimState = BaseCharacter.ANIM_STATE.ATTACK_LEFT;
                            }
                            else if (reciever_x > attacker_x && reciever_y == attacker_y)
                            {
                                //this.GetComponent<Animator>().Play("CharacterAnimationRight");
                                GameObject.Find("Controller").GetComponent<CharacterController>().CurrentControlledCharacter.GetComponent<BaseCharacter>().CurrentAnimState = BaseCharacter.ANIM_STATE.ATTACK_RIGHT;
                            }
                            else if (reciever_y < attacker_y && reciever_x == attacker_x)
                            {
                                //this.GetComponent<Animator>().Play("CharacterAnimationDown");
                                GameObject.Find("Controller").GetComponent<CharacterController>().CurrentControlledCharacter.GetComponent<BaseCharacter>().CurrentAnimState = BaseCharacter.ANIM_STATE.ATTACK_DOWN;
                            }
                            else if (reciever_y > attacker_y && reciever_x == attacker_x)
                            {
                                //this.GetComponent<Animator>().Play("CharacterAnimationUp");
                                GameObject.Find("Controller").GetComponent<CharacterController>().CurrentControlledCharacter.GetComponent<BaseCharacter>().CurrentAnimState = BaseCharacter.ANIM_STATE.ATTACK_UP;
                            }
                        }
                    }
                }

                if(playerPresent == true)
                {
                    GameObject.Find("Controller").GetComponent<CharacterController>().CurrentControlledCharacter.GetComponent<BaseCharacter>().restrictActions[0] = true;
                    GameObject.Find("Controller").GetComponent<CharacterController>().CurrentControlledCharacter.GetComponent<BaseCharacter>().restrictActions[1] = true;
                    GameObject.Find("Controller").GetComponent<CharacterController>().CurrentControlledCharacter.GetComponent<BaseCharacter>().restrictActions[2] = true;
                    GameObject.Find("Controller").GetComponent<CharacterController>().CurrentControlledCharacter.GetComponent<BaseCharacter>().restrictActions[3] = true;
                    GameObject.Find("Controller").GetComponent<CharacterController>().CurrentControlledCharacter.GetComponent<BaseCharacter>().restrictActions[4] = true;
                }
                GameObject.Find("Controller").GetComponent<CharacterController>().CurrentMode = CharacterController.CONTROL_MODE.FREE_ROAM;
                GameObject.Find("TurnManager").GetComponent<turnManage>().menuOpen = false;
                GameObject.Find("TurnManager").GetComponent<turnManage>().cancelAction = true;
                break;
        }
    }

    void OnMouseOver()
    {
        GameObject.Find("TurnManager").GetComponent<turnManage>().cancelAction = false;
    }
}
