using UnityEngine;
using FMODUnity;

public class VRHammerAudio : MonoBehaviour
{
    [Header("FMOD Settings")]
    public EventReference hammerHitEvent;
    public string impactParameterName = "SwingForce";

    [Header("Physics Settings")]
    [Tooltip("The part of the weapon that hits the hardest (e.g., the hammer head).")]
    public Transform hammerHead;

    [Tooltip("Minimum speed required to trigger the sound. Ignores gentle taps.")]
    public float velocityThreshold = 2.0f;

    [Tooltip("Prevents multiple hits from firing instantly if the collider drags.")]
    public float hitCooldown = 0.2f;

    // Tracking variables
    private Vector3 _previousHeadPosition;
    private float _actualHeadSpeed;
    private float _nextAllowedHitTime = 0f;

    private void Start()
    {
        // If you forget to assign a hammer head, fall back to the main object
        if (hammerHead == null)
        {
            hammerHead = transform;
            Debug.LogWarning("Hammer Head not assigned! Tracking the center of the object instead.");
        }

        _previousHeadPosition = hammerHead.position;
    }

    private void FixedUpdate()
    {
        // Calculate the real-world speed of the hammer head through space
        float distanceMoved = Vector3.Distance(hammerHead.position, _previousHeadPosition);
        _actualHeadSpeed = distanceMoved / Time.fixedDeltaTime;

        // Save position for the next frame
        _previousHeadPosition = hammerHead.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if we swung hard enough AND the cooldown has passed
        if (_actualHeadSpeed >= velocityThreshold && Time.time >= _nextAllowedHitTime)
        {
            PlayHitSound(_actualHeadSpeed, collision.contacts[0].point);

            // Reset the cooldown
            _nextAllowedHitTime = Time.time + hitCooldown;
        }
    }

    private void PlayHitSound(float swingSpeed, Vector3 hitPosition)
    {
        // 1. Create the FMOD instance
        FMOD.Studio.EventInstance hitSound = RuntimeManager.CreateInstance(hammerHitEvent);

        // 2. Play the sound exactly where the hammer touched the surface, not at the center
        hitSound.set3DAttributes(RuntimeUtils.To3DAttributes(hitPosition));

        // 3. Pass the speed of the swing into FMOD to change volume/intensity
        hitSound.setParameterByName(impactParameterName, swingSpeed);

        // 4. Fire and forget
        hitSound.start();
        hitSound.release();
    }
}