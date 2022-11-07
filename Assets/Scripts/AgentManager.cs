using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentManager : MonoBehaviour
{
    public GameObject agent_mesh;
    public int AGENT_NUMBER = 25;

    private List<GameObject> agents = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<AGENT_NUMBER; ++i)
        {
            agents.Add(Instantiate(agent_mesh));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
