using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class VolumeCtrl : MonoBehaviour 
{
    public Slider volumeSlider;
    void Awake()
    {
        //if there is a slider, set volume to slider
        if (volumeSlider)
        {
            //GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("CurVol");
            volumeSlider.value = PlayerPrefs.GetFloat("CurVol");
        }

        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("CurVol");
    }
	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () 
    {
        //if (Input.GetKeyUp(KeyCode.A))
        //{
        //    GetComponent<AudioSource>().volume += 10;
        //    PlayerPrefs.SetFloat("CurVol", GetComponent<AudioSource>().volume);
        //}

        //if (Input.GetKeyUp(KeyCode.D))
        //{
        //    GetComponent<AudioSource>().volume -= 10;
        //    PlayerPrefs.SetFloat("CurVol", GetComponent<AudioSource>().volume);
        //}
	}
    public void VolumeControl(float volumeControl)
    {
        GetComponent<AudioSource>().volume = volumeControl;
        PlayerPrefs.SetFloat("CurVol", GetComponent<AudioSource>().volume);
    }
}
