using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
  
    public class UseInventoryObject : InteractableObject
    {
        public InventoryHelper helper;
        [SerializeField]
        private string KeyId = "33";
    private bool isKeyOnInventory = false;
    protected override void OnActivate()
    {
            
            helper.RequestObject(this);

            isKeyOnInventory = helper.Search(KeyId);

        Debug.Log("isKeyOnInventory == " + isKeyOnInventory);
    }
    }
}