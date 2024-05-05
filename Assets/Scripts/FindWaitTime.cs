using UnityEngine;

public class FindWaitTime : MonoBehaviour
{
    float waittime;
    GameObject slider;
    void Start()
    {
        waittime = PlayerPrefs.GetFloat("waittime");
        slider = GameObject.Find("Canvas/Slider");
        slider.GetComponent<UnityEngine.UI.Slider>().value = waittime - 0.5f;
    }
}
