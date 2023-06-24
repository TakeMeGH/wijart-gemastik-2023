using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalipMobil : MonoBehaviour
{
    [Header("Vehicle Position")]
    public Transform leftPos;
    public Transform rightPos;
    [Header("Vehicle Type")]
    public GameObject mobilLeft;
    public GameObject mobilRight;
    // [Header("Vehicle Ahead")]
    // public GameObject leftAhead;
    // public GameObject rightAhead;
    [Header("Parrent Condition")]
    public SpawnMobil spawnMobil;
    public int posIdx;
    public Vector3 slowSpeed;
    public Vector3 fastSpeed;
    public float takeOverSpeed;
    GameObject fastMobil;
    GameObject slowMobil;
    [SerializeField] float waitTime;
    [SerializeField] float stableTime;
    bool firstTime = true;
    [SerializeField] float spawnOffsetY;
    // Start is called before the first frame update
    void Start()
    {
        slowMobil = Instantiate(mobilLeft, leftPos.position, leftPos.rotation);
        fastMobil = Instantiate(mobilRight, rightPos.position, rightPos.rotation);
        slowMobil.GetComponent<Movement>().speed = Vector3.zero;
        fastMobil.GetComponent<Movement>().speed = Vector3.zero;

        spawnMobil.curObject[posIdx] = fastMobil;
        spawnMobil.curObject[posIdx-1] = slowMobil;
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
            fastMobil.GetComponent<Movement>().speed = new Vector3(0, 0, slowSpeed.z - 5f);
            firstTime = false;
        }
        else if(stableTime > 0){
            stableTime -= Time.deltaTime;
        }
        else if(stableTime <= 0 && fastMobil.transform.position.z < slowMobil.transform.position.z + 5){
            fastMobil.GetComponent<Movement>().speed = fastSpeed;
        }
        else if(fastMobil.transform.position.z >= slowMobil.transform.position.z + 5){
            if(fastMobil.transform.position.x >= slowMobil.transform.position.x){
                fastMobil.GetComponent<CategoryPenalty>().curObjectPenalty = CategoryPenalty.Penalty.Ugal;
                fastMobil.transform.rotation = Quaternion.Euler(0, -25, 0);
                fastMobil.GetComponent<Movement>().speed = new Vector3(takeOverSpeed, 0, fastSpeed.z);
            }
            else{
                fastMobil.transform.rotation = Quaternion.Euler(0, 0, 0);
                fastMobil.GetComponent<Movement>().speed = slowSpeed;
                Destroy(this.gameObject);
            }
        }   
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
