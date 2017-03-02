using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

using UnityEditor.SceneManagement;


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

        if (SceneManager.GetActiveScene().name == "StorySelect")
        {
            PersistentData.m_Instance.CurrentGameMode = PersistentData.GAME_MODE.STORY;
        }
        else
        {
            PersistentData.m_Instance.CurrentGameMode = PersistentData.GAME_MODE.FREE_BATTLE;
        }
    }
}
