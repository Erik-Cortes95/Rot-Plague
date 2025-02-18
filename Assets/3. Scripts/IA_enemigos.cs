using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    [SerializeField]
    private PlayerHealth
    playerHealth;

    public Transform player;               // Referencia al jugador
    public float detectionRange = 10f;     // Rango de detecci�n
    public float attackRange = 2f;         // Rango de ataque
    private NavMeshAgent agent;            // Referencia al agente de NavMesh
    private Animator animator;             // Referencia al Animator del zombie
    private AudioSource audioSource;       // Componente AudioSource para reproducir sonidos
    public AudioClip walkSound;            // Sonido cuando el zombie camina
    public AudioClip attackSound;          // Sonido cuando el zombie ataca
    private bool isAttacking = false;      // Indica si el zombie est� atacando

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

            // Reproducir sonido de caminar solo si no se est� reproduciendo ya
            if (!audioSource.isPlaying && walkSound != null)
            {
                audioSource.clip = walkSound;
                audioSource.loop = true;  // El sonido se repite mientras el zombie camina
                audioSource.Play();
            }

            if (distance <= attackRange)
            {
                agent.isStopped = true;
                animator.SetBool("Walking", false);
                animator.SetTrigger("Attack");

                // Detener sonido de caminar cuando ataca y reproducir el sonido de ataque
                if (audioSource.isPlaying && walkSound != null)
                {
                    audioSource.Stop(); // Detiene el sonido de caminar
                }

                if (attackSound != null)
                {
                    audioSource.clip = attackSound;
                    audioSource.Play(); // Reproduce el sonido de ataque
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

            // Detener sonido de caminar si el zombie no est� cerca del jugador
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

    // M�todo para reproducir el sonido de ataque en bucle
    private void PlayAttackSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();  // Detener cualquier otro sonido antes de reproducir el de ataque
        }
        audioSource.clip = attackSound;
        audioSource.loop = true;  // El sonido se repite continuamente mientras ataca
        audioSource.Play();
    }

    // M�todo para detener el sonido de ataque
    private void StopAttackSound()
    {
        if (audioSource.isPlaying && attackSound != null)
        {
            audioSource.Stop();  // Detiene el sonido de ataque cuando el zombie ya no ataca
        }
        isAttacking = false;  // Resetea el estado de ataque
    }
}