using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalipMobil : MonoBehaviour
{
    [SerializeField] Transform leftPos;
    [SerializeField] Transform rightPos;
    [SerializeField] GameObject mobilLeft;
    [SerializeField] GameObject mobilRight;
    public Vector3 slowSpeed;
    public Vector3 fastSpeed;
    public float takeOverSpeed;
    GameObject fastMobil;
    GameObject slowMobil;
    [SerializeField] float waitTime;
    bool firstTime = true;
    [SerializeField] float spawnOffsetY;
    // Start is called before the first frame update
    void Start()
    {
        slowMobil = Instantiate(mobilLeft, leftPos.position, leftPos.rotation);
        fastMobil = Instantiate(mobilRight, rightPos.position, rightPos.rotation);
        slowMobil.GetComponent<Movement>().speed = Vector3.zero;
        fastMobil.GetComponent<Movement>().speed = Vector3.zero;
    }

    void FixedUpdate() {
        waitTime -= Time.deltaTime;
        if(waitTime > 0) return;
        if(firstTime){
            slowMobil.transform.position += new Vector3(0, spawnOffsetY, 0);
            fastMobil.transform.position += new Vector3(0, spawnOffsetY, 0);
            slowMobil.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            fastMobil.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;

            slowMobil.GetComponent<Movement>().speed = slowSpeed;
            fastMobil.GetComponent<Movement>().speed = fastSpeed;
            firstTime = false;
        }
        if(fastMobil.transform.position.z >= slowMobil.transform.position.z){
            if(fastMobil.transform.position.x >= slowMobil.transform.position.x){
                fastMobil.transform.rotation = Quaternion.Euler(0, -25, 0);
                fastMobil.GetComponent<Movement>().speed = new Vector3(takeOverSpeed, 0, fastSpeed.z);
            }
            else{
                fastMobil.transform.rotation = Quaternion.Euler(0, 0, 0);
                fastMobil.GetComponent<Movement>().speed = slowSpeed;
            }
        }   
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
