using UnityEngine;

public class HeightZComparer : MonoBehaviour
{
    public Transform object1;
    public Transform object2;

    void Update()
    {
        Debug.Log($"Y1: {object1.position.y} | Y2: {object2.position.y}");

        if (object1 == null || object2 == null)
            return;

        float y1 = object1.position.y;
        float y2 = object2.position.y;

        if (y1 > y2)
        {
            Debug.Log("Object 1 is higher than Object 2 on the Z axis");
        }
    }
}
