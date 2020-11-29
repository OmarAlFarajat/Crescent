using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonGravity : MonoBehaviour
{
    // Based on: http://answers.unity.com/answers/1785305/view.html

    // Initialize
    private Rigidbody rigidBody;
    private Vector3 normal;
    private Vector3 targetDirection;
    private Quaternion targetRotation;

    private const float GRAVITY = -20.0f;
    private const float RAYDISTANCE = 15f;
    private const float ROTATIONSPEED = 0.075f;

    // When script initializes
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Physics update
    void FixedUpdate()
    {
        // Set up ray to check the surface below player
        RaycastHit temp_hit;
        Ray temp_ray = new Ray(transform.position + transform.up, -transform.up * RAYDISTANCE);

        // Gets the normal of the surface below the character, and also creates a target direction for gravity
        if (Physics.Raycast(temp_ray, out temp_hit) && (temp_hit.transform.CompareTag("Moon") || temp_hit.transform.CompareTag("Platform")))
        {
            //Debug.Log(temp_hit.transform.name);
            normal = temp_hit.normal;
            targetDirection = (transform.position - temp_hit.point).normalized;
        }

        // Finds desired rotation relative to surface normal
        targetRotation = Quaternion.FromToRotation(transform.up, normal) * transform.rotation;

        // Apply rotation and gravity
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, ROTATIONSPEED);
        
        // Having a rigid body and 
        if(rigidBody)
            rigidBody.AddForce(targetDirection * GRAVITY);
    }
}
