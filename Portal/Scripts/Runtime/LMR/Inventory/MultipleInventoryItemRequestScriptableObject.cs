using Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new inventory item", menuName = "Portal/Multiple inventory item request", order = 0)]
public class MultipleInventoryItemRequestScriptableObject : ScriptableObject
{
    [SerializeField] private List<InventoryItemScriptableObject> _inventoryItemsToRequest;
}