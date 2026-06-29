using UnityEngine;

public class BottleBocController : MonoBehaviour
{
    private BottleGame bottleGameManager;

    void Start()
    {
        bottleGameManager = GetComponentInParent<BottleGame>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bottle"))
        {
            bottleGameManager.pointCounter += 1;
            bottleGameManager.ResetTimer();
        }
    }
}
