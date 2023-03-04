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
    void Awake() => Instance = this;

    // Start is called before the first frame update
    void Start()
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
    }

    public void AddVisitors(){
        visitor_number += visitor_increment;
        
        for(int i=0; i<visitor_increment; ++i)
        {
            CreateVisitor();
        }
    }

    public Vector3 GetRandomPosition()
    {
        float coord_x;
        float coord_z;

        NavMeshHit hit;
        Vector3 random_position = Vector3.zero;

        do
        {
            //1. Pick a point
            coord_x = UnityEngine.Random.Range(GroundManager.Instance.X_MIN, GroundManager.Instance.X_MAX);
            coord_z = UnityEngine.Random.Range(GroundManager.Instance.Z_MIN, GroundManager.Instance.Z_MAX);
        } while (!NavMesh.SamplePosition(new Vector3(coord_x, -10.0F, coord_z), out hit, 20.0f, NavMesh.AllAreas));

        random_position = hit.position;

        return random_position;
    }
}
