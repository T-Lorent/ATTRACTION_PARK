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

        Vector3 destination_position = set_destination();
        nav_mesh_agent.SetDestination(destination_position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 set_destination()
    {
        // // Pick a POI
        // int number_of_POI = POIs.transform.childCount;
        // int random_index = Random.Range(0, number_of_POI);
        // Transform POI = POIs.transform.GetChild(random_index);
        // return POI.transform.position;
        return new Vector3(0,0,0);
    }
}
