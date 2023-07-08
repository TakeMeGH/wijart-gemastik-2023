using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] GameObject waveSpawner;
    [SerializeField] TutorialDialogue tutorialDialogue;
    [SerializeField] GameObject inGame;
    bool isStartGame;
    bool tutorialStarted;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(tutorialStarted == true && GoalController.Instance.isFinished()){
            endTutorial();
        }
        if(isStartGame){
            startGame();
        }
    }

    public void startTutorial(){
        waveSpawner.SetActive(true);
        tutorialStarted = true;
    }

    void endTutorial(){
        if(tutorialStarted == true && GoalController.Instance.isFinished()){
            tutorialStarted = false;
            // Destroy(waveSpawner);
            tutorialDialogue.nextDialogue();
        }
    }

    public void startGame(){
        isStartGame = true;
        if(waveSpawner.transform.childCount == 0){
            inGame.SetActive(true);
            gameObject.SetActive(false);
        }
        
    }
}
