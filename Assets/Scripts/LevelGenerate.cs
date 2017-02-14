using UnityEngine;
using System.Collections;

public class LevelGenerate : MonoBehaviour {

    public TestingPlayer tester;
    public GameObject prefab;
    public Sprite tile_grass, tile_stone, tile_spike, tile_rock, tile_water;
    public int[,] mapgrid;
    private bool[,] mapcheck;
    private Vector3[,] mappositions;
    public int xsize;
    public int ysize;
    public GameObject possibleLoc;

    /* List of tiles:
     * 1 [NORMAL] Grass Tile
     * 2 [NORMAL] Stone Tile
     * 3 [DEBUFF] Spiked Tile
     * 4 [OBSTACLE] Rock Tile
     * 5 [OBSTAcLE] Water Tile
    */

    // Use this for initialization
    void Start () {
        xsize = Random.Range(7, 14);
        ysize = Random.Range(6, 11);
        //xsize = 6;
        //ysize = 6;
        mapgrid = new int[xsize, ysize];
        mapcheck = new bool[xsize, ysize];
        mappositions = new Vector3[xsize, ysize];

        prefab = GameObject.Find("Tile");

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
            while(mapgrid[tempi, tempj] >= 4)
            {
                tempi++;
                if(tempi == xsize - 1)
                {
                    tempi = 0;
                    tempj++;
                    if(tempj == ysize - 1)
                    {
                        break;
                    }
                }
            }

            validateMapCheck(tempi, tempj);

            for(int j = 0; j < ysize; j++)
            {
                for(int i = 0; i < xsize; i++)
                {
                    if(mapcheck[i, j] == false)
                    {
                        if(i == 0)
                        {
                            if (Random.value >= 0.5)
                                mapgrid[i + 1, j] = Random.Range(1, 4);
                            if (j == 0)
                            {
                                if (Random.value >= 0.5)
                                    mapgrid[i, j + 1] = Random.Range(1, 4);
                            }
                            else if(j == ysize - 1)
                            {
                                if (Random.value >= 0.5)
                                    mapgrid[i, j - 1] = Random.Range(1, 4);
                            }
                        }
                        else if(i == xsize - 1)
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
                    Instantiate(prefab, mappositions[i,j], Quaternion.identity);

                }
            }

            clearMapCheck();
            mapConfirmed = true;
        }
    }
	
	// Update is called once per frame
	void Update () {

        clearMapCheck();

        /* POSSIBLE MOVE PATTERNS
         * 1 PLUS-CROSS
         * 2 X-CROSS
         * 3 SQUARE
         * 4 DIAMOND
         * 5 QUEEN ( PLUS-X-CROSS )
         * 6 STEP
         */

        checkPossibleLoc(tester.mapposx, tester.mapposy, 6, 5);
        //Debug.Log(tester.mapposy);

        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("yellowSq");
        foreach (GameObject obj in allObjects)
        {
            if(obj.name == "CurrentLoc(Clone)" || obj.name == "Tile(Clone)")
                Destroy(obj);
        }

        for (int j = 0; j < ysize; j++)
        {
            for (int i = 0; i < xsize; i++)
            {
                if (mapcheck[i,j] == true && mapgrid[i,j] < 4)
                   Instantiate(possibleLoc, mappositions[i,j], Quaternion.identity);
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

    void checkPossibleLoc(int i, int j, int type, int range)
    {
        switch(type)
        {
            case 1:
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
                checkPossibleLocStep(i, j, range + 1);
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
        return new Vector3(0,0,0);
    }

}