using DG.Tweening;
using UnityEngine;

public class MyCrosshair : MonoBehaviour
{
    [SerializeField] private HumanGun m_Gun;
    [SerializeField] private InputControls m_InputControls;
    private float MoveHorizontal, MoveVertical,MinX,MinY,MaxX,MaxY;
    private Vector3 FinalPos;
    public float SmoothSpeed;
    public bool IsShootable;
    private Camera m_Camera;

    [SerializeField] private LayerMask RaycastLayer;
    // Start is called before the first frame update
    void Start()
    {
        IsShootable = true;
        m_Gun.Crosshair = this;
        m_Camera = Camera.main;
    }

    // Update is called once per frame
    void Update()   
    {
        //Getting Input
        GetInputs();
        FinalPos = new Vector3 (MoveHorizontal, MoveVertical, 0.0f);
        
        Clamping();
        
        //Assign new position
        // transform.position = Vector3.Lerp(transform.position, FinalPos, Time.deltaTime * SmoothSpeed);
        transform.position = FinalPos;
        //Shooting
        if(IsShootable)
            Shoot();

    }

    void Clamping()
    {
        // FinalPos.x = Mathf.Clamp (FinalPos.x, MinX,MaxX);
        // // FinalPos.y = Mathf.Clamp (FinalPos.y,MinY,MaxY)
    }
    
    void GetInputs()
    {
        MoveHorizontal = Input.mousePosition.x;
        MoveVertical = Input.mousePosition.y;
    }
    
    void Shoot()
    {
        Ray rayOrigin = m_Camera.ScreenPointToRay(FinalPos);
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, out hit,50,RaycastLayer))
        {
            Vector3 dir =hit.point - m_Gun.transform.position;
            m_Gun.transform.DORotateQuaternion(Quaternion.LookRotation(dir),0.2f);
            if (hit.transform.GetComponent<Enemies>())
            {
                m_Gun.GunAction(hit.transform.gameObject);
            }
        }
    }
    

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
