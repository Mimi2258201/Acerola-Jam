using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    Rigidbody projectileRigidbody;
    [HideInInspector] public EnemyAI owner;

    // Awake is called before the first frame update
    void Awake()
    {
        projectileRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire(Vector3 force)
    {
        projectileRigidbody.AddForce(force, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerCombat>().OnHit(owner.gameObject);
            Destroy(gameObject);
        }
    }
}
