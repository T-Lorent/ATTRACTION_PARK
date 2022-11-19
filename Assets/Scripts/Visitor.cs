using UnityEngine;
using UnityEngine.AI;

public class Visitor : MonoBehaviour
{
    /*====== PUBLIC ======*/
    public GameObject POIs;

    /*====== PRIVATE ======*/
    private NavMeshAgent nav_mesh_agent = null;



    // Start is called before the first frame update
    void Start()
    {
        nav_mesh_agent = this.GetComponent<NavMeshAgent>();

        Vector3 spawn_origin = new Vector3(0, 0, 0);
        float spawn_range = 300;

        Vector3 start_position = GetRandomNavMeshPosition(spawn_origin, spawn_range);
        transform.position = start_position;

        Vector3 destination_position = set_destination();
        nav_mesh_agent.SetDestination(destination_position);
    }

    // Update is called once per frame
    void Update()
    {
        
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

    private Vector3 set_destination()
    {
        // Pick a POI
        int number_of_POI = POIs.transform.childCount;
        int random_index = Random.Range(0, number_of_POI);
        Transform POI = POIs.transform.GetChild(random_index);
        
        return POI.transform.position;
    }
}
