using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    public float currentTime;
    public static Timer Instance { get; private set; }

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
        Destroy(gameObject);
        }
        else
        {
        Instance = this;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
    }
}
