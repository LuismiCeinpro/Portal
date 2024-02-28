using Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuBehaviour : MonoBehaviour
{
    [Header("Menu canvas")]
    [SerializeField] private int _mainMenuCanvasIndex;
    [SerializeField] private CanvasGroup[] _canvasGroups;
    private int _currentCanvasGroup;
    [Header("Main menu backgrounds")]
    [SerializeField] private CanvasGroup _background;
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _creditsImage;
    [SerializeField] private Sprite[] _backgrounds;
    private int _currentBackgroundIndex = 0;
    private Vector3 _backgroundDirection;
    [Header("Canvas windows")]
    [SerializeField] private GameObject mainWindow;
    [SerializeField] private GameObject optionsContainer;
    [SerializeField] private GameObject creditsContainer;
    [SerializeField] private GameObject displayContainer;
    [SerializeField] private GameObject soundContainer;
    [SerializeField] private GameObject accesibilityContainer;
    [SerializeField] private GameObject controlsContainer;



    private void Start()
    {
        _canvasGroups[_currentCanvasGroup].DOFadeIn();
    }

    public void ShowNextCanvasGroup()
    {
        _canvasGroups[_currentCanvasGroup].DOFadeOut(() =>
        {
            _currentCanvasGroup++;
            _canvasGroups[_currentCanvasGroup].DOFadeIn();
            if (_currentCanvasGroup == _mainMenuCanvasIndex) StartCoroutine(SelectBackground());
        });
    }

    public void Exit()
    {
        Application.Quit();
    }

    private IEnumerator SelectBackground()
    {
        _background.transform.localPosition = Vector3.zero;
        _currentBackgroundIndex++;
        if (_currentBackgroundIndex >= _backgrounds.Length) _currentBackgroundIndex = 0;
        _backgroundDirection.x = Random.Range(0f, 1f) > 0.5f ? Random.Range(0.5f, 1f) : Random.Range(-1f, -0.5f);
        _backgroundDirection.y = Random.Range(0f, 1f) > 0.5f ? Random.Range(0.5f, 1f) : Random.Range(-1f, -0.5f);
        yield return new WaitForSeconds(5f);
        _background.DOFadeOut(() =>
        {
            _backgroundImage.sprite = _backgrounds[_currentBackgroundIndex];
            StartCoroutine(SelectBackground());
            _background.DOFadeIn();
        });
    }
    public void showCredits()
    {
        _background.DOFadeOut(() =>
        {
            creditsContainer.SetActive(true);
            mainWindow.SetActive(false);


        });
        StopCoroutine(SelectBackground());
    }

    public void showOptions()
    {

            optionsContainer.SetActive(true);
            mainWindow.SetActive(false);

    }
    public void showDisplay()
    {
        displayContainer.SetActive(true);
        optionsContainer.SetActive(false);
    }
    public void showSound()
    {
        soundContainer.SetActive(true);
        optionsContainer.SetActive(false);
    }
    public void showControls()
    {
        controlsContainer.SetActive(true);
        optionsContainer.SetActive(false);
    }
    public void showAccesibility()
    {
        accesibilityContainer.SetActive(true);
        optionsContainer.SetActive(false);
    }

    public void goBack(GameObject sender)
    {
        string previousMenuName = sender.transform.parent.name;
        GameObject previousMenu = sender.transform.parent.gameObject;   
        if (previousMenuName == "OptionsContent")
        {
            previousMenu.SetActive(false);
            mainWindow.SetActive(true);
            
        }else if(previousMenuName == "SubmenuSound"|| previousMenuName == "SubmenuDisplay" || previousMenuName == "SubmenuAccesibility" || previousMenuName == "SubmenuControls")
        {
            previousMenu.SetActive(false);
            optionsContainer.SetActive(true);
        }
    }

    public void SelectInteractionType(int type)
    {
        GameManager.instance.SetInteractionType((GameManager.InteractionType)type);
    }

    private void Update()
    {
        if (_currentBackgroundIndex == -1) return;
        _background.transform.Translate(_backgroundDirection * Time.deltaTime * 10f);
    }
}
