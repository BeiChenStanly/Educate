using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterGame : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(EnterGameOnClick);
    }

    void EnterGameOnClick()
    {
        SceneManager.LoadScene("GameScene");
    }
}
