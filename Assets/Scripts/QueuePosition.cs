using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class QueuePosition : MonoBehaviour
{
    private bool _is_trigger = false;
    
    public int GetAttractionId()
    {
        return transform.parent.GetComponent<Attraction>().GetId();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out Visitor new_visitor) && !_is_trigger)
        {
            _is_trigger = true;
            Debug.Log("Queue end hit");
            // Vector3 direction = (collision.gameObject.transform.position - transform.position).normalized * 5.0F;
            Vector3 direction = -collision.gameObject.transform.forward * 5.0F;
            transform.position = collision.gameObject.transform.position;
            if(NavMesh.SamplePosition(transform.position + direction, out NavMeshHit hit, 5.0F, NavMesh.AllAreas))
            {
                transform.position = hit.position;
            }
            _is_trigger = false;
        }
    }
}
