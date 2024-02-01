using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "new inventory item", menuName = "Portal/Inventory item", order = 0)]
    public class InventoryItemScriptableObject : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private string _displayname;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private bool _removeFromInventoryOnUse;
        [SerializeField] private bool _removeFromRequestOnUse;
        public string id { get {  return _id; } }
        public string displayname { get { return _displayname; } }
        public string description { get { return _description; } }
        public Sprite sprite { get { return _sprite; } }
        public bool removeFromInventoryOnUse { get { return _removeFromInventoryOnUse; } }
        public bool removeFromRequestOnUse { get { return _removeFromInventoryOnUse; } }

    }
}