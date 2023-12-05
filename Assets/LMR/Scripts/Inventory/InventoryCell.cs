using Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Gameplay
{
    public class InventoryCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        [SerializeField] private Image _item;
        [SerializeField] private Image _hoverImage; 
        public delegate void OnPointerItemDelegate(InventoryItemScriptableObject item);
        public event OnPointerItemDelegate onItemEnter;
        public event OnPointerItemDelegate onItemExit;
        public event OnPointerItemDelegate onItemSelect;
        public InventoryItemScriptableObject attachedItem { get; private set; } 

        public void SetItem(InventoryItemScriptableObject item)
        {
            attachedItem = item;
            _item.sprite = item.sprite;
        }

        private void Update()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _hoverImage.transform.DOScaleIn();
            _item.transform.DOScale(1.5f);
            onItemEnter.Invoke(attachedItem);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _hoverImage.transform.DOScaleOut();
            _item.transform.DOScale(1f);
            onItemExit.Invoke(attachedItem);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            onItemSelect.Invoke(attachedItem);
            OnPointerExit(eventData);
        }
    }
}