using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StarPosition : MonoBehaviour
{
    [SerializeField] RectTransform firstStar;
    [SerializeField] RectTransform seccondStar;
    [SerializeField] RectTransform thirdStar;
    [SerializeField] EndGame endGame;
    [SerializeField] float leftPos;
    [SerializeField] float rightPos;
    float len;
    public bool isTimer;

    // Start is called before the first frame update
    void Start()
    {
        len = rightPos - leftPos + 1;
        if(isTimer){
            float thirdStarPos = (float)endGame.threeStar.timer / (float)endGame.oneStar.timer;
            thirdStarPos = leftPos + (thirdStarPos * len);

            float secondStarPos = (float)endGame.twoStar.timer / (float)endGame.oneStar.timer;
            secondStarPos = leftPos + (secondStarPos * len);

            thirdStar.localPosition = new Vector2(thirdStarPos, firstStar.localPosition.y);
            seccondStar.localPosition = new Vector2(secondStarPos, seccondStar.localPosition.y);
        }
        else{
            float firstStarPos = (float)endGame.oneStar.point / (float)endGame.threeStar.point;
            firstStarPos = leftPos + (firstStarPos * len);

            float seccondStarPos = (float)endGame.twoStar.point / (float)endGame.threeStar.point;
            seccondStarPos = leftPos + (seccondStarPos * len);

            firstStar.localPosition = new Vector2(firstStarPos, firstStar.localPosition.y);
            seccondStar.localPosition = new Vector2(seccondStarPos, seccondStar.localPosition.y);
        }
           
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
