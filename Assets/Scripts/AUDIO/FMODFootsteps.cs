using UnityEngine;
using FMODUnity;
using Mono.Cecil;

public class VRFootsteps : MonoBehaviour
{
    [Header("FMOD Settings")]
    public FMODUnity.EventReference footstepEvent;

    [Header("Movement Settings")]
    public Transform vrCamera;
    public float stepDistance = 1.5f;

    [Header("Raycast Settings")]
    public LayerMask groundLayer;
    public float raycastLength = 2.5f;

    private Vector3 lastPosition;
    private float distanceMoved = 0f;

    void Start()
    {
        if (vrCamera == null)
        {
            Debug.LogError("Please assign the VR Camera!");
            return;
        }

        lastPosition = vrCamera.position;
        lastPosition.y = 0f;
    }

    void Update()
    {
        // Track only horizontal movement to avoid triggering steps by squatting
        Vector3 currentPosition = vrCamera.position;
        currentPosition.y = 0f;

        float distanceThisFrame = Vector3.Distance(currentPosition, lastPosition);
        distanceMoved += distanceThisFrame;
        lastPosition = currentPosition;

        // When the player has moved the length of a stride, play a step
        if (distanceMoved >= stepDistance)
        {
            Debug.Log("stepping");
            PlayFootstep();
            distanceMoved = 0f;
        }
    }

    void PlayFootstep()
    {
        // Raycast prosto w dó³ z gogli, aby wykryæ pod³ogê
        if (Physics.Raycast(vrCamera.position, Vector3.down, out RaycastHit hit, raycastLength, groundLayer))
        {
            Debug.Log("stomp stomp");

            // Ustaw pozycjê dŸwiêku na ziemi (na wysokoœci punktu uderzenia raycasta)
            Vector3 footPosition = vrCamera.position;
            footPosition.y = hit.point.y;

            // Opcja A (Zalecana): Proste odtworzenie dŸwiêku
            RuntimeManager.PlayOneShot(footstepEvent, footPosition);

            /* // Opcja B: Jeœli w przysz³oœci bêdziesz chcia³ ustawiaæ parametry (np. rodzaj nawierzchni):
            FMOD.Studio.EventInstance footstep = RuntimeManager.CreateInstance(footstepEvent);

            // TEJ LINIJKI BRAKOWA£O W TWOIM KODZIE:
            footstep.set3DAttributes(RuntimeUtils.To3DAttributes(footPosition));

            // footstep.setParameterByName("Surface", 1f); 
            footstep.start();
            footstep.release();
            */
        }
        else
        {
            Debug.LogWarning("Raycast nie trafi³ w pod³ogê! DŸwiêk kroku nie zostanie odtworzony.");
        }
    }

}