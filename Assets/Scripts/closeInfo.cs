using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class closeInfo : MonoBehaviour 
{
    public Image infoPic;

    public void CloseInfo()
    {
        infoPic.enabled = false;
    }
}
