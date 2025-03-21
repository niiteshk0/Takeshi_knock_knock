using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class playerMovement : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] float walkSpeed = 0.5f;
    [SerializeField] float runSpeed = 1f;
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] float blendTreeSmoothTime = 0.1f;

    [Header("Reference")]
    [SerializeField] GameObject spineRig;
    private Animator anim;
    private CharacterController cc;

    private float currBlendTreeSpeed;
    private float blendTreeVelocity;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        currBlendTreeSpeed = 0f;
        SetWeight(0);
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            anim.SetBool("Fire", true);
            SetWeight(1);
        }
        else
        {
            SetWeight(0);
            anim.SetBool("Fire", false);
        }
        Movement();
    }

    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;
        cameraForward.y = 0f; // Flatten the vectors to ignore vertical rotation
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        
        Vector3 movDirection = (cameraForward * verticalInput + cameraRight * horizontalInput).normalized;

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currSpeed = isRunning ? runSpeed : walkSpeed;

        if (movDirection.magnitude > 0.1f)
        {

            Quaternion toRotation = Quaternion.LookRotation(movDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

            Vector3 movVector = movDirection * currSpeed * Time.deltaTime;
            cc.Move(movVector);

            float targetBlendTreeSpeed= isRunning ? 1f : 0.5f;

            currBlendTreeSpeed = Mathf.SmoothDamp(currBlendTreeSpeed, targetBlendTreeSpeed, ref blendTreeVelocity, blendTreeSmoothTime);
        }
        else
        {
            float targetBlendTreeSpeed = 0f;
            currBlendTreeSpeed = Mathf.SmoothDamp(currBlendTreeSpeed, targetBlendTreeSpeed, ref blendTreeVelocity, blendTreeSmoothTime);
        }
        anim.SetFloat("Move", currBlendTreeSpeed);
    }

    public void SetWeight(int weight)
    {
        if (spineRig == null)
        {
            Debug.Log("spine rig is null");
            return;
        }


        var cons = spineRig.GetComponent<MultiAimConstraint>();
        var sourceObjects = cons.data.sourceObjects;

        if (sourceObjects.Count > 0)
        {
            sourceObjects.SetWeight(0, weight);
            cons.data.sourceObjects = sourceObjects;
        }
        else
        {
            Debug.Log("SourceObjects count is zero");
        }

    }
}
