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
    [SerializeField] LayerMask layerMask;
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
        bool doChase = false;
        bool doAttack;

        

        Ray ray = new Ray(transform.position, player.position - transform.position);
        float dist = (player.position - transform.position).magnitude + 0.5f;
        RaycastHit hit;
        
        // Ray to player
        if (Physics.Raycast(ray, out hit, dist, layerMask))
        {
            // Check if is player
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                // Check for timer
                sightReactionTime.Tick(Time.deltaTime);
                if (sightReactionTime.currentTime == 0f)
                {
                    // Can see player
                    agent.destination = player.position;
                    doChase = true;

                    //Debug.DrawRay(transform.position, player.position - transform.position, Color.blue);


                    
                }
                else
                    Debug.DrawRay(transform.position, player.position - transform.position, Color.magenta);

            }
            else
            {
                // Saw something, but it wasn't the player                
                sightReactionTime.Reset();
                attackReactionTime.Reset();
                doChase = false;
                //Debug.DrawRay(transform.position, player.position - transform.position, Color.red);
            }
        }
        else
        {
            // Cannot see anything
            sightReactionTime.Reset();
            attackReactionTime.Reset();
            doChase = false;
            Debug.DrawRay(transform.position, player.position - transform.position, Color.black);
        }
        // Line of sight

        // Attack raycast
        if (doChase && (transform.position - player.position).magnitude < reach)
        {
            // Can reach player

            Debug.DrawRay(transform.position, transform.forward * reach, Color.red);
            attackReactionTime.Tick(Time.deltaTime);
            if (attackReactionTime.currentTime == 0f)
            {
                if (player.GetComponent<IAttackable>() is IAttackable target)
                {
                    target.OnHit(gameObject);
                }
                attackReactionTime.Reset();
            }
            
            
            attackReactionTime.Reset();
            
        }
        else
        {
            attackReactionTime.Reset();
        }
    }

    public void OnHit(GameObject instigator)
    {
        PlayerCombat playerCombat = instigator.GetComponent<PlayerCombat>();
        if (playerCombat != null)
        {
            playerCombat.enemies.Remove(this);
        }
        gameObject.SetActive(false);
    }
}
