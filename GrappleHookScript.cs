using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHookScript : MonoBehaviour
{
    private GameObject player;
    private Rigidbody playerRB;
    private float tautPoint;    // Distance from the hook where the "rope" is taut
    public float breakPoint;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRB = player.GetComponent<Rigidbody>();
        tautPoint = (this.transform.position - player.transform.position).magnitude;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.B))
        {
            Destroy(this.gameObject);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            tautPoint = (this.transform.position - player.transform.position).magnitude;
        }

    }

    public void FixedUpdate()
    {
        Vector3 between = this.transform.position - player.transform.position;
        if (between.magnitude >= breakPoint)
        {
            Destroy(this.gameObject);
        }
        else if (between.magnitude >= tautPoint)
        {
            float angle = Mathf.Abs(90 - Vector3.Angle(between, playerRB.velocity));
            float mag = playerRB.velocity.magnitude * Mathf.Cos(angle);
            mag = mag * mag / between.magnitude * 3;
            Vector3 accel = between.normalized * mag;
            accel += new Vector3(0, -Physics.gravity.y / 1.5f, 0);
            if (between.magnitude > tautPoint)
            {
                accel += between.normalized * 10;
            }
            Debug.Log(accel);
            playerRB.AddForce(accel, ForceMode.Acceleration);
        }
    }
}
