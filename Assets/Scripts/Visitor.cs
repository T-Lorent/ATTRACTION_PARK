using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Visitor : MonoBehaviour
{
    /*====== PRIVATE ======*/
    private enum State
    {
        READY_TO_GO,
        WALKING,
        ARRIVED,
        WAITING,
        IN_ATTRACTION
    };
    private State _state;
    private int _attraction_id;
    private NavMeshAgent _nav_mesh_agent = null;

    // Start is called before the first frame update
    void Start()
    {
        _state = State.READY_TO_GO;
        _nav_mesh_agent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
            case State.READY_TO_GO:
                SetState(State.WALKING);
                break;

            case State.WALKING:
                Vector3 queue_position_position = AttractionsManager.Instance.attractions[_attraction_id].GetQueuePosition();

                if ((_nav_mesh_agent.destination - queue_position_position).sqrMagnitude > 0.05)
                {
                    SetDestination(queue_position_position);
                }
                break;

            case State.WAITING:
                
                break;
            
            case State.IN_ATTRACTION:
                break;

            default:
                Debug.Log("Invalid Visitor state");
                break;
        }
    }

    private void SetState(State requested_state)
    {
        switch (requested_state)
        {
            case State.WALKING:
                if(_state == State.READY_TO_GO) SetDestinationToNewAttraction();
                _state = State.WALKING;
                break;

            case State.ARRIVED:
                SetDestination(transform.position);
                _state = State.ARRIVED;
                break;

            default:
                _state = requested_state;
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out QueuePosition queue_end))
        {
            if (queue_end.GetAttractionId() == _attraction_id)
            {
                SetState(State.ARRIVED);
            }
        }
    }

    private void SetDestinationToNewAttraction()
    {
        _attraction_id = AttractionsManager.Instance.GetRandomAttractionId();
        _nav_mesh_agent.SetDestination(AttractionsManager.Instance.attractions[_attraction_id].GetQueuePosition());
    }

    private void SetDestination(Vector3 destination)
    {
        _nav_mesh_agent.SetDestination(destination);
    }
}
