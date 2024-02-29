using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] float reach;
    Animator animator;
    bool canPunch = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.DrawRay(transform.position, transform.forward * reach, Color.red);
            // Do actual raycast here and check for interface
            Attack();
        }
        else
            Debug.DrawRay(transform.position, transform.forward * reach);
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
}
