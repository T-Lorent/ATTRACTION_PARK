using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractionsManager : MonoBehaviour
{
    private static List<Attraction> attractions_position = new List<Attraction>();
    
    void Start()
    {
        foreach (Transform attraction in transform)
        {
            attractions_position.Add(attraction.GetComponent<Attraction>());
        }
    }

    public static Attraction GetRandomAttraction()
    {
        return attractions_position[Random.Range(0, attractions_position.Count)];
    }
}
