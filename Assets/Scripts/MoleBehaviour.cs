using UnityEngine;
using System.Collections;

public class MoleBehaviour : MonoBehaviour
{
    [SerializeField] private float yDifference;
    [SerializeField] private float moleSpeed;
    [SerializeField] private float waitingTime;

    private Vector3 _hiddenPose;
    private Vector3 _upPose;

    private void Start()
    {
        _hiddenPose = transform.position;
        _upPose = new Vector3(transform.position.x, transform.position.y + yDifference, transform.position.z);
    }

    public void RiseUp()
    {
        StopAllCoroutines();
        StartCoroutine(MoveMole(_upPose, true));
    }

    public void Hide()
    {
        StopAllCoroutines();
        StartCoroutine(MoveMole(_hiddenPose, false));
    }

    IEnumerator MoveMole(Vector3 target, bool up)
    {
        while(Vector3.Distance(transform.localPosition, target) > 0.01f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * moleSpeed);
            yield return null;
        }
        transform.localPosition = target;

        if(up)
        {
            yield return new WaitForSeconds(waitingTime);
            Hide();
        }
    }


}
