using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] private Text _bestScore;
    [SerializeField] private GameObject _menuGO;
    [Header("ScoreBoard")]
    [SerializeField] List<PlayerDataUI> _playerDataUIs = new List<PlayerDataUI>();
    private PlayerDataUI _playerDataUIprefab;
    [SerializeField] private Transform _anchorScoreboardTransform;
    [SerializeField] private GameObject _scoreboardGO;
    private ILoad _scoreDataLoader;
    private SceneLoader _sceneLoader;

    private void Start()
    {
        _playerDataUIprefab = Resources.Load<PlayerDataUI>("Prefabs/PlayerInfoUI");
        _scoreDataLoader = new ScoreDataLoader();
        _sceneLoader = new SceneLoader();
        _bestScore.text = _scoreDataLoader.LoadHighScore().ToString();
    }

    public void PlayButton()
    {
        _sceneLoader.LoadGame();
    }

    public void ExitButton()
    {
        _sceneLoader.ExitGame();
    }

    public void ScoreboardButton()
    {
        _scoreboardGO.SetActive(true);
        _menuGO.SetActive(false);
        OpenScoreboard();
    }
    public void ReturnToMenuButton()
    {
        _scoreboardGO.SetActive(false);
        _menuGO.SetActive(true);
    }

    private void OpenScoreboard()
    {
        List<PlayerData> scoreList = _scoreDataLoader.LoadScore();

        for (int i = 0; i < scoreList.Count; i++)
        {
            var p = getPlayerDataUI(i);
            p.SetPlayerData(scoreList[i].Name, scoreList[i].Score);
            p.transform.SetAsFirstSibling();
        }
    }

    private PlayerDataUI getPlayerDataUI(int i)
    {
        if (_playerDataUIs.Count == i)
        {
            addPlayerDataUI();
        }
        PlayerDataUI playerDataUI = _playerDataUIs[i];
        return playerDataUI;
    }
    private void addPlayerDataUI()
    {
        PlayerDataUI playerDataUI = Instantiate(_playerDataUIprefab, Vector3.zero, Quaternion.identity);
        playerDataUI.transform.SetParent(_anchorScoreboardTransform);
        _playerDataUIs.Add(playerDataUI);
    }

}
