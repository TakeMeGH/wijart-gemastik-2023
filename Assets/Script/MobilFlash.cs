using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilFlash : MonoBehaviour
{
    [SerializeField] Material wrongPenalty;
    [SerializeField] Material correctPenalty;
    [SerializeField] Material netralPenalty;
    Material[] originalMaterial;
    Material[] wrongArray;
    Material[] correctArray;
    Material[] netralArray;
    CategoryPenalty categoryPenalty;
    Renderer curRenderer;
    Coroutine clickRoutine;
    bool firstCorrect;
    // Start is called before the first frame update
    void Start()
    {
        curRenderer = GetComponent<Renderer>();
        categoryPenalty = GetComponent<CategoryPenalty>();
        originalMaterial = curRenderer.materials;
        wrongArray = new Material[originalMaterial.Length];
        correctArray = new Material[originalMaterial.Length];
        netralArray = new Material[originalMaterial.Length];
        for(int i = 0; i < originalMaterial.Length; i++){
            wrongArray[i] = wrongPenalty;
            correctArray[i] = correctPenalty;
            netralArray[i] = netralPenalty;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clicked(){
        if(clickRoutine == null){
            int ret = GoalController.Instance.clickCar(categoryPenalty.curObjectPenalty);
            if(ret == 0){
                clickRoutine = StartCoroutine(changeMaterial(wrongArray));
                if(ScoreController.Instance != null){
                    ScoreController.Instance.missClick();
                }
            }
            else if(ret == 1 && firstCorrect == false){
                firstCorrect = true;
                clickRoutine = StartCoroutine(changeMaterial(correctArray));
                if(ScoreController.Instance != null){
                    ScoreController.Instance.correctClick();
                }
            }
            else{
                clickRoutine = StartCoroutine(changeMaterial(netralArray));
            }
        }
    }

    IEnumerator changeMaterial(Material[] usedMaterial){
        for(int i = 0; i < 10; i++){
            if(i % 2 == 1){
                curRenderer.materials = usedMaterial;
            }
            else{
                curRenderer.materials = originalMaterial;
            }
            yield return new WaitForSeconds(0.2f);
        }
        curRenderer.materials = originalMaterial;
        clickRoutine = null;
    }
}
