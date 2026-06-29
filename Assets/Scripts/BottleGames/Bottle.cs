using UnityEngine;

public class Bottle : MonoBehaviour
{
    private Vector3 _startPostions;
    private Quaternion _startRotation;
    private Rigidbody _rb;

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
}
