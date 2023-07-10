using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class BintangController : MonoBehaviour
{

    // -1 gembok
    // 0 belom ada bintang
    // 1 - 3 (banyak bintang)
    public int status;
    [SerializeField] GameObject gembok;
    [SerializeField] GameObject leftBintang;
    [SerializeField] GameObject middleBintang;
    [SerializeField] GameObject rightBintang;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setStatus(int _status){
        status = _status;
        if(status == -1){
            gembok.SetActive(true);
            GetComponent<Button>().enabled = false;
            GetComponent<EventTrigger>().enabled = false;
        }
        else if(status == 0){

        }
        else if(status == 1){
            leftBintang.SetActive(true);
        }
        else if(status == 2){
            leftBintang.SetActive(true);
            middleBintang.SetActive(true);
        }
        else if(status == 3){
            leftBintang.SetActive(true);
            middleBintang.SetActive(true);
            rightBintang.SetActive(true);
        }
    }
}
