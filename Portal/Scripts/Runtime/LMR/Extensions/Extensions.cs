using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Extensions
{
    public static class Extensions
    {
        public static void DOScaleIn(this Transform transform)
        {
            transform.gameObject.SetActive(true);
            transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
        }

        public static void DOScale(this Transform transform, float scale)
        {
            if (scale == 0) DOScaleOut(transform);
            else transform.DOScale(scale, 0.3f).SetEase(transform.localScale.x < scale ? Ease.OutBack : Ease.InBack);
        }

        public static void DOScaleOut(this Transform transform)
        {
            transform.DOScale(0f, 0.3f).SetEase(Ease.InBack).OnComplete(() => transform.gameObject.SetActive(false));
        }

        public static void DOFadeIn(this CanvasGroup canvasGroup) 
        {
            canvasGroup.gameObject.SetActive(true);
            canvasGroup.DOFade(1f, 0.3f).SetEase(Ease.OutQuad);
        }

        public static void DOFadeIn(this CanvasGroup canvasGroup, Action onComplete)
        {
            canvasGroup.gameObject.SetActive(true);
            canvasGroup.DOFade(1f, 0.3f).SetEase(Ease.OutQuad).OnComplete(() => {
                onComplete.Invoke();
            });
        }

        public static void DOFadeOut(this CanvasGroup canvasGroup)
        {
            canvasGroup.DOFade(0f, 0.3f).SetEase(Ease.OutQuad).OnComplete(() => canvasGroup.gameObject.SetActive(false));
        }

        public static void DOFadeOut(this CanvasGroup canvasGroup, Action onComplete)
        {
            canvasGroup.DOFade(0f, 0.3f).SetEase(Ease.OutQuad).OnComplete(() => {
                canvasGroup.gameObject.SetActive(false);
                onComplete.Invoke();
            });
        }

        public static void DoFadeOut(this TextMeshProUGUI text)
        {
            text.DOFade(0f, 0.3f);
        }

        public static void DoFadeOut(this TextMeshProUGUI text, Action onComplete)
        {
            text.DOFade(0f, 0.3f).OnComplete(() => onComplete.Invoke());
        }

        public static void DoFadeIn(this TextMeshProUGUI text)
        {
            text.DOFade(1f, 0.3f);
        }
    }
}


