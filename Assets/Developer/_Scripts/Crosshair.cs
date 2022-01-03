//SH
using UnityEngine;
using UnityEngine.UI;
using TouchControlsKit;
using Sirenix.OdinInspector;
using TCKAxisType = TouchControlsKit.EAxisType;

[HideMonoScript]
public class Crosshair : MonoBehaviour
{
    [Title("CROSSHAIR", null, titleAlignment: TitleAlignments.Centered, false, true)]

    [Title("Control Settings")]
    [Range(1f, 10f)]
    [SerializeField] float MouseSensitivity = 5.0f;
    [Space]
    [SerializeField] string Touchpad = "Touchpad";
    [Range(1f, 10f)]
    [SerializeField] float TouchSensitivity = 2.0f;
    [SerializeField] bool SmoothTouch = false;
    [EnableIf("SmoothTouch")]
    [Range(1f, 10f)]
    [SerializeField] float SmoothSensitivity;
    [EnableIf("SmoothTouch")]
    [Range(1f, 10f)]
    [SerializeField] float SmoothSpeed;
    
    [Space]
    [Title("Screen Size Settings")]
    [Range(0.0f, 1.0f)]
    [SerializeField] float MinHeight = 0.0f;
    [Range(0.0f, 1.0f)]
    [SerializeField] float MaxHeight = 1.0f;

    // [Space]
    // [Title("Target Aim Settings")]
    // [SerializeField] Camera Cam;
     [SerializeField] Transform CurrentAimTarget = null;
    // [SerializeField] float DetectionDistance = 120f;
    // [Range(1.0f,5.0f)]
    // [SerializeField] float AimSpeed;
    // [SerializeField] Vector3 AimOffset;

    // [Space]
    [SerializeField] Color UnlockedColor;
    [SerializeField] Color LockedColor;

    [SerializeField] Slider slider;
    [SerializeField] Toggle toggle;

    private bool CanRun = false;
    private Transform Self;
    private Transform Player;
    private Image Graphic;
    // private Vector3 StartPos;
    // private Vector3 Pos;

    // private float MinDist = Mathf.Infinity;
    // private float Dist    = Mathf.Infinity;

    private float MoveHorizontal = 0.0f;
    private float MoveVertical   = 0.0f;
    private float MinX, MaxX, MinY, MaxY;
    private Vector3 FinalPos;

    void Start()
    {
        Self      = transform;
        // Player    = SH_GameController.Instance.GetPlayer().transform;
        Graphic   = GetComponent<Image>();
        // if(!Cam)
        //     Cam = Camera.main;
        UpdateDimension();
        HideCursor();
        // slider.value = TouchSensitivity = SmoothSensitivity = SaveData.Instance.Sensitivity;
        //toggle.isOn  = SmoothTouch;
        //StartCoroutine(FindTarget());
    }//Start end

    void Update()
    {
        //CanRun = SH_GameController.Instance.GameStatus();

        // if(!CanRun)
        // {
        //     if(!Cursor.visible)
        //         ShowCursor();
        //     return;
        // }//if end

        // if(SH_GameController.Instance.Controls.Equals(SH_GameController.Control.Mobile))
            // TouchControls();
        // else
            MouseControls();
        
        // if(SH_GameController.Instance.GameStatus())
             UpdatePosition();
    }//Update end

    void MouseControls()
    {
        if(Cursor.visible)
            HideCursor();

        MoveHorizontal = Input.mousePosition.x;
        MoveVertical   = Input.mousePosition.y;

        FinalPos      = new Vector3 (MoveHorizontal, MoveVertical, 0.0f);
        FinalPos.x    = Mathf.Clamp (FinalPos.x, MinX, MaxX);
        FinalPos.y    = Mathf.Clamp (FinalPos.y, MinY, MaxY);
        Self.position  = Vector3.Lerp(Self.position, FinalPos, MouseSensitivity * Time.deltaTime);
    }//MouseControls() end

    public void TouchControls()
    {
        if(!Cursor.visible)
            ShowCursor();
        
        if(SmoothTouch)
        {
            if(TCKInput.CheckController(Touchpad))
            {
                MoveHorizontal = TCKInput.GetAxis( Touchpad, TCKAxisType.Horizontal ) * SmoothSensitivity * 100f;
                MoveVertical   = TCKInput.GetAxis( Touchpad, TCKAxisType.Vertical   ) * SmoothSensitivity * 100f;
            }//if end
            FinalPos     = new Vector3(MoveHorizontal, MoveVertical, 0.0f) + Self.position;
            FinalPos.x   = Mathf.Clamp(FinalPos.x, MinX, MaxX);
            FinalPos.y   = Mathf.Clamp(FinalPos.y, MinY, MaxY);
            Self.position = Vector3.Lerp(Self.position, FinalPos, Time.deltaTime * SmoothSpeed);
        }//if end
        else
        {
            if(TCKInput.CheckController(Touchpad))
            {
                MoveHorizontal = TCKInput.GetAxis( Touchpad, TCKAxisType.Horizontal ) * TouchSensitivity * 10f;
                MoveVertical   = TCKInput.GetAxis( Touchpad, TCKAxisType.Vertical   ) * TouchSensitivity * 10f;
            }//if end
            FinalPos     = new Vector3(MoveHorizontal, MoveVertical, 0.0f) + Self.position;
            FinalPos.x   = Mathf.Clamp(FinalPos.x, MinX, MaxX);
            FinalPos.y   = Mathf.Clamp(FinalPos.y, MinY, MaxY);
            Self.position = FinalPos;
        }//else end   
    }//TouchControls() end

    public void ShowCursor()
    {
        Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.None;
        //GetComponent<Image>().enabled = false;
    }//ShowCursor end

    public void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //GetComponent<Image>().enabled = true;
    }//HideCursor end

    void UpdatePosition()
    {
        if(CurrentAimTarget)
        {
            var Pos = Camera.main.WorldToScreenPoint(CurrentAimTarget.transform.position) ;
            Self.position = Vector3.Lerp(Self.position, Pos, 1f * Time.deltaTime);
            if(Vector3.Distance(Pos, Self.position) < 25f)
                Graphic.color = LockedColor;
        }//if end
        else
        {
            Graphic.color = UnlockedColor;
        }//else end
    }//UpdatePosition() end
    
    [Button("Update Dimensions")]
    public void UpdateDimension()
    {
        MinX = Screen.width * 0.05f;
        MinY = Screen.height * MinHeight;
        MaxX = Screen.width * 0.95f;
        MaxY = Screen.height * MaxHeight;
    }//UpdateDimension end

    public void TargetFound()
    {
        if(Graphic)
            Graphic.color = LockedColor;
    }//LockedColor() end

    public void TargetNotFound()
    {
        if(Graphic)
            Graphic.color = UnlockedColor;
    }//LockedColor() end

    public void ChangeSensitivity()
    {
        //TouchSensitivity = SmoothSensitivity = SaveData.Instance.Sensitivity = slider.value;
        //GDK_SaveSystem.SaveProgress();
    }//ChangeSensitivity() end

    public void SmoothToggle()
    {
        SmoothTouch = !SmoothTouch;
    }//SmoothToggle() end

}//class end