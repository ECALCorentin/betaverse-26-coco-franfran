
using UnityEngine;

public class FishTail : MonoBehaviour
{
    public ConfigurableJoint joint;
    public float strength = 50f;
    public float frequency = 5f;

    void Start()
    {
        joint = GetComponent<ConfigurableJoint>();
    }

    void FixedUpdate()
    {
        float targetAngle = Mathf.Sin(Time.time * frequency) * 25f;
        joint.targetRotation = Quaternion.Euler(0, targetAngle, 0);
    }
}
