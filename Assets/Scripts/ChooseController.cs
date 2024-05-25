using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class ChooseController : MonoBehaviour
{
    GameObject Double;
    public GameObject Content1;
    public GameObject Content2;
    GameObject Canvas;
    VideoPlayer videoPlayer;
    string[] namelist = {
    "Àî¿£ºÕ",
    "³ÂÒÕ·Æ",
    "ÀîêØÓê",
    "ÑîºÆÈ»",
    "Ö£¹âË·",
    "ÁõÑÇÖŞ",
    "ÊæË¼ç÷",
    "ÀèÈçİæ",
    "Ğ»Ê«åû",
    "Íõ¼Ñ",
    "¼ÖÕğÁÖ",
    "ÕÅÊææÂ",
    "ÀîÃú¬u",
    "¸ßÒ»Äş",
    "ÍõÔÏº­",
    "ÀîŞÈè¯",
    "ÕÔæºè¯",
    "ÀîÕşÁØ",
    "ÃÏÈñº¬",
    "ÑîĞÀÔÃ",
    "²ÜÜÇÃô",
    "Ñîèªî£",
    "´úË¼ºê",
    "´÷×ÓÎÄ",
    "ÕÅÜøÓî",
    "ÉòÁÖ·å",
    "ÕÔÎõÔ´",
    "ÍôĞÀâù",
    "Àîê¿ÜÇ",
    "ÍõÑ©Äş",
    "ÀîÈôİÕ",
    "ËïÁ¢Íú",
    "Áõ²©",
    "ÍõË¼²©",
    "ÕÔÉıî£",
    "¶Å¾©Ì©",
    "ÑîÊæÔÃ",
    "Æ¤Ë¼Ôµ",
    "ÍêÑÕÃ×ÄÈ",
    "ÕÔÕğÑô",
    "Í¿ÏÄ",
    "Íôå··«"
    };
    void Start()
    {
        Double = GameObject.Find("Double");
        Canvas = GameObject.Find("Canvas");
        videoPlayer = Double.GetComponent<VideoPlayer>();
        videoPlayer.url = Application.streamingAssetsPath+ "/double.mp4";
        Canvas.SetActive(false);
        Content1.SetActive(false);
        Content2.SetActive(false);
        videoPlayer.Play();
        videoPlayer.loopPointReached += EndReached;
    }
    void EndReached(VideoPlayer vp)
    {
        Canvas.SetActive(true);
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
