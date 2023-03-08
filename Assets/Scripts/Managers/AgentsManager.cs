using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentsManager : MonoBehaviour
{
    public static AgentsManager Instance;

    /*====== PUBLIC ======*/
    [Header("AGENT MANAGEMENT")]
    public Walker walker_prefab;
    public Visitor visitor_prefab;

    [Header("WALKERS MANAGEMENT (CROWD)")]
    public int walker_number = 50;
    public int walker_increment = 25;

    [Header("VISITORS MANAGEMENT")]
    public int visitor_number = 50;
    public int visitor_increment = 25;
    

    /*====== PRIVATE ======*/
    private List<Walker> walkers = new List<Walker>();
    private List<Visitor> visitors = new List<Visitor>();
   

    

    /*====== UNITY METHODS ======*/
    private void Awake() => Instance = this;

    private void Start()
    {
        // WALKERS
        for(int i=0; i < walker_number; ++i)
        {
            CreateWalker();
        }

        // VISITORS
        for(int i=0; i < visitor_number; ++i)
        {
            CreateVisitor();
        }

        UIManager.Instance.update_nb_agents(
            visitors.Count,
            walkers.Count
        );
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            AddVisitors();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            AddWalkers();
        }
    }

    /*====== METHODS ======*/
    private void CreateWalker()
    {
        Walker new_walker = Instantiate(
            walker_prefab,
            GetRandomPosition(),
            Quaternion.identity,
            transform
        );

        walkers.Add(new_walker);
    }

    private void CreateVisitor()
    {
        Visitor new_visitor = Instantiate(
            visitor_prefab,
            GetRandomPosition(),
            Quaternion.identity,
            transform
        );

        visitors.Add(new_visitor);
    }

    public void AddWalkers(){
        walker_number += walker_increment;
        
        for(int i=0; i<walker_increment; ++i)
        {
            CreateWalker();
        }

        UIManager.Instance.update_nb_agents(
            visitors.Count,
            walkers.Count
        );
    }

    public void AddVisitors(){
        visitor_number += visitor_increment;
        
        for(int i=0; i<visitor_increment; ++i)
        {
            CreateVisitor();
        }

        UIManager.Instance.update_nb_agents(
            visitors.Count,
            walkers.Count
        );
    }

    public Vector3 GetRandomPosition()
    {
        NavMeshHit hit;
        Vector3 random_position = Vector3.zero;
        int random;

        Vector2 cell_size= new Vector2(GroundManager.Instance.cellSizeX, GroundManager.Instance.cellSizeZ);

        //We calcul an offset from the center of the cell
        float position_offset_x = UnityEngine.Random.Range(-cell_size.x/2f, cell_size.x/2f);
        float position_offset_z = UnityEngine.Random.Range(-cell_size.y/2f, cell_size.y/2f);

        do
        {
            //1. Pick a random index in spawnable cell range
            random = UnityEngine.Random.Range(0, GroundManager.Instance.spawnable_cell.Count);

        } while (!NavMesh.SamplePosition(new Vector3(GroundManager.Instance.spawnable_cell[random].x+position_offset_x, -10.0f, GroundManager.Instance.spawnable_cell[random].z+position_offset_z), out hit, 50.0f, NavMesh.AllAreas));

        random_position = hit.position;

        return new Vector3(random_position.x, random_position.y, random_position.z);
    }



    
}
