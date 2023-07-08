using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreAdder : MonoBehaviour
{
    TMP_Text showedText;
    Coroutine addScoreUI;
    // [SerializeField] float disappearTime;
    // [SerializeField] float curTime;
    public RectTransform parentTransform;
    public GameObject flyingText;
    public Canvas m_canvas;
    // Start is called before the first frame update
    void Start()
    {
        showedText = GetComponent<TMP_Text>();
        // curTime = disappearTime;
        Vector2 anchoredPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentTransform, Input.mousePosition, null, out anchoredPos);
        GameObject temp = Instantiate(flyingText, new Vector2(anchoredPos.x, anchoredPos.y), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        // curTime += Time.deltaTime;
        // Color temp = showedText.color;
        // temp.a = 1;

        // temp.a = Mathf.Lerp(1, 0, curTime / disappearTime);
        // showedText.color = temp;

    }

    public void addScore(int value, bool correct){
        Vector2 anchoredPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentTransform, Input.mousePosition, null, out anchoredPos);
        GameObject temp = Instantiate(flyingText, new Vector2(anchoredPos.x, anchoredPos.y), Quaternion.identity);
        temp.SetActive(true);
        temp.transform.SetParent(m_canvas.transform, false);

        TMP_Text showedText = temp.GetComponent<TMP_Text>();
        FlyingText tempFlyingText = temp.GetComponent<FlyingText>();
        tempFlyingText.value = value;
        if(correct == false) tempFlyingText.value *= -1;

        if(correct == true) showedText.text = "+" + value.ToString();
        else showedText.text = "-" + value.ToString();

        Color tempColor = showedText.color;
        if(correct) tempColor = Color.green;
        else tempColor = Color.red;
        tempColor.a = 1;

        // curTime = 0;
        showedText.color = tempColor;


        // if(addScoreUI != null){
        //     StopCoroutine(addScoreUI);
        // }
        // addScoreUI = StartCoroutine(addScoreCoroutine(value, correct));
    }

    // IEnumerator addScoreCoroutine(int value, bool correct){
    //     if(correct == true) showedText.text = "+" + value.ToString();
    //     else showedText.text = "-" + value.ToString();

    //     Color temp = showedText.color;
    //     if(correct) temp = Color.green;
    //     else temp = Color.red;
    //     temp.a = 1;

    //     showedText.color = temp;
    //     Debug.Log(temp);

    //     for(int i = 0; i < 11; i++){
    //         temp = showedText.color;
    //         temp.a -= (float) 1/11;

    //         showedText.color = temp;

    //         yield return new WaitForSeconds(0.3f);
    //     }

    //     temp.a = 0;
    //     showedText.color = temp;
    // }

}
