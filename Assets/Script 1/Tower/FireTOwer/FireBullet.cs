using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    private Movement movement;
    private Transform target;
    private float damage;
    private float burn;
    public bool burnstate;

    private void Start()
    {
        burnstate = false;
    }

    public void Setup(Transform target, float damage, float burn)
    {
        movement = GetComponent<Movement>();
        this.target = target;
        this.damage = damage;
        this.burn = burn;
 
    }
    private void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            movement.MoveTo(direction);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.CompareTag("Enemy")) return;
        if (collider.transform != target) return;
        collider.GetComponent<EnemyHP>().TakeDamage(damage);
        collider.GetComponent<EnemyHP>().StateBrun(burn);
        Destroy(gameObject);

    }
}
