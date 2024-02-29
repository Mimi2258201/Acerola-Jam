using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour, IAttackable
{
    public Transform player;
    public Timer sightReactionTime;
    public Timer attackReactionTime;
    public Timer attentionSpan;
    [SerializeField] float reach;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        iTween.Init(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // Timers
        
        attackReactionTime.Tick(Time.deltaTime);
        attentionSpan.Tick(Time.deltaTime);



        Ray ray = new Ray(transform.position, player.position - transform.position);
        RaycastHit hit;
        Debug.DrawRay(transform.position, player.position - transform.position, Color.magenta);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                sightReactionTime.Tick(Time.deltaTime);
                if (sightReactionTime.currentTime == 0f)
                {
                    // Can see player
                    agent.destination = player.position;


                    Debug.DrawRay(transform.position, transform.forward * reach, Color.red);
                    if (Physics.Raycast(transform.position, transform.forward, out hit, reach))
                    {
                        if (hit.collider.CompareTag("Player"))
                        {
                            // Can reach player

                            attackReactionTime.Tick(Time.deltaTime);
                            if (attackReactionTime.currentTime == 0f)
                            {
                                if (hit.collider.GetComponent<IAttackable>() is IAttackable target)
                                {                    
                                    target.OnHit(gameObject);
                                }
                                attackReactionTime.Reset();
                            }
                        }
                    }
                }

            }
            else
            {
                // Cannot see player
                sightReactionTime.Reset();
                attackReactionTime.Reset();
            }
        }
    }

    public void OnHit(GameObject instigator)
    {
        gameObject.SetActive(false);
    }
}
