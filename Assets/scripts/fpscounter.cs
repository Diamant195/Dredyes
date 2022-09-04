using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class fpscounter : MonoBehaviour
{
    float pollingtime = 5f;
    float time;
    int frameCount;
    public Text displaytext;
    void Update()
    {
        time += Time.deltaTime;
        frameCount++;
        if (time >= pollingtime)
        {
            int framerate = Mathf.RoundToInt(frameCount / time);
            displaytext.text = "fps: " + framerate.ToString();
        }
    }
}
