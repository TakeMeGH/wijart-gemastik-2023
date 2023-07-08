using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCaller : MonoBehaviour
{
    SceneHandler sceneHandler;
    // Start is called before the first frame update
    void Start()
    {
        sceneHandler = GameObject.FindGameObjectWithTag("SceneHandler").GetComponent<SceneHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void level1(){
        sceneHandler = GameObject.FindGameObjectWithTag("SceneHandler").GetComponent<SceneHandler>();
        sceneHandler.LoadScene("Level 1");
    }
    public void level2(){
        sceneHandler = GameObject.FindGameObjectWithTag("SceneHandler").GetComponent<SceneHandler>();
        sceneHandler.LoadScene("Level 2");
    }
    public void level3(){
        sceneHandler = GameObject.FindGameObjectWithTag("SceneHandler").GetComponent<SceneHandler>();
        sceneHandler.LoadScene("Level 3");
    }
    public void level4(){
        sceneHandler = GameObject.FindGameObjectWithTag("SceneHandler").GetComponent<SceneHandler>();
        sceneHandler.LoadScene("Level 4");
    }
    public void level5(){
        sceneHandler = GameObject.FindGameObjectWithTag("SceneHandler").GetComponent<SceneHandler>();
        sceneHandler.LoadScene("Level 5");
    }
    public void level6(){
        sceneHandler = GameObject.FindGameObjectWithTag("SceneHandler").GetComponent<SceneHandler>();
        sceneHandler.LoadScene("Level 6");
    }

    public void quit(){
        sceneHandler = GameObject.FindGameObjectWithTag("SceneHandler").GetComponent<SceneHandler>();
        sceneHandler.QuitGame();

    }

    public void mainMenu(){
        Time.timeScale = 1;
        sceneHandler = GameObject.FindGameObjectWithTag("SceneHandler").GetComponent<SceneHandler>();
        sceneHandler.restartMainMenu();
    }

    public void restart(){
        Time.timeScale = 1;
        sceneHandler = GameObject.FindGameObjectWithTag("SceneHandler").GetComponent<SceneHandler>();
        sceneHandler.restartButton();
    }

    public void nextScene(){
        sceneHandler = GameObject.FindGameObjectWithTag("SceneHandler").GetComponent<SceneHandler>();
        sceneHandler.loadNextLevel();
    }
}
