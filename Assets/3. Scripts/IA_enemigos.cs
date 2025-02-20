using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    [SerializeField]
    private PlayerHealth playerHealth;

    public Transform player;               // Referencia al jugador
    public float detectionRange = 10f;     // Rango de detección
    public float attackRange = 2f;         // Rango de ataque
    private NavMeshAgent agent;            // Referencia al agente de NavMesh
    private Animator animator;             // Referencia al Animator del zombie
    private AudioSource audioSource;       // Componente AudioSource para reproducir sonidos
    public AudioClip walkSound;            // Sonido cuando el zombie camina
    public AudioClip attackSound;          // Sonido cuando el zombie ataca

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            agent.SetDestination(player.position);
            animator.SetBool("Walking", true);

            if (!audioSource.isPlaying && walkSound != null)
            {
                audioSource.clip = walkSound;
                audioSource.loop = true;
                audioSource.Play();
            }

            if (distance <= attackRange)
            {
                agent.isStopped = true;
                animator.SetBool("Walking", false);
                animator.SetTrigger("Attack");

                if (audioSource.isPlaying && walkSound != null)
                {
                    audioSource.Stop();
                }

                if (attackSound != null)
                {
                    audioSource.clip = attackSound;
                    audioSource.Play();
                }

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

            if (audioSource.isPlaying && walkSound != null)
            {
                audioSource.Stop();
            }
        }
    }

    private void AttackPlayer()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(playerHealth.damagePerHit);
        }
    }

    private void PlayAttackSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        audioSource.clip = attackSound;
        audioSource.loop = true;
        audioSource.Play();
    }

    private void StopAttackSound()
    {
        if (audioSource.isPlaying && attackSound != null)
        {
            audioSource.Stop();
        }
    }
}