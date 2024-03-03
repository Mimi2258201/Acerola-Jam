using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RangedAI : EnemyAI
{
    [Header("Projectiles")]
    public Projectile projectilePrefab;
    public float projectileSpeed;

    public void Reset()
    {
        doRangedAttack = true;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void LateUpdate()
    {
        base.LateUpdate();
        if (doChase)
            transform.eulerAngles = new Vector3(0f, Quaternion.LookRotation(player.position - transform.position).eulerAngles.y, 0f);
        
    }

    protected override void Attack()
    {
        Projectile projectile = Instantiate<Projectile>(projectilePrefab, transform.position, Quaternion.identity);
        projectile.owner = this;
        projectile.Fire((player.position - transform.position) * projectileSpeed);

        // Base stuff
        Debug.Log("projectile attack");
        base.attackReactionTime.Reset();
    }
}
