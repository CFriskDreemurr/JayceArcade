using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MoleBehaviour : MonoBehaviour
{
    private Vector3 _hiddenPose;
    private float _upDiff = 3f;
    private float _hideDuration = 0;
    private float _movingDuration = 1;
    private float _waitTime = 0.2f;

    private Tween _currentTween;
    private MoleGame _gameManager;

    private bool _isUp = false;

    private void Start()
    {
        _hiddenPose = transform.localPosition;
        _gameManager = GetComponentInParent<MoleGame>();
    }

    public void RiseUp()
    {
        _currentTween?.Kill();
        _currentTween = transform.DOLocalMoveY(transform.localPosition.y + _upDiff, _movingDuration)
            .OnComplete(() =>
            {
                StartCoroutine(Wait(_waitTime, false));
            });
        _isUp = true;
    }

    public void Hide(float hidespeed)
    {
        _currentTween?.Kill();
        _currentTween = transform.DOLocalMove(_hiddenPose, hidespeed).OnComplete(() =>
        {
            StartCoroutine(Wait(_waitTime, true));
        });
    }

    public void Hit()
    {
        _currentTween?.Kill();
        if (_isUp)
        {
            _gameManager.GetPoint();
            Hide(_hideDuration);
        }
    }

    private IEnumerator Wait(float delay, bool hidden)
    {
        yield return new WaitForSeconds(delay);
        if (hidden)
        {
            _isUp = false;  
            _gameManager.DrawMole();
        }
        else Hide(_movingDuration);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("MoleHammer"))
        {
            Hit();
        }
    }

}
