using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropFound : MonoBehaviour
{
    public string content;
    void Start()
    {
        try
        {
            GetComponent<Dropdown>().value = PlayerPrefs.GetInt(content);
        }
        catch
        {
            GetComponent<TMP_Dropdown>().value = PlayerPrefs.GetInt(content);
        }
    }
}
