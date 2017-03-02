using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.SceneManagement;

public class PersistentSoundManager : MonoBehaviour {

    public static PersistentSoundManager m_Instance;

    bool b_InitialLoad = false;
    //public List<AudioClip> InitList = new List<AudioClip>();
    public Dictionary<string, AudioClip> AudioList = new Dictionary<string, AudioClip>();

    string CurrentScene;

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
            //foreach (AudioClip aClip in InitList)
            //{
            //    switch (aClip.name)
            //    {
            //        case "battletheme1":
            //            AudioList.Add("BattleTheme_1", aClip);
            //            break;

            //        case "battletheme2":
            //            AudioList.Add("BattleTheme_2", aClip);
            //            break;

            //        case "Free Battle":
            //            AudioList.Add("BattlePrepBGM", aClip);
            //            break;

            //        case "gacha":
            //            AudioList.Add("GachaBGM", aClip);
            //            break;

            //        case "Mode Select":
            //            AudioList.Add("ModeSelectBGM", aClip);
            //            break;

            //        case "Shop":
            //            AudioList.Add("ShopBGM", aClip);
            //            break;

            //        case "Story":
            //            AudioList.Add("StoryBGM", aClip);
            //            break;

            //        case "Title":
            //            AudioList.Add("TitleBGM", aClip);
            //            break;

            //        case "melee_attack":
            //            AudioList.Add("MeleeAttack", aClip);
            //            break;

            //        case "fugo_blast":
            //            AudioList.Add("FugoSkill", aClip);
            //            break;

            //        case "Defend":
            //            AudioList.Add("Defend", aClip);
            //            break;

            //        case "Heal":
            //            AudioList.Add("Heal", aClip);
            //            break;

            //        case "PowerUp":
            //            AudioList.Add("PowerUp", aClip);
            //            break;

            //        case "Walk":
            //            AudioList.Add("Walk", aClip);
            //            break;
            //    }
            //}

            AudioList.Add("BattleTheme_1", Resources.Load("battletheme1", typeof(AudioClip)) as AudioClip);
            AudioList.Add("BattleTheme_2", Resources.Load("battletheme2", typeof(AudioClip)) as AudioClip);
            AudioList.Add("BattlePrepBGM", Resources.Load("Free Battle", typeof(AudioClip)) as AudioClip);
            AudioList.Add("GachaBGM", Resources.Load("gacha", typeof(AudioClip)) as AudioClip);
            AudioList.Add("ModeSelectBGM", Resources.Load("Mode Select", typeof(AudioClip)) as AudioClip);
            AudioList.Add("ShopBGM", Resources.Load("Shop", typeof(AudioClip)) as AudioClip);
            AudioList.Add("StoryBGM", Resources.Load("Story", typeof(AudioClip)) as AudioClip);
            AudioList.Add("TitleBGM", Resources.Load("Title", typeof(AudioClip)) as AudioClip);
            AudioList.Add("MeleeAttack", Resources.Load("melee_attack", typeof(AudioClip)) as AudioClip);
            AudioList.Add("FugoSkill", Resources.Load("fugo_blast", typeof(AudioClip)) as AudioClip);
            AudioList.Add("Defend", Resources.Load("Defend", typeof(AudioClip)) as AudioClip);
            AudioList.Add("Heal", Resources.Load("Heal", typeof(AudioClip)) as AudioClip);
            AudioList.Add("PowerUp", Resources.Load("PowerUp", typeof(AudioClip)) as AudioClip);
            AudioList.Add("Walk", Resources.Load("Walk", typeof(AudioClip)) as AudioClip);
            b_InitialLoad = true;
        }


        PlayBGM();

    }

    void PlayBGM()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "SplashScreen":
                PlaySoundInBackground("TitleBGM");
                break;

            case "MainMenu":
                PlaySoundInBackground("ModeSelectBGM");
                break;

            case "StorySelect":
                PlaySoundInBackground("StoryBGM");
                break;

            case "Shop":
                PlaySoundInBackground("ShopBGM");
                break;

            case "CharandItemSelect":
                PlaySoundInBackground("BattlePrepBGM");
                break;

            case "FreeBattle_1":
                PlaySoundInBackground("BattleTheme_1");
                break;

            case "Story_1":
                PlaySoundInBackground("BattleTheme_2");
                break;
        }

        CurrentScene = SceneManager.GetActiveScene().name;
    }

    public void PlaySoundInBackground(string name)
    {
        if (GameObject.Find("BackgroundSoundPlayer").GetComponent<AudioSource>().isPlaying && SceneManager.GetActiveScene().name != CurrentScene)
            GameObject.Find("BackgroundSoundPlayer").GetComponent<AudioSource>().Stop();

        if (!GameObject.Find("BackgroundSoundPlayer").GetComponent<AudioSource>().isPlaying && SceneManager.GetActiveScene().name == CurrentScene)
        {
            GameObject.Find("BackgroundSoundPlayer").GetComponent<AudioSource>().clip = AudioList[name];
            GameObject.Find("BackgroundSoundPlayer").GetComponent<AudioSource>().Play();
        }
    }

    public void PlaySoundEffect(string name)
    {
        GameObject.Find("EffectSoundPlayer").GetComponent<AudioSource>().clip = AudioList[name];
        GameObject.Find("EffectSoundPlayer").GetComponent<AudioSource>().Play();  
    }

    public void StopSound()
    {
        GameObject.Find("EffectSoundPlayer").GetComponent<AudioSource>().Stop();  
    }
}
