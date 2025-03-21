using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    [SerializeField] Transform currentTarget;
    [SerializeField] Vector3 offset;
    [SerializeField] Camera cam;

    float camFollowSpeed = 5f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    public float mouseSensitivity = 5f;
    void LateUpdate()
    {
        FollowCamera();
        RotateCamera();
    }
    void FollowCamera()
    {
        if (currentTarget != null)
        {
            Vector3 targetPosition = currentTarget.position + offset;     

            transform.position = Vector3.Lerp(transform.position, targetPosition, camFollowSpeed * Time.deltaTime);

        }
    }

    void RotateCamera()
    {
        float moveX = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        float moveY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

        // Rotate the camera around the player
        rotationX-= moveY;
        rotationY += moveX;

        rotationX = Mathf.Clamp(rotationX, -10f, 30f); 

        // Apply rotation to the camera
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
        transform.rotation = rotation;

    }

}
