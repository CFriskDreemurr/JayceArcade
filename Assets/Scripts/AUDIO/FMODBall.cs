using UnityEngine;
using FMODUnity;

public class BallAudio : MonoBehaviour
{
    [Header("FMOD Settings")]
    public EventReference bounceEvent;

    [Header("Physics Settings")]
    [Tooltip("How hard the ball must hit to make a sound. Keeps rolling quiet.")]
    public float velocityThreshold = 1f;

    [Tooltip("Prevents the machine-gun effect if the ball gets stuck in a corner.")]
    public float bounceCooldown = 0.1f;

    private float nextAllowedBounceTime = 0f;

    private void OnCollisionEnter(Collision collision)
    {
        // 1. Calculate how hard the ball hit the surface
        float hitForce = collision.relativeVelocity.magnitude;

        // 2. Check if it hit hard enough AND if the cooldown has passed
        if (hitForce >= velocityThreshold && Time.time >= nextAllowedBounceTime)
        {
            PlayBounceSound(hitForce);

            // 3. Reset the cooldown timer
            nextAllowedBounceTime = Time.time + bounceCooldown;
        }
    }

    private void PlayBounceSound(float force)
    {
        // Create an instance so we can change its parameters before playing it
        FMOD.Studio.EventInstance bounce = RuntimeManager.CreateInstance(bounceEvent);

        // Attach the sound to the ball's current position
        bounce.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));


        // Play the sound and tell FMOD to destroy it when it finishes
        bounce.start();
        bounce.release();
    }
}