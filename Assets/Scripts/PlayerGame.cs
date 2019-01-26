﻿
using UnityEngine;
using System.Collections;
using Rewired;
using Rewired.Demos;

[RequireComponent(typeof(Rigidbody))]
public class PlayerGame : MonoBehaviour {

    public int gamePlayerId = 0;

    public float turnSpeed = 3;
    public float speed = 10.0f;
    public float gravity = 10.0f;
    public float maxVelocityChange = 10.0f;
    public float jumpHeight = 2.0f;
    public bool canJump = true;

    private bool fire;
    private bool jump;
    private bool grounded = false;

    private Vector3 moveVector;

    private Vector3 lastMoveVector = Vector3.forward;

    private new Rigidbody rigidbody;
    private Rewired.Player player { get { return PressStartToJoinExample_Assigner.GetRewiredPlayer(gamePlayerId); } }


    void Awake()
    {
        rigidbody = this.GetComponent<Rigidbody>();

        rigidbody.freezeRotation = true;
        rigidbody.useGravity = false;
    }

    private void GetInput()
    {
        if (moveVector.magnitude > 0)
            lastMoveVector = moveVector;

        // Get the input from the Rewired Player. All controllers that the Player owns will contribute, so it doesn't matter
        // whether the input is coming from a joystick, the keyboard, mouse, or a custom controller.

        moveVector.x = player.GetAxis("Move Horizontal"); // get input by name or action id
        moveVector.y = player.GetAxis("Move Vertical");
        fire = player.GetButtonDown("Fire");
        jump = player.GetButtonDown("Jump");
    }

    void Update()
    {
        if (!ReInput.isReady) return; // Exit if Rewired isn't ready. This would only happen during a script recompile in the editor.
        if (player == null) return;

        GetInput();
    }

    void FixedUpdate()
    {
        if (grounded)
        {
            // Calculate how fast we should be moving
            Vector3 targetVelocity = new Vector3(lastMoveVector.x, 0, lastMoveVector.y).normalized;
            Debug.Log(targetVelocity);
            targetVelocity = transform.TransformDirection(targetVelocity);
            targetVelocity *= speed;

            // Apply a force that attempts to reach our target velocity
            Vector3 velocity = rigidbody.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
            rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);


            // Jump
            if (canJump && jump)
            {
                rigidbody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
            }
        }



        // We apply gravity manually for more tuning control
        rigidbody.AddForce(new Vector3(0, -gravity * rigidbody.mass, 0));



        grounded = false;
    }

    void OnCollisionStay()
    {
        grounded = true;
    }

    float CalculateJumpVerticalSpeed()
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }
}