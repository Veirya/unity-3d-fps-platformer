using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody myRB;
    public BoxCollider myGroundBC;
    private GroundCollisionLogic groundCheck;
    public GameObject grappleShot;  // Bullet for the grapple gun
    private GameObject grapple;     // Location of the grabble gun on the player
    private Transform cam;          // Reference to main camera's transform

    private bool canJump;       // Is touching the ground
    public float maxSpeed;      // Maximum horizontal speed
    public float rotSpeed;      // Multiplier for degree-based rotation
    private Vector3 movement;   // Placeholder for many Vector3s
    private bool grappling;     // Does a grapple hook exist?

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        groundCheck = myGroundBC.GetComponent<GroundCollisionLogic>();
        cam = this.transform.Find("Main Camera");
    }

    public void Update()
    {
        canJump = groundCheck.CanJump();

        grappling = GameObject.FindGameObjectWithTag("GrappleHook");
        if (Input.GetKeyDown(KeyCode.G))
        {
            RaycastHit target;
            if (Physics.Raycast(cam.position, cam.forward, out target, 100f))
            {
                Transform pos = transform.Find("GrappleGun");
                Vector3 aim = (target.point - pos.position).normalized;
                Quaternion rot = Quaternion.FromToRotation(-pos.up, aim);
                Instantiate(grappleShot, pos.position, rot);
            }
        }

        // Jump logic
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            Vector3 vel = myRB.velocity;
            vel.y = 15f;
            myRB.velocity = vel;

        }
    }

    public void FixedUpdate()
    {
        Vector3 horiVel;
        // Horizontal movement inputs
        float lrIn = Input.GetAxisRaw("Horizontal");
        float udIn = Input.GetAxisRaw("Vertical");

        float bodyRot = Input.GetAxis("Mouse X") * rotSpeed % 360;
        this.transform.Rotate(0, bodyRot, 0);

        // New velocity method
        if (canJump)
        {
            myRB.useGravity = false;
            horiVel = new Vector3(lrIn, 0, udIn).normalized * maxSpeed;
            horiVel = transform.TransformDirection(horiVel);
            myRB.velocity = horiVel + new Vector3(0, myRB.velocity.y, 0);
        } else
        {
            myRB.drag = myRB.velocity.magnitude > maxSpeed * 3 ? 10 : 0;
            myRB.useGravity = true;
            movement = new Vector3(lrIn, 0, udIn).normalized;
            movement = transform.TransformDirection(movement) * 15;
            myRB.AddForce(movement, ForceMode.Acceleration);
        }
    }

    public bool getCanJump()
    {
        return canJump;
    }
}