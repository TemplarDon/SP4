using UnityEngine;
//using UnityEngine.UI;
using System.Collections;

public class DebugScript : MonoBehaviour
{

    //public Text dbgtxt;
    public GameObject prefab;

    // Use this for initialization
    void Start()
    {
        //for (int i = 0; i < 10; i++)
        //    Instantiate(prefab, new Vector3(i * 2.0f, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_ANDROID
            Touch myTouch = Input.GetTouch(0);
            newpos = new Vector3(myTouch.position.x, myTouch.position.y, 1);
#else
        //newpos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 1);
#endif


    }

    public void ClickA()
    {

    }

    public void ClickB()
    {
        //audio1.PlayOneShot(impact, 0.7F);
        //dbgtxt.text = "B Clicked";
    }
}