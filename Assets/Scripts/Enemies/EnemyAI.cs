using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour, IAttackable
{
    public Transform player;
    [Range(-1f, 1f), Tooltip("At what angle does the enemy start to see you.\n-1 means it can only see something directly infront of it, 0 is 180 vision, and 1 is full 360 vision")]
    public float FOV = 0.75f;

    [Header("Timings")]
    public Timer sightReactionTime;
    public Timer attackReactionTime;
    public Timer attentionSpan;

    [Header("Attack parameters")]
    [SerializeField] float reach;
    [SerializeField] LayerMask layerMask;
    NavMeshAgent agent;

    [Header("Variants")]
    public bool doRangedAttack = false;

    protected bool doChase { get; private set; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        sightReactionTime = new Timer(sightReactionTime.duration);
        attackReactionTime = new Timer(attackReactionTime.duration);
        attentionSpan = new Timer(attentionSpan.duration);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        doChase = false;
        Vector3 lineOfSight = player.position - transform.position;


        Ray ray = new Ray(transform.position, lineOfSight);
        float dist = (lineOfSight).magnitude + 0.5f;
        RaycastHit hit;
        
        // Ray to player
        if (Physics.Raycast(ray, out hit, dist, layerMask))
        {
            // Check if is player
            
            //This was very useful debug code, apparently I'm bad at judging numbers
            //if (Vector3.Dot(transform.forward, lineOfSight.normalized) > -FOV)
            //{
            //    Debug.Log("Can see you " + Vector3.Dot(transform.forward, lineOfSight.normalized));
            //}
            //else
            //    Debug.Log("no see " + Vector3.Dot(transform.forward, lineOfSight.normalized));


            if (hit.collider.gameObject.CompareTag("Player") && Vector3.Dot(transform.forward, lineOfSight.normalized) > -FOV)
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
                    Debug.DrawRay(transform.position, lineOfSight, Color.magenta);

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
            // Cannot see anything - unlikely but just in case, let's not break the AI
            // AI as in behaviour lol, this isn't generative AI
            sightReactionTime.Reset();
            attackReactionTime.Reset();
            doChase = false;
            Debug.DrawRay(transform.position, lineOfSight, Color.black);
        }
        // Line of sight
        
        // Attack raycast
        if (doChase && (lineOfSight.magnitude < reach || doRangedAttack))
        {
            // Can reach player

            Debug.DrawRay(transform.position, transform.forward * reach, Color.red);
            attackReactionTime.Tick(Time.deltaTime);
            if (attackReactionTime.currentTime == 0f)
            {
                Attack();
            }
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

    protected virtual void Attack()
    {
        IAttackable target = player.GetComponent<IAttackable>();
        if (target != null)
        {
            target.OnHit(gameObject);
            Debug.Log("attack");
        }
        attackReactionTime.Reset();
    }
}
