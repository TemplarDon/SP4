using UnityEngine;
using System.Collections;

public class LevelGenerate : MonoBehaviour {

    public GameObject prefab;
    public int[,] mapgrid;
    private int xsize;
    private int ysize;

    // Use this for initialization
    void Start () {
        //xsize = Random.Range(5, 10);
        //ysize = Random.Range(5, 10);
        xsize = 6;
        ysize = 6;
        mapgrid = new int[xsize, ysize];
	}
	
	// Update is called once per frame
	void Update () {
	    if(xsize % 2 == 0)
        {
            for (int j = 0; j < ysize; j++)
            {
                for (int i = 0; i < xsize; i++)
                {
                    Instantiate(prefab, new Vector3(i * 1.0f - ((xsize / 2)) * 1.0f - 0.5f, j * 1.0f, 0), Quaternion.identity);
                }
            }
        }
        else
        {
            for (int i = 0; i < xsize; i++)
            {
                Instantiate(prefab, new Vector3(i * 1.0f - ((xsize / 2) + 1) * 1.0f, 0, 0), Quaternion.identity);
            }
        }
    }
}
