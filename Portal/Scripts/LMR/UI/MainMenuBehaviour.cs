using Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBehaviour : MonoBehaviour
{
    [Header("Menu canvas")]
    [SerializeField] private int _mainMenuCanvasIndex;
    [SerializeField] private CanvasGroup[] _canvasGroups;
    private int _currentCanvasGroup;
    [Header("Main menu backgrounds")]
    [SerializeField] private CanvasGroup[] _backgrounds;
    private int _currentBackgroundIndex = -1;

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
            if (_currentCanvasGroup == _mainMenuCanvasIndex) SelectBackground();
        });
    }

    private void SelectBackground()
    {
        _currentBackgroundIndex++;
    }

    public void SelectInteractionType(int type)
    {
        GameManager.instance.SetInteractionType((GameManager.InteractionType)type);
    }
}
