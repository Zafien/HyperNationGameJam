using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class BossUnity : BaseUnit
{
    public SwapObject playerParry;
    public EnemyUI EnemyUI;
    public Animator EnemyAnimator;
    public CharacterUnit playerGo; // Assign the player GameObject in the Inspector
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
        AddEvent(Ondead, _ => EnemyDead());

        EnemyUI.SetMaxHp(_unitStats.HealthAmount);
    }
    private void Awake()
    {

        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        EnemyUI.SetHealthBarUI(_unitStats.HealthAmount);
        MoveEnemy();

    }


    public void MoveEnemy()
    {
        if (playerGo == null) return;

        Transform PlayerTransform = playerGo.gameObject.transform;

        float distanceToPlayer = Vector3.Distance(transform.position, PlayerTransform.position);
        EnemyAnimator.SetBool("OnAttack", false);
        if (distanceToPlayer > attackDistance)
        {
            // Follow the player
            agent.isStopped = false;
            agent.SetDestination(PlayerTransform.position);
        }
        else
        {
            // Stop and attack
            agent.isStopped = true;

            // Attack logic
            if (Time.time > lastAttackTime + attackCooldown)
            {
                playerParry.EnemyAttack();
                EnemyAnimator.SetBool("OnAttack", true);
                Debug.LogError("ATTACKING THE PLAYER");
                Attack();
                lastAttackTime = Time.time;
            }

            // Face the player
            Vector3 direction = (PlayerTransform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    void Attack()
    {

        Debug.Log("Attacking PLAYER"); // Replace this with actual attack logic
        DoAttackDamage(playerGo, 5);
    }

    public void EnemyDead()
    {
        DropExpAndItem(playerGo);
    }
    public void DropExpAndItem(CharacterUnit receiver)
    {
        receiver.OnGainExp(80);
    }
    public void DoAttackDamage(BaseUnit receiver, float damageAmount)
    {
        receiver.ModifyHealthAmount(damageAmount);
    }
}
