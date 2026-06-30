using UnityEngine;
using DG.Tweening;
using System.Collections;

public class hammerBall : MonoBehaviour
{
    [SerializeField] private float duration;

    private bool blocker;
    [SerializeField] private HammerMachineHitPlace HMHP;
    private Vector3 startingPos;

    private void Start()
    {
        
        startingPos = transform.position;
    }

    public void MoveToThePosition(float height)
    {
        HMHP.blocker = true;
        float finalDest = startingPos.y + height;
        transform.DOMoveY(finalDest, duration).OnComplete(() => StartCoroutine(WaitOnTop(1)));
    }

    IEnumerator WaitOnTop(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        transform.DOMoveY(startingPos.y, duration).OnComplete(() => HMHP.blocker = false);
        
    }
}
