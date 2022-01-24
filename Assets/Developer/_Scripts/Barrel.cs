using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Barrel : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    [Button("Explode")]
    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 50);
        foreach (Collider nearby in colliders)
        {
            Rigidbody rb = nearby.GetComponent<Rigidbody>();
            if(rb)
                rb.AddExplosionForce(500,transform.position,50);
        }
    }
}
