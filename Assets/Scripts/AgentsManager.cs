using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentsManager : MonoBehaviour
{
    /*====== PUBLIC ======*/
    [Header("AGENT")]
    public GameObject walker_mesh;

    [Header("CROWD MANAGEMENT")]
    public int walker_number = 25;
    public int walker_increment = 25;

    /*====== PRIVATE ======*/
    private List<GameObject> walkers = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<walker_number; ++i)
        {
            CreateWalker();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateWalker(){
        GameObject new_walker = Instantiate(walker_mesh);
        new_walker.transform.parent = transform;
        walkers.Add(new_walker);
    }

    public void AddWalkers(){
        walker_number += walker_increment;
        
        for(int i=0; i<walker_increment; ++i)
        {
            CreateWalker();
        }
    }
}
