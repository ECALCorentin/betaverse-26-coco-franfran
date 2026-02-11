using UnityEngine;

public class Hook : MonoBehaviour
{
    public Transform object1;
    public Transform object2;

    private Rigidbody hookedFishRb;   // store the specific fish
    private bool fishHooked = false;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.attachedRigidbody &&
            collider.attachedRigidbody.GetComponent<Fish>())
        {
            // Store THIS specific fish
            hookedFishRb = collider.attachedRigidbody;

            // Attach it to hook
            var joint = hookedFishRb.gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = GetComponent<Rigidbody>();

            collider.enabled = false;

            fishHooked = true;
        }
    }

    void Update()
    {
        if (!fishHooked || hookedFishRb == null)
            return;

        float y1 = object1.position.y;
        float y2 = object2.position.y;

        if (y1 > y2)
        {
            Debug.Log("HOORAY :D Fish is above!");

            // Remove only Y freeze from THIS fish
           hookedFishRb.constraints &= ~RigidbodyConstraints.FreezePositionY;
        }
    }
}
