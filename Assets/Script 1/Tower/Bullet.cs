using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Movement movement;
    private Transform target;
    private float damage;
   

    public void Setup(Transform target, float damage)
    {
        movement = GetComponent<Movement>();
        this.target = target;
        this.damage = damage;
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
        Destroy(gameObject);

    }

}
