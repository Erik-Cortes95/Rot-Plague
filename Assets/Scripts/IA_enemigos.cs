using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public Transform player;               // Referencia al jugador
    public float detectionRange = 10f;     // Rango de detección
    public float attackRange = 2f;         // Rango de ataque
    private NavMeshAgent agent;            // Referencia al agente de NavMesh
    private Animator animator;             // Referencia al Animator del zombie

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();  
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            agent.SetDestination(player.position);  
            animator.SetBool("Walking", true);   

            if (distance <= attackRange)
            {
                agent.isStopped = true;
                animator.SetBool("Walking", false);  
                animator.SetTrigger("Attack");     

                AttackPlayer();
            }
            else
            {
                agent.isStopped = false;
            }
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }

    private void AttackPlayer()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(5f);
        }
    }
}