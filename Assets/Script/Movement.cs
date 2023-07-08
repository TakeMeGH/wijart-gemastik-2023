using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    public Vector3 speed;
    Vector3 baseSpeed;
    public bool stoped = false;
    [SerializeField] GameObject particleFast;
    bool fastParticleStatus;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        if(particleFast != null && fastParticleStatus == false && stoped == false){
            if(particleFast.activeSelf){
                fastParticleStatus = true;
            }
        }
        if(particleFast != null){
            if(!stoped && fastParticleStatus && !particleFast.activeSelf){
                particleFast.SetActive(true);
            }
            else if(stoped){
                particleFast.SetActive(false);
            }
        }
        
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
