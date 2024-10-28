using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform cam;
    // Start is called before the first frame update

    private void LateUpdate()
    {
        
        transform.LookAt(cam);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
