using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevel : MonoBehaviour
{
    [SerializeField] GameObject selectMenu;
    [SerializeField] GameObject mainMenu;
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
    }

    public void activateMainMenu(){
        SoundUI.Instance.playSoundByName("Click");
        selectMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}
