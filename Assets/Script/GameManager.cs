using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int gameSpeed;
    [SerializeField] private int hp;
    private Health _health;
    private BubbleSpawner _bubbleSpawner;
    private Score _score;
    private ScoreDataLoader _scoreDataLoader;

    [SerializeField] private GameUI _gameUI;

    public Action onPauseSwitch;

    private float i = 0;

    private void Start() => StartGame();
    private void Update() => bubbleSpawnLoop();


    private void bubbleSpawnLoop()
    {
        if (i > 1)
        {
            _bubbleSpawner.CreateBubble();
            i = 0;
        }
        i += Time.deltaTime * gameSpeed / 5;
    }

    private void addScore()
    {
        _score.AddScore(1);
        _gameUI.SetScore(_score.GetScore());
    }
    private void getHit()
    {
        int currentHp = _health.GetHit(1);
        if (currentHp <= 0) EndGame();
        _gameUI.SetHPValue(currentHp);
    }

    public void StartGame()
    {
        _scoreDataLoader = new ScoreDataLoader();
        _bubbleSpawner = new BubbleSpawner(gameSpeed);
        _score = new Score();
        _health = new Health(hp);

        _gameUI.SetScore(_score.GetScore());
        _gameUI.SetHPValue(_health.Value);

        _bubbleSpawner.addScore += addScore;
        _bubbleSpawner.getHit += getHit;
        onPauseSwitch += Pause;
        _gameUI.OnSave += SaveScore;
        _gameUI.PauseActionInit(onPauseSwitch);
        if (Time.timeScale == 0) onPauseSwitch();
    }

    public void EndGame()
    {
        _bubbleSpawner.addScore -= addScore;
        _bubbleSpawner.getHit -= getHit;
        _gameUI.LoseScreen();
        onPauseSwitch();
    }

    public void Pause()
    {
        if (Time.timeScale == 1.0f)
            Time.timeScale = 0;
        else Time.timeScale = 1.0f;
    }

    public void SaveScore(string name)
    {
        _scoreDataLoader.CreateNewPlayerData(name, _score.GetScore());
    }
}
