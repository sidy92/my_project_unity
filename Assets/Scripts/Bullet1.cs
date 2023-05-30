using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1 : MonoBehaviour
{
    public float timer;
    public float effectTimer;
    public GameObject effectParticle;
    public int gunDamage = 20;
    private bool hitTrigger = false;

    void Start()
    {

    }

    void Update()
    {
        if(effectTimer > 0)
        {
            effectTimer -= Time.deltaTime;
        }else
        {
            GameObject special = Instantiate(effectParticle, gameObject.transform.position, Quaternion.identity);
            Destroy(special, 1);
        }

        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }else
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target") && collision.collider.isTrigger)
        {
            Destroy(gameObject);
            return;
        }

        //Verifier si la collision touche un ennemi
        ReceiveDamage enemy = collision.gameObject.GetComponent<ReceiveDamage>();
        if (enemy != null)
        {
            //Infliger dégâts
            enemy.TakeDamage(gunDamage);
            Debug.Log("Balle a touché l'ennemi ! Dégâts infligés : " + gunDamage);
        }
        else
        {
            Debug.Log("Balle n'a pas touché l'ennemi.");
        }

        //Detruire la balle apres 4 secondes si elle ne touche pas une cible avec le "Trigger" active
        if (!hitTrigger)
        {
            Destroy(gameObject, 4f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {
            hitTrigger = true;
        }
    }
}