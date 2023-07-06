using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastParticleController : MonoBehaviour
{

    [SerializeField] GameObject fastParticle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enableFastParticle(){
        fastParticle.SetActive(true);
    }
}
