using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalSetter : MonoBehaviour
{
    [SerializeField] int mobilUgal;
    [SerializeField] int mobilSalip;
    [SerializeField] int mobilLambat;
    [SerializeField] int mobilCepat;
    [SerializeField] int mobilTerobos;
    [SerializeField] int mobilRoofLess;
    // Start is called before the first frame update
    void Start()
    {
        GoalController.Instance.setPenalty(CategoryPenalty.Penalty.Ugal, mobilUgal);
        GoalController.Instance.setPenalty(CategoryPenalty.Penalty.Salip, mobilSalip);
        GoalController.Instance.setPenalty(CategoryPenalty.Penalty.Lambat, mobilLambat);
        
        GoalController.Instance.setPenalty(CategoryPenalty.Penalty.Cepat, mobilCepat);
        GoalController.Instance.setPenalty(CategoryPenalty.Penalty.Terobos, mobilTerobos);
        GoalController.Instance.setPenalty(CategoryPenalty.Penalty.RoofLess, mobilRoofLess);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
