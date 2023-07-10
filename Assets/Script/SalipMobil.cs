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
    public bool isReversed;
    public int reverseWay = 1;
    [SerializeField] float rotationYVal = -25;
    public bool isFast;
    // Start is called before the first frame update
    void Start()
    {  
        slowMobil = Instantiate(mobilLeft, leftPos.position, leftPos.rotation);
        fastMobil = Instantiate(mobilRight, rightPos.position, rightPos.rotation);
        if(isFast){
            slowMobil.GetComponent<FastParticleController>().enableFastParticle();
            fastMobil.GetComponent<FastParticleController>().enableFastParticle();

            // fastMobil.GetComponent<MobilAudio>().audioCepat();
        }
        if(spawnMobil.soundCar == null){
            if(isFast){
                slowMobil.GetComponent<MobilAudio>().audioCepat();

            }
            else{
                slowMobil.GetComponent<MobilAudio>().audioLambat();
            }
            spawnMobil.soundCar = slowMobil;
        }
        // else{
        //     // fastMobil.GetComponent<MobilAudio>().audioLambat();
        // }
        if(isReversed){
            GameObject temp = fastMobil;
            fastMobil = slowMobil;
            slowMobil = temp;
            takeOverSpeed *= -1;
            rotationYVal *= -1f;

        }

        slowMobil.GetComponent<Movement>().speed = Vector3.zero;
        fastMobil.GetComponent<Movement>().speed = Vector3.zero;

        spawnMobil.curObject[posIdx] = fastMobil;
        spawnMobil.curObject[posIdx-1] = slowMobil;
    }
    bool isNotIntercepted(){
        if(reverseWay == -1){
            return fastMobil.transform.position.z > slowMobil.transform.position.z - 5;
        }
        return fastMobil.transform.position.z < slowMobil.transform.position.z + 5;
    }

    bool isNotPindahLajur(){
        if(isReversed) return fastMobil.transform.position.x <= slowMobil.transform.position.x;
        return (fastMobil.transform.position.x >= slowMobil.transform.position.x);
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
            fastMobil.GetComponent<Movement>().speed = new Vector3(0, 0, slowSpeed.z - 5f * reverseWay);
            firstTime = false;
        }
        else if(stableTime > 0){
            stableTime -= Time.deltaTime;
        }
        else if(stableTime <= 0 && isNotIntercepted()){
            fastMobil.GetComponent<Movement>().speed = fastSpeed;
        }
        else if(!isNotIntercepted()){
            if(isNotPindahLajur()){
                fastMobil.GetComponent<CategoryPenalty>().curObjectPenalty = CategoryPenalty.Penalty.Salip;
                fastMobil.transform.rotation = Quaternion.Euler(0, leftPos.localEulerAngles.y + rotationYVal, 0);
                fastMobil.GetComponent<Movement>().speed = new Vector3(takeOverSpeed, 0, fastSpeed.z);
            }
            else{
                fastMobil.transform.rotation = Quaternion.Euler(0, leftPos.localEulerAngles.y, 0);
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
