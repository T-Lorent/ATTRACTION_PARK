using UnityEngine;
using UnityEngine.AI;

public class Visitor : MonoBehaviour
{

    /*====== PRIVATE ======*/
    private NavMeshAgent nav_mesh_agent = null;



    // Start is called before the first frame update
    void Start()
    {
        nav_mesh_agent = this.GetComponent<NavMeshAgent>();
        SetDestination();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetDestination()
    {
        Vector3 attraction_position = AttractionsManager.GetRandomAttraction();
        nav_mesh_agent.SetDestination(attraction_position);
    }
}
