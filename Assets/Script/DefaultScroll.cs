using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultScroll : MonoBehaviour
{

    [SerializeField] RectTransform bg;
    // Start is called before the first frame update
    void Start()
    {
        bg.anchoredPosition = new Vector2(2496f, bg.anchoredPosition.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
