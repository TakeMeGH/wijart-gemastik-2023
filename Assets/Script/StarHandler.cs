using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarCounter : MonoBehaviour
{
        [SerializeField] public Image starOne;
        [SerializeField] public Image starTwo;
        [SerializeField] public Image starThree;

        bool conditionZero;
        public bool firstCondition;
        public bool secondCondition;
        public bool thirdCondition;

    // Start is called before the first frame update
    void Start()
    {
        conditionZero = false;
        firstCondition = false;
        secondCondition = false;
        thirdCondition = false;

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
        if(conditionZero) {
            starOne.enabled = false;
            starTwo.enabled = false;
            starThree.enabled = false;
        }
        if(firstCondition) {
            starOne.enabled = true;
        } else
        {
            starOne.enabled = false;
        }
        if(secondCondition) {
            starTwo.enabled = true;
        } else
        {
            starTwo.enabled = false;
        }
        if(thirdCondition) {
            starThree.enabled = true;
        } else
        {
            starThree.enabled = false;
        }
    }
}
