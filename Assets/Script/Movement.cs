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
    AudioSource audioSource;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        transform.position += offset;
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
            if(audioSource != null && audioSource.isPlaying) audioSource.Stop();
            speed = Vector3.zero;
        }
        else{
            if(audioSource != null && !audioSource.isPlaying) audioSource.Play();
            speed = baseSpeed;
        }
        rb.velocity = speed;
    }
    
}
