using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class LevelGenerate : MonoBehaviour {

    public int mapposx;
    public int mapposy;
    public GameObject prefab;
    public Sprite tile_grass, tile_stone, tile_spike, tile_rock, tile_water;
    public int[,] mapgrid;
    private bool[,] mapcheck;
    public Vector3[,] mappositions;
    public int xsize;
    public int ysize;
    public GameObject possibleLoc;
    public GameObject enemyLoc;

    public CSVLoader newMapDetected;

    private bool dragging;
    private int dragtimer;
    private Vector3 dragStart;
    private Vector3 dragChange;
    private Vector3 dragSave;

    public float dragSpeed = 2;
    private Vector3 dragOrigin;

    bool yellowGen = false;
    public bool redGen = false;

    /* List of tiles:
     * 1 [NORMAL] Grass Tile
     * 2 [NORMAL] Stone Tile
     * 3 [DEBUFF] Spiked Tile
     * 4 [OBSTACLE] Rock Tile
     * 5 [OBSTAcLE] Water Tile
    */

    // Use this for initialization
    void Awake () {
        //Debug.Log("Level Generated");

        prefab = GameObject.Find("Tile");
        dragging = false;
        dragtimer = 10;

        if (newMapDetected == null)
        {
            xsize = Random.Range(17, 24);
            ysize = Random.Range(16, 21);
            //xsize = 6;
            //ysize = 6;
            mapgrid = new int[xsize, ysize];
            mapcheck = new bool[xsize, ysize];
            mappositions = new Vector3[xsize, ysize];

            bool mapConfirmed = false;

            while (!mapConfirmed)
            {
                for (int j = 0; j < ysize; j++)
                {
                    for (int i = 0; i < xsize; i++)
                    {
                        mapgrid[i, j] = Random.Range(1, 6);
                        mapcheck[i, j] = false;
                        if (mapgrid[i, j] >= 4)
                        {
                            mapcheck[i, j] = true;
                        }
                    }
                }

                int tempi = 0;
                int tempj = 0;
                while (mapgrid[tempi, tempj] >= 4)
                {
                    tempi++;
                    if (tempi == xsize - 1)
                    {
                        tempi = 0;
                        tempj++;
                        if (tempj == ysize - 1)
                        {
                            break;
                        }
                    }
                }

                validateMapCheck(tempi, tempj);

                for (int j = 0; j < ysize; j++)
                {
                    for (int i = 0; i < xsize; i++)
                    {
                        if (mapcheck[i, j] == false)
                        {
                            if (i == 0)
                            {
                                if (Random.value >= 0.5)
                                    mapgrid[i + 1, j] = Random.Range(1, 4);
                                if (j == 0)
                                {
                                    if (Random.value >= 0.5)
                                        mapgrid[i, j + 1] = Random.Range(1, 4);
                                }
                                else if (j == ysize - 1)
                                {
                                    if (Random.value >= 0.5)
                                        mapgrid[i, j - 1] = Random.Range(1, 4);
                                }
                            }
                            else if (i == xsize - 1)
                            {
                                if (Random.value >= 0.5)
                                    mapgrid[i - 1, j] = Random.Range(1, 4);
                                if (j == 0)
                                {
                                    if (Random.value >= 0.5)
                                        mapgrid[i, j + 1] = Random.Range(1, 4);
                                }
                                else if (j == ysize - 1)
                                {
                                    if (Random.value >= 0.5)
                                        mapgrid[i, j - 1] = Random.Range(1, 4);
                                }
                            }
                            else
                            {
                                if (Random.value >= 0.5)
                                    mapgrid[i + 1, j] = Random.Range(1, 4);
                                if (Random.value >= 0.5)
                                    mapgrid[i - 1, j] = Random.Range(1, 4);
                                if (j == 0)
                                {
                                    if (Random.value >= 0.5)
                                        mapgrid[i, j + 1] = Random.Range(1, 4);
                                }
                                else if (j == ysize - 1)
                                {
                                    if (Random.value >= 0.5)
                                        mapgrid[i, j - 1] = Random.Range(1, 4);
                                }
                            }
                        }
                    }
                }

                for (int j = 0; j < ysize; j++)
                {
                    for (int i = 0; i < xsize; i++)
                    {
                        mappositions[i, j] = new Vector3(i * 1.0f + prefab.GetComponent<SpriteRenderer>().bounds.size.x, (j * -1.0f) - prefab.GetComponent<SpriteRenderer>().bounds.size.y, 0);

                        switch (mapgrid[i, j])
                        {
                            case 1:
                                prefab.GetComponent<SpriteRenderer>().sprite = tile_grass;
                                break;
                            case 2:
                                prefab.GetComponent<SpriteRenderer>().sprite = tile_stone;
                                break;
                            case 3:
                                prefab.GetComponent<SpriteRenderer>().sprite = tile_spike;
                                break;
                            case 4:
                                prefab.GetComponent<SpriteRenderer>().sprite = tile_rock;
                                break;
                            case 5:
                                prefab.GetComponent<SpriteRenderer>().sprite = tile_water;
                                break;
                        }
                        //Instantiate(prefab, new Vector3(i * 1.0f - ((xsize / 2)) * 1.0f, (j * -1.0f) + (((ysize / 2) + 1) * 1.0f), 0), Quaternion.identity);
                        Instantiate(prefab, mappositions[i, j], Quaternion.identity);

                    }
                }

                //clearMapCheck();
                mapConfirmed = true;
            }
        }
        else
        {
            generateLoadMap(newMapDetected.loadedMap, newMapDetected.newxsize, newMapDetected.newysize);
        }
    }
	
	// Update is called once per frame
	void Update () {

        clearMapCheck();

        /* POSSIBLE MOVE PATTERNS
         * 1 PLUS-CROSS
         * 2 X-CROSS
         * 3 SQUARE
         * --------
         * 5 QUEEN ( PLUS-X-CROSS )
         * 6 STEP
         * 7 BLIND STEP
         */

        BaseCharacter theCharacter = GameObject.Find("Controller").GetComponent<CharacterController>().CurrentControlledCharacter.GetComponent<BaseCharacter>();

        if (GameObject.Find("Controller").GetComponent<CharacterController>().CurrentMode == CharacterController.CONTROL_MODE.MOVING || GameObject.Find("Controller").GetComponent<CharacterController>().CurrentMode == CharacterController.CONTROL_MODE.ATTACKING)
        {
            //checkPossibleLoc(mapposx, mapposy, 6, theCharacter.BaseSpeed);
            if (redGen == false)
            {
                checkPossibleLoc(mapposx, mapposy, 6, theCharacter.BaseSpeed);
                GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Character");
                foreach (GameObject obj in allObjects)
                {
                    if (mapcheck[(int)obj.transform.position.x - 1, -(int)obj.transform.position.y - 1] == true)
                    {
                        mapcheck[(int)obj.transform.position.x - 1, -(int)obj.transform.position.y - 1] = false;
                    }
                }
            }
            else if(redGen == true)
            {
                checkPossibleLoc(mapposx, mapposy, 6, theCharacter.BaseAttackRange);
                GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Character");
                foreach (GameObject obj in allObjects)
                {
                    
                    if (mapcheck[(int)obj.transform.position.x - 1, -(int)obj.transform.position.y - 1] == true && ((int)obj.transform.position.x - 1 != mapposx || ((int)-obj.transform.position.y - 1) != mapposy))
                    {
                        mapcheck[(int)obj.transform.position.x - 1, -(int)obj.transform.position.y - 1] = false;
                        Instantiate(enemyLoc, mappositions[(int)obj.transform.position.x - 1, -(int)obj.transform.position.y - 1], Quaternion.identity);
                    }
                    else
                    {
                            mapcheck[(int)obj.transform.position.x - 1, -(int)obj.transform.position.y - 1] = false;
                    }
                }
            }

            if (yellowGen == false && redGen == false)
            {
                yellowGen = true;
                for (int j = 0; j < ysize; j++)
                {
                    for (int i = 0; i < xsize; i++)
                    {
                        if (mapcheck[i, j] == true && mapgrid[i, j] < 4)
                            Instantiate(possibleLoc, mappositions[i, j], Quaternion.identity);
                    }
                }
            }
            else if (redGen == true)
            {
                for (int j = 0; j < ysize; j++)
                {
                    for (int i = 0; i < xsize; i++)
                    {
                        if (mapcheck[i, j] == true && mapgrid[i, j] < 4)
                            Instantiate(enemyLoc, mappositions[i, j], Quaternion.identity);
                    }
                }
                redGen = false;
                yellowGen = true;
            }
        }
        else
        {
            yellowGen = false;
            GameObject[] allObjects = GameObject.FindGameObjectsWithTag("yellowSq");
            foreach (GameObject obj in allObjects)
            {
                if (obj.name == "CurrentLoc(Clone)")
                    Destroy(obj);
            }

            GameObject[] allObjects2 = GameObject.FindGameObjectsWithTag("redSq");
            foreach (GameObject obj in allObjects2)
            {
                if (obj.name == "AttackSq(Clone)")
                    Destroy(obj);
            }
        }
    }

    void clearMapCheck() {
        for(int j = 0; j < ysize; j++)
        {
            for(int i = 0; i < xsize; i++)
            {
                if(mapgrid[i, j] < 4)
                {
                    mapcheck[i, j] = false;
                }
                else
                {
                    mapcheck[i, j] = true;
                }
            }
        }
    }

    void validateMapCheck(int i, int j) {

        if (mapcheck[i, j] == true)
        {
            return;
        }

        if (mapgrid[i,j] < 6)
        {
            mapcheck[i, j] = true;
            if (i != xsize - 1)
            {
                if(j != ysize - 1)
                {
                    validateMapCheck(i + 1, j + 1);
                }
                else if(j != 0)
                {
                    validateMapCheck(i + 1, j - 1);
                }
            }
            else if(i != 0)
            {
                if (j != ysize - 1)
                {
                    validateMapCheck(i - 1, j + 1);
                }
                else if (j != 0)
                {
                    validateMapCheck(i - 1, j - 1);
                }
            }
        }
    }

    bool checkMapCheck() {
        for (int j = 0; j < ysize; j++)
        {
            for (int i = 0; i < xsize; i++)
            {
                if(mapcheck[i,j] == false)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void checkPossibleLoc(int i, int j, int type, int range)
    {
        int leftC = 0;
        int rightC = xsize - 1;
        int upC = 0;
        int downC = ysize - 1;
        if (range > 0)
        {
            leftC = i - range;
            rightC = i + range;
            upC = j - range;
            downC = j + range;
            if (leftC < 0)
                leftC = 0;
            if (rightC > xsize - 1)
                rightC = xsize - 1;
            if (upC < 0)
                upC = 0;
            if (downC > ysize - 1)
                downC = ysize - 1;
        }
        switch (type)
        {
            case 1:
                {
                    for (int i2 = i; i2 <= rightC; i2++)
                    {
                        if (mapgrid[i2, j] < 4)
                        {
                            mapcheck[i2, j] = true;
                        }
                        else
                        {
                            break;
                        }
                    }

                    for (int i3 = i; i3 >= leftC; i3--)
                    {
                        if (mapgrid[i3, j] < 4)
                        {
                            mapcheck[i3, j] = true;
                        }
                        else
                        {
                            break;
                        }
                    }

                    for (int j2 = j; j2 <= downC; j2++)
                    {
                        if (mapgrid[i, j2] < 4)
                        {
                            mapcheck[i, j2] = true;
                        }
                        else
                        {
                            break;
                        }
                    }

                    for (int j3 = j; j3 >= upC; j3--)
                    {
                        if (mapgrid[i, j3] < 4)
                        {
                            mapcheck[i, j3] = true;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                break;
            case 2:
                {
                    for (int i2 = i, j2 = j; i2 <= rightC && j2 >= upC; i2++, j2--)
                    {
                        if (mapgrid[i2, j2] < 4)
                        {
                            mapcheck[i2, j2] = true;
                        }
                        else
                        {
                            break;
                        }
                    }

                    for (int i3 = i, j3 = j; i3 <= rightC && j3 <= downC; i3++, j3++)
                    {
                        if (mapgrid[i3, j3] < 4)
                        {
                            mapcheck[i3, j3] = true;
                        }
                        else
                        {
                            break;
                        }
                    }

                    for (int i4 = i, j4 = j; i4 >= leftC && j4 >= upC; i4--, j4--)
                    {
                        if (mapgrid[i4, j4] < 4)
                        {
                            mapcheck[i4, j4] = true;
                        }
                        else
                        {
                            break;
                        }
                    }

                    for (int i5 = i, j5 = j; i5 >= leftC && j5 <= downC; i5--, j5++)
                    {
                        if (mapgrid[i5, j5] < 4)
                        {
                            mapcheck[i5, j5] = true;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                break;
            case 3:
                {
                    checkPossibleLocBlindStep(i, j, (range + 1) * 2 + 1, 1);
                    checkPossibleLocBlindStep(i, j, (range + 1) * 2 + 1, 2);
                    checkPossibleLocBlindStep(i, j, (range + 1) * 2 + 1, 3);
                    checkPossibleLocBlindStep(i, j, (range + 1) * 2 + 1, 4);
                    for(int j2 = 0; j2 < ysize; j2++)
                    {
                        for(int i2 = 0; i2 < xsize; i2++)
                        {
                            if (mapcheck[i2, j2] == true && mapgrid[i2, j2] < 4 && ((i2 > rightC || i2 < leftC) || (j2 < upC || j2 > downC)))
                            {
                                mapcheck[i2, j2] = false;
                            }
                            else if(mapgrid[i2, j2] >= 4)
                            {
                                if(i2 == i)
                                {
                                    if(j2 > j)
                                    {
                                        removeSight(i2, j2, 2);
                                    }
                                    else if(j2 < j)
                                    {
                                        removeSight(i2, j2, 1);
                                    }
                                }
                                if (j2 == j)
                                {
                                    if (i2 > i)
                                    {
                                        removeSight(i2, j2, 4);
                                    }
                                    else if (i2 < i)
                                    {
                                        removeSight(i2, j2, 3);
                                    }
                                }
                                if(i2 > i && j2 < j)
                                {
                                    removeSight(i2, j2, 5);
                                }
                                else if (i2 < i && j2 < j)
                                {
                                    removeSight(i2, j2, 6);
                                }
                                else if (i2 < i && j2 > j)
                                {
                                    removeSight(i2, j2, 7);
                                }
                                else if (i2 > i && j2 > j)
                                {
                                    removeSight(i2, j2, 8);
                                }
                            }
                        }
                    }
                }
                break;
            case 5:
                {
                    for (int i2 = i, j2 = j; i2 <= rightC && j2 >= upC; i2++, j2--)
                    {
                        if (mapgrid[i2, j2] < 4)
                        {
                            mapcheck[i2, j2] = true;
                        }
                        else
                        {
                            break;
                        }
                    }

                    for (int i3 = i, j3 = j; i3 <= rightC && j3 <= downC; i3++, j3++)
                    {
                        if (mapgrid[i3, j3] < 4)
                        {
                            mapcheck[i3, j3] = true;
                        }
                        else
                        {
                            break;
                        }
                    }

                    for (int i4 = i, j4 = j; i4 >= leftC && j4 >= upC; i4--, j4--)
                    {
                        if (mapgrid[i4, j4] < 4)
                        {
                            mapcheck[i4, j4] = true;
                        }
                        else
                        {
                            break;
                        }
                    }

                    for (int i5 = i, j5 = j; i5 >= leftC && j5 <= downC; i5--, j5++)
                    {
                        if (mapgrid[i5, j5] < 4)
                        {
                            mapcheck[i5, j5] = true;
                        }
                        else
                        {
                            break;
                        }
                    }

                    for (int i2 = i; i2 <= rightC; i2++)
                    {
                        if (mapgrid[i2, j] < 4)
                        {
                            mapcheck[i2, j] = true;
                        }
                        else
                        {
                            break;
                        }
                    }

                    for (int i3 = i; i3 >= leftC; i3--)
                    {
                        if (mapgrid[i3, j] < 4)
                        {
                            mapcheck[i3, j] = true;
                        }
                        else
                        {
                            break;
                        }
                    }

                    for (int j2 = j; j2 <= downC; j2++)
                    {
                        if (mapgrid[i, j2] < 4)
                        {
                            mapcheck[i, j2] = true;
                        }
                        else
                        {
                            break;
                        }
                    }

                    for (int j3 = j; j3 >= upC; j3--)
                    {
                        if (mapgrid[i, j3] < 4)
                        {
                            mapcheck[i, j3] = true;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                break;
            case 6:
                {
                    checkPossibleLocStep(i, j, range + 1);
                }
                break;
            case 7:
                {
                    checkPossibleLocBlindStep(i, j, range + 1, 1);
                    checkPossibleLocBlindStep(i, j, range + 1, 2);
                    checkPossibleLocBlindStep(i, j, range + 1, 3);
                    checkPossibleLocBlindStep(i, j, range + 1, 4);
                    for (int j2 = 0; j2 < ysize; j2++)
                    {
                        for (int i2 = 0; i2 < xsize; i2++)
                        {
                            if (mapgrid[i2, j2] >= 4)
                            {
                                if (i2 == i)
                                {
                                    if (j2 > j)
                                    {
                                        removeSight(i2, j2, 2);
                                    }
                                    else if (j2 < j)
                                    {
                                        removeSight(i2, j2, 1);
                                    }
                                }
                                if (j2 == j)
                                {
                                    if (i2 > i)
                                    {
                                        removeSight(i2, j2, 4);
                                    }
                                    else if (i2 < i)
                                    {
                                        removeSight(i2, j2, 3);
                                    }
                                }
                                if (i2 > i && j2 < j && (i2 - i) == (j - j2))
                                {
                                    removeSight(i2, j2, 5);
                                }
                                else if (i2 < i && j2 < j && (i - i2) == (j -j2))
                                {
                                    removeSight(i2, j2, 6);
                                }
                                else if (i2 < i && j2 > j && (i - i2) == (j2 - j))
                                {
                                    removeSight(i2, j2, 7);
                                }
                                else if (i2 > i && j2 > j && (i2 - i) == (j2 - j))
                                {
                                    removeSight(i2, j2, 8);
                                }
                                else if(i2 > i && j2 < j && (j - j2) > (i2 - i))
                                {
                                    removeSight(i2, j2, 11);
                                }
                                else if (i2 < i && j2 < j && (j - j2) > (i - i2))
                                {
                                    removeSight(i2, j2, 12);
                                }
                                else if (i2 > i && j2 > j && (j2 - j) > (i2 - i))
                                {
                                    removeSight(i2, j2, 13);
                                }
                                else if (i2 < i && j2 > j && (j2 - j) > (i - i2))
                                {
                                    removeSight(i2, j2, 14);
                                }


                                else if (i2 < i && j2 < j && (j - j2) < (i - i2))
                                {
                                    removeSight(i2, j2, 15);
                                }
                                else if (i2 < i && j2 > j && (j2 - j) < (i - i2))
                                {
                                    removeSight(i2, j2, 16);
                                }
                                else if (i2 > i && j2 < j && (j - j2) < (i2 - i))
                                {
                                    removeSight(i2, j2, 17);
                                }
                                else if (i2 > i && j2 > j && (j2 - j) < (i2 - i))
                                {
                                    removeSight(i2, j2, 18);
                                }
                            }
                        }
                    }
                }
                break;
        }
        
    }

    Vector3 getTilePos(int i, int j)
    {
        return mappositions[i, j];
    }

    void checkPossibleLocStep(int i, int j, int step)
    {
        if(step <= 0)
        {
            return;
        }

        if(i < 0)
        {
            return;
        }
        else if(i >= xsize)
        {
            return;
        }

        if (j < 0)
        {
            return;
        }
        else if (j >= ysize)
        {
            return;
        }

        if(mapgrid[i,j] >= 4)
        {
            return;
        }

        //if (mapcheck[i,j] == true)
        //{
        //    return;
        //}

        mapcheck[i, j] = true;

        checkPossibleLocStep(i, j + 1, step - 1);
        checkPossibleLocStep(i, j - 1, step - 1);
        checkPossibleLocStep(i + 1, j, step - 1);
        checkPossibleLocStep(i - 1, j, step - 1);
    }

    void checkPossibleLocBlindStep(int i, int j, int step, int direction)
    {
        if (step <= 0)
        {
            return;
        }

        if (i < 0)
        {
            return;
        }
        else if (i >= xsize)
        {
            return;
        }

        if (j < 0)
        {
            return;
        }
        else if (j >= ysize)
        {
            return;
        }

        if (mapgrid[i, j] >= 4)
        {
            return;
        }

        //if (mapcheck[i,j] == true)
        //{
        //    return;
        //}

        mapcheck[i, j] = true;

        switch(direction)
        {
            case 1:
                checkPossibleLocBlindStep(i, j - 1, step - 1, direction); // UP
                checkPossibleLocBlindStep(i + 1, j, step - 1, direction); // RIGHT
                break;
            case 2:
                checkPossibleLocBlindStep(i, j - 1, step - 1, direction); // UP
                checkPossibleLocBlindStep(i - 1, j, step - 1, direction); // LEFT
                break;
            case 3:
                checkPossibleLocBlindStep(i, j + 1, step - 1, direction); // DOWN
                checkPossibleLocBlindStep(i + 1, j, step - 1, direction); // RIGHT
                break;
            case 4:
                checkPossibleLocBlindStep(i, j + 1, step - 1, direction); // DOWN
                checkPossibleLocBlindStep(i - 1, j, step - 1, direction); // LEFT
                break;
        }
        //checkPossibleLocStep(i, j + 1, step - 1); // DOWN
        //checkPossibleLocStep(i, j - 1, step - 1); // UP
        //checkPossibleLocStep(i + 1, j, step - 1); // RIGHT
        //checkPossibleLocStep(i - 1, j, step - 1); // LEFT
    }

    // Used by pathfinding
    public int GetTileCost(int x, int y)
    {
        switch (mapgrid[x, y])
        {
            case 1: return 1;
            case 2: return 1;
            case 3: return 2;
            case 4: return -1;
            case 5: return -1;
        }

        return 0;
    }

    // Used by pathfinding
    public Vector3 GetTilePos(int x_idx, int y_idx)
    {
        return mappositions[x_idx, y_idx];
    }

    void removeSight(int i, int j, int direction)
    {
        bool cleaningDone = false;
        int x = i;
        int y = j;
        int counter = 0;
        while (!cleaningDone)
        {
            cleaningDone = true;
            counter++;
            switch (direction)
            {
                case 1:
                    {
                        y--;
                        if (y >= 0)
                        {
                            x = i - counter - 1;
                            while (x < i + counter)
                            {
                                x++;
                                if (x >= 0 && x < xsize)
                                {
                                    if (mapcheck[x, y] == true && mapgrid[x, y] < 4)
                                    {
                                        mapcheck[x, y] = false;
                                        cleaningDone = false;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case 2:
                    {
                        y++;
                        if (y < ysize)
                        {
                            x = i - counter - 1;
                            while (x < i + counter)
                            {
                                x++;
                                if (x >= 0 && x < xsize)
                                {
                                    if (mapcheck[x, y] == true && mapgrid[x, y] < 4)
                                    {
                                        mapcheck[x, y] = false;
                                        cleaningDone = false;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case 3:
                    {
                        x--;
                        if (x >= 0)
                        {
                            y = j - counter - 1;
                            while (y < j + counter)
                            {
                                y++;
                                if (y >= 0 && y < ysize)
                                {
                                    if (mapcheck[x, y] == true && mapgrid[x, y] < 4)
                                    {
                                        mapcheck[x, y] = false;
                                        cleaningDone = false;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case 4:
                    {
                        x++;
                        if (x < xsize)
                        {
                            y = j - counter - 1;
                            while (y < j + counter)
                            {
                                y++;
                                if (y >= 0 && y < ysize)
                                {
                                    if (mapcheck[x, y] == true && mapgrid[x, y] < 4)
                                    {
                                        mapcheck[x, y] = false;
                                        cleaningDone = false;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case 5:
                    {
                        for (int j3 = j; j3 >= 0; j3--)
                        {
                            for (int i3 = i; i3 < xsize; i3++)
                            {
                                if (mapgrid[i3, j3] < 4)
                                {
                                    mapcheck[i3, j3] = false;
                                }
                            }
                        }
                    }
                    break;
                case 6:
                    {
                        for (int j3 = j; j3 >= 0; j3--)
                        {
                            for (int i3 = i; i3 >= 0; i3--)
                            {
                                if (mapgrid[i3, j3] < 4)
                                {
                                    mapcheck[i3, j3] = false;
                                }
                            }
                        }
                    }
                    break;
                case 7:
                    {
                        for (int j3 = j; j3 < ysize; j3++)
                        {
                            for (int i3 = i; i3 >= 0; i3--)
                            {
                                if (mapgrid[i3, j3] < 4)
                                {
                                    mapcheck[i3, j3] = false;
                                }
                            }
                        }
                    }
                    break;
                case 8:
                    {
                        for (int j3 = j; j3 < ysize; j3++)
                        {
                            for (int i3 = i; i3 < xsize; i3++)
                            {
                                if (mapgrid[i3, j3] < 4)
                                {
                                    mapcheck[i3, j3] = false;
                                }
                            }
                        }
                    }
                    break;
                case 9:
                    {
                        
                    }
                    break;
                case 10:
                    {

                    }
                    break;
                case 11:
                    {
                        y--;
                        if (y >= 0)
                        {
                            x = i - 1;
                            while (x < i + counter)
                            {
                                x++;
                                if (x >= 0 && x < xsize)
                                {
                                    if (mapcheck[x, y] == true && mapgrid[x, y] < 4)
                                    {
                                        mapcheck[x, y] = false;
                                        cleaningDone = false;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case 12:
                    {
                        y--;
                        if (y >= 0)
                        {
                            x = i - counter - 1;
                            while (x < i)
                            {
                                x++;
                                if (x >= 0 && x < xsize)
                                {
                                    if (mapcheck[x, y] == true && mapgrid[x, y] < 4)
                                    {
                                        mapcheck[x, y] = false;
                                        cleaningDone = false;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case 13:
                    {
                        y++;
                        if (y < ysize)
                        {
                            x = i - 1;
                            while (x < i + counter)
                            {
                                x++;
                                if (x >= 0 && x < xsize)
                                {
                                    if (mapcheck[x, y] == true && mapgrid[x, y] < 4)
                                    {
                                        mapcheck[x, y] = false;
                                        cleaningDone = false;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case 14:
                    {
                        y++;
                        if (y < ysize)
                        {
                            x = i - counter - 1;
                            while (x < i)
                            {
                                x++;
                                if (x >= 0 && x < xsize)
                                {
                                    if (mapcheck[x, y] == true && mapgrid[x, y] < 4)
                                    {
                                        mapcheck[x, y] = false;
                                        cleaningDone = false;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case 15:
                    {
                        x--;
                        if (x >= 0)
                        {
                            y = j - counter - 1;
                            while (y < j)
                            {
                                y++;
                                if (y >= 0 && y < ysize)
                                {
                                    if (mapcheck[x, y] == true && mapgrid[x, y] < 4)
                                    {
                                        mapcheck[x, y] = false;
                                        cleaningDone = false;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case 16:
                    {
                        x--;
                        if (x >= 0)
                        {
                            y = j - 1;
                            while (y < j + counter)
                            {
                                y++;
                                if (y >= 0 && y < ysize)
                                {
                                    if (mapcheck[x, y] == true && mapgrid[x, y] < 4)
                                    {
                                        mapcheck[x, y] = false;
                                        cleaningDone = false;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case 17:
                    {
                        x++;
                        if (x < xsize)
                        {
                            y = j - counter - 1;
                            while (y < j)
                            {
                                y++;
                                if (y >= 0 && y < ysize)
                                {
                                    if (mapcheck[x, y] == true && mapgrid[x, y] < 4)
                                    {
                                        mapcheck[x, y] = false;
                                        cleaningDone = false;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case 18:
                    {
                        x++;
                        if (x < xsize)
                        {
                            y = j - 1;
                            while (y < j + counter)
                            {
                                y++;
                                if (y >= 0 && y < ysize)
                                {
                                    if (mapcheck[x, y] == true && mapgrid[x, y] < 4)
                                    {
                                        mapcheck[x, y] = false;
                                        cleaningDone = false;
                                    }
                                }
                            }
                        }
                    }
                    break;
            }
        }
    }

    public BaseCharacter GetCharacterInTile(Vector3 CheckPosition)
    {
        GameObject[] CharacterList = GameObject.FindGameObjectsWithTag("Character");

        foreach (GameObject checkChar in CharacterList)
        {
            if (!checkChar.GetComponent<BaseCharacter>().enabled)
                continue;

            if (checkChar.GetComponent<BaseCharacter>().pos.x == CheckPosition.x && checkChar.GetComponent<BaseCharacter>().pos.y == CheckPosition.y)
            {
                Debug.Log("Character Found!");
                return checkChar.GetComponent<BaseCharacter>();
            }
        }

        return null;

    }

    public void generateLoadMap(int [,] newMap, int newxsize, int newysize)
    {
        xsize = newxsize;
        ysize = newysize;

        mapgrid = new int[xsize, ysize];
        mapcheck = new bool[xsize, ysize];
        mappositions = new Vector3[xsize, ysize];
            for (int j = 0; j < ysize; j++)
            {
                for (int i = 0; i < xsize; i++)
                {
                    mapgrid[i, j] = newMap[i,j];
                    mapcheck[i, j] = false;
                    if (mapgrid[i, j] >= 4)
                    {
                        mapcheck[i, j] = true;
                    }
                }
            }

        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("yellowSq");
        foreach (GameObject obj in allObjects)
        {
            if (obj.name == "Tile(Clone)")
                Destroy(obj);
        }

        for (int j = 0; j < ysize; j++)
            {
                for (int i = 0; i < xsize; i++)
                {
                    mappositions[i, j] = new Vector3(i * 1.0f + prefab.GetComponent<SpriteRenderer>().bounds.size.x, (j * -1.0f) - prefab.GetComponent<SpriteRenderer>().bounds.size.y, 0);

                    switch (newMap[i, j])
                    {
                        case 1:
                            prefab.GetComponent<SpriteRenderer>().sprite = tile_grass;
                            break;
                        case 2:
                            prefab.GetComponent<SpriteRenderer>().sprite = tile_stone;
                            break;
                        case 3:
                            prefab.GetComponent<SpriteRenderer>().sprite = tile_spike;
                            break;
                        case 4:
                            prefab.GetComponent<SpriteRenderer>().sprite = tile_rock;
                            break;
                        case 5:
                            prefab.GetComponent<SpriteRenderer>().sprite = tile_water;
                            break;
                    }
                    //Instantiate(prefab, new Vector3(i * 1.0f - ((xsize / 2)) * 1.0f, (j * -1.0f) + (((ysize / 2) + 1) * 1.0f), 0), Quaternion.identity);
                    Instantiate(prefab, mappositions[i, j], Quaternion.identity);

                }
            }

            clearMapCheck();
        }

    public void generateYellow()
    {

    }
}
