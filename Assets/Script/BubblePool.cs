using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePool : IGetBubble
{
    private List<Bubble> _bubblesInPool;

    private Bubble bubblePrefab;

    private const int START_POOL_COUNT = 10;

    public BubblePool() //CreatePool
    {
        bubblePrefab = Resources.Load<Bubble>("Prefabs/Bubble");
        _bubblesInPool = new List<Bubble>();
        for (int i = 0; i < START_POOL_COUNT; i++)
        {
            AddBubbleInPool();
        }
    }

    private void returnBubbleInPool(Bubble bubble)
    {
        bubble.onCliked -= returnBubbleInPool;
        bubble.onDestroyOvertime -= returnBubbleInPool;
        _bubblesInPool.Add(bubble);
    }

    public Bubble GetBubble()
    {
        if (_bubblesInPool.Count == 0)
            AddBubbleInPool();
        Bubble bubble = _bubblesInPool[0];
        _bubblesInPool.Remove(bubble);
        bubble.onCliked += returnBubbleInPool;
        bubble.onDestroyOvertime += returnBubbleInPool;
        return bubble;
    }

    private void AddBubbleInPool()
    {
        Bubble bubble = MonoBehaviour.Instantiate(bubblePrefab, Vector3.zero, Quaternion.identity);
        _bubblesInPool.Add(bubble);
    }
}
