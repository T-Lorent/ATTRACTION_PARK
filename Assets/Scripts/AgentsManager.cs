using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentsManager : MonoBehaviour
{
    /*====== PUBLIC ======*/
    [Header("AGENT")]
    public GameObject agent_mesh;

    [Header("CROWD MANAGEMENT")]
    public int agent_number = 25;
    public int crowd_step = 25;

    /*====== PRIVATE ======*/
    private List<GameObject> agents = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<agent_number; ++i)
        {
            CreateAgent();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateAgent(){
        GameObject new_agent = Instantiate(agent_mesh);
        new_agent.transform.parent = transform;
        agents.Add(new_agent);
    }

    public void AddAgents(){
        agent_number += crowd_step;
        
        for(int i=0; i<agent_number; ++i)
        {
            CreateAgent();
        }
    }
}
