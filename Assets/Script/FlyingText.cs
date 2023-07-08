using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FlyingText : MonoBehaviour
{
    [SerializeField] RectTransform finishPos;
    [SerializeField] float finishTime;
    [SerializeField] float curTime;
    public int value;
    Vector2 originPosition;
    Vector2 finishPosition;
    RectTransform curRect;

    // Start is called before the first frame update
    void Start()
    {
        originPosition = GetComponent<RectTransform>().anchoredPosition;
        finishPosition = finishPos.anchoredPosition;
        curRect = GetComponent<RectTransform>();
        curTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(curTime >= finishTime){
            ScoreController.Instance.currentScore += value;
            Destroy(gameObject);
        }
        curTime += Time.deltaTime;
        curRect.anchoredPosition = Vector2.Lerp(originPosition, finishPosition, curTime / finishTime);
    }
}
