using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidBody;
    public Animator animator;
    private AudioManager audioManager; 

    [SerializeField]
    private float moveSpeed = 4f;
    [SerializeField]
    private float runMultiplier = 3f;
    [SerializeField]
    private float jumpForce = 12f;
    [SerializeField]
    private float attackForce = 12f;

    public bool isGrounded = false;
    public bool isMoving = false;
    public bool isRunning = false;
    public bool isAttacking = false;


    [SerializeField]
    private Transform currentMovingPlatform;


    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void Update()
    {
        MouseLook();
    }

    public void Move()
    {
        if (!isAttacking)
        {
            Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

            if (isRunning)
                rigidBody.MovePosition(rigidBody.position + transform.TransformDirection(moveDir) * moveSpeed * runMultiplier * Time.deltaTime);
            else
                rigidBody.MovePosition(rigidBody.position + transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime);
        }
    }
    public void Jump()
    {
        if (!isAttacking)
        {
            if (isGrounded)
            {
                audioManager.Play("Jump" + Random.Range(0, 4)); // Plays one of 4 random jumping sounds
                rigidBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
                animator.SetBool("isGrounded", false);
            }
        }        
    }

    public void Attack()
    {
        if (!isAttacking)
        {
            audioManager.Play("Attack" + Random.Range(0, 3)); // Plays one of 3 random attack sounds
            rigidBody.AddForce(transform.forward * attackForce, ForceMode.Impulse);
            isAttacking = true;
            animator.SetBool("isAttacking", true);
            StartCoroutine("Wait");
        }
    }
    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        isAttacking = false;
        animator.SetBool("isAttacking", false);
        rigidBody.isKinematic = true;
        rigidBody.isKinematic = false;
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
            animator.SetBool("isGrounded", true);
            isGrounded = true;
        }

        if (collision.transform.CompareTag("Platform"))
        {
            animator.SetBool("isGrounded", true);
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

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.CompareTag("Moon"))
        {
            animator.SetBool("isGrounded", true);
            isGrounded = true;
        }

        if (collider.transform.CompareTag("Platform"))
        {
            animator.SetBool("isGrounded", true);
            isGrounded = true;
            currentMovingPlatform = collider.gameObject.transform;
            transform.SetParent(currentMovingPlatform);
        }
    }

    private void OnTriggerExit(Collider collider)
    {

        if (collider.gameObject.tag == "Platform")
        {
            currentMovingPlatform = null;
            transform.parent = null;
        }
    }
}
