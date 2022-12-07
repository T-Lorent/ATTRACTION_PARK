using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractionsManager : MonoBehaviour
{
    private static List<Vector3> attractions_position = new List<Vector3>();
    
    void Start()
    {
        foreach (Transform POI in transform)
        {
            attractions_position.Add(POI.transform.position);
        }
    }

    public static Vector3 GetRandomAttraction()
    {
        return attractions_position[Random.Range(0, attractions_position.Count)];
    }
}
