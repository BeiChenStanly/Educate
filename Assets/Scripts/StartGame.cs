using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    GameObject slider;
    void Start()
    {
        try
        {
            slider = GameObject.Find("Canvas/Slider");
        }
        catch { }
        GetComponent<Button>().onClick.AddListener(()=>
        {
            PlayerPrefs.SetInt("subject", GameObject.Find("Canvas/Dropdown").GetComponent<Dropdown>().value);
            PlayerPrefs.SetInt("playtime", GameObject.Find("Canvas/TimeDropdown").GetComponent<Dropdown>().value);
            try
            {
                PlayerPrefs.SetFloat("waittime", (float)System.Math.Round((decimal)(slider.GetComponent<Slider>().value + 0.5), 2, System.MidpointRounding.AwayFromZero));
            }
            catch { }
            SceneManager.LoadScene("GameScene");
        });
    }
    void Update()
    {
        try { GameObject.Find("Canvas/TimeSeen").GetComponent<Text>().text = $"答完一题的禁用时间：{System.Math.Round((decimal)(slider.GetComponent<Slider>().value + 0.5), 2, System.MidpointRounding.AwayFromZero)}s"; } catch { }
    }
}
