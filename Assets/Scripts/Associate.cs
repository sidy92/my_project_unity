using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//creation d'un allie qui s'active dès que je m'approche de lui
public class Associate : MonoBehaviour
{
    //search and follow
    public NavMeshAgent agent;
    public Transform enemy;
    public LayerMask playerLayer, groundLayer;
    public Vector3 specialPoint;
    bool isPointSet;
    public float pointRange;
    public float visionRange;
    public bool inVisionRange;

    //Defense
    public float defenseTime;
    bool defensed;
    public GameObject bullet;
    public float defenseRange;
    public bool inDefenseRange;

    private void FollowEnemy()
    {
        agent.SetDestination(enemy.position);
        agent.speed = 10f;
    }
    
    private void Defense()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(enemy);
        if (!defensed)
        {
            Rigidbody bulletInstance = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            bulletInstance.AddForce(transform.forward * 10f, ForceMode.Impulse);
            //bulletInstance.AddForce(transform.up * 2f, ForceMode.Impulse);
            defensed = true;
            Invoke(nameof(Reset), defenseTime);
        }
    }
    
    private void Reset()
    {
        defensed = false;
    }
    
    private void Awake()
    {
        enemy = GameObject.Find("Enemy1").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        inVisionRange = Physics.CheckSphere(transform.position, visionRange, playerLayer);
        inDefenseRange = Physics.CheckSphere(transform.position, defenseRange, playerLayer);

        if(inVisionRange && !inDefenseRange)
        {
            FollowEnemy();
        }

        if(inVisionRange && inDefenseRange)
        {
            Defense();
        }
    }
}