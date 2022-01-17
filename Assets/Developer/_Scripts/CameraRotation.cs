using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform gun;
    private Quaternion targetRotaion;
    
    // Update is called once per frame
    void Update()
    {
        RotateCamera();
    }

    void RotateCamera()
    {
        targetRotaion = Quaternion.LookRotation(gun.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotaion, Time.deltaTime * rotationSpeed);
    }
}
