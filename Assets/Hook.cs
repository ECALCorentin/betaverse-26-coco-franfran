using Unity.VisualScripting;
using UnityEngine;

public class Hook : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider collider)
    {

        if(collider.attachedRigidbody &&
            collider.attachedRigidbody.GetComponent<Fish>())
        {
            var joint = collider.attachedRigidbody.AddComponent<FixedJoint>();
            joint.connectedBody = GetComponent<Rigidbody>();
            collider.enabled = false;
        }
        
    }
}
