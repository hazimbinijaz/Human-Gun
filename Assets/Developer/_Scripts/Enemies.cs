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
    [SerializeField] private SkinnedMeshRenderer[] meshes;
    [SerializeField] private SkinnedMeshRenderer mesh;
    [SerializeField] private Material deadMaterial;
    Outline outline;
    private PlatformManager m_PlatformManager;
    private void Awake()
    {
        Run = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        meshes = GetComponentsInChildren<SkinnedMeshRenderer>();
        outline = GetComponent<Outline>();
        Self = transform;
        m_RagdollController = GetComponent<RagdollController>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Anim = GetComponent<Animator>();
        m_Player = TheGameManager.Instance.Player;
        m_Platform = GetComponentInParent<Platform>();
        TheGameManager.Instance.OnGameFail += StopEnemy;
        m_PlatformManager = TheGameManager.Instance.ThePlatformManager;
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
        TurnOutline(false);
        m_PlatformManager.OnEnemyDeath(1);
        GetComponent<RagdollController>().RagdollOn();
        enabled = false;
        SetColourToGrey();
    }

    void SetColourToGrey()
    {
        Material [] mats = mesh.materials;
        for (int i = 0; i < mats.Length; i++)
        {
            mats[i] = deadMaterial;
        }

        mesh.materials = mats;
    }

    public void TurnOutline(bool state)
    {
        outline.enabled = state;
    }
}
