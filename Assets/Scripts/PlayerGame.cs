
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
    public float currentRotationSpeed = 3;
    public bool canJump = true;

    private bool fire;
    private bool jump;
    private bool grounded = false;

    private Vector3 moveVector;
    private Vector3 lookDirection;

    private Vector3 lastMoveVector = Vector3.zero;

    private new Rigidbody rigidbody;
    private float remainSlowTime;
    private float slowSpeed;
    private float slowVelocityChange;

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
        {
            lastMoveVector = moveVector;
            lookDirection.x = lastMoveVector.normalized.x;
            lookDirection.z = lastMoveVector.normalized.z;
        }

        // Get the input from the Rewired Player. All controllers that the Player owns will contribute, so it doesn't matter
        // whether the input is coming from a joystick, the keyboard, mouse, or a custom controller.

        moveVector.x = player.GetAxis("Move Horizontal"); // get input by name or action id
        moveVector.z = player.GetAxis("Move Vertical");
        fire = player.GetButtonDown("Fire");
        jump = player.GetButtonDown("Jump");
    }

    void Update()
    {
        if (!ReInput.isReady) return; // Exit if Rewired isn't ready. This would only happen during a script recompile in the editor.
        if (player == null) return;

        GetInput();

        if((remainSlowTime -= Time.deltaTime) > 0)
        {
            speed = slowSpeed;
            maxVelocityChange = slowVelocityChange;

        }
    }

    void FixedUpdate()
    {
        
        if (grounded)
        {
            // Calculate how fast we should be moving

            Vector3 targetVelocity = Vector3.forward;
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
        
        //lookDirection = rigidbody.velocity.normalized;
        
        if (lookDirection != Vector3.zero && currentRotationSpeed > 0f)
        {
            // Smoothly interpolate from current to target look direction
            Vector3 smoothedLookDirection = Vector3.Slerp(transform.forward, lookDirection, 1 - Mathf.Exp(-currentRotationSpeed * Time.deltaTime)).normalized;

            // Set the current rotation (which will be used by the KinematicCharacterMotor)
            transform.rotation = Quaternion.LookRotation(smoothedLookDirection, transform.up);
        }

        // We apply gravity manually for more tuning control
        rigidbody.AddForce(new Vector3(0, -gravity * rigidbody.mass, 0));

        grounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.tag.Equals("Obstacle"))
            return;

        remainSlowTime = 2f;
        
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