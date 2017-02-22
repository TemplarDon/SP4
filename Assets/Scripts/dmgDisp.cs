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

    // Use this for initialization
    void Start () {
        dmgIndicator = GameObject.Find("DmgIndicator").GetComponent<Text>();
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        heightUp = new Vector3(0, 0, 0);
        spawnPos = new Vector3(9999, 9999, 9999);
    }
	
	// Update is called once per frame
	void Update () {

        heightUp += new Vector3(0, 0.2f, 0);

        for (int i = 0; i < posList.Count; i++)
        {
            posList[i] = camera.WorldToScreenPoint(spawnList[i]) + heightUp;
            textList[i].transform.position = posList[i];
            textList[i].color = new Color(dmgIndicator.color.r, dmgIndicator.color.g, dmgIndicator.color.b, dmgIndicator.color.a - (Time.deltaTime * 0.4f));

            if (textList[i].color.a <= 0.0f)
            {
                textList.Remove(textList[i]);
                posList.Remove(posList[i]);
                spawnList.Remove(spawnList[i]);
            }
        }
    }

    public void dispAtk(int damageDealt, Vector3 location)
    {
        dmgIndicator.color = new Color(dmgIndicator.color.r, dmgIndicator.color.g, dmgIndicator.color.b, 1.0f);
        dmgIndicator.transform.position = camera.WorldToScreenPoint(location);
        dmgIndicator.text = damageDealt.ToString();
        spawnPos = location;
        heightUp = new Vector3(0, 0, 0);

        textList.Add(dmgIndicator);
        spawnList.Add(location);
        posList.Add(camera.WorldToScreenPoint(location));
        Instantiate(dmgIndicator, camera.WorldToScreenPoint(location), Quaternion.identity);
    }
}
