using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Queue : MonoBehaviour
{
    private Attraction _attraction;
    private Queue<Visitor> _waiting_visitors = new Queue<Visitor>();
    private List<Visitor> _last_waiting_visitors = new List<Visitor>(3);
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
        UpdateLastInQueue();
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
                    UpdateLastInQueue(new_visitor);
                    StepBackQueueEnd();
                }

                _collider.enabled = true;
            }
        }
    }

    private void StepBackQueueEnd()
    {
        // Replacing queue position
        if (_waiting_visitors.Count >= 3)
        {
            /* Store the position of the 3 last visitors in queue */
            Vector3 position_1 = _last_waiting_visitors[0].transform.position;
            Vector3 position_2 = _last_waiting_visitors[1].transform.position;
            Vector3 position_3 = _last_waiting_visitors[2].transform.position;
            
            Vector3 direction_1_to_2 = (position_2 - position_1).normalized;
            Vector3 direction_1_to_3 = (position_3 - position_1).normalized;

            /* Compute divergent angle between waiters on XZ plan */
            Quaternion z_rotation = Quaternion.FromToRotation(
                new Vector3(direction_1_to_2.x, 0, direction_1_to_2.z),
                new Vector3(direction_1_to_3.x, 0, direction_1_to_3.z)
            );

            /* MOVE QUEUE END TO NEXT POSITION */
            transform.position = position_3;
            transform.rotation *= Quaternion.FromToRotation(-transform.forward, position_2) * z_rotation;
            transform.Translate(transform.forward * 6.0F);
        }
        else
        {
            /* MOVE QUEUE END TO NEXT POSITION */
            transform.Translate(Vector3.forward * 6.0F);
        }

        /* MOVE THE COMPUTE POSITION TO A NAVMESH POSITION*/
        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 5.0F, NavMesh.AllAreas))
        {
            transform.position = hit.position;
        }

        /* RESET END QUEUE ROTATION */
        transform.rotation = _attraction.transform.rotation;
    }

    private void UpdateLastInQueue(Visitor last_visitor)
    {
        if(_last_waiting_visitors.Count < 3)
        {
            _last_waiting_visitors.Add(last_visitor);
        }
        else
        {
            List<Visitor> updated_last_in_queue = new List<Visitor>(3);

            for(int i = 1; i < _last_waiting_visitors.Count; ++i)
            {
                updated_last_in_queue.Add(_last_waiting_visitors[i]);
            }
            
            updated_last_in_queue.Add(last_visitor);
            _last_waiting_visitors = updated_last_in_queue;
            
        }
    }

    private void UpdateLastInQueue()
    {
        if(_waiting_visitors.Count < 3)
        {
            _last_waiting_visitors = new List<Visitor>(3);

            foreach(Visitor last_in_queue in _waiting_visitors)
            {
                _last_waiting_visitors.Add(last_in_queue);
            }
        }
    }
}
