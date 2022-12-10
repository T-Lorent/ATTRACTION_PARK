using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Queue : MonoBehaviour
{
    private Attraction _attraction;
    private Queue<GameObject> _waiting_visitors = new Queue<GameObject>();
    private GameObject[] _last_waiting_visitors = new GameObject[3];
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

    public bool ContainsVisitors()
    {
        return _waiting_visitors.Count > 0;
    }

    public GameObject GetFirstInLine()
    {
        GameObject first_in_line = _waiting_visitors.Dequeue();
        Advance(first_in_line.transform.position);
        return first_in_line;
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
                _last_waiting_visitors[0] = _last_waiting_visitors[1];
                _last_waiting_visitors[1] = _last_waiting_visitors[2];
                _last_waiting_visitors[2] = visitor_gameObject;
                StepBackQueueEnd();
            }

            _collider.enabled = true;
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

    private void Advance(Vector3 previous_position)
    {
        if(!ContainsVisitors())
        {
            transform.position = _attraction.GetEntrancePosition();
        }
        else
        {
            foreach (GameObject visitor in _waiting_visitors)
            {
                Vector3 current_visitor_position = visitor.transform.position;
                visitor.GetComponent<Visitor>().SetDestination(previous_position);
                previous_position = current_visitor_position;
            }

            transform.position = previous_position;
        }
    }
}
