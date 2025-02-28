using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    [SerializeField] Transform currentTarget;
    [SerializeField] Vector3 offset;
    [SerializeField] Camera cam;

    float camFollowSpeed = 0.2f;
    Vector3 camFollowVelocity = Vector3.zero;

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
            Vector3 targetPosition = currentTarget.position + offset;        // This only follow to the player front, back and left, right

            Vector3 camFollow = Vector3.SmoothDamp(transform.position, targetPosition, ref camFollowVelocity, camFollowSpeed);
            transform.position = camFollow;
        }
    }

    void RotateCamera()
    {
        float moveX = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        float moveY = Input.GetAxisRaw("Mouse X") * mouseSensitivity;

        Vector3 mouseRotation = new Vector3(0, moveY, 0);   // They move up and down only
        transform.Rotate(mouseRotation);

        Vector3 camRotaion = new Vector3(moveX, 0, 0);
        cam.transform.Rotate(-camRotaion );
    }

}
