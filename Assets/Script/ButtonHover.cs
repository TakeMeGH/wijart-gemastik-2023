using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour
{
    public Button button;
    public Color wantedColor;
    private Color originalColor;
    private ColorBlock cb;

    // Start is called before the first frame update
    void Start()
    {
        cb = button.colors;

        originalColor = cb.selectedColor;
    }

    // Update is called once per frame
    void Update() { }

    public void changeWhenHover()
    {
        cb.selectedColor = wantedColor;

        button.colors = cb;
    }

    public void changeWhenLeaves()
    {
        cb.selectedColor = originalColor;

        button.colors = cb;
    }
}
