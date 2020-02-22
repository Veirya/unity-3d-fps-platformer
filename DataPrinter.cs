using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPrinter : MonoBehaviour
{
    void Update()
    {
        Debug.Log("Mouse Input X: " + Input.GetAxis("Mouse X"));
        Debug.Log("Mouse Input Y: " + Input.GetAxis("Mouse Y"));
    }
}
