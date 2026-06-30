using UnityEngine;
using FMODUnity;

public class FMODCollisionAudio : MonoBehaviour
{
    [Header("FMOD Settings")]
    public EventReference impactEvent;

    [Header("Physics Settings")]
    [Tooltip("Minimum impact force required to trigger the sound.")]
    public float velocityThreshold = 1.0f;

    // This built-in Unity function triggers automatically when objects collide
    private void OnCollisionEnter(Collision collision)
    {
        // Calculate how hard the objects hit each other
        float hitForce = collision.relativeVelocity.magnitude;

        // Check if the hit was strong enough to bypass our threshold
        if (hitForce >= velocityThreshold)
        {
            // Play a "One Shot" (fire-and-forget) sound at the object's current position
            RuntimeManager.PlayOneShot(impactEvent, transform.position);
        }
    }
}