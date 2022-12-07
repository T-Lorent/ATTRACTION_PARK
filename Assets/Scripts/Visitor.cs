using UnityEngine;
using UnityEngine.AI;

public class Visitor : MonoBehaviour
{

    /*====== PRIVATE ======*/
    private Attraction attraction_destination;
    private NavMeshAgent nav_mesh_agent = null;

    // Start is called before the first frame update
    void Start()
    {
        nav_mesh_agent = this.GetComponent<NavMeshAgent>();
        SetDestinationToNewAttraction();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetDestinationToNewAttraction()
    {
        attraction_destination = AttractionsManager.GetRandomAttraction();
        SetDestination(attraction_destination.GetEffectiveEntrance());
    }

    private void SetDestination(Vector3 destination)
    {
        nav_mesh_agent.SetDestination(destination);
    }
}
