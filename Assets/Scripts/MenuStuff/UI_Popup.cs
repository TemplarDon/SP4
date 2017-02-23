using UnityEngine;
using System.Collections;

public class UI_Popup : MonoBehaviour 
{
    public Canvas panelInfoCanvas;
    bool infoOpen = false;
	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () 
    {

	}
    public void infoPanel()
    {
        if (infoOpen == false)
        {
            infoOpen = true;
            panelInfoCanvas.enabled = true;
        }
    }
    public void closeButton()
    {
        infoOpen = false;
        panelInfoCanvas.enabled = false;
    }
}
