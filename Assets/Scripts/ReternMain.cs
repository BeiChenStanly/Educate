using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReternMain : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => {
            try
            {
                PlayerPrefs.SetInt("subject", GameObject.Find("Canvas/Dropdown").GetComponent<Dropdown>().value);
                PlayerPrefs.SetInt("playtime", GameObject.Find("Canvas/TimeDropdown").GetComponent<Dropdown>().value);
            }
            catch
            {

            }
            SceneManager.LoadScene("StartScene"); 
        });
    }
}
