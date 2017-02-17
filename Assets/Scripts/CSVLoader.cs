﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System;

public class CSVLoader : MonoBehaviour
{

    public TextAsset csvFile; // Reference of CSV file
    public int[,] loadedMap;
    public int newxsize;
    public int newysize;
    private char lineSeperater = '\n'; // It defines line seperate character
    private char fieldSeperator = ','; // It defines field seperate chracter
    public LevelGenerate LevelLoad;

    // Use this for initialization
    void Awake()
    {

        string[] records = csvFile.text.Split(lineSeperater);
        int i = 0;
        int j = 0;
        foreach (string record in records)
        {
            if (record == "")
            {
                break;
            }
            string[] fields = record.Split(fieldSeperator);
            foreach (string field in fields)
            {
                i++;
            }
            newxsize = i;
            i = 0;
            j++;
        }
        //newxsize = i;
        newysize = j;
        //Debug.Log(newxsize + " " + newysize);

        loadedMap = new int[newxsize, newysize];
        readData();

        //LevelLoad.generateLoadMap(loadedMap, newxsize, newysize);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void readData()
    {
        string[] records = csvFile.text.Split(lineSeperater);
        int i = 0;
        int j = 0;
        foreach (string record in records)
        {
            if (record == "")
            {
                break;
            }

            string[] fields = record.Split(fieldSeperator);
            foreach (string field in fields)
            {
                //Debug.Log(j);
                loadedMap[i,j] = Convert.ToInt32(field);
                i++;
            }
            
            i = 0;
            j++;
        }
    }
}