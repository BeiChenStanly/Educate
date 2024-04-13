using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEnd : MonoBehaviour
{
    public TextAsset physics;
    public TextAsset chinese;
    public TextAsset chemistry;
    int score1;
    int score2;
    float accuracy1;
    float accuracy2;
    GameObject Score1;
    GameObject Score2;
    GameObject Accuracy1;
    GameObject Accuracy2;
    GameObject Winner1;
    GameObject Winner2;
    GameObject Text1;
    GameObject Text2;
    int[] wrong1;
    int[] wrong2;
    void Start()
    {
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
        score1 = PlayerPrefs.GetInt("Score1");
        score2 = PlayerPrefs.GetInt("Score2");
        accuracy1 = PlayerPrefs.GetFloat("Accuracy1");
        accuracy2 = PlayerPrefs.GetFloat("Accuracy2");
        wrong1 = PlayerPrefsX.GetIntArray("Wrong1");
        wrong2 = PlayerPrefsX.GetIntArray("Wrong2");
        Score1 = GameObject.Find("Canvas/Score1");
        Score2 = GameObject.Find("Canvas/Score2");
        Accuracy1 = GameObject.Find("Canvas/Accuracy1");
        Accuracy2 = GameObject.Find("Canvas/Accuracy2");
        Winner1 = GameObject.Find("Canvas/Winner1");
        Winner2 = GameObject.Find("Canvas/Winner2");
        Text1 = GameObject.Find("Canvas/Scroll View/Viewport/Content");
        Text2 = GameObject.Find("Canvas/Scroll View2/Viewport/Content");
        Score1.GetComponent<Text>().text = score1.ToString();
        Score2.GetComponent<Text>().text = score2.ToString();
        Accuracy1.GetComponent<Text>().text = $"Accuracy:{Math.Round(accuracy1, 4)*100}%";
        Accuracy2.GetComponent<Text>().text = $"Accuracy:{Math.Round(accuracy2, 4)*100}%";

        if (score1 != score2)
        {
            if (score1 > score2)
            {
                Winner2.SetActive(false);
            }
            else
            {
                Winner1.SetActive(false);
            }
        }
        else
        {
            if (accuracy1 > accuracy2)
            {
                Winner2.SetActive(false);
            }
            else if (accuracy1 < accuracy2)
            {
                Winner1.SetActive(false);
            }
        }
        
        List<string> wrong1List = new List<string>();
        List<string> wrong2List = new List<string>();
        int m = 1;
        string wrong1Text = "No wrong answer!";
        string wrong2Text = "No wrong answer!";
        if (wrong1[0] != -1)
        {
            foreach (int i in wrong1)
            {
                wrong1List.Add(m++.ToString() + "." + data.questions[i].question + " : " + questions[data.questions[i].question]);
                wrong1Text = string.Join("\n", wrong1List);
            }
        }
        Text1.GetComponent<Text>().text = wrong1Text;
        m = 1;
        if (wrong2[0] != -1)
        {
            foreach (int i in wrong2)
            {
                wrong2List.Add(m++.ToString() + "." + data.questions[i].question + " : " + questions[data.questions[i].question]);
                wrong2Text = string.Join("\n", wrong2List);
            }
        }
        Text2.GetComponent<Text>().text = wrong2Text;

    }
}
