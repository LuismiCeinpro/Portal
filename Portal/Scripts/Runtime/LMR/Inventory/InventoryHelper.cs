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
        public void OpenInventory(UseInventoryObject origin)
        {
            GameManager.instance.player.Inventory.ToggleInventory(origin);
        }

    }
}
