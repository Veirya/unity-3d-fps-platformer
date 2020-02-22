using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Speed multiplier for rotation
    public float rotSpeed;

    private float camRot;

    // Min and Max rotation angle in degrees
    public float MinimumX;
    public float MaximumX;

    void FixedUpdate()
    {
        camRot = -Input.GetAxis("Mouse Y") * rotSpeed % 360;
        transform.Rotate(camRot, 0, 0);
        transform.localRotation = ClampRotationAroundXAxis(transform.localRotation);
    }

    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }
}
