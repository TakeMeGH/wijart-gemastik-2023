using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilUgal : MonoBehaviour
{
    [Header("Interval Tilt")]
    [SerializeField] float minTilt;
    [SerializeField] float maxTilt;
    float curTilt;
    public int direction;
    [Header("Interval Turn Time")]
    [SerializeField] float minTurnTime;
    [SerializeField] float maxTurnTime;
    float curTime;
    [Header("Interval Turn Speed")]
    [SerializeField] float minTurnSpeed;
    [SerializeField] float maxTurnSpeed;
    float turnSpeed;
    [Header("Position Bound")]
    public Transform leftBound;
    public Transform rightBound;
    [Header("Transition Time")]
    [SerializeField] float transitionTime;
    float curTransitionTime;
    float originRotationY;
    [Header("Spawn Constraint")]
    public GameObject carModel;
    public GameObject car;
    public float carSpeed;
    [SerializeField] float spawnOffsetY;

    bool firstTime = true;
    [SerializeField] float waitTime;
    [Header("Parrent Condition")]
    public SpawnMobil spawnMobil;
    public int posIdx;
    public bool isFast;


    // Start is called before the first frame update
    void Start()
    {

        float percentage = Random.Range(0.1f, 1f);
        curTime = Random.Range(minTurnTime, maxTurnTime);

        turnSpeed = (maxTurnSpeed - minTurnSpeed + 1) * percentage + minTurnSpeed;
        curTilt = (maxTilt - minTilt + 1) * percentage + minTilt;

        turnSpeed *= direction;
        if(leftBound.localEulerAngles.y == 0) curTilt *= direction;
        else if(leftBound.localEulerAngles.y == 180) curTilt *= direction * -1;

        curTilt += leftBound.localEulerAngles.y;
        if(direction == 1) car = Instantiate(carModel, leftBound.position, leftBound.rotation);
        else car = Instantiate(carModel, rightBound.position, rightBound.rotation);

        if(isFast){
            car.GetComponent<FastParticleController>().enableFastParticle();
        }
        car.transform.rotation = Quaternion.Euler(0, curTilt, 0);
        car.GetComponent<Movement>().speed = new Vector3(0, 0 , 0);
        originRotationY = car.transform.localEulerAngles.y;
        spawnMobil.curObject[posIdx] = car;
        spawnMobil.curObject[posIdx-1] = car;
        car.GetComponent<CategoryPenalty>().curObjectPenalty = CategoryPenalty.Penalty.Ugal;

    }

    // Update is called once per frame
    void Update()
    {
        if(car == null) Destroy(gameObject);
        waitTime -= Time.deltaTime;
        if(waitTime > 0) return;
        if(firstTime){
            car.transform.position += new Vector3(0, spawnOffsetY, 0);
            car.GetComponent<Movement>().speed = new Vector3(turnSpeed, 0 , carSpeed);
            car.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;

            firstTime = false;
        }
        curTime -= Time.deltaTime;
        if(car != null && direction == -1 && car.transform.position.x <= leftBound.position.x){
            curTime = 0;
        }
        else if(car != null && direction == 1 && car.transform.position.x >= rightBound.position.x){
            curTime = 0;
        }
        else if(car == null){
            Destroy(gameObject);
        }
        curTransitionTime += Time.deltaTime;
        curTransitionTime = Mathf.Min(curTransitionTime, transitionTime);
        float degree = Mathf.Lerp(originRotationY, curTilt, curTransitionTime / transitionTime);
        car.transform.rotation = Quaternion.Euler(0, degree, 0);

        if(curTime <= 0){
            direction *= -1;
            float percentage = Random.Range(0.1f, 1f);
            curTime = Random.Range(minTurnTime, maxTurnTime);

            turnSpeed = (maxTurnSpeed - minTurnSpeed + 1) * percentage + minTurnSpeed;
            curTilt = (maxTilt - minTilt + 1) * percentage + minTilt;

            turnSpeed *= direction;
            if(leftBound.localEulerAngles.y == 0) curTilt *= direction;
            else if(leftBound.localEulerAngles.y == 180) curTilt *= direction * -1;
            curTilt += leftBound.localEulerAngles.y;
            if(direction == 1 && leftBound.localEulerAngles.y == 0){
                curTilt += 360;
            }

            // car.transform.rotation = Quaternion.Euler(0, curTilt, 0);
            car.GetComponent<Movement>().speed = new Vector3(turnSpeed, 0, carSpeed);
            originRotationY = car.transform.localEulerAngles.y;
            curTransitionTime = 0;

        }
    }
}
