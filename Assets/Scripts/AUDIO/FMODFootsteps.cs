using UnityEngine;
using FMODUnity;
using Mono.Cecil;

public class VRFootsteps : MonoBehaviour
{
    [Header("FMOD Settings")]
    public FMODUnity.EventReference footstepEvent;

    [Header("Movement Settings")]
    public Transform vrCamera;
    public float stepDistance = 1.5f;

    [Header("Raycast Settings")]
    public LayerMask groundLayer;
    public float raycastLength = 2.5f;

    private Vector3 lastPosition;
    private float distanceMoved = 0f;

    void Start()
    {
        if (vrCamera == null)
        {
            Debug.LogError("Please assign the VR Camera!");
            return;
        }

        lastPosition = vrCamera.position;
        lastPosition.y = 0f;
    }

    void Update()
    {
        // Track only horizontal movement to avoid triggering steps by squatting
        Vector3 currentPosition = vrCamera.position;
        currentPosition.y = 0f;

        float distanceThisFrame = Vector3.Distance(currentPosition, lastPosition);
        distanceMoved += distanceThisFrame;
        lastPosition = currentPosition;

        // When the player has moved the length of a stride, play a step
        if (distanceMoved >= stepDistance)
        {
            PlayFootstep();
            distanceMoved = 0f;
        }
    }

    void PlayFootstep()
    {
        // Raycast straight down from the headset to detect the ground
        if (Physics.Raycast(vrCamera.position, Vector3.down, out RaycastHit hit, raycastLength, groundLayer))
        {

            // Create the FMOD event instance
            FMOD.Studio.EventInstance footstep = RuntimeManager.CreateInstance(footstepEvent);

            // Set the audio position to the ground, directly below the player
            Vector3 footPosition = vrCamera.position;
            footPosition.y = hit.point.y;
            // Apply the surface parameter, play, and clean up
            footstep.start();
            footstep.release();
        }
    }

}