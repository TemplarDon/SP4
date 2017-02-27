using UnityEngine;
using System.Collections;

public class Modifier {

    public enum MODIFY_TYPE
    {
        HEALTH,
        ATTACK,
        MAGIC,
        RANGE,
        SPEED,
        ARMOUR,
    }

    public bool b_Active = false;
    public MODIFY_TYPE m_Type;
    public int i_ModifierAmount;
    public int i_Lifetime;

	public void Init (MODIFY_TYPE type, int amt, int lifetime) {

        m_Type = type;
        i_ModifierAmount = amt;
        i_Lifetime = lifetime;

        b_Active = true;

	}
	
	public void Update () {

        i_Lifetime -= 1;
        
        if (i_Lifetime < 0)
        {
            b_Active = false;
        }

	}
}
