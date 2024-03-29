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
        [SerializeField] private RectTransform _containerTransform;
        [SerializeField] private CanvasGroup _containerCanvasGroup;
        [SerializeField] private InventoryCell _itemPrefab;
        [SerializeField] private TextMeshProUGUI _itemDescription;
        private SortedList<string, InventoryCell> _items;
        private Action<InventoryItemScriptableObject, bool> _onItemSelected;
        private InteractableObject _currentObject;
        private InventoryCell _currentHoveredItem;

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
            instance.onItemSelect += OnItemSelected;
            _items.Add(item.id, instance);
        }
        public bool OnItemCheck(string id)
        {
            bool isOnInventory = false;
            foreach (string key in _items.Keys)
            {
                if (id == key)
                {
                    isOnInventory = true;
                }
            }
            return isOnInventory;
        }

        public bool OnItemCheck(string[] ids)
        {
            foreach (string id in ids)
            {
                if (_items.Keys.IndexOf(id) != -1) return true;
            }
            return false;
        }

        private void OnItemSelected(InventoryItemScriptableObject item, InventoryCell cell)
        {
            if (_currentObject == null) return;
            foreach (InventoryItemScriptableObject request in _currentObject.itemsToRequest)
            {
                if (request.id == item.id)
                {
                    _onItemSelected.Invoke(item, true);
                    if (item.removeFromInventoryOnUse)
                    {
                        cell.onItemExit -= OnItemExit;
                        Remove(item.id);
                    }
                    ToggleInventory();
                    return;
                }
            }
            _onItemSelected.Invoke(item, false);
            ToggleInventory();
        }

        private void OnItemExit(InventoryItemScriptableObject item, InventoryCell cell)
        {
            cell.Unselect();
            _itemDescription.text = string.Empty;
            _currentHoveredItem = null;
        }

        private void OnItemEnter(InventoryItemScriptableObject item, InventoryCell cell)
        {
            cell.Hover();
            _itemDescription.text = string.Format("<b>{0}</b>: {1}", item.displayname, item.description);
            _currentHoveredItem = cell;
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
                Cursor.lockState = CursorLockMode.Locked;
                _containerCanvasGroup.DOFadeOut(() => { if (_currentHoveredItem) _currentHoveredItem.Unselect(); });
                _isInventoryVisible = !_isInventoryVisible;
                _currentObject = null;
            }
            else if (!_isInventoryVisible)
            {
                if (_items.Count == 0) _itemDescription.text = "No llevas ning�n objeto contigo.";
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                _containerCanvasGroup.DOFadeIn();
                _isInventoryVisible = !_isInventoryVisible;
            }
        }

        public void ToggleInventory(InteractableObject currentObject, Action<InventoryItemScriptableObject, bool> onItemSelected)
        {
            if (_items.Count == 0) _itemDescription.text = "No llevas ning�n objeto contigo.";
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            _containerCanvasGroup.DOFadeIn();
            _isInventoryVisible = !_isInventoryVisible;
            _onItemSelected = onItemSelected;
            _currentObject = currentObject;
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