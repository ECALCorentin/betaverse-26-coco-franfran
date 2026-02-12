using UnityEngine;
using System.Collections;

public class LineLength : MonoBehaviour
{
    public ConfigurableJoint hookJoint;
    public float length = 1f;

    private Coroutine reelCoroutine;

    void FixedUpdate()
    {
        if (hookJoint == null)
            return;

        var limit = hookJoint.linearLimit;
        limit.limit = length;
        hookJoint.linearLimit = limit;
    }

    public void StartReelIn(float targetLength, float duration)
    {
        if (reelCoroutine != null)
            StopCoroutine(reelCoroutine);

        reelCoroutine = StartCoroutine(ReelIn(targetLength, duration));
    }

    private IEnumerator ReelIn(float targetLength, float duration)
    {
        float startLength = length;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;

            float t = time / duration;
            t = Mathf.SmoothStep(0f, 1f, t);   // smooth easing

            length = Mathf.Lerp(startLength, targetLength, t);

            yield return null;
        }

        length = targetLength;
    }
}