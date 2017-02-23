using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Fade : MonoBehaviour 
{
    public float timer;
    public Image image;
    public string sceneName;
    // Use this for initialization
    void Start()
    {
        image = GetComponent<Image>();

        Color c = image.color;
        c.a = 0;
        image.color = c;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            timer += Time.deltaTime;

            if (timer >= 0)
            {
                StartCoroutine(FadeTextToFullAlpha(1f, GetComponent<Image>()));
                //timer = 0;
            }

        }
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
    public IEnumerator FadeTextToFullAlpha(float t, Image i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime));
            yield return null;
        }
        if (i.color.a >= 1.0f)
        {
            //SceneManager.LoadScene("Story_MainMenu");
            ChangeScene();
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, Image i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime));
            yield return null;
        }
    }

   
}
