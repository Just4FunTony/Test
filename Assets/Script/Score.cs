using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score
{
    private int _value;
   
    public int GetScore()
    {
        return _value;
    }

    public void AddScore(int value)
    {
        _value += value;
    }
}
