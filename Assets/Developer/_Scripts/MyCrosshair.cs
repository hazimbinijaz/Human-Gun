using System.Collections;
using DG.Tweening;
using UnityEngine;
using TCKAxisType = TouchControlsKit.EAxisType;
using TouchControlsKit;

public class MyCrosshair : MonoBehaviour
{
    [SerializeField] private HumanGun m_Gun;
    [SerializeField] private InputControls m_InputControls;
    private float MoveHorizontal, MoveVertical,MinX,MinY,MaxX,MaxY;
    private Vector3 FinalPos;
    public float SmoothSpeed;
    public bool IsShootable;
    [SerializeField] Camera m_Camera;
    [SerializeField] private LayerMask RaycastLayer;

    public int ttemp;
    // Start is called before the first frame update
    void Start()
    {
        IsShootable = true;
        m_Gun.Crosshair = this;
        UpdateDimension();
    }

    // Update is called once per frame
    void Update()   
    {
        if (IsShootable)
        {
            //Getting Input
            GetInputs();
            FinalPos = new Vector3 (MoveHorizontal, MoveVertical, 0.0f) + transform.position;
            Clamping();
            
            //Assign new position
            transform.position = FinalPos;
            
            //Shooting
            Shoot();
            
        }

    }

    void Clamping()
    {
        FinalPos.x   = Mathf.Clamp(FinalPos.x, MinX, MaxX);
        FinalPos.y   = Mathf.Clamp(FinalPos.y, MinY, MaxY);
    }
    
    void GetInputs()
    {
        MoveHorizontal = TCKInput.GetAxis( $"Movepad", TCKAxisType.Horizontal ) * 10f;
        MoveVertical = TCKInput.GetAxis($"Movepad", TCKAxisType.Vertical) * 10f;
    }
    
    
    void Shoot()
    {
        Ray rayOrigin = m_Camera.ScreenPointToRay(FinalPos);
        RaycastHit hit;
       
        if (Physics.Raycast(rayOrigin, out hit,50,RaycastLayer))
        {
            // Vector3 dir =new Vector3(MoveHorizontal,MoveVertical,0) - m_Gun.transform.position;
            Vector3 dir =hit.point - m_Gun.transform.position;
            m_Gun.transform.DORotateQuaternion(Quaternion.LookRotation(new Vector3(dir.x,dir.y,dir.z)),0.2f);
            if (hit.transform.GetComponent<Enemies>())
            {
                m_Gun.GunAction(hit.transform.gameObject);
            }
        }
    }
    
    public void UpdateDimension()
    {
        MinX = 0;
        MinY = 0 ;
        MaxX = Screen.width ;
        MaxY = Screen.height;
    }//UpdateDimension end
    
}
