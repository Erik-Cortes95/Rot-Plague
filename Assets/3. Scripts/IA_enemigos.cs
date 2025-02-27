using UnityEngine;
using UnityEngine.AI;

public class IA_enemigos : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Animator ani;
    public float grado;
    private Quaternion angulo;

    public GameObject target;
    public bool atacando;

    private NavMeshAgent agent;

    public float rangoSeguimiento = 10f;

    void Start()
    {
        ani = GetComponent<Animator>();

        if (target == null)
        {
            Debug.LogError("No se encontró el jugador con la etiqueta 'Player'.");
            return;
        }

        agent = GetComponent<NavMeshAgent>();

        agent.SetDestination(target.transform.position);
    }

    void Update()
    {
      //  Comportamiento_Enemigo();
    }

    public void Comportamiento_Enemigo()
    {
        float distanciaAlJugador = Vector3.Distance(transform.position, target.transform.position);

        if (distanciaAlJugador > rangoSeguimiento)
        {
            ani.SetBool("Walk", false);
            ani.SetBool("Run", false);
            ani.SetBool("Attack", false);
            atacando = false;
        }
        else
        {
            if (distanciaAlJugador <= 5f)
            {
                agent.SetDestination(target.transform.position);
                ani.SetBool("Walk", true);
                ani.SetBool("Run", true);
            }
            else
            {
                cronometro += 1 * Time.deltaTime;
                if (cronometro >= 4)
                {
                    rutina = Random.Range(0, 2);
                    cronometro = 0;
                }

                switch (rutina)
                {
                    case 0:
                        ani.SetBool("Walk", false);
                        break;

                    case 1:
                        grado = Random.Range(0, 360);
                        angulo = Quaternion.Euler(0, grado, 0);
                        rutina++;
                        break;

                    case 2:
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                        transform.Translate(Vector3.forward * 1 * Time.deltaTime);
                        ani.SetBool("Walk", true);
                        break;
                }
            }
        }
    }

    public void FinalAnimacion()
    {
        ani.SetBool("Attack", false);
        atacando = false;
    }
}