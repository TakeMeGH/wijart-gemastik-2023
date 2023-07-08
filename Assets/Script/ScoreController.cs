using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public int currentScore;
    [SerializeField] int minCorrectScore;
    [SerializeField] int maxCorrectScore;
    [SerializeField] int minWrongScore;
    [SerializeField] int maxWrongScore;
    [SerializeField] ScoreAdder scoreAdder;

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
        int value = Random.Range(minCorrectScore, maxCorrectScore + 1);
        scoreAdder.addScore(value, true);
    }

    public void missClick(){
        int value = Random.Range(minWrongScore, maxWrongScore + 1);
        scoreAdder.addScore(value, false);

    }
}
