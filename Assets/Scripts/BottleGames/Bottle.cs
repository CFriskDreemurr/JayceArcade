using UnityEngine;

public class Bottle : MonoBehaviour
{
    private Vector3 _startPostions;

    private void Start()
    {
        _startPostions = transform.localPosition;
    }

    public void Reset()
    {
        transform.localPosition = _startPostions;    
    }
}
