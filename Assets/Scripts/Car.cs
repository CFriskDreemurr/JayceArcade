using UnityEngine;
using DG.Tweening;

public class Car : MonoBehaviour
{
    
    public void Initialize(Transform pointToGo, float speed)
    {
        transform.DOMove(pointToGo.position, speed).OnComplete(() => Destroy(gameObject));
    }
}
