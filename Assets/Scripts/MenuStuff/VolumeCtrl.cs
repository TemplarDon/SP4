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
            GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("CurVol");
         volumeSlider.value = GetComponent<AudioSource>().volume;
        }
    }
	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () 
    {

	}
    public void VolumeControl(float volumeControl)
    {
        GetComponent<AudioSource>().volume = volumeControl;
        PlayerPrefs.SetFloat("CurVol", GetComponent<AudioSource>().volume);
    }
}
