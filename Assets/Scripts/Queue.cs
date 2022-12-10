using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Queue : MonoBehaviour
{
    private Attraction _attraction;
    private Queue<GameObject> _waiting_visitors = new Queue<GameObject>();
    private Collider _collider;

    void Start()
    {
        _attraction = transform.parent.GetComponent<Attraction>();
        _collider = this.GetComponent<Collider>();
    }
    
    public int GetAttractionId()
    {
        return _attraction.GetId();
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public bool HasWaitingVisitor()
    {
        return _waiting_visitors.Count > 0;
    }

    public GameObject GetFirstInLine()
    {
        return _waiting_visitors.Dequeue();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out Visitor new_visitor))
        {
            _collider.enabled = false;

            GameObject visitor_gameObject = collision.gameObject;

            if (!_attraction.IsFull())
            {
                _attraction.BringInVisitor(visitor_gameObject);
            }
            else
            {
                _waiting_visitors.Enqueue(visitor_gameObject);
            }

            // Replacing queue position
            if (HasWaitingVisitor())
            {
                Vector3 direction = -new_visitor.transform.forward * 5.0F;
                transform.position = new_visitor.transform.position;
                if (NavMesh.SamplePosition(transform.position + direction, out NavMeshHit hit, 5.0F, NavMesh.AllAreas))
                {
                    transform.position = hit.position;
                }
            }

            _collider.enabled = true;
        }
    }

    private void UpdateQueue()
    {
        Debug.Log("Updating Queue");
        Vector3 previous_position = _attraction.GetEntrancePosition();

        foreach (GameObject visitor in _waiting_visitors)
        {
            Vector3 current_visitor_position = visitor.transform.position;
            visitor.GetComponent<Visitor>().SetDestination(previous_position);
            previous_position = current_visitor_position;
        }

        transform.position = previous_position;
    }
}
