using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundManager : MonoBehaviour
{
    public static GroundManager Instance;

    void Awake() => Instance = this;

    public Vector3 GetRandomPosition()
    {
        //1. Pick a point
        float coord_x;
        float coord_z;

        NavMeshHit hit;
        Vector3 random_position = Vector3.zero;

        do
        {
            coord_x = Random.Range(0, 400);
            coord_z = Random.Range(0, 400);
        } while (!NavMesh.SamplePosition(new Vector3(coord_x, 20.0F, coord_z), out hit, 20.0f, NavMesh.AllAreas));

        random_position = hit.position;

        return random_position;
    }
}