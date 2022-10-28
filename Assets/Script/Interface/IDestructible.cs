using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDestructible
{
    public void DestroyOnClick();

    public void DestroyOnOvertime(float timeToDestroy);
}
