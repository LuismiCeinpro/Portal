using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMinAlphaToIntercact : MonoBehaviour
{
    
    void Start()
    {
        Image image = transform.GetComponent<Image>();
        image.alphaHitTestMinimumThreshold = 1;
    }

}
