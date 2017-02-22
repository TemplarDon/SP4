using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharShopSelect : MonoBehaviour {

	public Canvas panelInfoCard;
	public Canvas panelInfoCard2;
	public Canvas panelInfoCard3;
	public Canvas panelInfoCard4;
	public Canvas panelInfoCard5;
	public Canvas panelInfoCard6;
	public Canvas panelInfoCard7;
	public Canvas panelInfoCard8;
	public Canvas panelInfoCard9;
    
    public bool infoOpen = false;
	public bool infoOpen2 = false;
	public bool infoOpen3 = false;
	public bool infoOpen4 = false;
	public bool infoOpen5 = false;
	public bool infoOpen6 = false;
	public bool infoOpen7 = false;
	public bool infoOpen8 = false;
	public bool infoOpen9 = false;
    
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void InfoPanel()
    {
        if (infoOpen == false)
        {
            infoOpen = true;
            panelInfoCard.enabled = true;

            infoOpen2 = false;
            panelInfoCard2.enabled = false;

            infoOpen3 = false;
            panelInfoCard3.enabled = false;

            infoOpen4 = false;
            panelInfoCard4.enabled = false;

            infoOpen5 = false;
            panelInfoCard5.enabled = false;

            infoOpen6 = false;
            panelInfoCard6.enabled = false;

            infoOpen7 = false;
            panelInfoCard7.enabled = false;

            infoOpen8 = false;
            panelInfoCard8.enabled = false;

            infoOpen9 = false;
            panelInfoCard9.enabled = false;
        }
    }
    public void InfoPanel2()
    {
        infoOpen = false;
        panelInfoCard.enabled = false;

        infoOpen2 = true;
        panelInfoCard2.enabled = true;

        infoOpen3 = false;
        panelInfoCard3.enabled = false;

        infoOpen4 = false;
        panelInfoCard4.enabled = false;

        infoOpen5 = false;
        panelInfoCard5.enabled = false;

        infoOpen6 = false;
        panelInfoCard6.enabled = false;

        infoOpen7 = false;
        panelInfoCard7.enabled = false;

        infoOpen8 = false;
        panelInfoCard8.enabled = false;

        infoOpen9 = false;
        panelInfoCard9.enabled = false;
    }
    public void InfoPanel3()
    {
        infoOpen = false;
        panelInfoCard.enabled = false;

        infoOpen2 = false;
        panelInfoCard2.enabled = false;

        infoOpen3 = true;
        panelInfoCard3.enabled = true;

        infoOpen4 = false;
        panelInfoCard4.enabled = false;

        infoOpen5 = false;
        panelInfoCard5.enabled = false;

        infoOpen6 = false;
        panelInfoCard6.enabled = false;

        infoOpen7 = false;
        panelInfoCard7.enabled = false;

        infoOpen8 = false;
        panelInfoCard8.enabled = false;

        infoOpen9 = false;
        panelInfoCard9.enabled = false;
    }
    public void InfoPanel4()
    {
        infoOpen = false;
        panelInfoCard.enabled = false;

        infoOpen2 = false;
        panelInfoCard2.enabled = false;

        infoOpen3 = false;
        panelInfoCard3.enabled = false;

        infoOpen4 = true;
        panelInfoCard4.enabled = true;

        infoOpen5 = false;
        panelInfoCard5.enabled = false;

        infoOpen6 = false;
        panelInfoCard6.enabled = false;

        infoOpen7 = false;
        panelInfoCard7.enabled = false;

        infoOpen8 = false;
        panelInfoCard8.enabled = false;

        infoOpen9 = false;
        panelInfoCard9.enabled = false;
    }
    public void InfoPanel5()
    {
        infoOpen = false;
        panelInfoCard.enabled = false;

        infoOpen2 = false;
        panelInfoCard2.enabled = false;

        infoOpen3 = false;
        panelInfoCard3.enabled = false;

        infoOpen4 = false;
        panelInfoCard4.enabled = false;

        infoOpen5 = true;
        panelInfoCard5.enabled = true;

        infoOpen6 = false;
        panelInfoCard6.enabled = false;

        infoOpen7 = false;
        panelInfoCard7.enabled = false;

        infoOpen8 = false;
        panelInfoCard8.enabled = false;

        infoOpen9 = false;
        panelInfoCard9.enabled = false;
    }
    public void InfoPanel6()
    {
        infoOpen = false;
        panelInfoCard.enabled = false;

        infoOpen2 = false;
        panelInfoCard2.enabled = false;

        infoOpen3 = false;
        panelInfoCard3.enabled = false;

        infoOpen4 = false;
        panelInfoCard4.enabled = false;

        infoOpen5 = false;
        panelInfoCard5.enabled = false;

        infoOpen6 = true;
        panelInfoCard6.enabled = true;

        infoOpen7 = false;
        panelInfoCard7.enabled = false;

        infoOpen8 = false;
        panelInfoCard8.enabled = false;

        infoOpen9 = false;
        panelInfoCard9.enabled = false;
    }
    public void InfoPanel7()
    {
        infoOpen = false;
        panelInfoCard.enabled = false;

        infoOpen2 = false;
        panelInfoCard2.enabled = false;

        infoOpen3 = false;
        panelInfoCard3.enabled = false;

        infoOpen4 = false;
        panelInfoCard4.enabled = false;

        infoOpen5 = false;
        panelInfoCard5.enabled = false;

        infoOpen6 = false;
        panelInfoCard6.enabled = false;

        infoOpen7 = true;
        panelInfoCard7.enabled = true;

        infoOpen8 = false;
        panelInfoCard8.enabled = false;

        infoOpen9 = false;
        panelInfoCard9.enabled = false;
    }
    public void InfoPanel8()
    {
        infoOpen = false;
        panelInfoCard.enabled = false;

        infoOpen2 = false;
        panelInfoCard2.enabled = false;

        infoOpen3 = false;
        panelInfoCard3.enabled = false;

        infoOpen4 = false;
        panelInfoCard4.enabled = false;

        infoOpen5 = false;
        panelInfoCard5.enabled = false;

        infoOpen6 = false;
        panelInfoCard6.enabled = false;

        infoOpen7 = false;
        panelInfoCard8.enabled = false;

        infoOpen8 = true;
        panelInfoCard8.enabled = true;

        infoOpen9 = false;
        panelInfoCard9.enabled = false;
    }
    public void InfoPanel9()
    {
        infoOpen = false;
        panelInfoCard.enabled = false;

        infoOpen2 = false;
        panelInfoCard2.enabled = false;

        infoOpen3 = false;
        panelInfoCard3.enabled = false;

        infoOpen4 = false;
        panelInfoCard4.enabled = false;

        infoOpen5 = false;
        panelInfoCard5.enabled = false;

        infoOpen6 = false;
        panelInfoCard6.enabled = false;

        infoOpen7 = false;
        panelInfoCard8.enabled = false;

        infoOpen8 = false;
        panelInfoCard8.enabled = false;

        infoOpen9 = true;
        panelInfoCard9.enabled = true;
    }
    
}
