
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [Serializable]
    public class Player
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private CharacterController _controller;
        public Inventory Inventory { get { return _inventory; } }
        public Camera camera { get {  return _camera; } }
        public CharacterController controller { get {  return _controller; } }
    }
}