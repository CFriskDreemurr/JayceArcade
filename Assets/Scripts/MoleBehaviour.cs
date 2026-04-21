using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MoleBehaviour : MonoBehaviour
{
    private Vector3 _hiddenPose;
    private Vector3 _upPose;
    private float _upDiff = 3f;
    private float _hideDuration = 0;
    private float _movingDuration = 1;

    private Tween _currentTween;

    private void Start()
    {
        _hiddenPose = transform.localPosition;
        _upPose = new Vector3(transform.localPosition.x, transform.localPosition.y + _upDiff, transform.localPosition.z);
        Debug.Log($"current: {_hiddenPose}, up: {_upPose}");
    }

    public void RiseUp()
    {
        _currentTween?.Kill();
        _currentTween = transform.DOLocalMoveY(transform.localPosition.y + _upDiff, _movingDuration);
        StartCoroutine(Wait(1f, false));
    }

    public void Hide()
    {
        _currentTween?.Kill();
        _currentTween = transform.DOLocalMove(_hiddenPose, _movingDuration);
        StartCoroutine(Wait(1f, true));
    }

    public void Hit()
    {
        _currentTween?.Kill();
        _currentTween = transform.DOLocalMove(_hiddenPose, _hideDuration);
        StartCoroutine(Wait(1f, true));
    }

    private IEnumerator Wait(float delay, bool hidden)
    {
        yield return new WaitForSeconds(delay);
        if(hidden)
        {
            MoleGame gameManager = GetComponentInParent<MoleGame>();
            gameManager.DrawMole();
        }
        else Hide();
    }

}
