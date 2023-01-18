using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractionsManager : MonoBehaviour
{
    public static AttractionsManager Instance;

    public Dictionary<int, Attraction> attractions { get; private set; } = new Dictionary<int, Attraction>();
    
    void Awake() => Instance = this;

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

    public int GetRandomAttractionId()
    {
        return attractions[Random.Range(0, attractions.Count)].GetId();
    }
}
