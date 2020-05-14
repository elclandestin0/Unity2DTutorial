using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    // Now UIHealthBar is accessible anywhere
    public static UIHealthBar instance { get; private set; }
    public Image mask;
    float originalSize;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        // original size is set to the width of the mask reference
        originalSize = mask.rectTransform.rect.width;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // This resizes the value accordingly. We pass on the current health of the player here
    public void SetValue(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
}
