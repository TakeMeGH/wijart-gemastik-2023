using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalipMobil : MonoBehaviour
{
    [SerializeField] Transform leftPos;
    [SerializeField] Transform rightPos;
    [SerializeField] GameObject mobil;
    public Vector3 slowSpeed;
    public Vector3 fastSpeed;
    public float takeOverSpeed;
    GameObject fastMobil;
    GameObject slowMobil;
    [SerializeField] float waitTime;
    bool firstTime = true;
    // Start is called before the first frame update
    void Start()
    {
        slowMobil = Instantiate(mobil, leftPos.position, leftPos.rotation);
        fastMobil = Instantiate(mobil, rightPos.position, rightPos.rotation);
        slowMobil.GetComponent<Movement>().speed = Vector3.zero;
        fastMobil.GetComponent<Movement>().speed = Vector3.zero;
    }

    void FixedUpdate() {
        waitTime -= Time.deltaTime;
        if(waitTime > 0) return;
        if(firstTime){
            slowMobil.GetComponent<Movement>().speed = slowSpeed;
            fastMobil.GetComponent<Movement>().speed = fastSpeed;
            firstTime = false;
        }
        if(fastMobil.transform.position.z >= slowMobil.transform.position.z){
            if(fastMobil.transform.position.x >= slowMobil.transform.position.x){
                fastMobil.transform.rotation = Quaternion.Euler(-90, 0, -25);
                fastMobil.GetComponent<Movement>().speed = new Vector3(takeOverSpeed, 0, fastSpeed.z);
            }
            else{
                fastMobil.transform.rotation = Quaternion.Euler(-90, 0, 0);
                fastMobil.GetComponent<Movement>().speed = slowSpeed;
            }
        }   
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
