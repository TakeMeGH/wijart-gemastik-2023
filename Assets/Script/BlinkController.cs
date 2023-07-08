using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkController : MonoBehaviour
{

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        InvokeRepeating("blink", 2f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void blink(){
        animator.Play("blink");
    }
}
