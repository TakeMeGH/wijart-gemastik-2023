using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{

    public List<Goal> goal = new List<Goal>();
    public bool goalFinish = false;
    public static GoalController Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) { 
            Destroy(this); 
        } 
        else { 
            Instance = this; 
        } 
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int clickCar(CategoryPenalty.Penalty penalty){
        for(int i = 0; i < goal.Count; i++){
            if(penalty == goal[i].penaltyCategory){
                if(goal[i].curNumbOfPenalty == goal[i].numbOfPenalty){
                    return -1;
                }
                else{
                    goal[i].curNumbOfPenalty++;
                    return 1;
                }
            }
        }
        return 0;
    }

    public int setPenalty(CategoryPenalty.Penalty penalty, int value){
        for(int i = 0; i < goal.Count; i++){
            if(penalty == goal[i].penaltyCategory){
                goal[i].curNumbOfPenalty = 0;
                goal[i].numbOfPenalty = value;
            }
        }
        return 0;
    }

    public bool isFinished(){
        for(int i = 0; i < goal.Count; i++){
            if(goal[i].numbOfPenalty != goal[i].curNumbOfPenalty){
                return false;
            }
        }
        return true;
    }
}


[System.Serializable]
public class Goal
{
    [TextAreaAttribute]
    public string goalText;
    public CategoryPenalty.Penalty penaltyCategory;
    public int numbOfPenalty;
    public int curNumbOfPenalty;
}