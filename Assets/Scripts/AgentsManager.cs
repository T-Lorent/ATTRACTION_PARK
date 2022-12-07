using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentsManager : MonoBehaviour
{
    /*====== PUBLIC ======*/
    [Header("AGENT MANAGEMENT")]
    public GameObject POIs;

    [Header("AGENT MANAGEMENT")]
    public GameObject walker_mesh;
    public GameObject visitor_mesh;

    [Header("WALKERS MANAGEMENT (CROWD)")]
    public int walker_number = 25;
    public int walker_increment = 25;

    [Header("VISITORS MANAGEMENT")]
    public int visitor_number = 25;
    public int visitor_increment = 25;

    /*====== PRIVATE ======*/
    private List<GameObject> walkers = new List<GameObject>();
    private List<GameObject> visitors = new List<GameObject>();
    private List<Vector3> POIs_position = new List<Vector3>();

    private static Vector3 spawn_origin = new Vector3(0, 0, 0);
    private static float spawn_range = 300;
    
    void CreateAgent(GameObject agent_mesh){
        Vector3 spawn_position = GetRandomNavMeshPosition();
        GameObject new_agent = Instantiate(agent_mesh, spawn_position, Quaternion.identity, transform);

        if(agent_mesh == walker_mesh) {
            new_agent.GetComponent<Renderer>().material.color = Color.red;
            walkers.Add(new_agent);
        }else{
            new_agent.GetComponent<Renderer>().material.color = Color.blue;
            visitors.Add(new_agent);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // POIs
        foreach (Transform POI in POIs.transform)
        {
            POIs_position.Add(POI.transform.position);
        }

        // AGENTS
        for(int i=0; i < walker_number; ++i)
        {
            CreateAgent(walker_mesh);
        }

        for(int i=0; i < visitor_number; ++i)
        {
            CreateAgent(visitor_mesh);
        }
    }

    public void AddWalkers(){
        walker_number += walker_increment;
        
        for(int i=0; i<walker_increment; ++i)
        {
            CreateAgent(walker_mesh);
        }
    }

    public void AddVisitors(){
        visitor_number += visitor_increment;
        
        for(int i=0; i<visitor_increment; ++i)
        {
            CreateAgent(visitor_mesh);
        }
    }

     public static Vector3 GetRandomNavMeshPosition()
    {
        //1. Pick a point
        Vector3 point_on_sphere = Vector3.zero;
        NavMeshHit hit;
        Vector3 result = Vector3.zero;

        do {
            point_on_sphere = spawn_origin + Random.insideUnitSphere * spawn_range;
        }while(!NavMesh.SamplePosition(point_on_sphere, out hit, 1.0f, NavMesh.AllAreas));

        result = hit.position;

        return result;
    }
}
