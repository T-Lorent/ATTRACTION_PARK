using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Walker : MonoBehaviour
{
    private NavMeshAgent _nav_mesh_agent = null;

    // Start is called before the first frame update
    void Start()
    {
        _nav_mesh_agent = this.GetComponent<NavMeshAgent>();
        SetDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_nav_mesh_agent.pathPending)
        {
            if (_nav_mesh_agent.remainingDistance <= _nav_mesh_agent.stoppingDistance)
            {
                if (!_nav_mesh_agent.hasPath)
                {
                    // Done
                    SetDestination();
                }
            }

            //If the walker is stuck , we give him a new destination
            if(_nav_mesh_agent.velocity.sqrMagnitude < 5f)
            {
                SetDestination();
            }       
        }
            
    }

    public void SetDestination()
    {
        _nav_mesh_agent.SetDestination(
            AgentsManager.Instance.GetRandomPosition()
        );
    }
}
