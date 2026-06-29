using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [SerializeField] private TextMeshProUGUI ticketText;
    public int tickets;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void AddTickets(int ticketsToAdd)
    {
        tickets += ticketsToAdd;
        ticketText.text = tickets.ToString();
        Events.ChangeTicket();
    }

    public void SubstractTickets(int ticketsToSub)
    {
        tickets -= ticketsToSub;
        ticketText.text = tickets.ToString();
        Events.ChangeTicket();
    }
}
