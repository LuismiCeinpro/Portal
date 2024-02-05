using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class InventoryHelper : MonoBehaviour
    {
        public void Add(InventoryItemScriptableObject item)
        {
            GameManager.instance.player.Inventory.Add(item);
        }

        public void Remove(InventoryItemScriptableObject item)
        {
            GameManager.instance.player.Inventory.Remove(item);
        }

        public void Remove(string id)
        {
            GameManager.instance.player.Inventory.Remove(id);
        }
        public bool Search(string id)
        {
            bool isOnInventory = GameManager.instance.player.Inventory.OnItemCheck(id);
            return isOnInventory;
        }

        public void RequestObject(InteractableObject itemToRequest)
        {
            if (itemToRequest.itemsToRequest.Count == 0) return;
            GameManager.instance.player.Inventory.ToggleInventory(itemToRequest, (InventoryItemScriptableObject item, bool isCorrect) => OnRequestObjectClosed(itemToRequest, item, isCorrect));
        }

        private void OnRequestObjectClosed(InteractableObject objectSelected, InventoryItemScriptableObject item, bool isCorrect)
        {
            if (isCorrect) objectSelected.OnCorrectItemSelected(item);
            else objectSelected.OnIncorrectItemSelected(item);
        }
    }
}
