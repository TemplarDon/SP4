using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using System.Collections.Generic;

public class dmgDisp : MonoBehaviour {

    public Text dmgIndicator;
    private Camera camera;
    private Vector3 heightUp;
    private Vector3 spawnPos;

    public List<Vector3> spawnList = new List<Vector3>();
    public List<Vector3> posList = new List<Vector3>();
    public List<Text> textList = new List<Text>();

    private Canvas canvas;

    public enum DISPLAY_TYPE
    {
        DAMAGE, 
        HEAL,
    }


    // Use this for initialization
    void Start () {
        dmgIndicator = GameObject.Find("DmgIndicator").GetComponent<Text>();
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        heightUp = new Vector3(0, 0, 0);
        spawnPos = new Vector3(9999, 9999, 9999);

        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }
	
	// Update is called once per frame
	void Update () {

        heightUp += new Vector3(0, 0.2f, 0);

        for (int i = 0; i < posList.Count; i++)
        {
            posList[i] = camera.WorldToScreenPoint(spawnList[i]) + heightUp;
            textList[i].transform.position = posList[i];
            textList[i].color = new Color(dmgIndicator.color.r, dmgIndicator.color.g, dmgIndicator.color.b, textList[i].color.a - (Time.deltaTime * 0.4f));
            //Debug.Log(textList[i].color.a);
            if (textList[i].color.a <= 0.0f)
            {
                //Destroy(textList[i]);
                textList[i].GetComponent<CleanUp>().enabled = true;
                textList.Remove(textList[i]);
                posList.Remove(posList[i]);
                spawnList.Remove(spawnList[i]);
                
            }
        }
    }

    public void dispAtk(int damageDealt, Vector3 location)
    {
        dmgIndicator.color = new Color(dmgIndicator.color.r, dmgIndicator.color.g, dmgIndicator.color.b, 1.0f);
        //dmgIndicator.transform.position = camera.WorldToScreenPoint(location);
        dmgIndicator.transform.position = new Vector3(999, 999, 999);
        dmgIndicator.text = damageDealt.ToString();
        spawnPos = location;
        heightUp = new Vector3(0, 0, 0);

        //textList.Add(dmgIndicator);
        spawnList.Add(location);
        posList.Add(camera.WorldToScreenPoint(location));
        dmgIndicator.transform.SetParent(canvas.transform, false);
        Text tempObj = (Text)(Instantiate(dmgIndicator, camera.WorldToScreenPoint(location), Quaternion.identity));
        tempObj.transform.SetParent(canvas.transform, false);
        textList.Add(tempObj);
    }

    public void dispNum(dmgDisp.DISPLAY_TYPE theType, int amt, Vector3 location)
    {
        switch (theType)
        {
            case DISPLAY_TYPE.DAMAGE:
                dmgIndicator.color = new Color(1, 0, 0, 1.0f);
                break;

            case DISPLAY_TYPE.HEAL:
                dmgIndicator.color = new Color(0, 1, 0, 1.0f);
                break;
        }

        dmgIndicator.color = new Color(dmgIndicator.color.r, dmgIndicator.color.g, dmgIndicator.color.b, 1.0f);
        //dmgIndicator.transform.position = camera.WorldToScreenPoint(location);
        dmgIndicator.transform.position = new Vector3(999, 999, 999);
        dmgIndicator.text = amt.ToString();
        spawnPos = location;
        heightUp = new Vector3(0, 0, 0);

        //textList.Add(dmgIndicator);
        spawnList.Add(location);
        posList.Add(camera.WorldToScreenPoint(location));
        dmgIndicator.transform.SetParent(canvas.transform, false);
        Text tempObj = (Text)(Instantiate(dmgIndicator, camera.WorldToScreenPoint(location), Quaternion.identity));
        tempObj.transform.SetParent(canvas.transform, false);
        textList.Add(tempObj);
    }
}
