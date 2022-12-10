using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attraction : MonoBehaviour
{
    [SerializeField] private Transform _entrance;
    [SerializeField] private Queue _queue;
    [SerializeField] private Transform _exit;
    [SerializeField] private int _visitors_capacity = 1;
    [SerializeField] private Queue<GameObject> _current_visitors = new Queue<GameObject>();
    [SerializeField] private float _duration = 5F;

    private int _id;

    // Start is called before the first frame update
    void Start()
    {
        if (NavMesh.SamplePosition(_entrance.position, out NavMeshHit entrance_hit, 10.0F, NavMesh.AllAreas))
        {
            _entrance.position = entrance_hit.position;
            _queue.transform.position = entrance_hit.position;
        }
        if(NavMesh.SamplePosition(_exit.position, out NavMeshHit exit_hit, 10.0F, NavMesh.AllAreas)) _exit.position = exit_hit.position;
    }

    void Update()
    {
    
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
        return _queue.transform.position;
    }

    public bool IsFull()
    {
        return _current_visitors.Count == _visitors_capacity;
    }

    public void BringInVisitor(GameObject new_visitor)
    {
        new_visitor.GetComponent<Visitor>().SetState(Visitor.State.IN_ATTRACTION);
        new_visitor.SetActive(false);
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

        if(_queue.ContainsVisitors())
        {
            GameObject first_in_queue = _queue.GetFirstInLine();
            BringInVisitor(first_in_queue);
        }
    }
}
