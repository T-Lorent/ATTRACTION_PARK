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

    [Header("SPAWN MANAGEMENT")]
    [SerializeField] private int grid_size = 10;
    [SerializeField] private Terrain terrain;
    [SerializeField] private GameObject lake;

    /*====== PRIVATE ======*/
    private List<Walker> walkers = new List<Walker>();
    private List<Visitor> visitors = new List<Visitor>();
    private List<Vector3> spawnable_case = new List<Vector3>();
    private float caseSizeX;
    private float caseSizeZ;

    

    /*====== UNITY METHODS ======*/
    private void Awake() => Instance = this;

    private void Start()
    {

       NavMesh.pathfindingIterationsPerFrame= 1000;
        CreateSpawnGrid();

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

        do
        {
            //1. Pick a random index in spawnable case range
            random = UnityEngine.Random.Range(0, spawnable_case.Count);

        } while (!NavMesh.SamplePosition(new Vector3(spawnable_case[random].x, -10.0f, spawnable_case[random].z), out hit, 50.0f, NavMesh.AllAreas));

        //We calcul an offset from the center of the case
        float position_offset_x = UnityEngine.Random.Range(-caseSizeX/2f, caseSizeX/2f);
        float position_offset_z = UnityEngine.Random.Range(-caseSizeZ/2f, caseSizeZ/2f);
        random_position = hit.position;

        return new Vector3(random_position.x + position_offset_x, random_position.y, random_position.z + position_offset_z);
    }



    public void CreateSpawnGrid()
    {
        float lake_height = lake.transform.position.y + (lake.transform.localScale.y/2);


        float minCoordX = terrain.transform.position.x;
        float minCoordZ = terrain.transform.position.z;

        float maxCoordX = minCoordX + terrain.terrainData.size.x;
        float maxCoordZ = minCoordZ + terrain.terrainData.size.z;

        caseSizeX= (maxCoordX - minCoordX) / grid_size;
        caseSizeZ= (maxCoordZ - minCoordZ) / grid_size;

        Vector2 center = new Vector2(caseSizeX/2f, caseSizeZ/2f);

        for(int i=0; i< grid_size; ++i)
        {
            for(int j=0; j< grid_size; ++j)
            {
                float positionX = i*caseSizeX + center.x + minCoordX;
                float positionZ = j*caseSizeZ + center.y + minCoordZ;


                float height = terrain.SampleHeight(new Vector3(positionX, 0, positionZ));
                if(height > lake_height)
                {
                    spawnable_case.Add(new Vector3(positionX, height, positionZ));
                }
            }
        }
    }
}
