using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAround : MonoBehaviour
{
    BoxCollider boxCollider;
    public GameObject car;
    public bool isNextMustStop;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "mobil"){
            car = other.gameObject;
            if(isNextMustStop){
                isNextMustStop = false;
                car.GetComponent<Movement>().stoped = true;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject == car){
            car = null;
        }
    }
}
