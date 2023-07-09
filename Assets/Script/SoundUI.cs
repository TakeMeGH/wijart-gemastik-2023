using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundUI : MonoBehaviour
{


     [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        [Range(0,1)]
        public float volume = 1;
    }

    public List<Sound> sounds;
    AudioSource audioSource;
    public static SoundUI Instance { get; private set; }

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(this.gameObject);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSoundByName(string soundName){
        for(int i = 0; i < sounds.Count; i++){
            if(sounds[i].name == soundName){
                audioSource.PlayOneShot(sounds[i].clip, sounds[i].volume);
            }
        }
    }
}
