using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialDialogue : MonoBehaviour
{
    public List<Dialogue> listDialogue = new List<Dialogue>();
    [SerializeField] TMP_Text textDialogue;
    [SerializeField] GameObject clickText;
    [SerializeField] TutorialController tutorial;
    public List<AudioClip> originalAudioClips;
    public List<AudioClip> usedAudioClips;

    public List<AudioSource> audioSources;    
    int idx = 0;
    bool isSkiped = false;
    int soundTextCount = 2;
    int curSoundCount = 0;
    Coroutine showTextCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        usedAudioClips = new List<AudioClip>(originalAudioClips);
        nextDialogue();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void nextDialogue(){
        if(showTextCoroutine == null){
            if(idx == listDialogue.Count){
                clickText.SetActive(false);
                tutorial.startGame();
            }
            else{
                isSkiped = false;
                showTextCoroutine = StartCoroutine(showText());
            }
        }
        else{
            if(listDialogue[idx].choosenAction == eventOption.waitClick){
                StopCoroutine(showTextCoroutine);
                idx++;
                if(idx == listDialogue.Count){
                    clickText.SetActive(false);
                    tutorial.startGame();
                }
                else{
                    showTextCoroutine = StartCoroutine(showText());
                }
            }
            else{
                isSkiped = true;
            }
        }
    }

    IEnumerator showText(){
        curSoundCount = 0 + 0;
        clickText.SetActive(false);
        if(listDialogue[idx].choosenAction == eventOption.normalButton){
            clickText.SetActive(true);
        }
        listDialogue[idx].textDialougue = listDialogue[idx].textDialougue.Trim();
        textDialogue.text = "";
        for(int i = 0; i < listDialogue[idx].textDialougue.Length; i++){
            if(isSkiped) break;
            if(usedAudioClips.Count < audioSources.Count) usedAudioClips = new List<AudioClip>(originalAudioClips);
            if(curSoundCount == 0){
                for(int j = 0; j < audioSources.Count; j++){
                    audioSources[j].Stop();
                    if(listDialogue[idx].textDialougue[i] >= 'a' && listDialogue[idx].textDialougue[i] <= 'z'){
                        int idxSound = listDialogue[idx].textDialougue[i] - 'a';
                        audioSources[j].PlayOneShot(originalAudioClips[idxSound]);
                    }
                    else if(listDialogue[idx].textDialougue[i] >= 'A' && listDialogue[idx].textDialougue[i] <= 'Z'){
                        int idxSound = listDialogue[idx].textDialougue[i] - 'A';
                        audioSources[j].PlayOneShot(originalAudioClips[idxSound]);
                    }
                    // audioSources[j].PlayOneShot(randomAudioClip);
                }

                curSoundCount = soundTextCount;
            }
            else{
                curSoundCount--;
            }
            textDialogue.text += listDialogue[idx].textDialougue[i];
            yield return new WaitForSeconds(0.05f);
        }
        textDialogue.text = listDialogue[idx].textDialougue;
        if(listDialogue[idx].choosenAction == eventOption.callTutorial){
            tutorial.startTutorial();
            yield return new WaitForSeconds(2f);
            idx++;
            showTextCoroutine = null;
            nextDialogue();
        }
        else if(listDialogue[idx].choosenAction == eventOption.normalButton){
            idx++;
            showTextCoroutine = null;
        }
        else if(listDialogue[idx].choosenAction == eventOption.waitClick){
            idx++;
            showTextCoroutine = null;
        }
        
    }
    public void setClickTextCondition(bool condition){
        clickText.SetActive(condition);
    }
}


[System.Serializable]
public enum eventOption{
    normalButton,
    callTutorial,
    waitClick
}

[System.Serializable]
public class Dialogue
{
    [TextAreaAttribute]
    public string textDialougue;
    public eventOption choosenAction;
}
