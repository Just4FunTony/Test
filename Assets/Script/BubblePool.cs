using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePool
{
    private List<Bubble> _bubblesInPool;

    private Bubble bubblePrefab;

    public BubblePool(int poolCount) //CreatePool
    {
        bubblePrefab = Resources.Load<Bubble>("Prefabs/Bubble");
        _bubblesInPool = new List<Bubble>();
        for (int i = 0; i < poolCount; i++)
        {
            AddBubbleInPool();
        }
    }

    public void ReturnBubbleInPool(Bubble bubble)
    {
        _bubblesInPool.Add(bubble);
    }


    public Bubble GetBubble()
    {
        if (_bubblesInPool.Count == 0)
            AddBubbleInPool();
        Bubble bubble = _bubblesInPool[0];
        _bubblesInPool.Remove(bubble);
        return bubble;
    }

    private void AddBubbleInPool()
    {
        Bubble bubble = MonoBehaviour.Instantiate(bubblePrefab, Vector3.zero, Quaternion.identity);
        _bubblesInPool.Add(bubble);
    }
}
