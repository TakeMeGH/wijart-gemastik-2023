using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeretaController : MonoBehaviour
{
    [Header("Kereta Spawner")]
    public Vector3 keretaSpeed;
    public Transform keretaTransform;
    public GameObject keretaModel;
    public GameObject spawnedKereta;
    bool isReseted;
    [Header("Environment")]
    public List<GameObject> lampuLaluLintas;
    public List<CarAround> antrianMobil;
    [Header("Time Interval")]
    // wait time -> invisMustWaitTime -> alert time (spawn time) -> spawn kereta
    public float waitTime;
    public float invisMustWaitTime;
    public float alertTime;
    public float spawnPenaltyTime;
    public float penaltyInterval;
    float lastPenalty;
    float curTime;
    bool isSpawned;
    bool isPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        lastPenalty = penaltyInterval;
        isSpawned = false;
        isReseted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isReseted == false && spawnedKereta == null){
            isReseted = true;
            curTime = 0;
        }
        else{
            curTime += Time.deltaTime;
            if( GoalController.Instance.isFinished() || curTime <= waitTime){
                isPlaying = false;
                for(int i = 0; i < lampuLaluLintas.Count; i++){
                    lampuLaluLintas[i].GetComponent<Animator>().Play("idle");
                    lampuLaluLintas[i].GetComponent<AudioSource>().Stop();
                }
                for(int i = 0; i < antrianMobil.Count; i++){
                    antrianMobil[i].isNextMustStop = false;
                    if(antrianMobil[i].car != null){
                        antrianMobil[i].car.GetComponent<Movement>().stoped = false;
                    }
                }
            }
            else if(curTime <= waitTime + invisMustWaitTime){
                for(int i = 0; i < antrianMobil.Count; i++){
                    antrianMobil[i].isNextMustStop = true;
                }
            }
            else if(curTime <= waitTime + invisMustWaitTime + alertTime){
                if(isPlaying == false){
                    isPlaying = true;
                    for(int i = 0; i < lampuLaluLintas.Count; i++){
                        lampuLaluLintas[i].GetComponent<Animator>().Play("alert");
                        lampuLaluLintas[i].GetComponent<AudioSource>().Play();
                    }
                }
               
                lastPenalty -= Time.deltaTime;
                if(curTime <= waitTime + invisMustWaitTime + spawnPenaltyTime && lastPenalty <= 0){
                    int idxSkip = Random.Range(0, antrianMobil.Count);
                    GameObject curCar = antrianMobil[idxSkip].car;
                    if(curCar != null){
                        if(curCar.GetComponent<CategoryPenalty>().curObjectPenalty == CategoryPenalty.Penalty.noPenalty){
                            curCar.GetComponent<Movement>().stoped = false;
                            curCar.GetComponent<CategoryPenalty>().curObjectPenalty = CategoryPenalty.Penalty.Terobos;
                            curCar.GetComponent<CarDetector>().makeBehindMove();
                            antrianMobil[idxSkip].isNextMustStop = true;
                            lastPenalty = penaltyInterval;
                        }
                    }
                }
            }
            else{
                if(isSpawned == false){
                    spawnedKereta = Instantiate(keretaModel, keretaTransform.position, keretaTransform.rotation);
                    spawnedKereta.GetComponent<Movement>().speed = keretaSpeed;
                    isSpawned = true;
                }
                else{
                    if(spawnedKereta == null){
                        isSpawned = false;
                        curTime = 0;
                        isReseted = false;
                    }
                }
            }
        }
    }
}
