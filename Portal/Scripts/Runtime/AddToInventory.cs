using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Gameplay
{ 
public class AddToInventory : InteractableObject
{
        public InventoryHelper helper;
        public InventoryItemScriptableObject Key;
    protected override void OnActivate()
    {        
            helper.Add(Key);
            Destroy(transform.gameObject);
            Debug.Log("Added to inventory");
        }
    

}
}