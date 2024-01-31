using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstepDetector : MonoBehaviour
{
    [SerializeField] private CharacterController _controller;
    [SerializeField] private float _footstepInterval = 0.5f;

    private void Start()
    {
        if (_controller == null) _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        
    }
}
