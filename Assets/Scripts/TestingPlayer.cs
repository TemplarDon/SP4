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
        xpos = 0;
        ypos = 0;
        mapposx = -1;
        mapposy = -1;
        //map = GameObject.Find("LevelGeneration");
    }
	
	// Update is called once per frame
	void Update () {
        chara.transform.position = new Vector3(xpos, ypos, -1);
        
        if(mapposx == -1 && mapposy == -1)
        {
            mapposx = map.xsize - map.xsize / 2;
            mapposy = map.ysize - 2;
        }

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

        if(map.xsize % 2 == 0)
        {
            if (xpos < -map.xsize / 2)
            {
                mapposx++;
                xpos = -map.xsize / 2;
            }
            else if (xpos > map.xsize / 2 - 1)
            {
                mapposx--;
                xpos = map.xsize / 2 - 1;
            }
        }
        else
        {
            if (xpos < -map.xsize / 2 - 1)
            {
                mapposx++;
                xpos = -map.xsize / 2 - 1;
            }
            else if (xpos > map.xsize / 2 - 1)
            {
                mapposx--;
                xpos = map.xsize / 2 - 1;
            }
        }
        
        if(map.ysize % 2 == 0)
        {
            if (ypos < -map.ysize / 2 + 2)
            {
                mapposy--;
                ypos = -map.ysize / 2 + 2;
            }
            else if (ypos > map.ysize / 2 + 1)
            {
                mapposy++;
                ypos = map.ysize / 2 + 1;
            }
        }
        else
        {
            if (ypos < -map.ysize / 2 + 1)
            {
                mapposy--;
                ypos = -map.ysize / 2 + 1;
            }
            else if (ypos > map.ysize / 2 + 1)
            {
                mapposy++;
                ypos = map.ysize / 2 + 1;
            }
        }
        
    }
}
