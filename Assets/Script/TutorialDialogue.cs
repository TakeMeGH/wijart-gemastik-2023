using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialDialogue : MonoBehaviour
{
    public List<Dialogue> listDialogue = new List<Dialogue>();
    [SerializeField] TMP_Text textDialogue;
    [SerializeField] GameObject clickText;
    int idx = 0;
    Coroutine showTextCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        nextDialogue();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void nextDialogue(){
        if(showTextCoroutine == null){
            showTextCoroutine = StartCoroutine(showText());
        }
    }

    IEnumerator showText(){
        textDialogue.text = "";
        clickText.SetActive(false);
        for(int i = 0; i < listDialogue[idx].textDialougue.Length; i++){
            Debug.Log(listDialogue[idx].textDialougue[i]);
            textDialogue.text += listDialogue[idx].textDialougue[i];
            yield return new WaitForSeconds(0.05f);
        }
        clickText.SetActive(true);
        idx++;
        showTextCoroutine = null;
    }
}
[System.Serializable]
public enum eventOption{
    normalButton,
    callTutorial
}

[System.Serializable]
public class Dialogue
{
    [TextAreaAttribute]
    public string textDialougue;
    public eventOption choosenAction;
}
