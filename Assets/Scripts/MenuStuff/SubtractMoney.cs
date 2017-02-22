using UnityEngine;
using System.Collections;

public class SubtractMoney : MonoBehaviour 
{
    public playerMoney PMoney;
	// Use this for initialization
	void Start () 
    {
        PMoney = FindObjectOfType<playerMoney>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        
	}
    public void buyItem(int value)
    {
        PMoney.subtractMoney(value);
    }
}
