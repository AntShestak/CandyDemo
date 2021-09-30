using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    public void SaveHighscores(List<Highscore> highscores)
    {
        string dataPath = Application.persistentDataPath;

        var serializer = new XmlSerializer(typeof(List<Highscore>));
        var stream = new FileStream(dataPath + "/highscores.save", FileMode.Create);
        serializer.Serialize(stream, highscores);
        stream.Close();

        Debug.Log("Saved!");
    }

    public List<Highscore> LoadHighscores()
    {
        List<Highscore> ret = new List<Highscore>();
        string dataPath = Application.persistentDataPath;
        //check if file exists
        if (System.IO.File.Exists(dataPath + "/highscores.save"))
        {
            var serializer = new XmlSerializer(typeof(List<Highscore>));
            var stream = new FileStream(dataPath + "/highscores.save", FileMode.Open);
            ret = serializer.Deserialize(stream) as List<Highscore>;
            stream.Close();

            Debug.Log("Loaded!");
        }

        return ret;
    }

    //method to reset scores
    public void DebugScoreReset()
    {
        foreach (var directory in Directory.GetDirectories(Application.persistentDataPath))
        {
            DirectoryInfo data_dir = new DirectoryInfo(directory);
            data_dir.Delete(true);
        }
        
        foreach (var file in Directory.GetFiles(Application.persistentDataPath))
        {
            FileInfo file_info = new FileInfo(file);
            file_info.Delete();
        }
    }
}

[System.Serializable]
public class Highscore : IComparable<Highscore>
{
    public string name;
    public int score;

    //parametreless contructor for serialization
    private Highscore() { }

    public Highscore(string newName, int newScore)
    {
        name = newName;
        score = newScore;
    }

    public int CompareTo(Highscore other)
    {
        if (other == null)
        {
            return 1;
        }

        return other.score - score;
    }

}

