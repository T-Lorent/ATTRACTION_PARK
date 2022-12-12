using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Visitor : MonoBehaviour
{
    /*====== PRIVATE ======*/
    public enum State
    {
        WALKING,
        WAITING,
        IN_ATTRACTION
    };
    private State _state;
    private NavMeshAgent _nav_mesh_agent = null;

    private int _attraction_id;
    private Visitor _before_in_line = null;

    // Start is called before the first frame update
    void Start()
    {
        _nav_mesh_agent = this.GetComponent<NavMeshAgent>();
        SetState(State.WALKING);
    }

    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
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

    public void SetState(State requested_state)
    {
        switch (requested_state)
        {
            case State.WALKING:
                _nav_mesh_agent.avoidancePriority = 50;
                SetDestinationToNewAttraction();
                _state = State.WALKING;
                break;

            case State.WAITING:
                SetDestination(transform.position);
                _nav_mesh_agent.avoidancePriority = 0;
                _state = State.WAITING;
                break;

            case State.IN_ATTRACTION:
                // The Visitor is SetActive(False) in the Attraction class
                break;

            default:
                _state = requested_state;
                break;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.TryGetComponent(out Queue queue_end))
        {
            if (queue_end.GetAttractionId() == _attraction_id)
            {
                SetState(State.WAITING);
            }
        }
    }

    private void SetDestinationToNewAttraction()
    {
        _attraction_id = AttractionsManager.Instance.GetRandomAttractionId();
        _nav_mesh_agent.SetDestination(AttractionsManager.Instance.attractions[_attraction_id].GetQueuePosition());
    }

    public void SetDestination(Vector3 destination)
    {
        _nav_mesh_agent.SetDestination(destination);
    }

    public void SetBeforeInLine(Visitor before_in_line)
    {
        _before_in_line = before_in_line;
    }
}
