using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidBody;
    public Animator animator;

    [SerializeField]
    private float moveSpeed = 4f;
    [SerializeField]
    private float jumpForce = 6f;

    public bool isGrounded = false;

    [SerializeField]
    private Transform currentMovingPlatform;


    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        MouseLook();
    }

    public void Move()
    {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        rigidBody.MovePosition(rigidBody.position + transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime);
    }
    public void Jump()
    {
        if (isGrounded)
        {
            rigidBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void MouseLook()
    {
        // http://answers.unity.com/answers/1401934/view.html
        float X = Input.GetAxis("Mouse X") * 2f;
        float Y = Input.GetAxis("Mouse Y") * 2f;

        transform.Rotate(0, X, 0); // Player rotates on Y axis, your Cam is child, then rotates too


        // To scurity check to not rotate 360º 
        if (Camera.main.transform.eulerAngles.x + (-Y) > 80 && Camera.main.transform.eulerAngles.x + (-Y) < 280)
        {

        }
        else
        {
            Camera.main.transform.RotateAround(transform.position, Camera.main.transform.right, -Y);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Moon"))
        {
            //animator.SetBool("isGrounded", true);
            isGrounded = true;
        }

        if (collision.transform.CompareTag("Platform"))
        {
            //animator.SetBool("isGrounded", true);
            isGrounded = true;
            currentMovingPlatform = collision.gameObject.transform;
            transform.SetParent(currentMovingPlatform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.tag == "Platform")
        {
            currentMovingPlatform = null;
            transform.parent = null;
        }
    }
}
