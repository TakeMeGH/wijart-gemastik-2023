using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningConditionSound : MonoBehaviour
{
    [SerializeField] AudioClip winningSound;
    AudioSource audioSource;
    bool isCalled;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playWinning(){
        if(isCalled) return;
        isCalled = true;
        audioSource.Stop();
        audioSource.PlayOneShot(winningSound);
    }
}
