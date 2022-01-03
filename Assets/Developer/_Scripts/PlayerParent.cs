using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;
using System.Linq;

public class PlayerParent : MonoBehaviour
{

    #region SerializeField

    [SerializeField] private float Velocity,StrafeSpeed;
    [SerializeField] Transform Parent;
    [SerializeField] private InputControls Inputs;
    

    #endregion

    #region Private

    private Rigidbody m_RB;
    private Collider m_Col;
    private float m_InputX;
    private Vector3 m_pos;

    #endregion

    #region Public

    public float XClampValue;
    public Transform Child;

    #endregion
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        m_RB = GetComponent<Rigidbody>();
        m_Col = GetComponent<Collider>();
        // StartCoroutine(MaintainNearestEnemyArray());
        // RaycastHit hit;
        // if (Physics.Raycast(transform.position, Vector3.down * 5f,out hit))
        // {
        //    print(hit.transform.GetComponent<MeshRenderer>().bounds.center.x);
        //    
        // }
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("RotateRight"))
        {
            other.enabled = false;
            RotateRight();
        }
        if (other.transform.CompareTag("RotateLeft"))
        {
            other.enabled = false;
            RotateLeft();
        }

       
    }

    

    private void Update()
    {
        m_InputX = Inputs.Horizontal;
    }
    

    // Update is called once per frame
    
    void FixedUpdate()
    {
        if (Inputs.firstTouch) return;
        
        transform.position += transform.forward * Velocity * Time.fixedDeltaTime;
        //     
        // //Strafe Movement
        // m_pos = Child.localPosition +Vector3.right* m_InputX * StrafeSpeed  * Time.fixedDeltaTime;
        //
        // m_pos.x = Mathf.Clamp(m_pos.x, -XClampValue, XClampValue);
        // Child.localPosition = m_pos;

    }

    void CalculateCenter()
    {
        
    }
    
    void SlopeManager()
    {
        
    }
    
    public void RotateRight()
    {
        float rotAngle = transform.rotation.eulerAngles.y + 90f;
        transform.transform.DORotate(new Vector3(0,rotAngle,0),0.2f).OnComplete(()=>SnapToCenter(rotAngle));
        
    }
    
    public void RotateLeft()
    {
        float rotAngle = transform.rotation.eulerAngles.y - 90f;
        transform.transform.DORotate(new Vector3(0,rotAngle,0),0.2f).OnComplete(()=>SnapToCenter(rotAngle));;
    }

    void SnapToCenter(float angle)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down * 5f, out hit))
        {
            if (angle == 0 || angle == 180 || angle == 360)
            {
                Parent.DOMoveX(hit.transform.position.x, 0);
            }
            else
            {
                Parent.DOMoveZ(hit.transform.position.z, 0);
            }
        }
    }

    // IEnumerator MaintainNearestEnemyArray()
    // {
    //     while (true)
    //     {
    //         nearestGameObjects=Physics.OverlapSphere(new Vector3(transform.position.x,transform.position.y,transform.position.z + 10f), 10f,collisionLayerMask).ToList();
    //         nearestGameObjects = nearestGameObjects.OrderByDescending(t => Vector3.Distance(transform.position, t.transform.position)).Reverse().ToList();
    //         yield return new WaitForSeconds(0.5f);
    //     }
    // }
    
}
