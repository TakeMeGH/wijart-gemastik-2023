using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public int currentScore;
    public static ScoreController Instance { get; private set; }

    private void Awake()
    {

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

    public void correctClick(){
        currentScore += 5;
    }

    public void missClick(){
        currentScore -= 3;
    }
}
