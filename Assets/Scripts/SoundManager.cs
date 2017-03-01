using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class SoundManager : MonoBehaviour {

    public List<AudioClip> InitList = new List<AudioClip>();
    public Dictionary<string, AudioClip> AudioList = new Dictionary<string,AudioClip>();

	// Use this for initialization
	void Start () {
	
        foreach (AudioClip aClip in InitList)
        {
            switch (aClip.name)
            {
                case "melee_attack":
                    AudioList.Add("MeleeAttack", aClip);
                    break;

                case "fugo_blast":
                    AudioList.Add("FugoSkill", aClip);
                    break;

                case "Defend":
                    AudioList.Add("Defend", aClip);
                    break;

                case "Heal":
                    AudioList.Add("Heal", aClip);
                    break;

                case "PowerUp":
                    AudioList.Add("PowerUp", aClip);
                    break;

                case "Walk":
                    AudioList.Add("Walk", aClip);
                    break;
            }
        }

	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void PlaySound(string name)
    {
        this.GetComponent<AudioSource>().clip = AudioList[name];
        this.GetComponent<AudioSource>().Play();
    }
}
