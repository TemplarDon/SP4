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
    private Vector3 endStartPos;

	// Use this for initialization
	void Start () {
        chargeStartPos = new Vector3(-89.79997f, 0, 0);
        chargeEndPos = new Vector3(83, 21, 0);
        statsStartPos = new Vector3(417, 60.16488f, 0);
        endStartPos = new Vector3(88, 60.16488f, 0);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
