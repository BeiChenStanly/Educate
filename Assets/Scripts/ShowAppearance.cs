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
    {0 ,"名著阅读"} ,
    {1 ,"病句识别与修改"}
};
    public static Dictionary<int, string> physicalparts = new Dictionary<int, string>()
{
    {0 ,"第二章 运动的世界" },
    {1 ,"第三章 声的世界"},
    {2 ,"第四章 多彩的光"},
    {3 ,"第五章 质量与密度"},
    {4 ,"第六章 熟悉而陌生的力"},
    {5 ,"第七章 力与运动"},
    {6 ,"第八章 压强"},
    {7 ,"第九章 浮力"},
    {8 ,"第十章 机械与人"},
    {9 ,"第十一章 小粒子与大宇宙"},
    {10 ,"第十二章 温度与物态变化"},
    {11 ,"第十三章 内能与热机"},
    {12 ,"第十四章 了解电路"},
    {13 ,"第十五章 探究电路"},
    {14 ,"第十六章 电流做功与电功率"},
    {15 ,"第十七章 从指南针到磁悬浮列车"},
    {16 ,"第十八章 电从哪里来"}
};
    public static Dictionary<int, string> chemicalparts = new Dictionary<int, string>()
{
    {0 ,"第一单元 走进化学世界" },
    {1 ,"第二单元 我们周围的空气" },
    {2 ,"第三单元 物质构成的奥秘" },
    {3 ,"第四单元 自然界的水" },
    {4 ,"第五单元 化学方程式" },
    {5 ,"第六单元 碳和碳的氧化物" },
    {6 ,"第七单元 燃料及其利用" },
    {7 ,"第八单元 金属和金属材料" },
    {8 ,"第十单元 酸和碱" },
    {9 ,"第十一单元 盐 化肥" },
    {10 ,"第十二单元 化学与生活" }
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
        timeSeen.GetComponent<Text>().text = $"答完一题的禁用时间：{System.Math.Round((decimal)(slider.GetComponent<Slider>().value + 0.5), 2, System.MidpointRounding.AwayFromZero)}s"; //显示当前禁用时间
        if (slider.GetComponent<Slider>().value == 0)
        {
            timeSeen.GetComponent<Text>().color = Color.red; //禁用时间为0时显示红色
        }
        else
        {
            timeSeen.GetComponent<Text>().color = Color.white; //禁用时间不为0时显示白色
        }
    }
    public void OnDestroy()
    {
        try
        {
            //清除所有toggle
            foreach (Transform child in Canvas.transform)
            {
                if (child.tag == "Toggle")
                    Destroy(child.gameObject);
            }
            //新建另一个学科的toggle
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
