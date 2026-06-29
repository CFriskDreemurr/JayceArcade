using UnityEngine;
using DG.Tweening;
using System.Collections;

public class hammerBall : MonoBehaviour
{
    [SerializeField] private float duration;

    private bool blocker;
    private HammerMachineHitPlace HMHP;
    private Vector3 startingPos;

    private void Start()
    {
        HMHP = GetComponentInParent<HammerMachineHitPlace>();
        startingPos = transform.position;
    }

    public void MoveToThePosition(float height)
    {
        HMHP.blocker = true;
        transform.DOMoveY(height, duration).OnComplete(() => StartCoroutine(WaitOnTop(1)));
    }

    IEnumerator WaitOnTop(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        transform.DOMoveY(startingPos.y, duration).OnComplete(() => HMHP.blocker = false);
        
    }
}
