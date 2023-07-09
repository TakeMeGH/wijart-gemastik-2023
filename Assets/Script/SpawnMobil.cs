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
    public List<GameObject> openCar;

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
    [SerializeField] bool mobilCepat;
    [SerializeField] bool mobilRoofLess;
    [SerializeField] float spawnInterval = 2f;
    public List<bool> isFastTrack;
    [Header("Special Event Prefab")]
    [SerializeField] GameObject listEvent;
    public List<int> listSpecialEvent = new List<int>();
    [Header("Extra spacing to car")]
    [SerializeField] float spawnOffsetY;
    [Header("Interval to Special Event")]
    [SerializeField] int minCarCounter;
    [SerializeField] int maxCarCounter;
    int counterStandardCar = 0;    
    List<int> originalList = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        if(mobilSalip) listSpecialEvent.Add(1);
        if(MobilUgal) listSpecialEvent.Add(2);
        if(mobilBesarPadaJalurCepat) listSpecialEvent.Add(3);
        if(mobilCepat) listSpecialEvent.Add(4);
        if(mobilRoofLess) listSpecialEvent.Add(5);

        originalList = new List<int>(listSpecialEvent);

        for(int i = 0; i < 1; i++){
            listSpecialEvent.AddRange(originalList);
        }

        isSpecialEvent = new List<bool>(new bool[startPos.Count]);
        curObject = new List<GameObject>();
        for(int i = 0; i < startPos.Count; i++){
            curObject.Add(null);
        }

        counterStandardCar = Random.Range(minCarCounter, maxCarCounter + 1);
        InvokeRepeating("spawnRandomCar",0, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        int flag = 0;
        for(int i = 0; i < curObject.Count; i++){
            if(curObject[i] != null){
                flag = 1;
                break;
            }
        }
        if(flag == 0 && GoalController.Instance.isFinished()){
            Destroy(gameObject);
        }
    }

    void spawnRandomCar(){
        if(GoalController.Instance.isFinished()){
            return;
        }
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
            StartCoroutine(customInstantiateCar(randomPos, choosenCar, randomSpeed, isFastTrack[randomPos], false));

        }
    }
    IEnumerator customInstantiateCar(int posIdx, GameObject choosenCar, float speed, bool fastParticle, bool isSpecial, CategoryPenalty.Penalty carPenalty = CategoryPenalty.Penalty.noPenalty){
        GameObject spawnedCar = Instantiate(choosenCar, 
        startPos[posIdx].position, startPos[posIdx].rotation);
        // int luckyNumber = 10;
        // int rndNumber = Random.Range(1, 11);
        if(isSpecial){
            if(fastParticle && spawnedCar.tag != "orang"){
                // spawnedCar.GetComponent<FastParticleController>().enableFastParticle();
                spawnedCar.GetComponent<MobilAudio>().audioCepat();
            }
            else if(spawnedCar.tag != "orang"){
                spawnedCar.GetComponent<MobilAudio>().audioLambat();
            }
        }

        if(fastParticle){
            spawnedCar.GetComponent<FastParticleController>().enableFastParticle();
        }
        
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
        for(int i = listSpecialEvent.Count - 1; i >= 0; i--){
            if(listSpecialEvent[i] == 1){
                Goal curGoal = GoalController.Instance.getGoal(CategoryPenalty.Penalty.Salip);
                if(curGoal.curNumbOfPenalty == curGoal.numbOfPenalty){
                    listSpecialEvent.RemoveAt(i);
                }
            }

            if(listSpecialEvent[i] == 2){
                Goal curGoal = GoalController.Instance.getGoal(CategoryPenalty.Penalty.Ugal);
                if(curGoal.curNumbOfPenalty == curGoal.numbOfPenalty){
                    listSpecialEvent.RemoveAt(i);
                }
            }

            if(listSpecialEvent[i] == 3){
                Goal curGoal = GoalController.Instance.getGoal(CategoryPenalty.Penalty.Lambat);
                if(curGoal.curNumbOfPenalty == curGoal.numbOfPenalty){
                    listSpecialEvent.RemoveAt(i);
                }
            }

            if(listSpecialEvent[i] == 4){
                Goal curGoal = GoalController.Instance.getGoal(CategoryPenalty.Penalty.Cepat);
                if(curGoal.curNumbOfPenalty == curGoal.numbOfPenalty){
                    listSpecialEvent.RemoveAt(i);
                }
            }

            if(listSpecialEvent[i] == 5){
                Goal curGoal = GoalController.Instance.getGoal(CategoryPenalty.Penalty.RoofLess);
                if(curGoal.curNumbOfPenalty == curGoal.numbOfPenalty){
                    listSpecialEvent.RemoveAt(i);
                }
            }
        }
        if(listSpecialEvent.Count == 0){
            listSpecialEvent = new List<int>(originalList);
        }
        int randomSpecialIdx = Random.Range(0, listSpecialEvent.Count);
        int randomSpecial = listSpecialEvent[randomSpecialIdx];

        // if(availEvent.Count >= 1){
        //     randomSpecialIdx = Random.Range(0, availEvent.Count);

        //     randomSpecialIdx = availEvent[randomSpecialIdx];
        //     randomSpecial = listSpecialEvent[randomSpecialIdx];
        // }

        if(randomSpecial == 2){
            int randomPos = Random.Range(0, startPos.Count - 1);
            float randomSpeed = Random.Range(lowerBoundSpd, upperBoundSpd);
            int reverseWay = 1;
            if(isFastTrack[randomPos]){
                randomSpeed += extraSpeed;
            }
            if(startPos[randomPos].transform.localEulerAngles.y == 180){
                reverseWay = -1;
            }
            if(isFastTrack[randomPos]){
                randomSpeed += extraSpeed;
            }
            int flag = 0;
            if(curObject[randomPos] == null ||
            !CrashCallculator.Instance.isCrash(curObject[randomPos], 
            startPos[randomPos].position, new Vector3(0, 0, randomSpeed + 5 * reverseWay), endPos[randomPos].position)){
                flag += 1;
            }

            randomPos += 1;
            if(curObject[randomPos] == null ||
                !CrashCallculator.Instance.isCrash(curObject[randomPos], 
                startPos[randomPos].position, new Vector3(0, 0, randomSpeed + 5 * reverseWay), endPos[randomPos].position)){
                flag += 2;
            }
            if(flag == 3){
                GameObject curEvent = Instantiate(listEvent, transform.position, transform.rotation);
                MobilUgal ugalScript = curEvent.GetComponent<MobilUgal>();
                
                int randomCar = Random.Range(0, spawnableObject.Count);
                ugalScript.carModel = spawnableObject[randomCar];
                ugalScript.isFast = isFastTrack[randomPos];

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
            int reverseWay = 1;
            if(isFastTrack[randomPos]){
                randomSpeed += extraSpeed;
            }
            if(startPos[randomPos].transform.localEulerAngles.y == 180){
                reverseWay = -1;
            }
            int flag = 0;
            if(curObject[randomPos] == null ||
            !CrashCallculator.Instance.isCrash(curObject[randomPos], 
            startPos[randomPos].position, new Vector3(0, 0, randomSpeed + 5 * reverseWay), endPos[randomPos].position)){
                flag += 1;
            }

            randomPos += 1;
            if(curObject[randomPos] == null ||
                !CrashCallculator.Instance.isCrash(curObject[randomPos], 
                startPos[randomPos].position, new Vector3(0, 0, randomSpeed + 5 * reverseWay), endPos[randomPos].position)){
                flag += 2;
            }
            if(flag == 3){
                GameObject curEvent = Instantiate(listEvent, transform);
                SalipMobil curSalipMobilScript = curEvent.GetComponent<SalipMobil>();

                curSalipMobilScript.reverseWay = reverseWay;
                curSalipMobilScript.isFast = isFastTrack[randomPos];
                
                int randomCar = Random.Range(0, spawnableObject.Count);
                int randomCar2 = Random.Range(0, spawnableObject.Count);

                curSalipMobilScript.isReversed = Random.Range(0, 2) == 1;
                curSalipMobilScript.mobilLeft = spawnableObject[randomCar];
                curSalipMobilScript.mobilRight = spawnableObject[randomCar2];

                curSalipMobilScript.leftPos = startPos[randomPos-1];
                curSalipMobilScript.rightPos = startPos[randomPos];

                curSalipMobilScript.slowSpeed = new Vector3(0, 0, randomSpeed);
                curSalipMobilScript.fastSpeed = new Vector3(0, 0, randomSpeed + 15 * reverseWay);

                curSalipMobilScript.spawnMobil = this;
                curSalipMobilScript.posIdx = randomPos;
                curSalipMobilScript.enabled = true;
            }
            else{
                return;
            }
        }
        else if(randomSpecial == 3){
            List<int> availPos = new List<int>();
            for(int i = 0; i < startPos.Count; i++){
                if(isFastTrack[i]){
                    availPos.Add(i);
                }
            }
            int randomPos = Random.Range(0, availPos.Count);
            randomPos = availPos[randomPos];
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
                    choosenCar, randomSpeed, false, true, CategoryPenalty.Penalty.Lambat));
            }
            else{
                return;
            }
        }
        else if(randomSpecial == 4){
            List<int> availPos = new List<int>();
            for(int i = 0; i < startPos.Count; i++){
                if(isFastTrack[i] == false){
                    availPos.Add(i);
                }
            }
            int randomPos = Random.Range(0, availPos.Count);
            randomPos = availPos[randomPos];
            int flag = 0;
            float randomSpeed = Random.Range(lowerBoundSpd, upperBoundSpd);
            randomSpeed += extraSpeed;

            if(curObject[randomPos] == null ||
            !CrashCallculator.Instance.isCrash(curObject[randomPos], 
            startPos[randomPos].position, new Vector3(0, 0, randomSpeed), endPos[randomPos].position)){
                flag += 1;
            }

            if(flag == 1){
                int randomCar = Random.Range(0, spawnableObject.Count);
                GameObject choosenCar = spawnableObject[randomCar];
                StartCoroutine(customInstantiateCar(randomPos, 
                    choosenCar, randomSpeed, true, true, CategoryPenalty.Penalty.Cepat));
            }
            else{
                return;
            }
        }
        else if(randomSpecial == 5){
            int randomPos = Random.Range(0, startPos.Count);
            int flag = 0;
            float randomSpeed = Random.Range(lowerBoundSpd, upperBoundSpd);

            if(isFastTrack[randomPos]){
                randomSpeed += extraSpeed;
            }

            if(curObject[randomPos] == null ||
            !CrashCallculator.Instance.isCrash(curObject[randomPos], 
            startPos[randomPos].position, new Vector3(0, 0, randomSpeed), endPos[randomPos].position)){
                flag += 1;
            }

            if(flag == 1){
                int randomCar = Random.Range(0, openCar.Count);
                GameObject choosenCar = openCar[randomCar];
                StartCoroutine(customInstantiateCar(randomPos, 
                    choosenCar, randomSpeed, isFastTrack[randomPos], true, CategoryPenalty.Penalty.RoofLess));
            }
            else{
                return;
            }
        }
        counterStandardCar = Random.Range(minCarCounter, maxCarCounter + 1);
        listSpecialEvent.RemoveAt(randomSpecialIdx);
        if(listSpecialEvent.Count == 0){
            List<int> availEvent = new List<int>();
            for(int i = 0; i < originalList.Count; i++){
                if(originalList[i] == 1){
                    Goal curGoal = GoalController.Instance.getGoal(CategoryPenalty.Penalty.Salip);
                    if(curGoal.curNumbOfPenalty < curGoal.numbOfPenalty){
                        availEvent.Add(originalList[i]);
                    }
                }

                if(originalList[i] == 2){
                    Goal curGoal = GoalController.Instance.getGoal(CategoryPenalty.Penalty.Ugal);
                    if(curGoal.curNumbOfPenalty < curGoal.numbOfPenalty){
                        availEvent.Add(originalList[i]);
                    }
                }

                if(originalList[i] == 3){
                    Goal curGoal = GoalController.Instance.getGoal(CategoryPenalty.Penalty.Lambat);
                    if(curGoal.curNumbOfPenalty < curGoal.numbOfPenalty){
                        availEvent.Add(originalList[i]);
                    }
                }

                if(originalList[i] == 4){
                    Goal curGoal = GoalController.Instance.getGoal(CategoryPenalty.Penalty.Cepat);
                    if(curGoal.curNumbOfPenalty < curGoal.numbOfPenalty){
                        availEvent.Add(originalList[i]);
                    }
                }

                if(originalList[i] == 5){
                    Goal curGoal = GoalController.Instance.getGoal(CategoryPenalty.Penalty.RoofLess);
                    if(curGoal.curNumbOfPenalty < curGoal.numbOfPenalty){
                        availEvent.Add(originalList[i]);
                    }
                }

            }
            if(availEvent.Count == 0){
                availEvent = new List<int>(originalList);
            }
            for(int i = 0; i < 2; i++){
                listSpecialEvent.AddRange(availEvent);
            }
        }
    }
}
