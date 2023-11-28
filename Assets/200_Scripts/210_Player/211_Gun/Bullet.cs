using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Color color;

    private void Awake()
    {
        color = GetComponent<Renderer>().material.color;
    }
    private void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.layer == 10 && target.gameObject.TryGetComponent(out EnemyHealth enemyHealth))
        {
            Debug.Log($"Hit {target.gameObject.name} !");
            enemyHealth.TakeDamage(10);
            Destroy(gameObject);
        }

        if (color == Color.blue && target.gameObject.layer == 8)
        {
            Destroy(target);
            Destroy(gameObject);
        }

        if (color == Color.red && target.gameObject.layer == 9)
        {
            Destroy(target);
            Destroy(gameObject);
        }
    }
}