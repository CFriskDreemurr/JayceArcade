using UnityEngine;

public class HammerGameHitPlace : MonoBehaviour
{

    public float massModifier = 1.5f; 
    public float minVelocityThreshold = 1.0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hammer"))
        {
            Rigidbody hammerRb = collision.gameObject.GetComponent<Rigidbody>();

            if (hammerRb != null)
            {
                float hitVelocity = collision.relativeVelocity.magnitude;

                if (hitVelocity > minVelocityThreshold)
                {
                    CalculateScore(hitVelocity);
                }
            }
        }
    }

    private void CalculateScore(float velocity)
    {
        float finalForce = velocity * massModifier;

        int score = Mathf.RoundToInt(finalForce * 100);

        Debug.Log($"TRAFIENIE! Prêdkoœæ: {velocity} | Wynik: {score}");
    }
}
