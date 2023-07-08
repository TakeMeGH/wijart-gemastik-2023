using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GoalUIController : MonoBehaviour
{
    public List<goalUI> queueUI;
    public List<Image> fillImage;
    public List<Image> logo;

    public int numShowenImage;
    int showenIdx;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < queueUI.Count; i++){
            Goal curUI = GoalController.Instance.getGoal(queueUI[i].penalty);
            if(curUI.curNumbOfPenalty == curUI.numbOfPenalty){
                for(int j = queueUI.Count - 1; j > i; j--){
                    Debug.Log(i + " " + j);
                    Goal nextUI = GoalController.Instance.getGoal(queueUI[j].penalty);
                    if(nextUI.curNumbOfPenalty != nextUI.numbOfPenalty){
                        goalUI temp = queueUI[i];
                        queueUI[i] = queueUI[j];
                        queueUI[j] = temp;
                        break;
                    }
                }
            }
        }
        for(int i = showenIdx; i < showenIdx + numShowenImage; i++){
            int idx = i - showenIdx;
            int UIIdx = i % queueUI.Count;
            Goal curGoal = GoalController.Instance.getGoal(queueUI[UIIdx].penalty);
            logo[idx].sprite = queueUI[UIIdx].gambarPenalty;
            fillImage[idx].fillAmount = ((float)curGoal.curNumbOfPenalty / (float)curGoal.numbOfPenalty);
            if(curGoal.curNumbOfPenalty == curGoal.numbOfPenalty){
                Color tempColor = logo[idx].color;
                tempColor.a = 0.1f;
                logo[idx].color = tempColor;
            }
            else{
                Color tempColor = logo[idx].color;
                tempColor.a = 1f;
                logo[idx].color = tempColor;
            }
        
        }
    }

    public void nextIdx(){
        showenIdx++;
        showenIdx %= queueUI.Count;
    }

    public void prevIdx(){
        showenIdx--;
        showenIdx = (showenIdx + queueUI.Count) % queueUI.Count;
    }
}


[System.Serializable]
public class goalUI
{
    public Sprite gambarPenalty;
    public CategoryPenalty.Penalty penalty;
}