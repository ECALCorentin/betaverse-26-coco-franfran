using UnityEngine;
using UnityEngine.InputSystem;

public class Rope : MonoBehaviour
{
    public ConfigurableJoint joint;

    public float reelSpeed = 0.2f;
    public float minLength = 0.02f;

    void Update()
    {
        //if (joint == null) return;
if (Keyboard.current.spaceKey.wasPressedThisFrame)        {
            Debug.Log("hello!");
        }
    }
}
