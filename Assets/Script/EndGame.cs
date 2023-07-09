using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] GameObject winningFrame;
    [SerializeField] GameObject ingameUI;
    [SerializeField] StarCounter starCounter;
    [SerializeField] Condition threeStar;
    [SerializeField] Condition twoStar;
    [SerializeField] Condition oneStar;
    [SerializeField] WinningConditionSound winningConditionSound;
    float waitTime = 2f;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LateUpdate() {
        if(GoalController.Instance.isFinished()){
            waitTime -= Time.deltaTime;
            if(waitTime > 0) return;
            winningConditionSound.playWinning();
            ingameUI.SetActive(false);
            winningFrame.SetActive(true);
            if(Timer.Instance.currentTime <= threeStar.timer && ScoreController.Instance.currentScore >= threeStar.point){
                starCounter.thirdCondition = true;
            }
            else if(Timer.Instance.currentTime <= twoStar.timer && ScoreController.Instance.currentScore >= twoStar.point){
                starCounter.secondCondition = true;
            }
            else if(Timer.Instance.currentTime <= oneStar.timer && ScoreController.Instance.currentScore >= oneStar.point){
                starCounter.firstCondition = true;
            }
            else{
                starCounter.conditionZero = true;
            }
        }    
    }
}

[System.Serializable]
public class Condition
{
    public float timer;
    public float point;
}