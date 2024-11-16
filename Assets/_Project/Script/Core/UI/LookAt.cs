using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform cam;
    // Start is called before the first frame update

    private void LateUpdate()
    {
        Vector3 directionToCamera = cam.position - transform.position;

        // Keep the UI element upright by fixing the y-axis rotation
        directionToCamera.y = 0;
    
        // Set the UI to look at the camera along this adjusted direction
        transform.rotation = Quaternion.LookRotation(-directionToCamera);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
