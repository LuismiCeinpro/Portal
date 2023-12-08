using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

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
    }
}
