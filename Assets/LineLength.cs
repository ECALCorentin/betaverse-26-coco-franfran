using UnityEngine;
using System.Collections;

public class LineLength : MonoBehaviour
{
    public ConfigurableJoint hookJoint;
    public float length = 1f;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _reelInSound;

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

        // --- AJOUT : Lancer le son au début du rembobinage ---
        if (_audioSource != null && _reelInSound != null)
        {
            _audioSource.clip = _reelInSound;
            _audioSource.loop = true; // On veut que le son boucle tant qu'on mouline
            _audioSource.Play();
        }

        while (time < duration)
        {
            time += Time.deltaTime;

            float t = time / duration;
            t = Mathf.SmoothStep(0f, 1f, t);

            length = Mathf.Lerp(startLength, targetLength, t);

            yield return null;
        }

        length = targetLength;

        // --- AJOUT : Arrêter le son quand on a fini de remonter ---
        if (_audioSource != null && _audioSource.clip == _reelInSound)
        {
            _audioSource.Stop();
        }
    }
}