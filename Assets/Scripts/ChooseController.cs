using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class ChooseController : MonoBehaviour
{
    public GameObject Double;
    public GameObject Content1;
    public GameObject Content2;
    public GameObject Canvas;
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
    "��巷�"
    };
    int choose;
    enum Urls
    {
        yuanshen = 0,
        moon = 1,
        contact = 2
    }
    void Start()
    {
        choose = PlayerPrefs.GetInt("choose");
        print(choose);
        videoPlayer = Double.GetComponent<VideoPlayer>();
        print(Application.streamingAssetsPath+ $"/{((Urls)choose).ToString()}.mp4");
        videoPlayer.url = Application.streamingAssetsPath+ $"/{((Urls)choose).ToString()}.mp4";
        videoPlayer.Play();
        videoPlayer.loopPointReached += EndReached;
    }
    void EndReached(VideoPlayer vp)
    {
        Double.SetActive(false);
        foreach(Transform child in Canvas.transform)
        {
            if(child.gameObject.tag == "Finish")
            {
                child.gameObject.SetActive(true);
            }
        }
        int index = Random.Range(0, namelist.Length);
        GameObject.Find("Canvas/Content1/Text1").GetComponent<TextMeshProUGUI>().text = "<rotate=90>" + namelist[index];
        GameObject.Find("Canvas/Content1/Text2").GetComponent<TextMeshProUGUI>().text = "<rotate=90>" + namelist[index];
        int index2;
        do { index2 = Random.Range(0, namelist.Length); }
        while (index2 == index);
        GameObject.Find("Canvas/Content2/Text1").GetComponent<TextMeshProUGUI>().text = "<rotate=90>" + namelist[index2];
        GameObject.Find("Canvas/Content2/Text2").GetComponent<TextMeshProUGUI>().text = "<rotate=90>" + namelist[index2];
    }
}
