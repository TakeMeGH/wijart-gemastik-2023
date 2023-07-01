using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarHandler : MonoBehaviour
{
        [SerializeField] public Image starOne;
        [SerializeField] public Image starTwo;
        [SerializeField] public Image starThree;
        
        public int tempScore;

        int score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;

        starOne.enabled = false;
        starTwo.enabled = false;
        starThree.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        setScore();
    }

    void setScore() {
        if(score == 1) {
            starOne.enabled = true;
        }
        if(score == 2) {
            starTwo.enabled = true;
        }
        if(score == 3) {
            starThree.enabled = true;
        }
    }
}
