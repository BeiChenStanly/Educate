using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

enum E_difficuly
{
    Easy,Normal,Hard,Impossible
}
[Serializable]
class NewData
{
    public NewQAPair[] questions;
}
[Serializable]
class NewQAPair
{
    public string question;
    public string answer;
    public E_difficuly difficuly;
    public NewQAPair(string question, string answer, E_difficuly difficuly)
    {
        this.question = question;
        this.answer = answer;
        this.difficuly = difficuly;
    }
}

public class SingleController : MonoBehaviour
{
    public TextAsset physics;
    public TextAsset chinese;
    public TextAsset chemistry;
    float time;
    float playtime;
    GameObject T1;
    GameObject F1;
    GameObject Score1;
    GameObject GameTime;
    GameObject Wrong;
    GameObject Correct;
    GameObject Question1;
    GameObject Difficulty;
    int[] ran1;
    List<int> wrong = new List<int>();
    int i1 = 0;
    int score1;
    int total1;
    int right1;
    float waittime = 1;
    Dictionary<string, string> questions = new Dictionary<string, string>();
    Dictionary<string, E_difficuly> difficulties = new Dictionary<string, E_difficuly>();
    void Start()
    {
        playtime = (PlayerPrefs.GetInt("playtime") + 1) * 30;
        waittime = PlayerPrefs.GetFloat("waittime");
        Dictionary<int, TextAsset> stp = new Dictionary<int, TextAsset>()
        {
            {0,physics },
            {1,chinese},
            {2,chemistry}
        };
        time = Time.time;
        NewData data = JsonUtility.FromJson<NewData>(stp[PlayerPrefs.GetInt("subject")].text);
        foreach (NewQAPair item in data.questions)
        {
            questions[item.question] = item.answer;
            difficulties[item.question] = item.difficuly;
        }
        T1 = GameObject.Find("T1");
        F1 = GameObject.Find("F1");
        Wrong = GameObject.Find("Wrong");
        Correct = GameObject.Find("Correct");
        GameTime = GameObject.Find("Time");
        Score1 = GameObject.Find("Canvas/Score1");
        Question1 = GameObject.Find("Canvas/Question1");
        Difficulty = GameObject.Find("Canvas/Difficulty");
        ran1 = OutputRandom(0, questions.Count - 1, 200);
        T1.GetComponent<Button>().onClick.AddListener(OnClick1);
        F1.GetComponent<Button>().onClick.AddListener(OnClick2);
    }
    void Update()
    {
        try
        {
            Question1.GetComponent<Text>().text = questions.ElementAt(ran1[i1]).Key;
            Difficulty.GetComponent<Text>().text = "难度："+ difficulties.ElementAt(ran1[i1]).Value.ToString();
        }
        catch (IndexOutOfRangeException)
        {
            i1 = 0;
        }
        Score1.GetComponent<Text>().text = $"Score:{score1}";
        GameTime.GetComponent<Text>().text = $"Time:{Math.Round(playtime - Time.time + time,2)}";
        if (Time.time - time >= playtime)
        {
            PlayerPrefs.SetInt("Score", score1);
            if (total1 != 0)
            {
                PlayerPrefs.SetFloat("Accuracy", ((float)right1) / ((float)total1));
            }
            else
            {
                PlayerPrefs.SetFloat("Accuracy", 0.0f);
            }
            if (wrong.Count!= 0)
            PlayerPrefsX.SetIntArray("Wrong", wrong.ToArray());
            else
            PlayerPrefsX.SetIntArray("Wrong", new int[1] {-1});
            SceneManager.LoadScene("SingleFinal");
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
            wrong.Add(ran1[i1]);
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
            wrong.Add(ran1[i1]);
        }
        total1++;
        T1.GetComponent<Button>().interactable = false;
        F1.GetComponent<Button>().interactable = false;
        i1++;
        Invoke("Addi1", waittime);
    }
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