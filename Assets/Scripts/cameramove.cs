using UnityEngine;
using System.Collections;

public class cameramove : MonoBehaviour {

    public float dragSpeed = 2;
    private Vector3 dragOrigin;
    public Vector3 currentLoc;
    private Vector3 pos;
    private Vector3 move;
    private Vector3 combinepos;
    private bool specialTrigger;
    private bool specialTrigger2;
    public turnManage turnManager;

    public float extraBorder;
    private float extraBordersave;

    public LevelGenerate map;

    // Use this for initialization
    void Start () {
        //currentLoc = new Vector3(7.99695f, -5.0f, -9.0f);
        currentLoc = this.gameObject.transform.position;
        //pos = new Vector3(0.0f, 0.0f, 0.0f);
        specialTrigger2 = false;
        //Camera.main.orthographic = false;
        extraBordersave = extraBorder;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, transform.position.y, -9);
        extraBorder = extraBordersave * (30.0f / (Camera.main.fieldOfView));

        // -------------------Code for Zooming Out------------
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
       {
           if (Camera.main.fieldOfView <= 125)
               Camera.main.fieldOfView += 2;
           if (Camera.main.orthographicSize <= 20)
               Camera.main.orthographicSize += 0.5f;
       
       }
       // ---------------Code for Zooming In------------------------
       if (Input.GetAxis("Mouse ScrollWheel") > 0)
       {
           if (Camera.main.fieldOfView > 2)
               Camera.main.fieldOfView -= 2;
           if (Camera.main.orthographicSize >= 1)
               Camera.main.orthographicSize -= 0.5f;
       }
       
       // -------Code to switch camera between Perspective and Orthographic--------
       if (Input.GetKeyUp(KeyCode.B))
       {
           if (Camera.main.orthographic == true)
               Camera.main.orthographic = false;
           else
               Camera.main.orthographic = true;
       }

        if (turnManager.menuOpen == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //currentLoc = currentLoc + move;
                dragOrigin = Input.mousePosition;
                return;
            }

            if (Input.GetMouseButton(0))
            {
                //if (!Input.GetMouseButton(0)) return;

                //Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
                pos = Input.mousePosition - dragOrigin;
                //Vector3 move = new Vector3(-pos.x * dragSpeed, -pos.y * dragSpeed, 0);
                move = new Vector3(-pos.x * 0.0285f, -pos.y * 0.0285f, 0);
                combinepos = currentLoc + move;

                if (combinepos.x < 7.99695f - extraBorder)
                {
                    //combinepos.x = 7.99695f;
                    combinepos += new Vector3(7.99695f - combinepos.x - extraBorder, 0, 0);
                }
                else if (map.mappositions[map.xsize - 1, 0].x - 7.99695f + 1 + extraBorder > 7.99695f + extraBorder)
                {
                    //combinepos.x = (map.xsize + 1) * 1.5f;
                    if (combinepos.x > map.mappositions[map.xsize - 1, 0].x - 7.99695f + 1 + extraBorder)
                        combinepos -= new Vector3(combinepos.x - (map.mappositions[map.xsize - 1, 0].x - 7.99695f + 1 + extraBorder), 0, 0);
                }
                else
                {
                    combinepos.x = 7.99695f;
                }

                if (combinepos.y > -5.0f + extraBorder)
                {
                    //combinepos.y = -5.0f;
                    combinepos -= new Vector3(0, combinepos.y - (-5.0f + extraBorder), 0);
                }
                else if (map.mappositions[0, map.ysize - 1].y - -5.0f - 1 - extraBorder < -5.0f - extraBorder)
                {
                    //combinepos.y = (map.ysize) * -1.5f;
                    if (combinepos.y < map.mappositions[0, map.ysize - 1].y - -5.0f - 1 - extraBorder)
                        combinepos += new Vector3(0, (map.mappositions[0, map.ysize - 1].y - -5.0f - 1 - extraBorder) - combinepos.y, 0);
                }
                else
                {
                    combinepos.y = -5.0f;
                }

                //transform.Translate(move, Space.World);
                transform.position = combinepos;

                if (!Input.GetMouseButton(0)) currentLoc = currentLoc + move;
            }

            specialTrigger = false;
            if (!Input.GetMouseButton(0) && (Camera.main.ScreenToViewportPoint(Input.mousePosition).x > 0.99f || Camera.main.ScreenToViewportPoint(Input.mousePosition).x < 0.01f || Camera.main.ScreenToViewportPoint(Input.mousePosition).y > 0.99f || Camera.main.ScreenToViewportPoint(Input.mousePosition).y < 0.01f))
            {
                pos = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2);
                move += new Vector3(pos.x * 0.0285f, pos.y * 0.0285f, 0) * 0.02f;
                combinepos = currentLoc + move;

                if (combinepos.x < 7.99695f - extraBorder)
                {
                    combinepos += new Vector3(7.99695f - combinepos.x - extraBorder, 0, 0);
                }
                else if (map.mappositions[map.xsize - 1, 0].x - 7.99695f + 1 + extraBorder > 7.99695f + extraBorder)
                {
                    if (combinepos.x > map.mappositions[map.xsize - 1, 0].x - 7.99695f + 1 + extraBorder)
                        combinepos -= new Vector3(combinepos.x - (map.mappositions[map.xsize - 1, 0].x - 7.99695f + 1 + extraBorder), 0, 0);
                }
                else
                {
                    combinepos.x = 7.99695f;
                }

                if (combinepos.y > -5.0f + extraBorder)
                {
                    //combinepos.y = -5.0f;
                    combinepos -= new Vector3(0, combinepos.y - (-5.0f + extraBorder), 0);
                }
                else if (map.mappositions[0, map.ysize - 1].y - -5.0f - 1 - extraBorder < -5.0f - extraBorder)
                {
                    //combinepos.y = (map.ysize) * -1.5f;
                    if (combinepos.y < map.mappositions[0, map.ysize - 1].y - -5.0f - 1 - extraBorder)
                        combinepos += new Vector3(0, (map.mappositions[0, map.ysize - 1].y - -5.0f - 1 - extraBorder) - combinepos.y, 0);
                }
                else
                {
                    combinepos.y = -5.0f;
                }

                transform.position = combinepos;
                specialTrigger = true;
            }
        }
        else
        {
            pos = new Vector3(0, 0, 0);
            move = new Vector3(0, 0, 0);
            dragOrigin = new Vector3(0, 0, 0);
            combinepos = new Vector3(0, 0, 0);
            specialTrigger = false;
            specialTrigger2 = true;

            //Debug.Log(turnManager.charac.xpos + " x " + currentLoc.x);
            if (currentLoc.x < turnManager.characNEW.pos.x - 0.26f)
            {
                transform.Translate(0.5f, 0, 0);
                currentLoc.x += 0.5f;
            }
            else if (currentLoc.x > turnManager.characNEW.pos.x + 0.26f)
            {
                transform.Translate(-0.5f, 0, 0);
                currentLoc.x -= 0.5f;
            }
            else
            {
                currentLoc.x = turnManager.characNEW.pos.x;
            }

            //Debug.Log(turnManager.charac.ypos + " y " + currentLoc.y);
            if (currentLoc.y < turnManager.characNEW.pos.y - 0.26f)
            {
                transform.Translate(0, 0.5f, 0);
                currentLoc.y += 0.5f;
            }
            else if (currentLoc.y > turnManager.characNEW.pos.y + 0.26f)
            {
                transform.Translate(0, -0.5f, 0);
                currentLoc.y -= 0.5f;
            }
            else
            {
                currentLoc.y = turnManager.characNEW.pos.y;
            }

            if (currentLoc.x < 7.99695f - extraBorder)
            {
                currentLoc += new Vector3(7.99695f - currentLoc.x - extraBorder, 0, 0);
            }
            else if (map.mappositions[map.xsize - 1, 0].x - 7.99695f + 1 + extraBorder > 7.99695f + extraBorder)
            {
                if (currentLoc.x > map.mappositions[map.xsize - 1, 0].x - 7.99695f + 1 + extraBorder)
                    currentLoc -= new Vector3(currentLoc.x - (map.mappositions[map.xsize - 1, 0].x - 7.99695f + 1 + extraBorder), 0, 0);
            }
            else
            {
                currentLoc.x = 7.99695f;
            }

            if (currentLoc.y > -5.0f + extraBorder)
            {
                //currentLoc.y = -5.0f;
                currentLoc -= new Vector3(0, currentLoc.y - (-5.0f + extraBorder), 0);
            }
            else if (map.mappositions[0, map.ysize - 1].y - -5.0f - 1 - extraBorder < -5.0f - extraBorder)
            {
                //currentLoc.y = (map.ysize) * -1.5f;
                if (currentLoc.y < map.mappositions[0, map.ysize - 1].y - -5.0f - 1 - extraBorder)
                    currentLoc += new Vector3(0, (map.mappositions[0, map.ysize - 1].y - -5.0f - 1 - extraBorder) - currentLoc.y, 0);
            }
            else
            {
                currentLoc.y = -5.0f;
            }

            transform.position = currentLoc;
        }

        if ((specialTrigger2 == false) && (turnManager.menuOpen == false) && (Input.GetMouseButtonUp(0) || specialTrigger))
        {
            //Debug.Log(map.mappositions[map.xsize - 1, 0]);
            currentLoc = combinepos;
            pos = new Vector3(0, 0, 0);
            move = new Vector3(0, 0, 0);
            dragOrigin = new Vector3(0, 0, 0);
            combinepos = new Vector3(0, 0, 0);
        }
        if(Input.GetMouseButtonUp(0))
        {
            specialTrigger2 = false;
        }
        
        transform.position = new Vector3(transform.position.x, transform.position.y, -9);
    }
}
