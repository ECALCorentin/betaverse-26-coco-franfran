using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Fish : MonoBehaviour
{
    public bool isAttached;

    [Header("Movement")]
    public float forwardForce = 2.5f;   // was 5f → half speed

    [Header("Rotation")]
    public float randomTorqueY = 1f;    // was 2f → half turning power
    public float torqueChangeInterval = 3f; // slower direction changes

    private Rigidbody rb;
    private float torqueTimer;
    private float currentTorqueY;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();


    }

    void FixedUpdate()
    {
        if (isAttached)
        {
          
            return;
        }

        // Slower forward movement
        rb.AddForce(transform.forward * forwardForce, ForceMode.Force);

        // Change direction less often
        torqueTimer -= Time.fixedDeltaTime;
        if (torqueTimer <= 0f)
        {
            currentTorqueY = Random.Range(-randomTorqueY, randomTorqueY);
            torqueTimer = torqueChangeInterval;
        }

        // Slower Y rotation
        rb.AddTorque(Vector3.up * currentTorqueY, ForceMode.Force);
    }
}
