using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Prize : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI prizeText;
    [SerializeField] private int prizeValue;

    private Rigidbody _rb;
    private XRGrabInteractable grabInteract;

    void Start()
    {
        prizeText.text = prizeValue.ToString();
        _rb = GetComponent<Rigidbody>();
        BlockReward();
        Events.OnTicketsChange += CheckTickets;
    }

    
    void Update()
    {
        if(_rb.isKinematic)
        {
            PlayerManager.instance.SubstractTickets(prizeValue);
            // Remove stand on prizeWall
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
