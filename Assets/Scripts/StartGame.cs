using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class StartGame : MonoBehaviour
{
    List<GameObject> toggles = new List<GameObject>();
    List<int> parts = new List<int>();
    GameObject Canvas;
    void Start()
    {
        Canvas = GameObject.Find("Canvas");
        GetComponent<Button>().onClick.AddListener(() =>
        {
            try
            {
                PlayerPrefs.SetInt("subject", GameObject.Find("Canvas/Dropdown").GetComponent<Dropdown>().value);
                PlayerPrefs.SetInt("playtime", GameObject.Find("Canvas/TimeDropdown").GetComponent<Dropdown>().value);
                PlayerPrefs.SetFloat("waittime", (float)System.Math.Round((decimal)(GameObject.Find("Canvas/Slider").GetComponent<Slider>().value + 0.5), 2, System.MidpointRounding.AwayFromZero));
            }
            catch { }
            foreach (Transform child in Canvas.transform)
            {
                if (child.tag == "Toggle")
                {
                    toggles.Add(child.gameObject);
                    if (child.gameObject.GetComponent<Toggle>().isOn)
                    {
                        parts.Add(int.Parse(child.gameObject.name));
                    }
                }
            }
            if (parts.Count == 0&&SceneManager.GetActiveScene().name=="StartScene")
            {
                Debug.Log("Please select at least one part");
                return;
            }
            PlayerPrefsX.SetIntArray("parts", parts.ToArray());
            SceneManager.LoadScene("GameScene");
        });
    }
}
