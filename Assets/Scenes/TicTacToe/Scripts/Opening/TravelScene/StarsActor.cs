using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsActor : MonoBehaviour
{
    private void OnDrawGizmosSelected()
    {
        ParticleSystem particleSystem;

        particleSystem = GetComponent<ParticleSystem>();

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, particleSystem.shape.radius);
    }
}
