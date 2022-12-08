using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentsManager : MonoBehaviour
{
    /*====== PUBLIC ======*/
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
        float coord_x;
        float coord_z;
        NavMeshHit hit;
        Vector3 result = Vector3.zero;

        do {
            coord_x = Random.Range(0, 400);
            coord_z = Random.Range(0, 400);
        }while(!NavMesh.SamplePosition(new Vector3(coord_x, 20.0F,coord_z), out hit, 20.0f, NavMesh.AllAreas));

        result = hit.position;

        return result;
    }
}
