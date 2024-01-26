using Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryCallbackBehaviour
{
    void OnCorrectItemSelected(InventoryItemScriptableObject item);
    void OnIncorrectItemSelected(InventoryItemScriptableObject item);
}
