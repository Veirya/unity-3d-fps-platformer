using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollisionLogic : MonoBehaviour
{
    private HashSet<Collider> groundsOn;

    public void Start()
    {
        groundsOn = new HashSet<Collider>();
    }

    public void OnTriggerEnter(Collider other)
    {
        groundsOn.Add(other);
    }

    public void OnTriggerExit(Collider other)
    {
        groundsOn.Remove(other);
    }

    public bool CanJump()
    {
        return groundsOn.Count != 0;
    }
}
