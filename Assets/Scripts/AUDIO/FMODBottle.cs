using UnityEngine;
using FMODUnity;
using System;

public class FMODCollisionAudio : MonoBehaviour
{
    [Header("FMOD Settings")]
    public EventReference impactEvent;
    public float cooldown = 10f;
    private float NextGlassed = 0.0f;

    
    public void HitTHatGlass(Vector3 pos)
    {
        Debug.Log("hehe");
        if (Time.time > NextGlassed)
        {
            RuntimeManager.PlayOneShot(impactEvent, pos);
            NextGlassed = Time.time+cooldown;
        }
    }
}