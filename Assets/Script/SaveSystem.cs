using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{

  public static SaveSystem Instance { get; private set; }
  private void Awake(){
    if (Instance != null && Instance != this)
    {
      Destroy(gameObject);
    }
    else
    {
      Instance = this;
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

    void saveBintang(string levelName, int bintangCount){
      int curBintang = PlayerPrefs.GetInt(levelName);
      PlayerPrefs.SetInt(levelName, Mathf.Max(curBintang, bintangCount));
    }

    int getBintang(string levelName){
      return PlayerPrefs.GetInt(levelName);
    }
}
