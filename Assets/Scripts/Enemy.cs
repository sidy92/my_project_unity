using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //search and follow
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask playerLayer, groundLayer;
    public Vector3 specialPoint;
    bool isPointSet;
    public float pointRange;
    public float visionRange;
    public bool inVisionRange;

    //Attack
    public float attackTime;
    bool attacked;
    public GameObject bullet;
    public float attackRange;
    public bool inAttackRange;


    private void SearchPoint()
    {
        float randPointZ = Random.Range(-pointRange, pointRange);
        float randPointX = Random.Range(-pointRange, pointRange);
        specialPoint = new Vector3(transform.position.x + randPointX, transform.position.y, transform.position.z + randPointZ);

        if(Physics.Raycast(specialPoint, -transform.up, 2f, groundLayer))
        {
            isPointSet = true;
        }

        //Vector3 distance = transform.position - specialPoint;
    }
    private void Patrol()
    {
        if(!isPointSet)
        {
            SearchPoint();
        }
        if(isPointSet)
        {
            agent.SetDestination(specialPoint);
        }
        Vector3 distance = transform.position - specialPoint;
        if(distance.magnitude < 1f)
        {
            isPointSet = false;
        }
    }
    private void FollowPlayer()
    {
        agent.SetDestination(player.position);
        agent.speed = 12f;
    }
    private void Attack()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        if (!attacked)
        {
            Rigidbody bulletInstance = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            bulletInstance.AddForce(transform.forward * 10f, ForceMode.Impulse);
            bulletInstance.AddForce(transform.up * 5f, ForceMode.Impulse);
            attacked = true;
            Invoke(nameof(Reset), attackTime);
        }
    }
    
    private void Reset()
    {
        attacked = false;
    }
    
    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        inVisionRange = Physics.CheckSphere(transform.position, visionRange, playerLayer);
        inAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (!inVisionRange && !inAttackRange)
        {
            Patrol();
        }

        if(inVisionRange && !inAttackRange)
        {
            FollowPlayer();
        }

        if(inVisionRange && inAttackRange)
        {
            Attack();
        }
    }
}