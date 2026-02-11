using UnityEngine;
using System.Collections;

public class Hook : MonoBehaviour
{
    public Transform object1;          // Usually hook
    public Transform object2;          // Usually rod tip or reference point
    public LineLength lineLength;      // Reference to LineLength script

    private Rigidbody hookedFishRb;   
    private bool fishHooked = false;
    private bool reelStarted = false;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.attachedRigidbody &&
            collider.attachedRigidbody.GetComponent<Fish>())
        {
            // Store THIS specific fish
            hookedFishRb = collider.attachedRigidbody;

            // Attach fish to hook
            var joint = hookedFishRb.gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = GetComponent<Rigidbody>();

            collider.enabled = false;

            fishHooked = true;
        }
    }

    void Update()
    {
        if (!fishHooked || hookedFishRb == null || reelStarted)
            return;

        float y1 = object1.position.y;
        float y2 = object2.position.y;

        if (y1 > y2)
        {
            Debug.Log("HOORAY :D Fish is above!");

            // Remove only Y freeze from THIS fish
            hookedFishRb.constraints &= ~RigidbodyConstraints.FreezePositionY;

            reelStarted = true;
            StartCoroutine(ReelAfterDelay());
        }
    }

    private IEnumerator ReelAfterDelay()
    {
        yield return new WaitForSeconds(2f);

        if (lineLength != null)
        {
            lineLength.StartReelIn(0.1f, 2f); // Reel to 0.1 over 2 seconds
        }
    }
}
