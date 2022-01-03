using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class RagdollController : MonoBehaviour
{
    private Rigidbody m_RigidBody;
    private Collider m_Collider;
    private Animator m_Anim;
    public bool IsRagdollActive = false;
    void Start()
    {
        m_Collider = GetComponent<Collider>();
        m_RigidBody = GetComponent<Rigidbody>();
        m_Anim = GetComponent<Animator>();
        RagdollOff();
    }
    [Button("Ragdoll On")]
    public void RagdollOn()
    {
        
        IsRagdollActive = true;
        foreach (Collider collider in GetComponentsInChildren<Collider>())
        {
            collider.enabled = true;
        }

        foreach (Rigidbody rigidbody in GetComponentsInChildren<Rigidbody>())
        {
            rigidbody.isKinematic = false;
            rigidbody.gameObject.layer= LayerMask.NameToLayer($"Dead");
        }

        m_Anim.enabled = false;
        m_Collider.enabled = false;
        m_RigidBody.isKinematic = true;
        gameObject.layer= LayerMask.NameToLayer($"Dead");
    }

    public void RagdollOff()
    {
        IsRagdollActive = false;
        foreach (Collider collider in GetComponentsInChildren<Collider>())
        {
            collider.enabled = false;
        }

        foreach (Rigidbody rigidbody in GetComponentsInChildren<Rigidbody>())
        {
            rigidbody.isKinematic = true;
        }
        m_Anim.enabled = true;
        m_Collider.enabled = true;
        m_RigidBody.isKinematic = false;
    }
}