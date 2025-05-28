using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float gravity = -9.81f;

    public LayerMask groundLayer;
    public float rotationSpeed = 10.0f;

    private CharacterController characterController;
    private Animator animator;

    private float yVelocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Move();
        //RotateToMouse();
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // 카메라 기준 방향
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        cameraForward.y = 0.0f;
        cameraRight.y = 0.0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        // x, z 이동 방향 계산
        Vector3 moveDirection = (cameraRight * horizontal + cameraForward * vertical).normalized;

        // 중력 적용
        if(characterController.isGrounded && yVelocity < 0)
        {
            yVelocity = -1f;
        }
        else
        {
            yVelocity += gravity * Time.deltaTime;
        }

        Vector3 moveXZ = moveDirection * moveSpeed;
        Vector3 moveY = Vector3.up * yVelocity;
        Vector3 finalMove = moveXZ + moveY;

        characterController.Move(finalMove * Time.deltaTime);

        float currentSpeed = moveXZ.magnitude;

        Debug.Log("현재 speed 파라미터 값: " + currentSpeed);
        animator.SetFloat("speed", currentSpeed);

        if(moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void RotateToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            Vector3 targetPostion = hit.point;

            Vector3 direction = (targetPostion - transform.position).normalized;
            direction.y = 0.0f;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
