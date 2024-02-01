using Gameplay;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstepDetector : MonoBehaviour
{
    [SerializeField] private StarterAssetsInputs _controller;
    [SerializeField] private Transform _footTransform;
    [SerializeField] private float _walkFootstepInterval = 0.5f;
    [SerializeField] private float _runFootstepInterval = 0.25f;
    [SerializeField] private PlayerFootstepsScriptableObject[] _footsteps;
    private float _footstepInterval;
    private float _footstepElapsedTime;

    private void Start()
    {
        if (_controller == null) _controller = GetComponent<StarterAssetsInputs>();
        _footstepInterval = _walkFootstepInterval;
    }

    private void Update()
    {
        float newFootstepInterval = _controller.sprint ? _runFootstepInterval : _walkFootstepInterval;
        if (newFootstepInterval != _footstepInterval || _controller.move == Vector2.zero) _footstepElapsedTime = 0;
        if (_controller.move != Vector2.zero)
        {
            _footstepElapsedTime += Time.deltaTime;
            if (_footstepElapsedTime >= _footstepInterval)
            {
                RaycastHit hit;
                if (Physics.Raycast(_footTransform.position, Vector3.down, out hit, 0.1f))
                {
                    string tag = hit.collider.tag;
                    foreach (PlayerFootstepsScriptableObject footsteps in _footsteps)
                    {
                        if (footsteps.tag == tag)
                        {
                            PlayRandomAudio(footsteps, _controller.sprint);
                            return;
                        }
                    }
                }
            }
        }
    }

    private void PlayRandomAudio(PlayerFootstepsScriptableObject footsteps, bool isSprint)
    {
        int index = isSprint ? Random.Range(0, footsteps.runFootsteps.Length) : Random.Range(0, footsteps.walkFootsteps.Length);
        AudioClip clip = isSprint ? footsteps.runFootsteps[index] : footsteps.walkFootsteps[index];
        AudioSource.PlayClipAtPoint(clip, _footTransform.position);
        _footstepElapsedTime = 0;
    }
}
