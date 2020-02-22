using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleShotController : MonoBehaviour
{

    private Rigidbody myRB;
    public GameObject hookEnd;
    private bool started;   // ensure it doesn't despawn when first fired

    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myRB.velocity = transform.TransformDirection(new Vector3(0, -50, 0));
        started = true;
        Destroy(this.gameObject, 2f);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && !started)
        {
            Destroy(this.gameObject);
        }

        started = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Environment"))
        {
            Instantiate(hookEnd, collision.GetContact(0).point, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
