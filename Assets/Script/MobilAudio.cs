using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilAudio : MonoBehaviour
{
    [SerializeField] AudioClip ngebut;
    [SerializeField] AudioClip lambat;
    AudioSource audioSource;
    void Awake() {
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void audioCepat(){
        audioSource.clip = ngebut;
        audioSource.volume = 1f;
        audioSource.Play();
    }

    public void audioLambat(){
        audioSource.clip = lambat;
        audioSource.Play();
    }
}
