﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadScenes : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update ()
    {

	}
    public void Loadscene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
