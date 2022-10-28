using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameUI : MonoBehaviour
{
    private SceneLoader _sceneLoader;
    [SerializeField] private Text _currentScore;
    [SerializeField] private Text _lostScreenScore;
    [SerializeField] private Text _hpValue;
    [SerializeField] private Text _name; 

    [Header("PauseScreen")]
    [SerializeField] private GameObject _pauseMenuGO;
    [Header("GameScreen")]
    [SerializeField] private GameObject _gameUI_GO;
    [Header("LoseScreen")]
    [SerializeField] private GameObject _loseScreenGO;
    [SerializeField] private GameObject _imputField;
    [SerializeField] private GameObject _saveScoreButton;

    private Action _onPauseSwich;
    public Action <string> OnSave;

    public void PauseActionInit(Action onPauseSwich)
    {
        _onPauseSwich = onPauseSwich;
    }
    private void Start()
    {
        _sceneLoader = new SceneLoader();
    }
    public void ExitToMenu()
    {
        _sceneLoader.LoadMenu();
    }
    public void RestartGame()
    {
        _sceneLoader.LoadGame();
    }

    public void PauseButton()
    {
        _gameUI_GO.SetActive(false);
        _pauseMenuGO.SetActive(true);
        _onPauseSwich();
    }

    public void SaveScoreButton()
    {
        string name = "Player";
        if (_name.text.Length != 0)
            name = _name.text;

        _saveScoreButton.SetActive(false);
        _imputField.SetActive(false);
        OnSave(name);
    }

    public void GameReturn()
    {
        _gameUI_GO.SetActive(true);
        _pauseMenuGO.SetActive(false);
        _onPauseSwich();
    }

    public void LoseScreen()
    {
        _gameUI_GO.SetActive(false);
        _loseScreenGO.SetActive(true);
    }

    public void SetScore(int value)
    {
        _currentScore.text = value.ToString();
        _lostScreenScore.text = value.ToString();
    }
    public void SetHPValue(int value)
    {
        _hpValue.text = value.ToString();
    }
}
