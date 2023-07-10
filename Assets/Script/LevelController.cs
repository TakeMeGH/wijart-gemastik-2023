using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    
    public List<BintangController> bintangControllers;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int bfr = SaveSystem.Instance.getBintang("Level 1");
        bintangControllers[0].setStatus(bfr);
        for(int i = 1; i < bintangControllers.Count; i++){
            if(bfr == 0){
                bintangControllers[i].setStatus(-1);
            }
            else{
                bfr = SaveSystem.Instance.getBintang("Level " + (i + 1));
                bintangControllers[i].setStatus(bfr);
            }
        }
    }
}
