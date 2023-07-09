using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetKereta : MonoBehaviour
{
    [SerializeField] KeretaController keretaController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        keretaController.spawnedKereta = null;
    }
}
