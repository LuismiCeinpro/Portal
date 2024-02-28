using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreditsAnimation : MonoBehaviour
{
    public TextMeshProUGUI creditsTitles;
    public TextMeshProUGUI creditsNames;
    public Canvas canvas;

    public float creditsSpeed = 1f;

    private int linecount;
    private float fontsize;
    private float scale;

    private float titleHeight;
    private float nameHeight;
    private float highestHeight;

    private float currentHeight = 0;
    private float targetHeight;

    public GameObject mainMenuContainer;
    RectTransform titlesTransform;
    RectTransform namesTransform;

    private void Start()
    {
        titlesTransform = creditsTitles.gameObject.GetComponent<RectTransform>();
        namesTransform = creditsNames.gameObject.GetComponent<RectTransform>();

        titleHeight = CalculateHeight(creditsTitles);
        nameHeight = CalculateHeight(creditsNames);

        highestHeight = (titleHeight > nameHeight)?titleHeight : nameHeight;
        targetHeight = highestHeight + canvas.gameObject.GetComponent<RectTransform>().rect.height;
    }

    private float CalculateHeight(TextMeshProUGUI textComponent)
    {
        float height; 

        linecount = textComponent.textInfo.lineCount;
        fontsize = (textComponent.fontSize/96)*72; // Adjust for pixel to font size ratio
        scale = textComponent.transform.localScale.y; 

        height = linecount * fontsize * scale;
        return height;

    }
    void FixedUpdate()
    {
        if(currentHeight< targetHeight+400)
        {
            titlesTransform.position = new Vector3(titlesTransform.position.x, titlesTransform.position.y + creditsSpeed, titlesTransform.position.z);
            namesTransform.position = new Vector3(namesTransform.position.x, namesTransform.position.y + creditsSpeed, namesTransform.position.z);
            currentHeight= currentHeight+ creditsSpeed;
        }
        else
        {
            gameObject.SetActive(false);
            mainMenuContainer.SetActive(true);
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            gameObject.SetActive(false);
            mainMenuContainer.SetActive(true);
        }

    }
}
