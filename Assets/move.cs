using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FishHeadSwim : MonoBehaviour
{
    public float forwardForce = 5f;
    public float torqueStrength = 2f;
    public float rotationChangeSpeed = 1f;   // how often direction changes
    public float maxRandomTorque = 1.5f;

    private Rigidbody rb;
    private Vector3 currentTorque;
    private float torqueTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;          // usually fish don�t use gravity
        rb.linearDamping = 1f;                   // water resistance
        rb.angularDamping = 2f;
    }

    void FixedUpdate()
    {
        // Constant forward movement
        rb.AddForce(transform.forward * forwardForce, ForceMode.Force);

        // Random torque change over time
        torqueTimer -= Time.fixedDeltaTime;
        if (torqueTimer <= 0f)
        {
            currentTorque = new Vector3(
                Random.Range(-maxRandomTorque, maxRandomTorque),
                Random.Range(-maxRandomTorque, maxRandomTorque),
                Random.Range(-maxRandomTorque, maxRandomTorque)
            );

            torqueTimer = 1f / rotationChangeSpeed;
        }

        // Apply smooth torque
        rb.AddTorque(currentTorque * torqueStrength, ForceMode.Force);
    }
}
