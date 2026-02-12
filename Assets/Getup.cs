using UnityEngine;

public class Getup : MonoBehaviour
{
    public float torqueMultiplier = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var rb = GetComponent<Rigidbody>();
        var targetRotation = Quaternion.LookRotation(transform.forward, Vector3.up);
        var rotationDiff = targetRotation * Quaternion.Inverse(rb.rotation);

        //rb.rotation = targetRotation;
        rotationDiff.ToAngleAxis(out var angle, out var axis);
        if (angle > 180f)
            angle -= 360f;
        rb.AddTorque(axis *angle* torqueMultiplier*GetComponent<Floating>().waterFade);
    }
}
