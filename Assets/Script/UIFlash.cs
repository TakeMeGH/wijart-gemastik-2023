using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIFlash : MonoBehaviour
{
    [SerializeField] Material flashCamera;
    Material originalMaterial;
    Image curImage;
    // Start is called before the first frame update
    void Start()
    {
        curImage = GetComponent<Image>();
        originalMaterial = curImage.material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takePicture(){
        StartCoroutine(flash());
    }

    IEnumerator flash(){
        Color temp = curImage.color;
        temp.a = 0.5f;
        curImage.material = flashCamera;
        curImage.color = temp;
        yield return new WaitForSecondsRealtime(3f);
        curImage.material = originalMaterial;
        temp.a = 1;
        curImage.color = temp;
    }
}
