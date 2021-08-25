using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookDirection : MonoBehaviour
{
    private float rotateYSpeed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y");
        transform.localEulerAngles = new Vector3((transform.localEulerAngles.x + mouseY) * rotateYSpeed, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
}
