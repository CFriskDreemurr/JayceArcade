using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Prize : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI prizeText;
    [SerializeField] private int prizeValue;

    private Rigidbody _rb;
    private XRGrabInteractable grabInteract;
    private bool bought = false;

    void Start()
    {
        prizeText.text = prizeValue.ToString();
        _rb = GetComponent<Rigidbody>();
        grabInteract = GetComponent<XRGrabInteractable>();
        BlockReward();
        Events.OnTicketsChange += CheckTickets;
        CheckTickets();
    }

    
    public void BuyItem()
    {
        if(!bought)
        { 
            PlayerManager.instance.SubstractTickets(prizeValue);
            prizeValue = 0;
            Events.ItemBought();
            bought = true;  
        }

    }

    private void BlockReward()
    {
        grabInteract.interactionLayers = 0;
    }

    private void UnlockReward()
    {
        if(PlayerManager.instance.tickets >= prizeValue) grabInteract.interactionLayers = int.MaxValue;
    }
    
    private void CheckTickets()
    {
        if (PlayerManager.instance.tickets >= prizeValue) UnlockReward();
        else BlockReward();
    }

    private void OnDisable()
    {
        Events.OnTicketsChange -= CheckTickets;
    }
}
