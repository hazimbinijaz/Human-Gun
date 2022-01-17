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
    private bool Run;
    [SerializeField] private bool CanRun;
    public bool CanBeSucked;
    private Platform m_Platform;
    [SerializeField] private int m_Health = 1;
    // public bool HasCollided;
    private void Awake()
    {
        Run = false;
        // HasCollided = false;
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
        // if (other.transform.CompareTag("Enemy") )
        // {
        //     HasCollided = true;
        //     if(other.transform.GetComponent<Enemies>())
        //         other.transform.GetComponent<Enemies>().Damage();
        //     else if(other.transform.GetComponentInParent<Enemies>())
        //         other.transform.GetComponentInParent<Enemies>().Damage();
        //     Damage();
        // }
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
        if (!CanRun) return;
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

    public void Damage()
    {
        m_Health--;
        if(m_Health<=0)
            Dead();
    }
    
    
    void Dead()
    {
        print("Damaged");
        // foreach (Rigidbody rigidbody in GetComponentsInChildren<Rigidbody>())
        // {
        //     rigidbody.gameObject.layer= LayerMask.NameToLayer($"Dead");
        // }
        GetComponent<RagdollController>().RagdollOn();
        // gameObject.layer= LayerMask.NameToLayer($"Default");
        enabled = false;
        // gameObject.SetActive(false);
        // Destroy(gameObject);
    }
    
}
