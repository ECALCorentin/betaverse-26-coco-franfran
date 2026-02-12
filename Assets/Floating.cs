using UnityEngine;

public class Floating : MonoBehaviour
{
    public float fishHeight = 0.2f;
    public float waterHeight = 0.4f;
    public float outOfWaterHeight = 0.5f;
    public LayerMask raycastLayers = 0;

    public float outOfWaterDrag = 0;
    public float inWaterDrag = 5;

    public bool isInWater;
    public float waterFade;

    public float springForce = 10;
    public float springDamping = 10;

    void FixedUpdate()
    {
        var rb = GetComponent<Rigidbody>();
        var ray = new Ray(rb.worldCenterOfMass, Vector3.down);

        var yPos = 0f;
        if (Physics.Raycast(ray, out var hitInfo, 10, raycastLayers))
        {
            yPos = hitInfo.distance;
        }

        waterFade = Mathf.InverseLerp(outOfWaterHeight, waterHeight, hitInfo.distance);
        rb.linearDamping = Mathf.Lerp(outOfWaterDrag, inWaterDrag, waterFade);
        rb.angularDamping = Mathf.Lerp(outOfWaterDrag, inWaterDrag, waterFade);

        var force = Vector3.up * (fishHeight - yPos) * springForce - rb.linearVelocity * springDamping;
        rb.AddForce(force * waterFade);

        isInWater = waterFade > 0;
    }
}