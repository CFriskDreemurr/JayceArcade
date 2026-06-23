using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MoleBehaviour : MonoBehaviour
{
    [SerializeField] private float upDistance = 0.2f;
    [SerializeField] private float hideSpeed = 0.5f;

    private Vector3 _hiddenPos;
    private Vector3 _visiblePos;
    private Tween _currentTween;

    public bool IsVisible { get; private set; } = false;
    public bool IsHit { get; private set; } = false;

    private MoleGame _gameManager;

    private void Start()
    {
        _hiddenPos = transform.localPosition;
        _visiblePos = _hiddenPos + new Vector3(0, upDistance, 0);
        _gameManager = GetComponentInParent<MoleGame>();
        
        Hide(hideSpeed);
    }

    public void RiseUp()
    {
        if (IsVisible) return;

        IsVisible = true;
        IsHit = false;
        _currentTween?.Kill();

        transform.localPosition = _hiddenPos;
        _currentTween = transform.DOLocalMoveY(_visiblePos.y, 0.5f).SetEase(Ease.OutQuad);
    }

    public void Hide(float duration)
    {
        if (!IsVisible) return;

        IsVisible = false;
        IsHit = false;
        _currentTween?.Kill();

        _currentTween = transform.DOLocalMoveY(_hiddenPos.y, duration).OnComplete(() =>
        {
            _currentTween = null;
        });
    }

    public void Hit()
    {
        if (IsHit || !IsVisible) return;

        IsHit = true;
        _gameManager.AddPoint();

        Vector3 originalScale = transform.localScale;
        transform.localScale *= 0.8f;
        transform.DOScale(originalScale, 0.2f);

        Hide(hideSpeed);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Hammer"))
        {
            Hit();
        }
    }

}
