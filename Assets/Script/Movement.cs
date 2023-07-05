using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    public Vector3 speed;
    Vector3 baseSpeed;
    public bool stoped = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        if(speed != Vector3.zero) baseSpeed = speed;
        if(stoped){
            speed = Vector3.zero;
        }
        else{
            speed = baseSpeed;
        }
        rb.velocity = speed;
    }
    
}
