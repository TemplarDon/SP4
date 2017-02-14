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
         */

        checkPossibleLoc(tester.mapposx, tester.mapposy);
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

    void checkPossibleLoc(int i, int j)
    {
        for (int i2 = i; i2 < xsize; i2++)
        {
            if(mapgrid[i2, j] < 4)
            {
                mapcheck[i2, j] = true;
            }
            else
            {
                break;
            }
        }

        for (int i3 = i; i3 >= 0; i3--)
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

        for (int j2 = j; j2 < ysize; j2++)
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

        for (int j3 = j; j3 >= 0; j3--)
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

    Vector3 getTilePos(int i, int j)
    {
        return mappositions[i, j];
    }

}