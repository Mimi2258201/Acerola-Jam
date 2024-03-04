using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    List<EnemyAI> aliveEnemies;

    // Start is called before the first frame update
    void Start()
    {
        aliveEnemies = FindObjectsByType<EnemyAI>(FindObjectsSortMode.None).ToList();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var enemy in aliveEnemies)
        {
            if (enemy.gameObject.activeSelf)
            {
                return;
            }
        }

        Debug.Log("Level win!");
        this.enabled = false;
    }
}
