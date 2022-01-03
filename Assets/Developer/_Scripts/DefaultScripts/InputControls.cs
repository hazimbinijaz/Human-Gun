using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.EventSystems;

[HideMonoScript]
public class InputControls : MonoBehaviour
{
    [Title("INPUT CONTROLS", titleAlignment: TitleAlignments.Centered)]
    [DisplayAsString]
    [SerializeField] bool FingerDown  = false;
    [DisplayAsString]
    [SerializeField] float MoveDeltaX = 0.0f;
    [SerializeField] float MoveDeltaY = 0.0f;
    private float LastPosX = 0.0f;
    private float LastPosY = 0.0f;
    public bool firstTouch { get; private set; }
    
    public bool TouchDown   => FingerDown;
    public float Horizontal => MoveDeltaX;
    public float Vertical => MoveDeltaY;

    private void Start()
    {
        Input.multiTouchEnabled = false;
        firstTouch = true;  
    } 
    
    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
            GetInput();
    }//Update() end

    private void GetInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (firstTouch)
            {
                firstTouch = false;
                UIManager.Instance.OnClickStartGame();
            }
            FingerDown = true;
            LastPosX   = Input.mousePosition.x;
        }//if end
        else if (Input.GetMouseButton(0))
        {
            MoveDeltaX = Input.mousePosition.x - LastPosX;
            MoveDeltaY = Input.mousePosition.y - LastPosY;
            LastPosX   = Input.mousePosition.x;
            LastPosY   = Input.mousePosition.y;
        }//else if end
        else if (Input.GetMouseButtonUp(0))
        {
            FingerDown = false;
            MoveDeltaX = 0.0f;
            MoveDeltaY = 0.0f;
        }//else if end
    }//GetInput() end

}//class end