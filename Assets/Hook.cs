using UnityEngine;
using System.Collections;
using Oculus.Interaction;
using Oculus.Haptics;


public class Hook : MonoBehaviour
{   
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _Fishbite;
    [SerializeField] private AudioClip _FishGrabSound;

    [Header("Haptics")]
    [SerializeField] private HapticClipPlayer player; // Le lecteur
    [SerializeField] public HapticClip biteHapticClip;



    public Transform object1;
    public Transform object2;
    public LineLength lineLength;

    private FixedJoint fishJoint;
    private GrabInteractable grabInteractable;

    public enum State
    {
        Waiting,
        Caught,
        Reel,
        Releasing,
        WaitAfterRelease
    }

    State state = State.Waiting;
    float stateTime = 0;

    void Awake()
    {
        player = new HapticClipPlayer(biteHapticClip);
    }


    void OnTriggerEnter(Collider collider)
    {
        if(state != State.Waiting)
            return;
        if (fishJoint)
            return;

        if (collider.attachedRigidbody &&
            collider.attachedRigidbody.GetComponent<Fish>())
        {
            var hookedFishRb = collider.attachedRigidbody;

            fishJoint = hookedFishRb.gameObject.AddComponent<FixedJoint>();
            fishJoint.connectedBody = GetComponent<Rigidbody>();


            collider.attachedRigidbody.GetComponent<Fish>().isAttached = true;

            player.Play(Controller.Left);

            if(_audioSource != null && _Fishbite != null)
            {
                _audioSource.PlayOneShot(_Fishbite);
            }

              grabInteractable = hookedFishRb.GetComponentInChildren<GrabInteractable>();
            if (grabInteractable != null)
            {
                grabInteractable.WhenStateChanged += OnGrabStateChanged;
            }
        }
    }

    private void OnGrabStateChanged(InteractableStateChangeArgs args)
    {
        // When grab starts
        if (args.NewState == InteractableState.Select)
        {
            if(_audioSource != null && _FishGrabSound != null)
            {
                _audioSource.PlayOneShot(_FishGrabSound);
            }
                UnhookFish();
        }
    }

    private void UnhookFish()
    {
        Debug.Log("Fish grabbed — freeing from hook!");

        if (fishJoint != null)
        {
            Destroy(fishJoint);
            fishJoint = null;
      

            if (grabInteractable != null)
            {
                grabInteractable.WhenStateChanged -= OnGrabStateChanged;
            }

            fishJoint = null;
        }
    }

    void Update()
    {

        State newState = state;
        stateTime += Time.deltaTime;
        switch(state)
        {
            case State.Waiting:
                {
                    if(fishJoint)
                    {
                        newState = State.Caught;
                    }
                    break;
                }
            case State.Caught:
                {
                    if (fishJoint && !fishJoint.GetComponent<Floating>().isInWater)
                    {
                        newState = State.Releasing;
                    }

                    break;
                }
            case State.Releasing:
                {
                    if (!fishJoint)
                    {
                        newState = State.WaitAfterRelease;
                    }
                    break;
                }
            case State.WaitAfterRelease:
                {
                    if(stateTime>1)
                    {
                        newState = State.Waiting;
                    }
                    break;
                }

        }

        if(newState != state)
            {
            stateTime = 0;
            switch (newState)
            {
                case State.Releasing:
                    {
                        if (lineLength != null)
                        {
                            lineLength.StartReelIn(0.1f, 2f);
                        }
                        break;
                    }
                case State.Waiting:
                {
                    if (lineLength != null)
                    {
                        lineLength.StartReelIn(1, 2f);
                    }
                    break;
                }

            }

            state = newState;
        }
    }
}
