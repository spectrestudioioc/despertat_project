using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public List<Transform> waypoints; // Llista de waypoints per on l'enemic es mour�
    public float speed = 1f; // Velocitat de despla�ament
    private int currentWaypointIndex = 0;

    private NavMeshAgent agent; // Refer�ncia al NavMeshAgent
    


    void Start()
    {
        if (waypoints.Count == 0)
        {
            Debug.LogWarning("No hi ha waypoints assignats per aquest enemic!");
            return;
        }

        agent = GetComponent<NavMeshAgent>();  // Obtenim el component NavMeshAgent
        if (agent == null)
        {
            Debug.LogError("Aquest enemic necessita un NavMeshAgent per moure's!");
            return;
        }

        

        agent.speed = speed;  // Assignem la velocitat a l'agent
        agent.autoBraking = false; // Evitem que freni abans d'arribar al waypoint

        MoveToNextWaypoint();  // Moure l'enemic al primer waypoint
    }

    void Update()
    {
        // Comprovem si l'agent ha arribat al waypoint actual
        if (HasArrived())
        {
            // Si ha arribat, actualitzem el waypoint al seg�ent
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Count)
            {
                currentWaypointIndex = 0;  // Torna al primer waypoint
            }

            // Assignem el seg�ent waypoint com a destinaci�
            MoveToNextWaypoint();
        }

        
    }

    private void MoveToNextWaypoint()
    {
        // Assignem el seg�ent waypoint com a destinaci� per l'agent
        agent.SetDestination(waypoints[currentWaypointIndex].position);
    }

    private bool HasArrived()
    {
        // Comprovem si l'agent ha arribat al waypoint (amb un marge de toler�ncia)
        return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance;
    }
}