using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Barrel : MonoBehaviour
{

    public Collider[] colliders;

    public LayerMask LM;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    [Button("Explode")]
    void Explode()
    {
        colliders = Physics.OverlapSphere(transform.position, 50,LM);
        foreach (Collider nearby in colliders)
        {
            Rigidbody rb = nearby.GetComponent<Rigidbody>();
            if (nearby.GetComponent<Enemies>())
            {
                nearby.GetComponent<Enemies>().Damage();
                colliders = Physics.OverlapSphere(transform.position, 50);
            }
            if(rb)
                rb.AddExplosionForce(500,transform.position,50);
        }
    }
}
