using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevel : MonoBehaviour
{
    [SerializeField] GameObject selectMenu;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject CreditsScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activateSelectMenu(){
        SoundUI.Instance.playSoundByName("Click");
        selectMenu.SetActive(true);
        mainMenu.SetActive(false);
        CreditsScene.SetActive(false);
    }

    public void activateMainMenu(){
        SoundUI.Instance.playSoundByName("Click");
        mainMenu.SetActive(true);
        selectMenu.SetActive(false);
        CreditsScene.SetActive(false);

    }

    public void activateCredits(){
        SoundUI.Instance.playSoundByName("Click");
        CreditsScene.SetActive(true);
        mainMenu.SetActive(false);
        selectMenu.SetActive(false);

    }
}
