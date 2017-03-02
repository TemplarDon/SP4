using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Popup : MonoBehaviour 
{
    public Canvas panelInfoCanvas;
    bool infoOpen = false;

    public Image tut;
    public Image tutToShopButton;
	public Text tutText;
    
	// Use this for initialization
	void Start () 
    {
        

	}
	
	// Update is called once per frame
	void Update () 
    {
        if (PersistentData.m_Instance.firstTimeAtModeSelect == true)
        {
            tut.enabled = true;
            tutToShopButton.enabled = true;
			tutText.enabled = true;
        }
        if (PersistentData.m_Instance.firstTimeAtModeSelect == false)
        {
            tut.enabled = false;
            tutToShopButton.enabled = false;
			tutText.enabled = false;
        }
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
