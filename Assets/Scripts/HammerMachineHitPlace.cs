using System;
using UnityEngine;

public class HammerMachineHitPlace : MonoBehaviour
{
    private float hammerMass;
    private float minVelocity = 1f;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Hammer"))
        {
            Rigidbody hammerRB = collision.gameObject.GetComponent<Rigidbody>();
            collision.gameObject.TryGetComponent<hammerParameters>(out var hammerParamteres);
            hammerMass = hammerParamteres.mass;


            if (hammerRB != null)
            {
                float hitVelocity = collision.relativeVelocity.magnitude;

                if(hitVelocity > minVelocity)
                {
                    CalculateSocre(hitVelocity);
                }
            }
        }
    }

    private void CalculateSocre(float velocity)
    {
        float FinalForce = velocity * hammerMass;

        int score = Mathf.RoundToInt(FinalForce * 100);

        Debug.Log($"Hammer mass: {hammerMass} | Final Score: {score}");
    }
}
