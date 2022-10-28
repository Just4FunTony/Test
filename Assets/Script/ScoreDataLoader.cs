using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

public class ScoreDataLoader 
{

    private List <PlayerData> _playersData = new List<PlayerData>();

    private string _path;

    public ScoreDataLoader()
    {
        _path = Application.persistentDataPath + "/save.json";
        _playersData = LoadScore();
    }

    public int GetHighScore()
    {
        int max = 0;
        if(_playersData.Count != 0)
        foreach(PlayerData playerData in _playersData)
        {
            if (max < playerData.Score) max = playerData.Score;
        }
        return max;
    }

    public void CreateNewPlayerData(string name, int score)
    {
        PlayerData playerData = new PlayerData(name, score);
        _playersData.Add(playerData);
        SaveScore(); 
    }
    public List <PlayerData> LoadScore()
    {
        string json = "";
        FileStream fs = new FileStream(_path, FileMode.OpenOrCreate);///Не успел убрать этот костыль :(
        fs.Close();
        using (var reader = new StreamReader(_path))
        {
            string line;
            while((line = reader.ReadLine())!= null)
            {
                json += line;
            }
        }
        if (string.IsNullOrEmpty(json))
        {
            return new List<PlayerData>();
        }
        List<PlayerData> pd = JsonConvert.DeserializeObject<List<PlayerData>>(json);
        return pd;
    }
    private void SaveScore()
    {
        var json = JsonConvert.SerializeObject(_playersData);
        using (var writer = new StreamWriter(_path))
        {
            writer.WriteLine(json);
        }
    }
}

public struct PlayerData
{
    public string Name { get; private set; }
    public int Score { get; private set; }

    public PlayerData(string name, int score)
    {
        Name = name;
        Score = score;
    }
}