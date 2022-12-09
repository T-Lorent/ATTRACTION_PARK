using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attraction : MonoBehaviour
{
    [SerializeField] private Transform _entrance;
    [SerializeField] private Transform _queue_position;
    [SerializeField] private Transform _exit;
    [SerializeField] private int _visitors_capacity = 1;
    [SerializeField] private Queue<GameObject> _current_visitors = new Queue<GameObject>();
    [SerializeField] private float _duration = 5F;

    private int _id;
    private Queue<GameObject> _queue = new Queue<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        if(NavMesh.SamplePosition(_entrance.position, out NavMeshHit entrance_hit, 10.0F, NavMesh.AllAreas)) _queue_position.position = entrance_hit.position;
        if(NavMesh.SamplePosition(_exit.position, out NavMeshHit exit_hit, 10.0F, NavMesh.AllAreas)) _exit.position = exit_hit.position;
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
        return _entrance.position;
    }

    public Vector3 GetQueuePosition()
    {
        return _queue_position.position;
    }

    public void AddVisitor(ref GameObject new_visitor)
    {
        if (_current_visitors.Count < _visitors_capacity)
        {
            BringInVisitor(ref new_visitor);
        }
        else
        {
            _queue.Enqueue(new_visitor);
        }
        
    }

    private void BringInVisitor(ref GameObject new_visitor)
    {
        new_visitor.GetComponent<Visitor>().SetState(Visitor.State.IN_ATTRACTION);
        _current_visitors.Enqueue(new_visitor);
        StartCoroutine("RollVisitors");
    }

    private IEnumerator RollVisitors()
    {
        yield return new WaitForSeconds(5);
        GameObject previous_visitor = _current_visitors.Dequeue();
        previous_visitor.transform.position = _exit.position;
        previous_visitor.SetActive(true);
        previous_visitor.GetComponent<Visitor>().SetState(Visitor.State.WALKING);

        if(_queue.Count > 0)
        {
            GameObject first_in_queue = _queue.Dequeue();
            BringInVisitor(ref first_in_queue);
            // UpdateQueue();
        }
    }

    private void UpdateQueue()
    {
        Debug.Log("Updating Queue");
        Vector3 previous_position = _entrance.transform.position;

        foreach(GameObject visitor in _queue)
        {
            Vector3 current_visitor_position = visitor.transform.position;
            visitor.GetComponent<Visitor>().SetDestination(previous_position);
            previous_position = current_visitor_position;
        }

        _queue_position.position = previous_position;
    }
}
