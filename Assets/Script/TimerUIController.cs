using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimerUIController : MonoBehaviour
{
    [SerializeField] Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int minute = (int)Timer.Instance.currentTime / 60;
        int second = (int)Timer.Instance.currentTime % 60;
        text.text = minute.ToString() + ":" + second.ToString();
    }
}
