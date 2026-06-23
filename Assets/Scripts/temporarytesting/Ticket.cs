using UnityEngine;

public class Ticket : MonoBehaviour
{
    [HideInInspector]
    public TicketPrinting machineReference;

    // Example using Mouse Click. 
    // If you are using Raycasts or an Interaction system, call CollectTickets() from there instead.
    private void OnMouseDown()
    {
        if (machineReference != null)
        {
            machineReference.CollectTickets();
        }
    }
}