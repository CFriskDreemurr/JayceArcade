using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class TicketPrinting : MonoBehaviour
{
    [Header("Ticket Settings")]
    public GameObject ticketPrefab;
    public Transform spawnPoint; // An empty GameObject placed at the machine's ticket slot
    public float printSpeed = 0.05f; // Time between each ticket printing
    public float ejectionForce = 1f; // How hard the ticket is pushed out

    // Keeps track of the current uncollected chain
    private List<GameObject> currentTicketChain = new List<GameObject>();
    private Rigidbody lastSpawnedTicket;

    /// <summary>
    /// Call this method when the player wins a game to start printing.
    /// </summary>
    /// 
    private void Start()
    {
        StartPrintingTickets(30);
    }
    public void StartPrintingTickets(int amount)
    {
        StartCoroutine(PrintTicketsRoutine(amount));
    }

    private IEnumerator PrintTicketsRoutine(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            // 1. Spawn the ticket exactly at the spawn point
            GameObject newTicket = Instantiate(ticketPrefab, spawnPoint.position, spawnPoint.rotation);
            Rigidbody ticketRb = newTicket.GetComponent<Rigidbody>();

            // FREEZE IT: Make it kinematic so gravity and forces don't move it yet
            ticketRb.isKinematic = true;

            // 2. Connect to the previous ticket if one exists
            if (lastSpawnedTicket != null)
            {
                HingeJoint joint = newTicket.AddComponent<HingeJoint>();
                joint.connectedBody = lastSpawnedTicket;

                // Adjust these anchors based on your model's size
                joint.anchor = new Vector3(0, 0, 0.5f);
                joint.connectedAnchor = new Vector3(0, 0, -0.5f);

                joint.useLimits = true;
                JointLimits limits = new JointLimits { min = -45, max = 45 };
                joint.limits = limits;

                // UNFREEZE PREVIOUS: Now that they are perfectly linked, let the previous ticket fall
                lastSpawnedTicket.isKinematic = false;

                // Push the previous ticket forward out of the slot
                lastSpawnedTicket.AddForce(spawnPoint.forward * ejectionForce, ForceMode.Impulse);
            }

            // 3. Register the ticket to the chain
            currentTicketChain.Add(newTicket);
            newTicket.GetComponent<Ticket>().machineReference = this;

            // Update the reference to this ticket
            lastSpawnedTicket = ticketRb;

            // Wait for the next ticket. The current ticket stays frozen at the slot, acting as the anchor.
            yield return new WaitForSeconds(printSpeed);
        }

        // 4. CLEANUP: Once printing is totally done, unfreeze the very last ticket 
        // so the whole chain dangles freely from the machine instead of floating!
        if (lastSpawnedTicket != null)
        {
            lastSpawnedTicket.isKinematic = false;
            lastSpawnedTicket.AddForce(spawnPoint.forward * ejectionForce, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// Called when the player collects the tickets.
    /// </summary>
    public void CollectTickets()
    {
        int ticketsCollected = currentTicketChain.Count;

        // Add to player score here! 
        Debug.Log($"Collected {ticketsCollected} tickets at once!");

        // Destroy all tickets in the chain
        foreach (GameObject ticket in currentTicketChain)
        {
            if (ticket != null)
            {
                Destroy(ticket);
            }
        }

        // Reset the chain
        currentTicketChain.Clear();
        lastSpawnedTicket = null;
    }
}