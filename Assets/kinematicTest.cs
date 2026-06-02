using UnityEngine;

public class kinematicTest : MonoBehaviour
{
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.isKinematic) rb.isKinematic = false;
    }
}
