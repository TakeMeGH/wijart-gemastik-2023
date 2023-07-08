using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimerUIController : MonoBehaviour
{
    [SerializeField] Text text;
    public bool onlyFirstTime;
    // Start is called before the first frame update
    void Start()
    {
        if(onlyFirstTime){
            int minute = (int)Timer.Instance.currentTime / 60;
            int second = (int)Timer.Instance.currentTime % 60;
            string firstString = minute.ToString();
            if(minute < 10){
                firstString = "0" + firstString;
            }
            string lastString = second.ToString();
            if(second < 10){
                lastString = "0" + lastString;
            }
            text.text = firstString + ":" + lastString;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(onlyFirstTime) return;
        int minute = (int)Timer.Instance.currentTime / 60;
        int second = (int)Timer.Instance.currentTime % 60;
        string firstString = minute.ToString();
        if(minute < 10){
            firstString = "0" + firstString;
        }
        string lastString = second.ToString();
        if(second < 10){
            lastString = "0" + lastString;
        }
        text.text = firstString + ":" + lastString;
    }
}
