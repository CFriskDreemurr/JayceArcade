using System;
using UnityEngine;

public class HammerMachineHitPlace : MonoBehaviour
{
    private float hammerMass;
    private float minVelocity = 1f;
    [SerializeField] private hammerBall _ball;

    private TicketPrinting _printer;

    public bool blocker;

    private void Start()
    {
        _printer = GetComponentInChildren<TicketPrinting>();
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
        if (score/1000 > 4)
        {
            _ball.MoveToThePosition(4);
            _printer.StartPrintingTickets(8);
        }
        else
        {
            _ball.MoveToThePosition(score / 1000);
            _printer.StartPrintingTickets((score/1000)*2);
        }
       
        
        Debug.Log($"Hammer mass: {hammerMass} | Final Score: {score}");
    }
}
