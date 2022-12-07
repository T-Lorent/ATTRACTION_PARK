using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractionsManager : MonoBehaviour
{
    private static Dictionary<int, Attraction> attractions = new Dictionary<int, Attraction>();
    
    void Start()
    {
        int index = 0;
        foreach (Transform attraction in transform)
        {
            attraction.GetComponent<Attraction>().SetId(index);
            attractions.Add(index, attraction.GetComponent<Attraction>());
            index++;
        }
    }

    public static Vector3 GetQueuePosition(int id)
    {
        return attractions[id].GetQueuePosition();
    }

    public static int GetRandomAttractionId()
    {
        return attractions[Random.Range(0, attractions.Count)].GetId();
    }
}
