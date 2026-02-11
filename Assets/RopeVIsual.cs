using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RopeVisual : MonoBehaviour
{
    public Transform startPoint; // player / gun / hand
    public Transform endPoint;   // hook

    private LineRenderer line;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
    }

    void LateUpdate()
    {
        if (startPoint == null || endPoint == null)
            return;

        line.SetPosition(0, startPoint.position);
        line.SetPosition(1, endPoint.position);
    }


    }
