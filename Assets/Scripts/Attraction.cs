using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attraction : MonoBehaviour
{
    [SerializeField] private Transform entrance;
    [SerializeField] private Transform queue_position;
    [SerializeField] private Transform exit;
    [SerializeField] private int visitors_capacity = 1;
    [SerializeField] private int current_visitors = 0;
    [SerializeField] private float duration = 5F;

    private int _id;
    private Queue<Visitor> _queue = new Queue<Visitor>();

    // Start is called before the first frame update
    void Start()
    {
        if(NavMesh.SamplePosition(entrance.position, out NavMeshHit hit, 10.0F, NavMesh.AllAreas)) queue_position.position = hit.position;
    }

    public int GetId()
    {
        return _id;
    }

    public void SetId(int id)
    {
        _id = id;
    }

    public Vector3 GetEntrancePosition()
    {
        return entrance.position;
    }

    public Vector3 GetQueuePosition()
    {
        return queue_position.position;
    }

    public void addVisitor(Visitor new_visitor)
    {
        _queue.Enqueue(new_visitor);
        queue_position.Translate(new_visitor.transform.position - new_visitor.transform.forward);
    }
}
