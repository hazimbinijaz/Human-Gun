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
    private Camera m_Camera;
    [SerializeField] private DOTweenAnimation m_SwayAnimation;
    [SerializeField] private LayerMask RaycastLayer;
    // Start is called before the first frame update
    void Start()
    {
        UpdateDimension();
        IsShootable = true;
        m_Gun.Crosshair = this;
        m_Camera = Camera.main;
    }

    // Update is called once per frame
    void Update()   
    {
        //Getting Input
        GetInputs();
        FinalPos = new Vector3 (MoveHorizontal, MoveVertical, 0.0f) + transform.position;
        Clamping();
        
        //Assign new position
        transform.position = FinalPos;
        //Shooting
        if(IsShootable)
            Shoot();

    }

    void Clamping()
    {
        FinalPos.x   = Mathf.Clamp(FinalPos.x, MinX, MaxX);
        FinalPos.y   = Mathf.Clamp(FinalPos.y, MinY, MaxY);
    }
    
    void GetInputs()
    {
        // MoveHorizontal = Input.mousePosition.x;
        // MoveVertical = Input.mousePosition.y;
        
        MoveHorizontal = TCKInput.GetAxis( $"Movepad", TCKAxisType.Horizontal ) * 10f;
        MoveVertical   = TCKInput.GetAxis( $"Movepad", TCKAxisType.Vertical   ) * 10f;
        // if (MoveHorizontal != 0 || MoveVertical != 0)
        // {
        //     print($"1");
        //     if(!m_SwayAnimation.isActiveAndEnabled)
        //         m_SwayAnimation.DOPause();
        // }
        // else
        // {
        //     print($"2");
        //     if (!m_SwayAnimation.isActiveAndEnabled)
        //         m_SwayAnimation.DOPlay();
        // }
    }
    
    void Shoot()
    {
        Ray rayOrigin = m_Camera.ScreenPointToRay(FinalPos);
        RaycastHit hit;
       
        if (Physics.Raycast(rayOrigin, out hit,50,RaycastLayer))
        {
            // Vector3 dir =new Vector3(MoveHorizontal,MoveVertical,0) - m_Gun.transform.position;
            Vector3 dir =hit.point - m_Gun.transform.position;
            m_Gun.transform.DORotateQuaternion(Quaternion.LookRotation(dir),0.2f);
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

    public void ShowCursor()
    {
        Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.None;
        //GetComponent<Image>().enabled = false;
    }//ShowCursor end

    public void HideCursor()
    {
        Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        //GetComponent<Image>().enabled = true;
    }//HideCursor end
}
