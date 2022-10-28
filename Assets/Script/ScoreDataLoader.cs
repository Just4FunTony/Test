using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

public class ScoreDataLoader : ISave, ILoad
{

    private List <PlayerData> _playersData = new List<PlayerData>();

    private string _path;

    public ScoreDataLoader()
    {
        _path = Application.persistentDataPath + "/save.json";
        _playersData = LoadScore();
    }

    public int LoadHighScore()
    {
        return _playersData.Last().Score;
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
        FileStream fileStream = new FileStream(_path, FileMode.OpenOrCreate);
        using (var reader = new StreamReader(fileStream))
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
        List<PlayerData> pd = JsonConvert.DeserializeObject<List<PlayerData>>(json).OrderBy(p => p.Score).ToList();
        fileStream.Close();
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