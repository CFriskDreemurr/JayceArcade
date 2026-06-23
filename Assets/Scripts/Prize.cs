using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Prize : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI prizeText;
    [SerializeField] private float prizeValue;

    private Rigidbody _rb;
    private XRGrabInteractable grabInteract;

    void Start()
    {
        prizeText.text = prizeValue.ToString();
        _rb = GetComponent<Rigidbody>();
        BlockReward();
        // podpiêcie pod event zgarniêcia biletów 
        // podpiêcie pod wydanie ticketów
    }

    
    void Update()
    {
        if(_rb.isKinematic)
        {
            //minus tickets
            // Remove stand on prizeWall
        }
    }

    private void BlockReward()
    {
        grabInteract.interactionLayers = 0;
    }

    private void UnlockReward()
    {
        // if(tickety gracza >= prize Value)
        grabInteract.interactionLayers = int.MaxValue;
    }
    
    private void CheckTickets()
    {
        // if(tickety gracza >= prize Value)
        UnlockReward();
        // else
        BlockReward();

    }
}
