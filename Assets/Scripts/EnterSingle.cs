using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterSingle : MonoBehaviour
{
    GameObject slider;
    void Start()
    {
        try
        {
            slider = GameObject.Find("Canvas/Slider");
        }
        catch { }
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(()=>
        {
            PlayerPrefs.SetInt("subject", GameObject.Find("Canvas/Dropdown").GetComponent<Dropdown>().value);
            PlayerPrefs.SetInt("playtime", GameObject.Find("Canvas/TimeDropdown").GetComponent<Dropdown>().value);
            try
            {
                PlayerPrefs.SetFloat("waittime", (float)System.Math.Round((decimal)(slider.GetComponent<UnityEngine.UI.Slider>().value + 0.5), 2, System.MidpointRounding.AwayFromZero));
            }
            catch { }
            SceneManager.LoadScene("SingleScene");
        });
    }
    void Update()
    {
        try { GameObject.Find("Canvas/TimeSeen").GetComponent<Text>().text = $"答完一题的禁用时间：{System.Math.Round((decimal)(slider.GetComponent<UnityEngine.UI.Slider>().value + 0.5), 2, System.MidpointRounding.AwayFromZero)}s"; } catch { }
    }
}
