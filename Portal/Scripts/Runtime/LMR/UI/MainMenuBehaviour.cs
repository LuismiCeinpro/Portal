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
           
        });
        StopCoroutine(SelectBackground());
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
