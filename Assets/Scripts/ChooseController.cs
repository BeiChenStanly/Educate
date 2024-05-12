using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class ChooseController : MonoBehaviour
{
    GameObject Double;
    public GameObject Background;
    public GameObject Content1;
    public GameObject Content2;
    GameObject Canvas;
    VideoPlayer videoPlayer;
    string[] namelist = {
    "���",
    "���շ�",
    "������",
    "���Ȼ",
    "֣��˷",
    "������",
    "��˼��",
    "������",
    "лʫ��",
    "����",
    "������",
    "������",
    "�����u",
    "��һ��",
    "���Ϻ�",
    "�����",
    "����",
    "������",
    "����",
    "������",
    "������",
    "����",
    "��˼��",
    "������",
    "������",
    "���ַ�",
    "����Դ",
    "������",
    "�����",
    "��ѩ��",
    "������",
    "������",
    "����",
    "��˼��",
    "�����",
    "�ž�̩",
    "������",
    "Ƥ˼Ե",
    "��������",
    "������",
    "Ϳ��",
    "������"
    };
    void Start()
    {
        Double = GameObject.Find("Double");
        Canvas = GameObject.Find("Canvas");
        videoPlayer = Double.GetComponent<VideoPlayer>();
        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, "double.mp4");
        Canvas.SetActive(false);
        Content1.SetActive(false);
        Content2.SetActive(false);
        videoPlayer.Play();
        Background.SetActive(false);
        videoPlayer.loopPointReached += EndReached;
    }
    void EndReached(VideoPlayer vp)
    {
        Canvas.SetActive(true);
        Background.SetActive(true);
        Content1.SetActive(true);
        Content2.SetActive(true);
        int index = Random.Range(0, namelist.Length);
        GameObject.Find("Canvas/Content1/Text1").GetComponent<TextMeshProUGUI>().text = "<rotate=90>" + namelist[index];
        GameObject.Find("Canvas/Content1/Text2").GetComponent<TextMeshProUGUI>().text = "<rotate=90>" + namelist[index];
        index = Random.Range(0, namelist.Length);
        GameObject.Find("Canvas/Content2/Text1").GetComponent<TextMeshProUGUI>().text = "<rotate=90>" + namelist[index];
        GameObject.Find("Canvas/Content2/Text2").GetComponent<TextMeshProUGUI>().text = "<rotate=90>" + namelist[index];
    }
}
