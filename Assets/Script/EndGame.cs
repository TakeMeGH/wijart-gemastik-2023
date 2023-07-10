using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    bool isCalled = false;
    

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
            if(isCalled) return;
            isCalled = true;
            string levelName = SceneManager.GetActiveScene().name;
            winningConditionSound.playWinning();
            ingameUI.SetActive(false);
            winningFrame.SetActive(true);
            if(Timer.Instance.currentTime <= threeStar.timer && ScoreController.Instance.currentScore >= threeStar.point){
                starCounter.thirdCondition = true;
                SaveSystem.Instance.saveBintang(levelName, 3);
            }
            else if(Timer.Instance.currentTime <= twoStar.timer && ScoreController.Instance.currentScore >= twoStar.point){
                starCounter.secondCondition = true;
                SaveSystem.Instance.saveBintang(levelName, 2);

            }
            else if(Timer.Instance.currentTime <= oneStar.timer && ScoreController.Instance.currentScore >= oneStar.point){
                starCounter.firstCondition = true;
                SaveSystem.Instance.saveBintang(levelName, 1);

            }
            else{
                starCounter.conditionZero = true;
                SaveSystem.Instance.saveBintang(levelName, 0);
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