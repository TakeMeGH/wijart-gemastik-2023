using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashCallculator : MonoBehaviour
{

    public static CrashCallculator Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) { 
            Destroy(this); 
        } 
        else { 
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
        
    }

    public bool isCrash(Vector3 pos, Vector3 speed, Vector3 otherPos, Vector3 otherSpd, Vector3 endPos, float tol = 10f){
        if(speed.z < 0){
            tol *= -1;
        }
        if(speed.z <= 0 && otherSpd.z > speed.z && otherPos.z > pos.z) return false;
        if(speed.z <= 0 && otherSpd.z < speed.z && otherPos.z < pos.z) return false;
        if(speed.z >= 0 && otherSpd.z < speed.z && otherPos.z < pos.z) return false;
        if(speed.z >= 0 && otherSpd.z > speed.z && otherPos.z > pos.z) return false;

        float destination = pos.z - tol;

        float difPos = destination - otherPos.z;
        float difSpd = otherSpd.z - speed.z;

        float timeNeeded = difPos / difSpd;

        float crashPos = otherPos.z + timeNeeded * otherSpd.z;

        if(speed.z >= 0 && crashPos < endPos.z) return true;
        else if(speed.z <= 0 && crashPos > endPos.z) return true;
        else{
            return false;
        }
    }

    public bool isCrash(GameObject curVehicle, GameObject otherVehicle, Vector3 endPos, float tol = 10f){
        Vector3 pos = curVehicle.transform.position;
        Vector3 speed = curVehicle.GetComponent<Movement>().speed;

        Vector3 otherPos = otherVehicle.transform.position;
        Vector3 otherSpd = otherVehicle.GetComponent<Movement>().speed;
        if(speed.z < 0){
            tol *= -1;
        }
        if(speed.z <= 0 && otherSpd.z > speed.z && otherPos.z > pos.z) return false;
        if(speed.z <= 0 && otherSpd.z < speed.z && otherPos.z < pos.z) return false;
        if(speed.z >= 0 && otherSpd.z < speed.z && otherPos.z < pos.z) return false;
        if(speed.z >= 0 && otherSpd.z > speed.z && otherPos.z > pos.z) return false;

        float destination = pos.z - tol;

        float difPos = destination - otherPos.z;
        float difSpd = otherSpd.z - speed.z;

        float timeNeeded = difPos / difSpd;

        float crashPos = otherPos.z + timeNeeded * otherSpd.z;

        if(speed.z >= 0 && crashPos < endPos.z) return true;
        else if(speed.z <= 0 && crashPos > endPos.z) return true;
        else{
            return false;
        }
    }

    public bool isCrash(GameObject curVehicle, Vector3 otherPos, Vector3 otherSpd, Vector3 endPos, float tol = 15f){
        
        
        Vector3 pos = curVehicle.transform.position;
        Vector3 speed = curVehicle.GetComponent<Movement>().speed;
        if(speed.z < 0){
            tol *= -1;
        }
        if(speed.z <= 0 && otherSpd.z > speed.z && otherPos.z > pos.z) return false;
        if(speed.z <= 0 && otherSpd.z < speed.z && otherPos.z < pos.z) return false;
        if(speed.z >= 0 && otherSpd.z < speed.z && otherPos.z < pos.z) return false;
        if(speed.z >= 0 && otherSpd.z > speed.z && otherPos.z > pos.z) return false;

        float destination = pos.z - tol;

        float difPos = destination - otherPos.z;
        float difSpd = otherSpd.z - speed.z;

        float timeNeeded = difPos / difSpd;

        float crashPos = otherPos.z + timeNeeded * otherSpd.z;

        if(speed.z >= 0 && crashPos < endPos.z) return true;
        else if(speed.z <= 0 && crashPos > endPos.z) return true;
        else{
            return false;
        }
    }
}
