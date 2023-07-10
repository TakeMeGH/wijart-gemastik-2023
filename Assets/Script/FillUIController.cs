using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FillUIController : MonoBehaviour
{
    [SerializeField] Image fillScore;
    [SerializeField] Image fillTimer;
    [SerializeField] EndGame endGame;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fillScore.fillAmount = (float)ScoreController.Instance.currentScore / (float)endGame.threeStar.point;
        fillTimer.fillAmount = 1f - ((float)Timer.Instance.currentTime / (float)endGame.oneStar.timer);
    }
}
