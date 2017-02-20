using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProfileUI : MonoBehaviour {

    public Image chargeMenu;
    public Image profileImage;
    public Image statsMenu;

    private Vector3 chargeStartPos;
    private Vector3 chargeEndPos;
    private Vector3 statsStartPos;
    private Vector3 statsEndPos;

    private bool hoverProfile;

	// Use this for initialization
	void Start () {
        chargeStartPos = new Vector3(-257.5f, 0, 0);
        chargeEndPos = new Vector3(-585.6f, 0, 0);
        statsStartPos = new Vector3(445, 21, 0);
        statsEndPos = new Vector3(103.5f, 21, 0);
        hoverProfile = false;
    }
	
	// Update is called once per frame
	void Update () {
        if(hoverProfile == true)
        {
            if(chargeMenu.transform.localPosition.x >= chargeEndPos.x)
            {
                chargeMenu.transform.Translate(-5, 0, 0);
            }
            if (statsMenu.transform.localPosition.x >= statsEndPos.x)
            {
                statsMenu.transform.Translate(-5, 0, 0);
            }
        }
        else
        {
            if (chargeMenu.transform.localPosition.x <= chargeStartPos.x)
            {
                chargeMenu.transform.Translate(5, 0, 0);
            }
            if (statsMenu.transform.localPosition.x <= statsStartPos.x)
            {
                statsMenu.transform.Translate(5, 0, 0);
            }
        }
	}

    public void mouseHover()
    {
        hoverProfile = true;
    }

    public void mouseLeave()
    {
        hoverProfile = false;
    }
}
