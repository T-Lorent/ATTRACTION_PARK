using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class QueuePosition : MonoBehaviour
{
    private Attraction _attraction;
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

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out Visitor new_visitor))
        {
            _collider.enabled = false;

            GameObject visitor_gameObject = collision.gameObject;
            _attraction.AddVisitor(ref visitor_gameObject);

            // Replacing queue position
            Vector3 direction = -new_visitor.transform.forward * 5.0F;
            transform.position = new_visitor.transform.position;
            if(NavMesh.SamplePosition(transform.position + direction, out NavMeshHit hit, 5.0F, NavMesh.AllAreas))
            {
                transform.position = hit.position;
            }

            _collider.enabled = true;
        }
    }
}
