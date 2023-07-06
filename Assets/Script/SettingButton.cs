using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingButton : MonoBehaviour
{
    [SerializeField] GameObject ingameUI;
    [SerializeField] GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pauseGame(){
        Time.timeScale = 0;
        ingameUI.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void unpauseGame(){
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        ingameUI.SetActive(true);
    }
}
