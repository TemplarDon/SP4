using UnityEngine;
using System.Collections;

public class StandEyesStore : MonoBehaviour {

    public Sprite stand_giorno;
    public Sprite stand_bruno;
    public Sprite stand_narancia;
    public Sprite stand_fugo;
    public Sprite stand_mista;
    public Sprite stand_polneraff;
    public Sprite stand_diavolo;
    public Sprite stand_koichi;
    public Sprite stand_jotaro;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Sprite getSprite(string characterName)
    {
        switch(characterName)
        {
            case "Giorno":
                return stand_giorno;
            case "Bruno":
                return stand_bruno;
            case "Narancia":
                return stand_narancia;
            case "Fugo":
                return stand_fugo;
            case "Mista":
                return stand_mista;
            case "Polneraff":
                return stand_polneraff;
            case "Diavolo":
                return stand_diavolo;
            case "Koichi":
                return stand_koichi;
            case "Jotaro":
                return stand_jotaro;
        }
        return stand_diavolo;
    }
}
