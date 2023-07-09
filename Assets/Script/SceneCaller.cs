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
        SoundUI.Instance.playSoundByName("Click");
        sceneHandler = GameObject.FindGameObjectWithTag("SceneHandler").GetComponent<SceneHandler>();
        sceneHandler.LoadScene("Level 1");
    }
    public void level2(){
        SoundUI.Instance.playSoundByName("Click");
        sceneHandler = GameObject.FindGameObjectWithTag("SceneHandler").GetComponent<SceneHandler>();
        sceneHandler.LoadScene("Level 2");
    }
    public void level3(){
        SoundUI.Instance.playSoundByName("Click");
        sceneHandler = GameObject.FindGameObjectWithTag("SceneHandler").GetComponent<SceneHandler>();
        sceneHandler.LoadScene("Level 3");
    }
    public void level4(){
        SoundUI.Instance.playSoundByName("Click");
        sceneHandler = GameObject.FindGameObjectWithTag("SceneHandler").GetComponent<SceneHandler>();
        sceneHandler.LoadScene("Level 4");
    }
    public void level5(){
        SoundUI.Instance.playSoundByName("Click");
        sceneHandler = GameObject.FindGameObjectWithTag("SceneHandler").GetComponent<SceneHandler>();
        sceneHandler.LoadScene("Level 5");
    }
    public void level6(){
        SoundUI.Instance.playSoundByName("Click");
        sceneHandler = GameObject.FindGameObjectWithTag("SceneHandler").GetComponent<SceneHandler>();
        sceneHandler.LoadScene("Level 6");
    }

    public void quit(){
        SoundUI.Instance.playSoundByName("Click");
        sceneHandler = GameObject.FindGameObjectWithTag("SceneHandler").GetComponent<SceneHandler>();
        sceneHandler.QuitGame();

    }

    public void mainMenu(){
        SoundUI.Instance.playSoundByName("Click");
        Time.timeScale = 1;
        sceneHandler = GameObject.FindGameObjectWithTag("SceneHandler").GetComponent<SceneHandler>();
        sceneHandler.restartMainMenu();
    }

    public void restart(){        
        SoundUI.Instance.playSoundByName("Click");
        Time.timeScale = 1;
        sceneHandler = GameObject.FindGameObjectWithTag("SceneHandler").GetComponent<SceneHandler>();
        sceneHandler.restartButton();
    }

    public void nextScene(){
        SoundUI.Instance.playSoundByName("Click");
        sceneHandler = GameObject.FindGameObjectWithTag("SceneHandler").GetComponent<SceneHandler>();
        sceneHandler.loadNextLevel();
    }
}
