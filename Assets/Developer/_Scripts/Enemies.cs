using System;
using DG.Tweening;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    private Transform Self;
    private RagdollController m_RagdollController;
    private Rigidbody m_Rigidbody;
    public float Speed;
    public Animator m_Anim;
    private GameObject m_Player;
    public bool Run;
    private Platform m_Platform;

    private void Awake()
    {
        Run = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Run = false;
        Self = transform;
        m_RagdollController = GetComponent<RagdollController>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Anim = GetComponent<Animator>();
        m_Player = TheGameManager.Instance.Player;
        m_Platform = GetComponentInParent<Platform>();
        TheGameManager.Instance.OnGameFail += StopEnemy;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("BorderLine"))
        {
            TheGameManager.Instance.LevelFail();
        }
    }

    public void StartPlayer()
    {
        GetComponent<Animator>().SetBool("Run",true);
        Run = true;
    }
    
    private void Update()
    {
        if (Run)
        {
            transform.DOLookAt(m_Player.transform.position, 0.2f,AxisConstraint.Y);
            transform.position += transform.forward * Speed * Time.deltaTime;
        }
    }

    void StopEnemy()
    {
        Run = false;
        m_Anim.SetBool("Run",false);
    }
    
    public void Dead()
    {
        GetComponent<RagdollController>().RagdollOn();
        gameObject.layer= LayerMask.NameToLayer($"Default");
        enabled = false;
    }
    
}
