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
    [Header("Spawned Car")]
    public List<GameObject> curObject;
    public List<bool> isSpecialEvent;

    [Header("Limit Speed")]
    [SerializeField] float lowerBoundSpd;
    [SerializeField] float upperBoundSpd;
    [Header("Special Event")]
    [SerializeField] bool mobilSalip;
    [SerializeField] bool mobilNgebut;
    [SerializeField] bool mobilCepatPadaJalurLambat;
    [Header("Special Event Prefab")]
    [SerializeField] GameObject salipMobil;
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
        if(mobilNgebut) listSpecialEvent.Add(2);
        if(mobilCepatPadaJalurLambat) listSpecialEvent.Add(3);

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
        int randomCar = Random.Range(0, spawnableObject.Count);
        float randomSpeed = Random.Range(lowerBoundSpd, upperBoundSpd);

        if(isSpecialEvent[randomPos]) return;

        if(curObject[randomPos] == null ||
            !CrashCallculator.Instance.isCrash(curObject[randomPos], 
            startPos[randomPos].position, new Vector3(0, 0, randomSpeed), endPos[randomPos].position))
        {
            StartCoroutine(customInstantiateCar(randomPos, randomCar, randomSpeed));

        }
    }
    IEnumerator customInstantiateCar(int posIdx, int carIdx, float speed){
        GameObject spawnedCar = Instantiate(spawnableObject[carIdx], 
            startPos[posIdx].position, Quaternion.identity);

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

        if(randomSpecial == 1){
            int randomPos = Random.Range(0, startPos.Count - 1);

            int flag = 0;
            if(curObject[randomPos] == null ||
            !CrashCallculator.Instance.isCrash(curObject[randomPos], 
            startPos[randomPos].position, new Vector3(0, 0, 25f), endPos[randomPos].position)){
                flag += 1;
            }

            randomPos += 1;
            if(curObject[randomPos] == null ||
                !CrashCallculator.Instance.isCrash(curObject[randomPos], 
                startPos[randomPos].position, new Vector3(0, 0, 25f), endPos[randomPos].position)){
                flag += 2;
            }
            Debug.Log(flag + " " + randomPos);
            if(flag == 3){
                GameObject curEvent = Instantiate(salipMobil, transform);
                SalipMobil curSalipMobilScript = curEvent.GetComponent<SalipMobil>();
                int randomCar = Random.Range(0, spawnableObject.Count);
                int randomCar2 = Random.Range(0, spawnableObject.Count);

                curSalipMobilScript.mobilLeft = spawnableObject[randomCar];
                curSalipMobilScript.mobilRight = spawnableObject[randomCar2];

                curSalipMobilScript.leftPos = startPos[randomPos-1];
                curSalipMobilScript.rightPos = startPos[randomPos];

                curSalipMobilScript.spawnMobil = this;
                curSalipMobilScript.posIdx = randomPos;
                curSalipMobilScript.enabled = true;
            }
            else{
                return;
            }            
        }
        counterStandardCar = Random.Range(minCarCounter, maxCarCounter + 1);

    }
}
