using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] GameObject winningFrame;
    [SerializeField] GameObject ingameUI;
    [SerializeField] StarCounter starCounter;

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
            ingameUI.SetActive(false);
            winningFrame.SetActive(true);
            starCounter.thirdCondition = true;
        }    
    }
}
