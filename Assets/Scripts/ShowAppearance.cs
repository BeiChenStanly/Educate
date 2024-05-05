using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SubjectType
{
    physics,
    chinese,
    chemistry
}
public class ShowAppearance : MonoBehaviour
{
    public static Dictionary<int, string> chineseparts = new Dictionary<int, string>()
{
    {0 ,"�����Ķ�"} ,
    {1 ,"����ʶ�����޸�"}
};
    public static Dictionary<int, string> physicalparts = new Dictionary<int, string>()
{
    {0 ,"�ڶ��� �˶�������" },
    {1 ,"������ ��������"},
    {2 ,"������ ��ʵĹ�"},
    {3 ,"������ �������ܶ�"},
    {4 ,"������ ��Ϥ��İ������"},
    {5 ,"������ �����˶�"},
    {6 ,"�ڰ��� ѹǿ"},
    {7 ,"�ھ��� ����"},
    {8 ,"��ʮ�� ��е����"},
    {9 ,"��ʮһ�� С�����������"},
    {10 ,"��ʮ���� �¶�����̬�仯"},
    {11 ,"��ʮ���� �������Ȼ�"},
    {12 ,"��ʮ���� �˽��·"},
    {13 ,"��ʮ���� ̽����·"},
    {14 ,"��ʮ���� ����������繦��"},
    {15 ,"��ʮ���� ��ָ���뵽�������г�"},
    {16 ,"��ʮ���� ���������"}
};
    public static Dictionary<int, string> chemicalparts = new Dictionary<int, string>()
{
    {0 ,"��һ��Ԫ �߽���ѧ����" },
    {1 ,"�ڶ���Ԫ ������Χ�Ŀ���" },
    {2 ,"������Ԫ ���ʹ��ɵİ���" },
    {3 ,"���ĵ�Ԫ ��Ȼ���ˮ" },
    {4 ,"���嵥Ԫ ��ѧ����ʽ" },
    {5 ,"������Ԫ ̼��̼��������" },
    {6 ,"���ߵ�Ԫ ȼ�ϼ�������" },
    {7 ,"�ڰ˵�Ԫ �����ͽ�������" },
    {8 ,"��ʮ��Ԫ ��ͼ�" },
    {9 ,"��ʮһ��Ԫ �� ����" },
    {10 ,"��ʮ����Ԫ ��ѧ������" }
};
    public static Dictionary<int, Dictionary<int, string>> fdic = new Dictionary<int, Dictionary<int, string>>()
{
    {0, physicalparts},
    {1, chineseparts},
    {2, chemicalparts}
};
    public GameObject toggle;
    GameObject timeSeen;
    GameObject slider;
    GameObject dropdown;
    int[] parts;
    GameObject Canvas;
    void Start()
    {
        timeSeen = GameObject.Find("Canvas/TimeSeen");
        slider = GameObject.Find("Canvas/Slider");
        Canvas = GameObject.Find("Canvas");
        dropdown = GameObject.Find("Canvas/Dropdown");
        parts = PlayerPrefsX.GetIntArray("parts");
        for (int i = 0; i < fdic[dropdown.GetComponent<Dropdown>().value].Count; i++)
        {
            GameObject obj = Instantiate(toggle, transform);
            obj.transform.SetParent(Canvas.transform);
            obj.transform.localPosition = transform.localPosition+new Vector3(0, -i*50, 0);
            obj.transform.localScale = transform.localScale;
            obj.transform.name = i.ToString();
            for (int j = 0; j < parts.Length; j++)
            {
                if (i == parts[j])
                {
                    obj.GetComponent<Toggle>().isOn = true;
                    break;
                }
                else
                {
                    obj.GetComponent<Toggle>().isOn = false;
                }
            }
            obj.GetComponentInChildren<Text>().text = fdic[dropdown.GetComponent<Dropdown>().value][i];
        }
    }
    void Update()
    {
        timeSeen.GetComponent<Text>().text = $"����һ��Ľ���ʱ�䣺{System.Math.Round((decimal)(slider.GetComponent<Slider>().value + 0.5), 2, System.MidpointRounding.AwayFromZero)}s"; //��ʾ��ǰ����ʱ��
        if (slider.GetComponent<Slider>().value == 0)
        {
            timeSeen.GetComponent<Text>().color = Color.red; //����ʱ��Ϊ0ʱ��ʾ��ɫ
        }
        else
        {
            timeSeen.GetComponent<Text>().color = Color.white; //����ʱ�䲻Ϊ0ʱ��ʾ��ɫ
        }
    }
    public void OnDestroy()
    {
        try
        {
            //�������toggle
            foreach (Transform child in Canvas.transform)
            {
                if (child.tag == "Toggle")
                    Destroy(child.gameObject);
            }
            //�½���һ��ѧ�Ƶ�toggle
            for (int i = 0; i < fdic[dropdown.GetComponent<Dropdown>().value].Count; i++)
            {
                GameObject obj = Instantiate(toggle, transform);
                obj.transform.SetParent(Canvas.transform);
                obj.transform.localPosition = transform.localPosition + new Vector3(0, -i * 50, 0);
                obj.transform.localScale = transform.localScale;
                obj.transform.name = i.ToString();
                obj.GetComponentInChildren<Text>().text = fdic[dropdown.GetComponent<Dropdown>().value][i];
            }
        }
        catch { }
    }
}
