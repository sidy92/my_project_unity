using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveDamage : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    //private EnemyRespawn enemyRespawn;

    private void Start()
    {
        currentHealth = maxHealth;
        //enemyRespawn = GetComponent<EnemyRespawn>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Bullet1 bullet = other.GetComponent<Bullet1>();
        if (bullet != null)
        {
            TakeDamage(bullet.gunDamage);
            Destroy(bullet.gameObject);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            Debug.Log("Enemy took " + amount + " damage. Remaining health: " + currentHealth);
        }
    }

    private void Die()
    {

        Destroy(gameObject);
        Debug.Log("Enemy is dead !");
        /*if (enemyRespawn != null)
        {
            enemyRespawn.StartRespawnTimer();
        }
        else
        {
            Debug.LogWarning("EnemyRespawn component not found!");
            Destroy(gameObject);
        }*/
    }
}
