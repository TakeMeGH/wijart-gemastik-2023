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
    public bool isNotScore;

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
        if(isNotScore){
            curTime += Time.deltaTime;
            curRect.anchoredPosition = new Vector2(curRect.anchoredPosition.x, curRect.anchoredPosition.y + Time.deltaTime * 30f);
        }
        else{
            curTime += Time.deltaTime;
            curRect.anchoredPosition = Vector2.Lerp(originPosition, finishPosition, curTime / finishTime);

        }
    }
}
