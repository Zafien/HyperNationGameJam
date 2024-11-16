using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyUnit : BaseUnit
{
    BaseUnit basetest;
    public EnemyUI EnemyUI;

    public Transform player; // Assign the player GameObject in the Inspector
    public float attackDistance = 5f; // Distance to stop and attack
    public float attackCooldown = 2f; // Time between attacks

    private NavMeshAgent agent;
    private float lastAttackTime = 0f;
    // Update is called once per frame
    public override void Initialize(object data = null)
    {
        base.Initialize(data);
       
    }


    public override void OnSubscriptionSet()
    {
        base.OnSubscriptionSet();

   

    }
    private void Awake()
    {
        EnemyUI.SetMaxHp(200);
         agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        EnemyUI.SetHealthBarUI(_healthData.HealthAmount);
        MoveEnemy();
    }


    public void MoveEnemy()
    {

        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > attackDistance)
        {
            // Follow the player
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
        else
        {
            // Stop and attack
            agent.isStopped = true;

            // Attack logic
            if (Time.time > lastAttackTime + attackCooldown)
            {
                Attack();
                lastAttackTime = Time.time;
            }

            // Face the player
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }
   
    void Attack()
    {
        Debug.Log("Attack!"); // Replace this with actual attack logic
    }
    public void DropSomething()
    {

    }



}
