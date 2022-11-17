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
    
    private void CreateAgent(GameObject agent_mesh){
        GameObject new_agent = Instantiate(agent_mesh);
        new_agent.transform.parent = transform;

        if(agent_mesh == walker_mesh) {
            walkers.Add(new_agent);
        }else{
            visitors.Add(new_agent);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // POIs
        // foreach (Transform POI in Pois)
        // {
            
        // }

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
}
