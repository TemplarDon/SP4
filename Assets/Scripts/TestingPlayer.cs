using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TestingPlayer : MonoBehaviour {

    public int xpos;
    public int ypos;
    public GameObject chara;
    public LevelGenerate map;
    public int mapposx;
    public int mapposy;

	// Use this for initialization
	void Start () {
        xpos = 1;
        ypos = -1;
        mapposx = 0;
        mapposy = 0;
        //map = GameObject.Find("LevelGeneration");
    }
	
	// Update is called once per frame
	void Update () {
        chara.transform.position = new Vector3(xpos, ypos, -1);
        map.mapposx = mapposx;
        map.mapposy = mapposy;

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            ypos++;
            mapposy--;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            ypos--;
            mapposy++;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            xpos--;
            mapposx--;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            xpos++;
            mapposx++;
        }

        if (xpos < 1)
        {
            mapposx++;
            xpos = 1;
        }
        else if (xpos > map.xsize)
        {
            mapposx--;
            xpos = map.xsize;
        }
        
        if (ypos > -1)
        {
            mapposy++;
            ypos = -1;
        }
        else if (ypos < -map.ysize)
        {
            mapposy--;
            ypos = -map.ysize;
        }
    }
}
