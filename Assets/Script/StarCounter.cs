using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StarCounter : MonoBehaviour
{
        [SerializeField] public GameObject starOne;
        [SerializeField] public GameObject starTwo;
        [SerializeField] public GameObject starThree;

        Animation anim;
        public bool conditionZero;

        public bool firstCondition;
        public bool secondCondition;
        public bool thirdCondition;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animation>();

        // conditionZero = false;
        // firstCondition = false;
        // secondCondition = false;
        // thirdCondition = false;

        starOne.SetActive(false);
        starTwo.SetActive(false);
        starThree.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        setScore();
    }

    void setScore() {
        if(conditionZero) {
            starOne.SetActive(false);
            starTwo.SetActive(false);
            starThree.SetActive(false);
        }
        if(firstCondition || secondCondition || thirdCondition) {
            starOne.SetActive(true);
        } else
        {
            starOne.SetActive(false);
        }
        if(secondCondition || thirdCondition) {
            starTwo.SetActive(true);
        } else
        {
            starTwo.SetActive(false);
        }
        if(thirdCondition) {
            starThree.SetActive(true);
        } else
        {
            starThree.SetActive(false);
        }
    }
}
