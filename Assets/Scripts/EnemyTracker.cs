using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    PlayerCombat playerCombat;
    // Start is called before the first frame update
    void Start()
    {
        playerCombat = transform.parent.GetComponent<PlayerCombat>();
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyAI enemy = other.GetComponent<EnemyAI>();
        if (enemy != null)
        {
            playerCombat.enemies.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        EnemyAI enemy = other.GetComponent<EnemyAI>();
        if (enemy != null)
        {
            playerCombat.enemies.Remove(enemy);
        }
    }
}
