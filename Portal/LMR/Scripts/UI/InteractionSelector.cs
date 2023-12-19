using DG.Tweening;
using Extensions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Portal.UI
{
    public class InteractionSelector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private string _description;

        public void OnPointerEnter(PointerEventData eventData)
        {
            _descriptionText.text = _description;
            _descriptionText.DoFadeIn();
            
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _descriptionText.DoFadeOut();
        }
    }
}