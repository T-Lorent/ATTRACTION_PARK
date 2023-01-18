using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Queue : MonoBehaviour
{
    static public float distance_between_visitors = 7.0F;
    private Attraction _attraction;
    private Queue<Visitor> _waiting_visitors = new Queue<Visitor>();
    private Visitor _last_in_queue = null;
    private Collider _collider;

    void Start()
    {
        _attraction = transform.parent.GetComponent<Attraction>();
        _collider = this.GetComponent<Collider>();
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public bool ContainsVisitors()
    {
        return _waiting_visitors.Count > 0;
    }

    public Visitor GetFirstInLine()
    {
        Visitor first_in_line = _waiting_visitors.Dequeue();
        if (ContainsVisitors())
        {
            _waiting_visitors.Peek().SetBeforeInQueue(null);
        }
        else
        {
            _last_in_queue = null;
            transform.parent = null;
            transform.position = _attraction.GetEntrancePosition();
        }
        return first_in_line;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out Visitor new_visitor))
        {
            if(new_visitor._state == Visitor.State.WALKING && new_visitor._attraction_id == _attraction.GetId())
            {
                _collider.enabled = false;

                new_visitor.SetState(Visitor.State.WAITING);
                
                if (!_attraction.IsFull())
                {
                    _attraction.BringInVisitor(new_visitor);
                }
                else
                {
                    _waiting_visitors.Enqueue(new_visitor);
                    if(_last_in_queue != null) new_visitor.SetBeforeInQueue(_last_in_queue);
                    _last_in_queue = new_visitor;
                    transform.position = new_visitor.transform.position;
                    transform.parent = new_visitor.transform;
                }

                _collider.enabled = true;
            }
        }
    }
}
