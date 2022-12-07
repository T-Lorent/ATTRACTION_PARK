using UnityEngine;
using UnityEngine.AI;

public class Visitor : MonoBehaviour
{

    /*====== PRIVATE ======*/
    private enum State
    {
        READY_TO_GO,
        WALKING,
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
                SetDestinationToNewAttraction();
                _state = State.WALKING;
                break;

            case State.WALKING:
                Vector3 queue_position_position = AttractionsManager.GetQueuePosition(_attraction_id);

                if((_nav_mesh_agent.destination - transform.position).sqrMagnitude < 10)
                {
                    if(_nav_mesh_agent.destination != queue_position_position)
                    {
                        SetDestination(queue_position_position);
                    }
                    else if((queue_position_position - transform.position).sqrMagnitude < 5)
                    {
                        _state = State.WAITING;
                    }
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

    private void SetDestinationToNewAttraction()
    {
        _attraction_id = AttractionsManager.GetRandomAttractionId();
        SetDestination(AttractionsManager.GetQueuePosition(_attraction_id));
    }

    private void SetDestination(Vector3 destination)
    {
        _nav_mesh_agent.SetDestination(destination);
    }
}
