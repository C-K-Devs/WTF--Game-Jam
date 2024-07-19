using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensitivity = 100f;
    public Transform playerCamera;

    private float verticalLookRotation;
    private float horizontalLookRotation;
    private float smoothVerticalLookRotation;
    private float smoothHorizontalLookRotation;
    public float rotationSmoothTime = 0.1f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Move();
        LookAround();
    }

    void Move()
    {
        float moveForwardBackward = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float moveLeftRight = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        Vector3 move = transform.right * moveLeftRight + transform.forward * moveForwardBackward;
        transform.Translate(move, Space.World);
    }

    void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        horizontalLookRotation += mouseX;
        verticalLookRotation -= mouseY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

        smoothHorizontalLookRotation = Mathf.Lerp(smoothHorizontalLookRotation, horizontalLookRotation, rotationSmoothTime * Time.deltaTime);
        smoothVerticalLookRotation = Mathf.Lerp(smoothVerticalLookRotation, verticalLookRotation, rotationSmoothTime * Time.deltaTime);

        transform.localEulerAngles = new Vector3(0, smoothHorizontalLookRotation, 0);
        playerCamera.localEulerAngles = Vector3.right * smoothVerticalLookRotation;
    }
}