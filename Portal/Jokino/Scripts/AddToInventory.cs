using System.Collections;
using Base;
using System.Collections.Generic;
using UnityEngine;


namespace Gameplay
{ 
public class AddToInventory : BaseInteractable
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