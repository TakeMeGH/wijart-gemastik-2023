using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMobil : MonoBehaviour
{
    [Header("Position")]
    [SerializeField] List<Transform> startPos;
    [SerializeField] List<Transform> endPos;
    [Header("Car Selection")]
    public List<GameObject> spawnableObject;
    public List<GameObject> smallCar;
    public List<GameObject> bigCar;
    [Header("Spawned Car")]
    public List<GameObject> curObject;
    public List<bool> isSpecialEvent;

    [Header("Limit Speed")]
    [SerializeField] float lowerBoundSpd;
    [SerializeField] float upperBoundSpd;
    [SerializeField] float extraSpeed;
    [Header("Special Event")]
    [SerializeField] bool mobilSalip;
    [SerializeField] bool MobilUgal;
    [SerializeField] bool mobilBesarPadaJalurCepat;
    public List<bool> isFastTrack;
    [Header("Special Event Prefab")]
    [SerializeField] GameObject listEvent;
    List<int> listSpecialEvent = new List<int>();
    [Header("Extra spacing to car")]
    [SerializeField] float spawnOffsetY;
    [Header("Interval to Special Event")]
    [SerializeField] int minCarCounter;
    [SerializeField] int maxCarCounter;
    int counterStandardCar = 0;    
    // Start is called before the first frame update
    void Start()
    {
        if(mobilSalip) listSpecialEvent.Add(1);
        if(MobilUgal) listSpecialEvent.Add(2);
        if(mobilBesarPadaJalurCepat) listSpecialEvent.Add(3);

        isSpecialEvent = new List<bool>(new bool[startPos.Count]);
        curObject = new List<GameObject>();
        for(int i = 0; i < startPos.Count; i++){
            curObject.Add(null);
        }

        counterStandardCar = Random.Range(minCarCounter, maxCarCounter + 1);
        InvokeRepeating("spawnRandomCar",0, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnRandomCar(){
        if(counterStandardCar == 0){
            spawnSpecialCar();
            return;
        }
        counterStandardCar--;
        int randomPos = Random.Range(0, startPos.Count);
        if(isSpecialEvent[randomPos]) return;

        int randomCar = Random.Range(0, spawnableObject.Count);
        GameObject choosenCar = spawnableObject[randomCar];
        float randomSpeed = Random.Range(lowerBoundSpd, upperBoundSpd);

        if(isFastTrack[randomPos]){
            randomCar = Random.Range(0, smallCar.Count);
            choosenCar = smallCar[randomCar];
            randomSpeed += extraSpeed;
        }


        if(curObject[randomPos] == null ||
            !CrashCallculator.Instance.isCrash(curObject[randomPos], 
            startPos[randomPos].position, new Vector3(0, 0, randomSpeed), endPos[randomPos].position))
        {
            StartCoroutine(customInstantiateCar(randomPos, choosenCar, randomSpeed));

        }
    }
    IEnumerator customInstantiateCar(int posIdx, GameObject choosenCar, float speed, CategoryPenalty.Penalty carPenalty = CategoryPenalty.Penalty.noPenalty){
        GameObject spawnedCar = Instantiate(choosenCar, 
            startPos[posIdx].position, Quaternion.identity);

        spawnedCar.GetComponent<CategoryPenalty>().curObjectPenalty = carPenalty;
        spawnedCar.GetComponent<Movement>().speed.z = speed;
        curObject[posIdx] = spawnedCar;
        yield return new WaitForSeconds(1f);
        spawnedCar.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        spawnedCar.transform.position += new Vector3(0, spawnOffsetY, 0);
    }

    void spawnSpecialCar(){
        if(listSpecialEvent.Count == 0){
            counterStandardCar = Random.Range(minCarCounter, maxCarCounter + 1);
            return;
        }
        int randomSpecialIdx = Random.Range(0, listSpecialEvent.Count);

        int randomSpecial = listSpecialEvent[randomSpecialIdx];

        if(randomSpecial == 2){
            int randomPos = Random.Range(0, startPos.Count - 1);
            float randomSpeed = Random.Range(lowerBoundSpd, upperBoundSpd);
            int flag = 0;
            if(curObject[randomPos] == null ||
            !CrashCallculator.Instance.isCrash(curObject[randomPos], 
            startPos[randomPos].position, new Vector3(0, 0, randomSpeed + 5), endPos[randomPos].position)){
                flag += 1;
            }

            randomPos += 1;
            if(curObject[randomPos] == null ||
                !CrashCallculator.Instance.isCrash(curObject[randomPos], 
                startPos[randomPos].position, new Vector3(0, 0, randomSpeed + 5), endPos[randomPos].position)){
                flag += 2;
            }
            if(flag == 3){
                GameObject curEvent = Instantiate(listEvent, transform);
                MobilUgal ugalScript = curEvent.GetComponent<MobilUgal>();
                
                int randomCar = Random.Range(0, spawnableObject.Count);
                ugalScript.carModel = spawnableObject[randomCar];

                int dir = Random.Range(0, 2);
                if(dir == 1){
                    ugalScript.direction = 1;
                }
                else{
                    ugalScript.direction = -1;
                }
                ugalScript.carSpeed = randomSpeed;
                ugalScript.leftBound = startPos[randomPos-1];
                ugalScript.rightBound = startPos[randomPos];  

                ugalScript.spawnMobil = this;
                ugalScript.posIdx = randomPos;
                ugalScript.enabled = true;
            }
            else{
                return;
            }            
        }
        else if(randomSpecial == 1){

            int randomPos = Random.Range(0, startPos.Count - 1);
            float randomSpeed = Random.Range(lowerBoundSpd, upperBoundSpd);

            int flag = 0;
            if(curObject[randomPos] == null ||
            !CrashCallculator.Instance.isCrash(curObject[randomPos], 
            startPos[randomPos].position, new Vector3(0, 0, randomSpeed + 5), endPos[randomPos].position)){
                flag += 1;
            }

            randomPos += 1;
            if(curObject[randomPos] == null ||
                !CrashCallculator.Instance.isCrash(curObject[randomPos], 
                startPos[randomPos].position, new Vector3(0, 0, randomSpeed + 5), endPos[randomPos].position)){
                flag += 2;
            }
            if(flag == 3){
                GameObject curEvent = Instantiate(listEvent, transform);
                SalipMobil curSalipMobilScript = curEvent.GetComponent<SalipMobil>();
                int randomCar = Random.Range(0, spawnableObject.Count);
                int randomCar2 = Random.Range(0, spawnableObject.Count);

                curSalipMobilScript.isReversed = Random.Range(0, 2) == 1;
                curSalipMobilScript.mobilLeft = spawnableObject[randomCar];
                curSalipMobilScript.mobilRight = spawnableObject[randomCar2];

                curSalipMobilScript.leftPos = startPos[randomPos-1];
                curSalipMobilScript.rightPos = startPos[randomPos];

                curSalipMobilScript.slowSpeed = new Vector3(0, 0, randomSpeed);
                curSalipMobilScript.fastSpeed = new Vector3(0, 0, randomSpeed + 15);

                curSalipMobilScript.spawnMobil = this;
                curSalipMobilScript.posIdx = randomPos;
                curSalipMobilScript.enabled = true;
            }
            else{
                return;
            }
        }
        else if(randomSpecial == 3){
            int randomPos = Random.Range(0, startPos.Count);
            int flag = 0;
            float randomSpeed = Random.Range(lowerBoundSpd, upperBoundSpd);

            if(curObject[randomPos] == null ||
            !CrashCallculator.Instance.isCrash(curObject[randomPos], 
            startPos[randomPos].position, new Vector3(0, 0, randomSpeed), endPos[randomPos].position)){
                flag += 1;
            }

            if(flag == 1){
                int randomCar = Random.Range(0, bigCar.Count);
                GameObject choosenCar = bigCar[randomCar];
                StartCoroutine(customInstantiateCar(randomPos, 
                    choosenCar, randomSpeed, CategoryPenalty.Penalty.Lambat));
            }
            else{
                return;
            }
        }
        counterStandardCar = Random.Range(minCarCounter, maxCarCounter + 1);

    }
}
