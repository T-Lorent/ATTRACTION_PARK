using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attraction : MonoBehaviour
{
    [SerializeField] private Transform entrance;
    [SerializeField] private Transform exit;
    [SerializeField] private int visitors_capacity = 1;
    [SerializeField] private int current_visitors = 0;
    [SerializeField] private float duration = 5F;

    private Queue<Visitor> queue = new Queue<Visitor>();
    private Vector3 effective_entrance;

    // Start is called before the first frame update
    void Start()
    {
        effective_entrance = entrance.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetEffectiveEntrance()
    {
        return effective_entrance;
    }
}
