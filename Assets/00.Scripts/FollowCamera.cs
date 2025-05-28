using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform target;

    [SerializeField] private Vector3 offset = new Vector3(0, 1.5f, -5f); 
    [SerializeField] private float rotationSpeed = 3f; 
    [SerializeField] private float followSmoothness = 10f; 

    private float yaw;   // 좌우 회전
    private float pitch = 3f; // 상하 회전
    [SerializeField] private float minPitch = -20f;
    [SerializeField] private float maxPitch = 80f;
    
    private void Start()
    {
        yaw = target.eulerAngles.y;
    }

    void LateUpdate()
    {
        RotateCamera();
        FollowTarget();

        if (Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        yaw += mouseX;

        if(Input .GetMouseButton(1))
        {
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

            pitch -= mouseY;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        }
    }

    void FollowTarget()
    {
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 desiredPosition = target.position + rotation * offset;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * followSmoothness);
        transform.LookAt(target);
    }
}

