using UnityEngine;
using UnityEngine.UI;

public class DropFound : MonoBehaviour
{
    void Start()
    {
        GetComponent<Dropdown>().value = PlayerPrefs.GetInt("subject");
    }
}
