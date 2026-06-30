using FMODUnity;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    private Vector3 _startPostions;
    private Quaternion _startRotation;
    private Rigidbody _rb;
    [Header("Physics Settings")]
    [Tooltip("Minimum impact force required to trigger the sound.")]
    public float velocityThreshold = 0.1f;
    public FMODCollisionAudio bottleManager;

    private void Start()
    {
        _startPostions = transform.localPosition;
        _startRotation = transform.localRotation;
        _rb = GetComponent<Rigidbody>();
    }

    public void Reset()
    {
        transform.localPosition = _startPostions;
        transform.localRotation = _startRotation;
        
        if(_rb != null)
        {
            _rb.angularVelocity = Vector3.zero;
            _rb.linearVelocity = Vector3.zero;
        }
    }
    

    // This built-in Unity function triggers automatically when objects collide
    private void OnCollisionEnter(Collision collision)
    {
        // Calculate how hard the objects hit each other
        Debug.Log("1");
        float hitForce = collision.impulse.magnitude / Time.fixedDeltaTime;

        // Check if the hit was strong enough to bypass our threshold
        if (hitForce >= velocityThreshold)
        {
            Debug.Log("2");
            bottleManager.HitTHatGlass(transform.position);
        }
    }
}
