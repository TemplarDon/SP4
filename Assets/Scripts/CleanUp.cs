using UnityEngine;
using System.Collections;

public class CleanUp : MonoBehaviour {

    public double d_LifeTime = 1.0;
    double d_LifeTimer = 0.0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (d_LifeTimer < d_LifeTime)
        {
            d_LifeTimer += Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
	}
}
