using UnityEngine;
using UnityEngine.AI;

public class IA_enemigos : MonoBehaviour
{
    public GameObject target;
    public float rangoSeguimiento = 10f;
    public Animator ani;

    private NavMeshAgent agent;

    void Start()
    {
        ani = GetComponent<Animator>();

        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
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
            agent.SetDestination(target.transform.position);
            ani.SetBool("Walk", true);
        }
        else
        {
            ani.SetBool("Walk", false);
        }
    }
}