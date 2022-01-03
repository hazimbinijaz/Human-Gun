using DG.Tweening;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    private Transform Self;
    private RagdollController m_RagdollController;
    private Rigidbody m_Rigidbody;
    public float Speed;
    private Animator m_Anim;
    private GameObject m_Player;

    private Platform m_Platform;
    // Start is called before the first frame update
    void Start()
    {
        Self = transform;
        m_RagdollController = GetComponent<RagdollController>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Anim = GetComponent<Animator>();
        m_Anim.SetBool("Run",true);
        m_Player = TheGameManager.Instance.Player;
        m_Platform = GetComponentInParent<Platform>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            if(other.transform.GetComponent<Enemies>())
                other.transform.GetComponent<Enemies>().Dead();
            Dead();
        }
    }

    private void Update()
    {
        transform.DOLookAt(m_Player.transform.position, 0.2f);
        transform.position += transform.forward * Speed * Time.deltaTime;
    }

    public void Dead()
    {
        GetComponent<RagdollController>().RagdollOn();
        gameObject.layer= LayerMask.NameToLayer($"Default");
        enabled = false;
    }
    
}
