using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public Transform player;            
    public float detectionRange = 10f;  
    public float attackRange = 2f;      
    private NavMeshAgent agent;         
    private Animator animator;          

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
            animator.SetBool("IsWalking", true);

            if (distance <= attackRange)
            {
                agent.isStopped = true;
                animator.SetBool("IsWalking", false);
                animator.SetTrigger("Attack");
            }
            else
            {
                agent.isStopped = false;
            }
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }
}