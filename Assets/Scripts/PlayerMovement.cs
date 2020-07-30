using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // public
    public Transform cam;
    public CharacterController controller;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;

    // private
    float turnSmoothVelocity;

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        // normalize this so that the player does not move faster when going diagonally.
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            // x value first cause unity has weird coordinate system.
            float targetAngle = Mathf.Atan2(direction.x, direction.z) *
                Mathf.Rad2Deg +
                cam.eulerAngles.y; // this adds the cameras t rotation to
                // the player rotation so that we look where the camera looks.

            // Apply smoothing so that caracter doesnt snap rotation
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                targetAngle,
                ref turnSmoothVelocity,
                turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // this lines makes us move in the direction of the camera.
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }
    }
}
