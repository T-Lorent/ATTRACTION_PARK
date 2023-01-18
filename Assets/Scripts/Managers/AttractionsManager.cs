using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractionsManager : MonoBehaviour
{
    public static AttractionsManager Instance;

    public Dictionary<int, Attraction> attractions { get; private set; } = new Dictionary<int, Attraction>();

    [SerializeField]
    private Transform _attractions_container;
    
    void Awake() => Instance = this;

    void Start()
    {
        int attraction_idx = 0;
        foreach (Transform attraction in _attractions_container)
        {
            attraction.GetComponent<Attraction>().SetId(attraction_idx);
            attractions.Add(attraction_idx, attraction.GetComponent<Attraction>());
            attraction_idx++;
        }
    }

    public int GetRandomAttractionId()
    {
        return attractions[Random.Range(0, attractions.Count)].GetId();
    }
}
