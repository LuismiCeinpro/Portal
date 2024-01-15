using Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainMenuBehaviour : MonoBehaviour
{
    [Header("Menu canvas")]
    [SerializeField] private int _mainMenuCanvasIndex;
    [SerializeField] private CanvasGroup[] _canvasGroups;
    private int _currentCanvasGroup;
    [Header("Main menu backgrounds")]
    [SerializeField] private RectTransform _backgroundsParent;
    [SerializeField] private CanvasGroup[] _backgrounds;
    private int _currentBackgroundIndex = -1;
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

    private IEnumerator SelectBackground()
    {
        _currentBackgroundIndex++;
        if (_currentBackgroundIndex >= _backgrounds.Length) _currentBackgroundIndex = 0;
        _backgroundDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        yield return new WaitForSeconds(5f);
        StartCoroutine(SelectBackground());
    }

    public void SelectInteractionType(int type)
    {
        GameManager.instance.SetInteractionType((GameManager.InteractionType)type);
    }

    private void Update()
    {
        if (_currentBackgroundIndex == -1) return;
        _backgroundsParent.Translate(_backgroundDirection * Time.deltaTime * 10f);
    }
}
