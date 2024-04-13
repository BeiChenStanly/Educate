using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleEnd : MonoBehaviour
{
    public TextAsset physics;
    public TextAsset chinese;
    public TextAsset chemistry;
    int score1;
    int[] wrong1;
    float accuracy1;
    GameObject Score1;
    GameObject WrongSeen1;
    GameObject Accuracy1;
    void Start()
    {
        WrongSeen1 = GameObject.Find("Canvas/Scroll View/Viewport/Content");
        Dictionary<int, TextAsset> stp = new Dictionary<int, TextAsset>()
        {
            {0,physics },
            {1,chinese},
            {2,chemistry}
        };
        Dictionary<string, string> questions = new Dictionary<string, string>();
        NewData data = JsonUtility.FromJson<NewData>(stp[PlayerPrefs.GetInt("subject")].text);
        foreach (NewQAPair item in data.questions)
        {
            questions[item.question] = item.answer;
        }
        wrong1 = PlayerPrefsX.GetIntArray("Wrong");
        score1 = PlayerPrefs.GetInt("Score");
        accuracy1 = PlayerPrefs.GetFloat("Accuracy");
        Score1 = GameObject.Find("Canvas/Score1");
        Accuracy1 = GameObject.Find("Canvas/Accuracy1");
        Score1.GetComponent<Text>().text = score1.ToString();
        Accuracy1.GetComponent<Text>().text = $"Accuracy:{Math.Round(accuracy1, 4) * 100}%";
        List<string> wrong1List = new List<string>();
        int m = 1;
        string wrong1Text = "No wrong answer!";
        if (wrong1[0]!= -1)
        {
            foreach (int i in wrong1)
            {
                wrong1List.Add(m++.ToString() + "." + data.questions[i].question + " : " + questions[data.questions[i].question]);
                wrong1Text = string.Join("\n", wrong1List);
            }
        }
        WrongSeen1.GetComponent<Text>().text = wrong1Text;
    }
}
