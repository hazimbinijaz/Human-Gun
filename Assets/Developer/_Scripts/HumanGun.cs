using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class HumanGun : MonoBehaviour
{
    private GameObject m_Self;
    [SerializeField] private PlatformManager m_PlatformManager;
    [SerializeField] private Transform m_Muzzle;
    private GameObject m_LoadedHuman;
    [ShowInInspector] public bool m_IsHumanLoaded { get; private set; }
    [SerializeField] private GameObject m_HumanInMagazine;
    [SerializeField] private float ShootForce;
    public MyCrosshair Crosshair;
    // Start is called before the first frame update
    void Start()
    {
        m_Self = gameObject;
        m_IsHumanLoaded = false;
    }

    public void GunAction(GameObject m_enemy)
    {
        if (m_IsHumanLoaded)
            ShootItOut(m_enemy);
        else
            LoadInGun(m_enemy);
    }
    
    void LoadInGun(GameObject m_OtherEnemy)
    {
        if(!m_OtherEnemy.GetComponent<Enemies>().CanBeSucked) return;
        Crosshair.IsShootable = false;
        // m_OtherEnemy.GetComponent<RagdollController>().RagdollOn();
        m_OtherEnemy.GetComponent<Enemies>().enabled = false;
        Transform Hip = m_OtherEnemy.transform.GetChild(0);
        m_IsHumanLoaded = !m_IsHumanLoaded;
        m_LoadedHuman = m_OtherEnemy;
        m_OtherEnemy.transform.DOLocalRotate(new Vector3(-20f, 0f, 0f), 0.1f);
        m_OtherEnemy.GetComponent<Animator>().SetBool("Suck In",true);
        // m_OtherEnemy.transform.DOScale(new Vector3(0,0,0), 0.8f);
       
        m_OtherEnemy.transform.DOMove(m_Muzzle.position + new Vector3(0f,-1.5f,0f), 1f).OnComplete(() =>
        {
            m_LoadedHuman.SetActive(false);
            foreach (Rigidbody rigidbody in m_OtherEnemy.GetComponentsInChildren<Rigidbody>())
            {
                rigidbody.gameObject.layer= LayerMask.NameToLayer($"EnemySucked");
            }
            m_HumanInMagazine.SetActive(true);
            m_HumanInMagazine.transform.DOScale(new Vector3(0.2f,0.2f,0.2f), 0.2f).SetEase(Ease.OutBounce);
            m_OtherEnemy.GetComponent<RagdollController>().RagdollOn();
            Crosshair.IsShootable = true;
        });;
        
    }

     void ShootItOut(GameObject m_OtherEnemy)
    {
        // m_OtherEnemy.GetComponent<RagdollController>().RagdollOn();
        m_OtherEnemy.GetComponent<Enemies>().CanBeSucked = false;
        Transform Hip=m_LoadedHuman.transform.GetChild(0);
        // Hip.DOScale(Vector3.one, 0.8f);
        m_IsHumanLoaded = !m_IsHumanLoaded;
        m_LoadedHuman.SetActive(true);
        m_HumanInMagazine.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.OutBounce).OnComplete(()=>m_HumanInMagazine.SetActive(false));
        Vector3 direction = m_OtherEnemy.transform.position - transform.position;
        Hip.GetComponent<Rigidbody>().AddForceAtPosition(direction.normalized * ShootForce, transform.position);
        Hip.transform.DOMove(m_OtherEnemy.transform.position, 0.5f).OnComplete(() =>
        {
            m_OtherEnemy.GetComponent<Enemies>().Damage();
            m_LoadedHuman.GetComponent<Enemies>().Damage();
            m_LoadedHuman = null;
            m_PlatformManager.OnEnemyDeath(2);
        });
        
    }
    
}
