using Gameplay;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        if (newFootstepInterval != _footstepInterval || _controller.move == Vector2.zero)
        {
            _footstepElapsedTime = 0;
            _footstepInterval = newFootstepInterval;
        }
        if (_controller.move != Vector2.zero)
        {
            _footstepElapsedTime += Time.deltaTime;
            if (_footstepElapsedTime >= _footstepInterval)
            {
                RaycastHit hit;
                if (Physics.Raycast(_footTransform.position, Vector3.down, out hit, 0.2f))
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
                    Terrain terrain = null;
                    if (hit.collider.TryGetComponent(out terrain))
                    {
                        float normalizedX = (transform.position.x - terrain.transform.position.x) / terrain.terrainData.size.x;
                        float normalizedZ = (transform.position.z - terrain.transform.position.z) / terrain.terrainData.size.z;
                        float[,,] splatmapData = terrain.terrainData.GetAlphamaps(Mathf.FloorToInt(normalizedX * terrain.terrainData.alphamapWidth), Mathf.FloorToInt(normalizedZ * terrain.terrainData.alphamapHeight), 1, 1);
                        string layerName = "";
                        float maxMix = 0;
                        for (int i = 0; i < terrain.terrainData.alphamapLayers; i++)
                        {
                            if (splatmapData[0, 0, i] > maxMix)
                            {
                                layerName = terrain.terrainData.terrainLayers[i].name;
                                maxMix = splatmapData[0, 0, i];
                            }
                        }
                        if (!string.IsNullOrEmpty(layerName))
                        {
                            foreach (PlayerFootstepsScriptableObject footsteps in _footsteps)
                            {
                                if (footsteps.terrainLayers.Contains(layerName, System.StringComparer.InvariantCultureIgnoreCase))
                                {
                                    PlayRandomAudio(footsteps, _controller.sprint);
                                    return;
                                }
                            }
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
