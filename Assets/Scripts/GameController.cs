using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum E_difficuly
{
    Easy,
    Normal,
    Hard,
    Impossible
}

[Serializable]
public class QAPair
{
    public string question;
    public string answer;
    public E_difficuly difficuly;
}

[Serializable]
public class Data
{
    public List<QAPair> questions;
}
public class GameController : MonoBehaviour
{
    public TextAsset physics;
    public TextAsset chinese;
    public TextAsset chemistry;
    float time;
    float playtime;
    GameObject T1;
    GameObject F1;
    GameObject T2;
    GameObject F2;
    GameObject Question1;
    GameObject Question2;
    GameObject Score1;
    GameObject Score2;
    GameObject GameTime;
    GameObject Correct;
    GameObject Wrong;
    GameObject Difficulty1;
    GameObject Difficulty2;
    Dictionary<string, string> questions = new Dictionary<string, string>();
    Dictionary<string, E_difficuly> difficulties = new Dictionary<string, E_difficuly>();
    int[] ran1;
    int[] ran2;
    int i1 = 0;
    int i2 = 0;
    int score1;
    int score2;
    int total1;
    int total2;
    int right1;
    int right2;
    List<int> wrong1 = new List<int>();
    List<int> wrong2 = new List<int>();
    float waittime = 1;
    void Start()
    {
        playtime = PlayerPrefs.GetInt("playtime") * 30 + 30;
        waittime = PlayerPrefs.GetFloat("waittime");
        Dictionary<int, TextAsset> stp = new Dictionary<int, TextAsset>()
        {
            {0,physics },
            {1,chinese},
            {2,chemistry}
        };
        time = Time.time;
        Data data = JsonUtility.FromJson<Data>(stp[PlayerPrefs.GetInt("subject")].text);
        foreach (QAPair item in data.questions)
        {
            questions[item.question] = item.answer;
            difficulties[item.question] = item.difficuly;
        }
        T1 = GameObject.Find("T1");
        F1 = GameObject.Find("F1");
        T2 = GameObject.Find("T2");
        F2 = GameObject.Find("F2");
        Wrong = GameObject.Find("Wrong");
        Correct = GameObject.Find("Correct");
        GameTime = GameObject.Find("Time");
        Score1 = GameObject.Find("Canvas/Score1");
        Score2 = GameObject.Find("Canvas/Score2");
        Question1 = GameObject.Find("Canvas/Question1");
        Question2 = GameObject.Find("Canvas/Question2");
        Difficulty1 = GameObject.Find("Canvas/Difficulty1");
        Difficulty2 = GameObject.Find("Canvas/Difficulty2");
        T1.GetComponent<Button>().onClick.AddListener(OnClick1);
        F1.GetComponent<Button>().onClick.AddListener(OnClick2);
        T2.GetComponent<Button>().onClick.AddListener(OnClick3);
        F2.GetComponent<Button>().onClick.AddListener(OnClick4);
        ran1 = OutputRandom(0, questions.Count - 1, 200);
        ran2 = OutputRandom(0, questions.Count - 1, 200);
    }

    void Update()
    {
        try
        {
            Question1.GetComponent<Text>().text = questions.ElementAt(ran1[i1]).Key;
            Difficulty1.GetComponent<Text>().text = "难度：" + difficulties.ElementAt(ran1[i1]).Value.ToString();
        }
        catch (IndexOutOfRangeException)
        {
            i1 = 0;
        }
        try
        {
            Question2.GetComponent<Text>().text = questions.ElementAt(ran2[i2]).Key;
            Difficulty2.GetComponent<Text>().text = "难度：" + difficulties.ElementAt(ran2[i2]).Value.ToString();
        }
        catch (IndexOutOfRangeException)
        {

            i2 = 0;
        }
        Score1.GetComponent<Text>().text = $"Score:{score1}";
        Score2.GetComponent<Text>().text = $"Score:{score2}";
        GameTime.GetComponent<Text>().text = $"Time:{Math.Round(playtime - Time.time + time, 2)}";
        if (Time.time - time > playtime)
        {
            PlayerPrefs.SetInt("Score1", score1);
            PlayerPrefs.SetInt("Score2", score2);
            if (total1 != 0)
            {
                PlayerPrefs.SetFloat("Accuracy1", right1 / ((float)total1));
            }
            else
            {
                PlayerPrefs.SetFloat("Accuracy1", 0.0f);
            }
            if (total2 != 0)
            {
                PlayerPrefs.SetFloat("Accuracy2", right2 / ((float)total2));
            }
            else
            {
                PlayerPrefs.SetFloat("Accuracy2", 0.0f);
            }
            PlayerPrefs.SetInt("Total1", total1);
            PlayerPrefs.SetInt("Total2", total2);
            if (wrong1.Count != 0)
                PlayerPrefsX.SetIntArray("Wrong1", wrong1.ToArray());
            else
                PlayerPrefsX.SetIntArray("Wrong1", new int[1] { -1 }); 
            if (wrong2.Count != 0)
                PlayerPrefsX.SetIntArray("Wrong2", wrong2.ToArray());
            else
                PlayerPrefsX.SetIntArray("Wrong2", new int[1] { -1 });
            SceneManager.LoadScene("FinalScene");
        }
    }
    void OnClick1()
    {
        if (questions.ElementAt(ran1[i1]).Value == "T")
        {
            if (difficulties.ElementAt(ran1[i1]).Value == E_difficuly.Easy) score1 += 5;
            else if (difficulties.ElementAt(ran1[i1]).Value == E_difficuly.Normal) score1 += 10;
            else if (difficulties.ElementAt(ran1[i1]).Value == E_difficuly.Hard) score1 += 15;
            else score1 += 20;
            right1++;
            Correct.GetComponent<AudioSource>().Play();
        }
        else
        {
            if (difficulties.ElementAt(ran1[i1]).Value == E_difficuly.Easy) score1 -= 20;
            else if (difficulties.ElementAt(ran1[i1]).Value == E_difficuly.Normal) score1 -= 15;
            else if (difficulties.ElementAt(ran1[i1]).Value == E_difficuly.Hard) score1 -= 10;
            else score1 -= 5;
            Wrong.GetComponent<AudioSource>().Play();
            wrong1.Add(ran1[i1]);
        }
        total1++;
        T1.GetComponent<Button>().interactable = false;
        F1.GetComponent<Button>().interactable = false;
        i1++;
        Invoke("Addi1", waittime);
    }
    void Addi1()
    {
        T1.GetComponent<Button>().interactable = true;
        F1.GetComponent<Button>().interactable = true;
    }
    void OnClick2()
    {
        if (questions.ElementAt(ran1[i1]).Value == "F")
        {
            if (difficulties.ElementAt(ran1[i1]).Value == E_difficuly.Easy) score1 += 5;
            else if (difficulties.ElementAt(ran1[i1]).Value == E_difficuly.Normal) score1 += 10;
            else if (difficulties.ElementAt(ran1[i1]).Value == E_difficuly.Hard) score1 += 15;
            else score1 += 20;
            right1++;
            Correct.GetComponent<AudioSource>().Play();
        }
        else
        {
            if (difficulties.ElementAt(ran1[i1]).Value == E_difficuly.Easy) score1 -= 20;
            else if (difficulties.ElementAt(ran1[i1]).Value == E_difficuly.Normal) score1 -= 15;
            else if (difficulties.ElementAt(ran1[i1]).Value == E_difficuly.Hard) score1 -= 10;
            else score1 -= 5;
            Wrong.GetComponent<AudioSource>().Play();
            wrong1.Add(ran1[i1]);
        }
        total1++;
        T1.GetComponent<Button>().interactable = false;
        F1.GetComponent<Button>().interactable = false;
        i1++;
        Invoke("Addi1", waittime);
    }
    void OnClick3()
    {
        if (questions.ElementAt(ran2[i2]).Value == "T")
        {
            if (difficulties.ElementAt(ran2[i2]).Value == E_difficuly.Easy) score2 += 5;
            else if (difficulties.ElementAt(ran2[i2]).Value == E_difficuly.Normal) score2 += 10;
            else if (difficulties.ElementAt(ran2[i2]).Value == E_difficuly.Hard) score2 += 15;
            else score2 += 20;
            right2++;
            Correct.GetComponent<AudioSource>().Play();
        }
        else
        {
            if (difficulties.ElementAt(ran2[i2]).Value == E_difficuly.Easy) score2 -= 20;
            else if (difficulties.ElementAt(ran2[i2]).Value == E_difficuly.Normal) score2 -= 15;
            else if (difficulties.ElementAt(ran2[i2]).Value == E_difficuly.Hard) score2 -= 10;
            else score2 -= 5;
            Wrong.GetComponent<AudioSource>().Play();
            wrong2.Add(ran2[i2]);
        }
        total2++;
        T2.GetComponent<Button>().interactable = false;
        F2.GetComponent<Button>().interactable = false;
        i2++;
        Invoke("Addi2", waittime);
    }
    void Addi2()
    {
        T2.GetComponent<Button>().interactable = true;
        F2.GetComponent<Button>().interactable = true;
    }
    void OnClick4()
    {
        if (questions.ElementAt(ran2[i2]).Value == "F")
        {
            if (difficulties.ElementAt(ran2[i2]).Value == E_difficuly.Easy) score2 += 5;
            else if (difficulties.ElementAt(ran2[i2]).Value == E_difficuly.Normal) score2 += 10;
            else if (difficulties.ElementAt(ran2[i2]).Value == E_difficuly.Hard) score2 += 15;
            else score2 += 20;
            right2++;
            Correct.GetComponent<AudioSource>().Play();
        }
        else
        {
            if (difficulties.ElementAt(ran2[i2]).Value == E_difficuly.Easy) score2 -= 20;
            else if (difficulties.ElementAt(ran2[i2]).Value == E_difficuly.Normal) score2 -= 15;
            else if (difficulties.ElementAt(ran2[i2]).Value == E_difficuly.Hard) score2 -= 10;
            else score2 -= 5;
            Wrong.GetComponent<AudioSource>().Play();
            wrong2.Add(ran2[i2]);
        }
        total2++;
        T2.GetComponent<Button>().interactable = false;
        F2.GetComponent<Button>().interactable = false;
        i2++;
        Invoke("Addi2", waittime);
    }
    // n 为生成随机数个数
    public int[] OutputRandom(int minValue, int maxValue, int n)
    {
        //如果生成随机数个数大于指定范围的数字总数，则最多只生成该范围内数字总数个随机数
        if (n > maxValue - minValue + 1)
            n = maxValue - minValue + 1;

        int maxIndex = maxValue - minValue + 2;// 索引数组上限
        int[] indexArr = new int[maxIndex];
        for (int i = 0; i < maxIndex; i++)
        {
            indexArr[i] = minValue - 1;
            minValue++;
        }

        System.Random ran = new System.Random();
        int[] randNum = new int[n];
        int index;
        for (int j = 0; j < n; j++)
        {
            index = ran.Next(1, maxIndex - 1);// 生成一个随机数作为索引

            //根据索引从索引数组中取一个数保存到随机数数组
            randNum[j] = indexArr[index];

            // 用索引数组中最后一个数取代已被选作随机数的数
            indexArr[index] = indexArr[maxIndex - 1];
            maxIndex--; //索引上限减 1
        }
        return randNum;
    }
}
