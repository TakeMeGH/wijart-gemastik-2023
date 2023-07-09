using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDetector : MonoBehaviour
{
    private int _myLayerMask = 1 << 6;
    private RaycastHit longHit;
    private RaycastHit _hit;
    Vector3 speed; 
    Movement curMovement;
    bool everZero;

    // Start is called before the first frame update
    void Start()
    {
        curMovement = GetComponent<Movement>();
        everZero = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 curPosition = transform.position;
        Vector3 direction = Vector3.forward;
        if (Physics.Raycast(curPosition, direction, out longHit, 20f, _myLayerMask)){
            Movement otherLongMovement = longHit.transform.gameObject.GetComponent<Movement>();
            CategoryPenalty.Penalty otherPenalty = longHit.transform.gameObject.GetComponent<CategoryPenalty>().curObjectPenalty;
            if(otherPenalty != CategoryPenalty.Penalty.Terobos && Physics.Raycast(curPosition, direction, out _hit, 15f, _myLayerMask)){
                if (_hit.transform.gameObject.tag == "mobil"){
                    Movement otherMovement = _hit.transform.gameObject.GetComponent<Movement>();
                    if(otherMovement.stoped == true){
                        curMovement.stoped = true;
                        everZero = true;
                    }
                    else if(everZero){
                        curMovement.stoped = otherMovement.stoped;
                        curMovement.speed = otherMovement.speed;
                    }
                }
                
            }
            else if(everZero && otherPenalty != CategoryPenalty.Penalty.Terobos){
                curMovement.stoped = otherLongMovement.stoped;
                curMovement.speed = otherLongMovement.speed;
            }
        }    
    }

    public void makeBehindMove(){
        Vector3 curPosition = transform.position;
        Vector3 direction = Vector3.back;
        if (Physics.Raycast(curPosition, direction, out longHit, 30f, _myLayerMask)){
            if(longHit.transform.gameObject.tag == "mobil"){
                longHit.transform.gameObject.GetComponent<Movement>().stoped = false;
            }
        }
    }
}
