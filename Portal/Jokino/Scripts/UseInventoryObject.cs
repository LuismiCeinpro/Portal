using Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
  
    public class UseInventoryObject : BaseInteractable
    {
        public InventoryHelper helper;
        [SerializeField]
        private string KeyId = "33";
    private bool isKeyOnInventory = false;
    protected override void OnActivate()
    {
            
            helper.OpenInventory(this);

            isKeyOnInventory = helper.Search(KeyId);

        Debug.Log("isKeyOnInventory == " + isKeyOnInventory);
    }
    }
}