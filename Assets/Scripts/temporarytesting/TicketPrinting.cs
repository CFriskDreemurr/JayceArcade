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
            // 1. Spawn the ticket
            GameObject newTicket = Instantiate(ticketPrefab, spawnPoint.position, spawnPoint.rotation);
            Rigidbody ticketRb = newTicket.GetComponent<Rigidbody>();

            // Push it forward out of the slot
            ticketRb.AddForce(spawnPoint.forward * ejectionForce, ForceMode.Impulse);

            // 2. Connect to the previous ticket if one exists
            if (lastSpawnedTicket != null)
            {
                HingeJoint joint = newTicket.AddComponent<HingeJoint>();
                joint.connectedBody = lastSpawnedTicket;

                // IMPORTANT: Adjust the anchors based on your ticket model's size!
                // This assumes the Z-axis is the length of your ticket.
                joint.anchor = new Vector3(0, 0, 0.4f); // Top edge of the new ticket
                joint.connectedAnchor = new Vector3(0, 0, -0.4f); // Bottom edge of the previous ticket

                // Optional: Add limits to the joint so it doesn't spin 360 degrees
                joint.useLimits = true;
                JointLimits limits = new JointLimits { min = -45, max = 45 };
                joint.limits = limits;
            }

            // 3. Register the ticket to the chain and update the last spawned reference
            currentTicketChain.Add(newTicket);
            newTicket.GetComponent<Ticket>().machineReference = this; // Give the ticket a reference to this machine
            lastSpawnedTicket = ticketRb;

            // Wait before printing the next one
            yield return new WaitForSeconds(printSpeed);
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