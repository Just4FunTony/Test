using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataUI : MonoBehaviour
{
    [SerializeField] private Text _text;

    public void SetPlayerData(string name , int score)
    {
        _text.text = name + " : " + score.ToString();
    }
}
