using UnityEngine;

public class Fish : MonoBehaviour
{

    public bool isAttached;

    public float randomForce;

    void FixedUpdate()
    {
        var rb = GetComponent<Rigidbody>();
        if(isAttached)
            rb.AddForce(Random.insideUnitSphere * randomForce);
    }
}

