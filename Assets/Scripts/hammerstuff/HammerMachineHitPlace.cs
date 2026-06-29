using System;
using UnityEngine;

public class HammerMachineHitPlace : MonoBehaviour
{
    private float hammerMass;
    private float minVelocity = 1f;
    private hammerBall _ball;

    public bool blocker;

    private void Start()
    {
        _ball = GetComponentInChildren<hammerBall>();
        blocker = false;
    }


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

                if(hitVelocity > minVelocity && !blocker)
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
        _ball.MoveToThePosition(score/1000);

        Debug.Log($"Hammer mass: {hammerMass} | Final Score: {score}");
    }
}
