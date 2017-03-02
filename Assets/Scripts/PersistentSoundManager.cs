using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersistentSoundManager : MonoBehaviour {

    public static PersistentSoundManager m_Instance;

    bool b_InitialLoad = false;
    public List<AudioClip> InitList = new List<AudioClip>();
    public Dictionary<string, AudioClip> AudioList = new Dictionary<string, AudioClip>();

    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (!m_Instance)
        {
            m_Instance = this;
        }
        else if (m_Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!b_InitialLoad)
        {
            foreach (AudioClip aClip in InitList)
            {
                switch (aClip.name)
                {

                    case "gacha":
                        AudioList.Add("GachaBGM", aClip);
                        break;

                    case "Mode Select":
                        AudioList.Add("ModeSelectBGM", aClip);
                        break;

                    case "Shop":
                        AudioList.Add("ShopBGM", aClip);
                        break;

                    case "Story":
                        AudioList.Add("StoryBGM", aClip);
                        break;

                    case "Title":
                        AudioList.Add("TitleBGM", aClip);
                        break;

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

            b_InitialLoad = true;
        }
    }

    public void PlaySound(string name)
    {
        this.GetComponent<AudioSource>().clip = AudioList[name];
        this.GetComponent<AudioSource>().Play();
    }

}
