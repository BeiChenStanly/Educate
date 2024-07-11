using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class StartGame : MonoBehaviour
{
    public GameObject Chou;
    public GameObject ChooseDropdown;
    List<GameObject> toggles = new List<GameObject>();
    List<int> parts = new List<int>();
    public GameObject Parts;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            try
            {
                PlayerPrefs.SetInt("subject", GameObject.Find("Canvas/Dropdown").GetComponent<Dropdown>().value);
                PlayerPrefs.SetInt("playtime", GameObject.Find("Canvas/TimeDropdown").GetComponent<Dropdown>().value);
                PlayerPrefs.SetFloat("waittime", (float)System.Math.Round((decimal)(GameObject.Find("Canvas/Slider").GetComponent<Slider>().value + 0.5), 2, System.MidpointRounding.AwayFromZero));
            }
            catch { }
            foreach (Transform child in Parts.transform)
            {
                toggles.Add(child.gameObject);
                if (child.gameObject.GetComponent<Toggle>().isOn)
                {
                    parts.Add(int.Parse(child.gameObject.name));
                }
            }
            if (parts.Count == 0 && SceneManager.GetActiveScene().name == "StartScene")
            {
                Debug.Log("Please select at least one part");
                return;
            }
            PlayerPrefsX.SetIntArray("parts", parts.ToArray());
            if (Chou.GetComponent<Toggle>().isOn)
            {
                PlayerPrefs.SetInt("choose", ChooseDropdown.GetComponent<TMP_Dropdown>().value);
                SceneManager.LoadScene("ChooseScene");
            }
            else
            {
                SceneManager.LoadScene("GameScene");
            }
        });
    }
}
