using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform cam;
    // Start is called before the first frame update

    private Quaternion fixedRotation; // Store the fixed rotation for the canvas

    void Start()
    {
        // Store the initial rotation of the canvas
        fixedRotation = transform.rotation;
    }

    private void LateUpdate()
    {
        // Prevent the canvas from rotating by reapplying the fixed rotation
        transform.rotation = fixedRotation;
    }
}
