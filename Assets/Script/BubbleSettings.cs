using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSettings
{
    private int _bubbleStartCount = 10; // count bubble pool in start
    public int BubbleStartCount => _bubbleStartCount;

    private Vector3 maxPos;
    private Vector3 minPos;

    private float offset = 0.71f;

    private int _gameSpeed;

    public BubbleSettings(int gameSpeed)
    {
        _gameSpeed = gameSpeed;
        maxPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width , Screen.height));
        minPos = Camera.main.ScreenToWorldPoint(new Vector3(0,0));
    }
    public float DestroyTime()
    {
        float r = Random.Range((_gameSpeed % 20), (_gameSpeed + _gameSpeed%20));
        return (maxPos.y - minPos.y - offset*2) / r;
    }

    public float maxPosY()
    {
        return maxPos.y - offset;
    }
    public Vector3 GetBubbleTransform()
    {
        return new Vector3(Random.Range(maxPos.x - offset, minPos.x + offset), minPos.y + offset);
    }
}
