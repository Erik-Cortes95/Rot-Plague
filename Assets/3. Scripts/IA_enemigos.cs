using UnityEngine;
using UnityEngine.AI;

public class IA_enemigos : MonoBehaviour
{
    public GameObject target;  // Referencia al jugador
    public float rangoSeguimiento = 10f;  // Rango de seguimiento del jugador
    public Animator ani;  // Animator del zombie
    public float dañoAlJugador = 10f;  // Daño que el zombie inflige al jugador
    public float tiempoEntreAtaques = 1f;  // Tiempo de espera entre ataques
    private float siguienteAtaque = 0f;  // Control para no atacar continuamente

    private NavMeshAgent agent;

    void Start()
    {
        ani = GetComponent<Animator>();

        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");  // Encontrar el jugador
            if (target == null)
            {
                Debug.LogError("No se encontró el jugador con la etiqueta 'Player'.");
                return;
            }
        }

        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distanciaAlJugador = Vector3.Distance(transform.position, target.transform.position);

        if (distanciaAlJugador <= rangoSeguimiento)
        {
            agent.SetDestination(target.transform.position);  // El zombie sigue al jugador
            ani.SetBool("Walk", true);

            // Verificar si el zombie está cerca del jugador y listo para atacar
            if (distanciaAlJugador <= agent.stoppingDistance && Time.time >= siguienteAtaque)
            {
                AttackPlayer();
                siguienteAtaque = Time.time + tiempoEntreAtaques;  // Establecer el tiempo para el siguiente ataque
            }
        }
        else
        {
            ani.SetBool("Walk", false);
        }
    }

    // Método para atacar al jugador
    private void AttackPlayer()
    {
        // Comprobar si el jugador tiene el script PlayerHealth
        PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(dañoAlJugador);  // Infligir daño al jugador
            ani.SetTrigger("Attack");  // Activar animación de ataque (asegúrate de tener esta animación en tu Animator)
        }
    }

    // Método que se llama cuando el zombie entra en contacto con el jugador
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Time.time >= siguienteAtaque)
        {
            // Cuando el zombie colisiona con el jugador, hacerle daño
            AttackPlayer();
            siguienteAtaque = Time.time + tiempoEntreAtaques;  // Establecer intervalo entre ataques
        }
    }
}