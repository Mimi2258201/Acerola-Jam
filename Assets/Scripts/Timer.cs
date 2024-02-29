using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Timer
{
    public float duration;
    public float currentTime {  get; private set; }

    public Timer(float duration)
    {
        this.duration = duration;
        currentTime = duration;
    }

    public void Tick(float deltaTime)
    {
        currentTime -= deltaTime;
        currentTime = Mathf.Clamp(currentTime, 0, float.MaxValue);
    }

    public void Reset()
    {
        currentTime = duration;
    }
}
