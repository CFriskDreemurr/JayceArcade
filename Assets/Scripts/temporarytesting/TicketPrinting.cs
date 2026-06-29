using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class TicketPrinting : MonoBehaviour
{
    [Header("Ticket Settings")]
    public GameObject ticketPrefab;
    public Transform spawnPoint; // An empty GameObject placed at the machine's ticket slot
    public float printSpeed = 0.2f; // Time between each ticket printing
    public int ticketAmount = 30;

    [Header("Tego lepiej nie zmieniaj")]
    public float ejectionForce = 0.02f; // How hard the ticket is pushed out
    public float ticketLength = 1.05f;
    public float jointAngle = 30f;

    // Keeps track of the current uncollected chain
    [SerializeField] private List<GameObject> currentTicketChain = new List<GameObject>();
    private Rigidbody lastSpawnedTicket;

    /// <summary>
    /// Call this method when the player wins a game to start printing.
    /// </summary>
    /// 
    private void Start()
    {
        StartPrintingTickets(ticketAmount);
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

            // FREEZE IT: It becomes our mechanical anchor
            ticketRb.isKinematic = true;

            // 2. Connect to the previous ticket
            if (lastSpawnedTicket != null)
            {
                HingeJoint joint = newTicket.AddComponent<HingeJoint>();
                joint.connectedBody = lastSpawnedTicket;

                joint.anchor = new Vector3(0, 0, 0.057f);
                joint.connectedAnchor = new Vector3(0, 0, -0.057f);

                joint.useLimits = true;
                JointLimits limits = new JointLimits { min = -jointAngle, max = jointAngle };
                joint.limits = limits;

                // UNFREEZE PREVIOUS: Let the previous ticket fall and dangle
                lastSpawnedTicket.isKinematic = false;
            }

            // 3. Register the ticket
            currentTicketChain.Add(newTicket);
            newTicket.GetComponent<Ticket>().machineReference = this;
            lastSpawnedTicket = ticketRb;

            // 4. THE ROLLER EFFECT: Smoothly slide this kinematic ticket forward
            float elapsedTime = 0f;
            Vector3 startPos = spawnPoint.position;
            // Calculate the destination exactly one ticket-length forward
            Vector3 endPos = spawnPoint.position + (spawnPoint.forward * ticketLength);

            while (elapsedTime < printSpeed)
            {
                // MovePosition smoothly moves a kinematic Rigidbody while keeping physics happy
                ticketRb.MovePosition(Vector3.Lerp(startPos, endPos, (elapsedTime / printSpeed)));
                elapsedTime += Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            // Ensure it ends up exactly at the target position at the end of the loop
            ticketRb.MovePosition(endPos);
        }

        // 5. CLEANUP: Once all printing is done, unfreeze the final ticket 
        // and give it a tiny nudge so the whole chain dangles freely
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
        PlayerManager.instance.AddTickets(ticketsCollected);

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