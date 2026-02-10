using UnityEngine;

public class HeightZComparer : MonoBehaviour
{
    public Transform object1;
    public Transform object2;

    void Update()
    {
        Debug.Log($"Z1: {object1.position.z} | Z2: {object2.position.z}");

        if (object1 == null || object2 == null)
            return;

        float z1 = object1.position.z;
        float z2 = object2.position.z;

        if (z1 > z2)
        {
            Debug.Log("Object 1 is higher than Object 2 on the Z axis");
        }
    }
}
