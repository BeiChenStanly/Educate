using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum e_subject { physics, chinese, chemistry}
[Serializable]
public class NewQAPair
{
    public string question;
    public string answer;
    public E_difficuly difficuly;
    public string part;
    //new operations can be added here
}

[Serializable]
public class NewData
{
    public List<NewQAPair> questions;
}
public class ChangeJson : MonoBehaviour
{
    public TextAsset physics;
    public TextAsset chinese;
    public TextAsset chemistry;
    List<NewQAPair> newQuestions = new List<NewQAPair>();
    NewData newData = new NewData();
    void Start()
    {
        Dictionary<int, TextAsset> stp = new Dictionary<int, TextAsset>()
        {
            {0,physics },
            {1,chinese},
            {2,chemistry}
        };
        for (int i = 0; i < 3; i++)
        {
            //load the original data from the text file
            Data data = JsonUtility.FromJson<Data>(stp[i].text);
            foreach (QAPair item in data.questions)
            {
                NewQAPair newItem = new NewQAPair();
                newItem.question = item.question;
                newItem.answer = item.answer;
                newItem.difficuly = item.difficuly;
                newItem.part = "尚未完成的功能";
                //new operations can be added here
                newQuestions.Add(newItem);
            }

            //save the new data to the text file
            newData.questions = newQuestions;
            string json = JsonUtility.ToJson(newData, true);
            File.WriteAllText($"Assets/Texts/{(e_subject)i}.json", json);
            newData.questions.Clear();
        }
    }
}
