using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlinkingStars : MonoBehaviour 
{

    public float timer;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.5f && timer < 3.8f)
        {
            
            StartCoroutine(FadeTextToZeroAlpha(1f, GetComponent<Image>()));
        }
        if (timer >= 4.0f)
        {
            StartCoroutine(FadeTextToFullAlpha(1f, GetComponent<Image>()));
            timer = 0.0f;
        }
 
    }
    public IEnumerator FadeTextToFullAlpha(float t, Image i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, Image i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
 
}
