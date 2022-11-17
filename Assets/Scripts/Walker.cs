using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Walker : MonoBehaviour
{
    private NavMeshAgent nav_mesh_agent = null;

    // Start is called before the first frame update
    void Start()
    {
        nav_mesh_agent = this.GetComponent<NavMeshAgent>();

        Vector3 spawn_origin = new Vector3(270, 0, 120);
        float spawn_range = 100;

        Vector3 start_position = GetRandomNavMeshPosition(spawn_origin, spawn_range);
        transform.position = start_position;
        set_destination(spawn_origin, spawn_range);
    }

    // Update is called once per frame
    void Update()
    {
        
        // Check if we've reached the destination
        if (!nav_mesh_agent.pathPending)
        {
            if (nav_mesh_agent.remainingDistance <= nav_mesh_agent.stoppingDistance)
            {
                if (!nav_mesh_agent.hasPath || nav_mesh_agent.velocity.sqrMagnitude == 0f)
                {
                    // Done
                    this.set_destination(new Vector3(200, 0, 200), 200);
                }
            }
        }
            
    }

    public void set_destination(Vector3 origin, float range)
    {
        Vector3 new_destination = GetRandomNavMeshPosition(origin, range);
        nav_mesh_agent.SetDestination(new_destination);
    }

    private Vector3 GetRandomNavMeshPosition(Vector3 origin, float range)
    {
        //1. Pick a point
        Vector3 point_on_sphere = Vector3.zero;
        NavMeshHit hit;
        Vector3 result = Vector3.zero;

        do {
            point_on_sphere = origin + Random.insideUnitSphere * range;
        }while(!NavMesh.SamplePosition(point_on_sphere, out hit, 1.0f, NavMesh.AllAreas));

        result = hit.position;

        return result;
    }
}
