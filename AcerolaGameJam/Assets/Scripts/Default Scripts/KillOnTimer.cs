using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnTimer : MonoBehaviour
{
    public void StartTimer(float secondsAlive)
    {
        StartCoroutine(DoTimer(secondsAlive));
    }

    IEnumerator DoTimer(float secondsAlive)
    {
        yield return new WaitForSeconds(secondsAlive);
        Destroy(gameObject);
    }
}
