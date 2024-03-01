using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour, IAttackable
{
    [SerializeField] float reach;
    Animator animator;
    bool canPunch = true;
    public List<EnemyAI> enemies = new List<EnemyAI>();

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float bestDist = float.MaxValue;
        EnemyAI bestEnemy = null;
        foreach (var enemy in enemies)
        {
            if ((enemy.transform.position - transform.position).magnitude < bestDist)
            {
                bestDist = (enemy.transform.position - transform.position).magnitude;
                bestEnemy = enemy;
            }
        }

        

        if (Input.GetMouseButtonDown(0))
        {
            // Do actual raycast here and check for interface

            Attack();

            if (bestEnemy == null) return;

            Ray ray = new Ray(transform.position, bestEnemy.transform.position - transform.position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, reach) && !canPunch)
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

    void Attack()
    {
        if (canPunch)
        {
            animator.Play("SimplePunch");
            canPunch = false;
        }
    }

    public void FinishAttack()
    {
        canPunch = true;
    }

    public void OnHit(GameObject instigator)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    
}
