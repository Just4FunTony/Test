using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BubbleSpawner
{
    private IGetBubble _bubblePool;
    private BubbleSettings _bubbleSettings;

    public Action addScore, getHit;
    public BubbleSpawner(int gameSpeed)
    {
        _bubbleSettings = new BubbleSettings(gameSpeed);
        _bubblePool = new BubblePool();
    }
    public void CreateBubble()
    {
        Bubble bubble = _bubblePool.GetBubble();

        Color color = new Color(UnityEngine.Random.Range(0.000f, 1.000f), UnityEngine.Random.Range(0.000f, 1.000f), UnityEngine.Random.Range(0.000f, 1.000f));// RandomCollor 

        Vector3 pos = _bubbleSettings.GetBubbleTransform();

        bubble.InitBubble(color, pos, new Vector3(pos.x, _bubbleSettings.maxPosY()), _bubbleSettings.DestroyTime());

        bubble.onCliked += AddScore;
        bubble.onDestroyOvertime += GetHit;
    }

    public void AddScore(Bubble bubble)
    {
        bubble.onCliked -= AddScore;
        bubble.onDestroyOvertime -= GetHit;
        addScore();
    }
    public void GetHit(Bubble bubble)
    {
        bubble.onCliked -= AddScore;
        bubble.onDestroyOvertime -= GetHit;
        getHit();
    }
}
