using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour, IAttackable
{
    [SerializeField] float reach;
    [SerializeField] LayerMask layerMask;
    [Range(-1f, 1f), Tooltip("To what extent does the player have to be facing the enemy for a hit to count.\n1 means player has to be facing it exactly (near impossible), 0 means the player can be facing 90 degrees away, and -1 means any angle is fine.")]
    public float attackAccuracy = 0.75f;

    Animator animator;
    [HideInInspector] public List<EnemyAI> enemies = new List<EnemyAI>();

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * reach, Color.red);
        float bestDist = float.MaxValue;
        EnemyAI bestEnemy = null;
        foreach (var enemy in enemies)
        {
            if (Vector3.Dot(enemy.transform.position - transform.position, transform.forward) >= attackAccuracy)
            {
                Debug.DrawRay(transform.position, enemy.transform.position - transform.position, Color.magenta);
                if ((enemy.transform.position - transform.position).magnitude < bestDist)
                {
                    bestDist = (enemy.transform.position - transform.position).magnitude;
                    bestEnemy = enemy;
                }
            }
            else
                Debug.DrawRay(transform.position, enemy.transform.position - transform.position, Color.green);

        }

        

        if (Input.GetMouseButtonDown(0))
        {
            // Attack

            animator.Play("SimplePunch");

            if (bestEnemy == null) return;

            Ray ray = new Ray(transform.position, bestEnemy.transform.position - transform.position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, reach, layerMask))
            {
                IAttackable enemy = hit.collider.gameObject.GetComponent<IAttackable>();
                if (enemy != null)
                {
                    Debug.DrawRay(transform.position, (bestEnemy.transform.position - transform.position) * reach, Color.blue, 0.2f);
                    enemy.OnHit(gameObject);
                    Debug.Log("hit");
                }
                else
                    Debug.DrawRay(transform.position, (bestEnemy.transform.position - transform.position) * reach, Color.yellow, 0.2f);
            }
            else
                Debug.DrawRay(transform.position, (bestEnemy.transform.position - transform.position) * reach, Color.magenta, 0.2f);
        }
    }

    public void OnHit(GameObject instigator)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    
}
