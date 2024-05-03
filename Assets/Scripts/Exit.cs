using UnityEngine;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    void Start()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                Application.Quit();
            });
        }
        else
        {
            gameObject.SetActive(false);
        }
        
    }
}
