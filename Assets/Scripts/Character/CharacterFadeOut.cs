using UnityEngine;
using System.Collections;

public class CharacterFadeOut : MonoBehaviour {

    public float FadeoutTime = 1;
    public bool StartFade = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (StartFade)
           StartCoroutine(Fade());
	}

    public IEnumerator Fade()
    {
        // a == 1, can see
        // a == 0, cant see

        this.GetComponent<SpriteRenderer>().color = new Color(this.GetComponent<SpriteRenderer>().color.r, this.GetComponent<SpriteRenderer>().color.g, this.GetComponent<SpriteRenderer>().color.b, 1);
        while (this.GetComponent<SpriteRenderer>().color.a >= 0.0f)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(this.GetComponent<SpriteRenderer>().color.r, this.GetComponent<SpriteRenderer>().color.g, this.GetComponent<SpriteRenderer>().color.b, this.GetComponent<SpriteRenderer>().color.a - (Time.deltaTime / FadeoutTime));
            yield return null;
        }

        if (this.GetComponent<SpriteRenderer>().color.a < 0.0f)
            StartFade = false;
    }
}
