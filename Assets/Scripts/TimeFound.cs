using UnityEngine;
using UnityEngine.UI;

public class TimeFound : MonoBehaviour
{
    void Start()
    {
        GetComponent<Dropdown>().value = PlayerPrefs.GetInt("playtime");
    }
}
