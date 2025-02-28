using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] float speed = 5f;
    [SerializeField] float rotationSpeed = 5f;

    [Header("Reference")]
    Animator anim;
    CharacterController cc;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        Movement();
    }
    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;


        if (movDirection.magnitude > 0.1f)
        {

            Quaternion toRotation = Quaternion.LookRotation(movDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

            Vector3 movVector = transform.forward * speed * Time.deltaTime;
            cc.Move(movVector);

            anim.SetBool("walk", true);
        }
        else
        {
            anim.SetBool("walk", false);
        }
    }
}
