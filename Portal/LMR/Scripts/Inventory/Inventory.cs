using Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private InventoryItemScriptableObject testItem;
        [SerializeField] private RectTransform _containerTransform;
        [SerializeField] private CanvasGroup _containerCanvasGroup;
        [SerializeField] private InventoryCell _itemPrefab;
        [SerializeField] private TextMeshProUGUI _itemDescription;
        private SortedList<string, InventoryCell> _items;

        private bool _isInventoryVisible = false;

        private void Start()
        {
            _items = new SortedList<string, InventoryCell>();
        }

        public void Add(InventoryItemScriptableObject item)
        {
            InventoryCell instance = Instantiate(_itemPrefab, _containerTransform);
            instance.SetItem(item);
            instance.onItemEnter += OnItemEnter;
            instance.onItemExit += OnItemExit;
            instance.onItemSelect += OnItemSelect;
            _items.Add(item.id, instance);
        }


        public bool OnItemCheck(string id)
        {
            bool IsOnInventory = false;
            foreach (string key in _items.Keys)
            {
                if (id == key)
                {
                    IsOnInventory = true;
                }
            }
            return IsOnInventory;
        }

        private void OnItemSelect(InventoryItemScriptableObject item)
        {

        }

        private void OnItemExit(InventoryItemScriptableObject item)
        {
            _itemDescription.text = string.Empty;
        }

        private void OnItemEnter(InventoryItemScriptableObject item)
        {
            _itemDescription.text = string.Format("<b>{0}</b>: {1}", item.displayname, item.description);
        }

        public void Remove(InventoryItemScriptableObject item)
        {
            Remove(item.id);
        }

        public void Remove(string id)
        {
            if (_items.ContainsKey(id))
            {
                Destroy(_items[id].gameObject);
                _items.Remove(id);
            }
        }

        public void ToggleInventory()
        {
            if (_isInventoryVisible)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Confined;
                _containerCanvasGroup.DOFadeOut();
                _isInventoryVisible = !_isInventoryVisible;
            }
            else if (!_isInventoryVisible)
            {
                if (_items.Count == 0) _itemDescription.text = "No llevas ningún objeto contigo.";
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                _containerCanvasGroup.DOFadeIn();
                _isInventoryVisible = !_isInventoryVisible;
            }
        }

        public void ToggleInventory(UseInventoryObject origin)
        {
            if (_items.Count == 0) _itemDescription.text = "No llevas ningún objeto contigo.";
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            _containerCanvasGroup.DOFadeIn();
            _isInventoryVisible = !_isInventoryVisible;

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                ToggleInventory();
            }
        }
    }
}