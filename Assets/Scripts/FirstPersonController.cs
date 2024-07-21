using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensitivity = 100f;
    public Transform playerCamera;

    private float verticalLookRotation;
    private float horizontalLookRotation;
    private float smoothHorizontalLookRotation;
    private float smoothVerticalLookRotation;
    public float rotationSmoothTime = 10;
    private CharacterController characterController;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (QuestManager.instance.canMove)
        {
            Move();
        }
        if (QuestManager.instance.canLook)
        {
            LookAround();
        }
    }

    void Move()
    {
        float moveForwardBackward = Input.GetAxis("Vertical") * speed;
        float moveLeftRight = Input.GetAxis("Horizontal") * speed;

        Vector3 move = transform.right * moveLeftRight + transform.forward * moveForwardBackward;
        move *= Time.deltaTime;
        
        characterController.Move(move);
        transform.position = new Vector3(transform.position.x, -4.26f, transform.position.z);
    }

    void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        horizontalLookRotation += mouseX;
        verticalLookRotation -= mouseY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

        smoothHorizontalLookRotation = Mathf.Lerp(smoothHorizontalLookRotation, horizontalLookRotation, rotationSmoothTime * Time.deltaTime);
        smoothVerticalLookRotation = Mathf.Lerp(smoothVerticalLookRotation, verticalLookRotation, rotationSmoothTime * Time.deltaTime);

        transform.localEulerAngles = new Vector3(0, smoothHorizontalLookRotation, 0);
        playerCamera.localEulerAngles = Vector3.right * smoothVerticalLookRotation;
    }
    public void UpdateRotationValues(Quaternion targetRotation)
    {
        Vector3 targetEulerAngles = targetRotation.eulerAngles;
        horizontalLookRotation = targetEulerAngles.y;
        verticalLookRotation = targetEulerAngles.x;

        smoothHorizontalLookRotation = horizontalLookRotation;
        smoothVerticalLookRotation = verticalLookRotation;
    }
}
