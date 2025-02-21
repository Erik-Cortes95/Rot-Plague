using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Animator ani;
    public float grado;
    private Quaternion angulo;

    public GameObject target;
    public bool atacando;

    private NavMeshAgent agent;

    void Start()
    {
        ani = GetComponent<Animator>();
        target = GameObject.Find("Link");
        agent = GetComponent<NavMeshAgent>();
    }

    public void Comportamiento_Enemigo()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > 5)
        {
            cronometro += 1 * Time.deltaTime;
            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }
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

        if (Vector3.Distance(transform.position, target.transform.position) <= 5)
        {
            agent.SetDestination(target.transform.position);
            ani.SetBool("Walk", true);
            ani.SetBool("Run", true);
            transform.Translate(Vector3.forward * 2 * Time.deltaTime);
        }
        else
        {
            ani.SetBool("Walk", false);
            ani.SetBool("Attack", true);
            atacando = true;
        }
    }

    public void FinalAnimacion()
    {
        ani.SetBool("Attack", false);
        atacando = false;
    }
}