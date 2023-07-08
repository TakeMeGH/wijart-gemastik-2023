using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIFlash : MonoBehaviour
{
    [SerializeField] Material flashCamera;
    [SerializeField] Image fill;
    Material originalMaterial;
    Image curImage;
    float curTime;
    
    // Start is called before the first frame update
    void Start()
    {
        curImage = GetComponent<Image>();
        originalMaterial = curImage.material;
        curTime = 1.6f;
    }

    // Update is called once per frame
    void Update()
    {
        if(curTime > 1.5f){
            Color temp = fill.color;
            temp.a  = 0;
            fill.color = temp;
        }
        else{
            curTime += Time.deltaTime;
            Color temp = fill.color;
            temp.a  = 1;
            fill.color = temp;

            fill.fillAmount = Mathf.Min(1f, (float)curTime / 1.5f);
        }
    }

    public void takePicture(){
        curTime = 0;
        StartCoroutine(flash());
    }

    IEnumerator flash(){
        for(int i = 0; i < 10; i++){
            Color temp = curImage.color;
            if(i % 2 == 0){
                temp.a = 0.5f;
                curImage.material = flashCamera;
                curImage.color = temp;
            }
            else{
                curImage.material = originalMaterial;
                temp.a = 1;
                curImage.color = temp;
            }
            yield return new WaitForSecondsRealtime(0.15f);
        }
      
    }
}
