using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "new player footsteps", menuName = "Portal/Player footsteps", order = 1)]
    public class PlayerFootstepsScriptableObject : ScriptableObject
    {
        [SerializeField] private string _tag;
        [SerializeField] private string[] _terrainLayers;
        [SerializeField] private AudioClip[] _walkFootsteps;
        [SerializeField] private AudioClip[] _runFootsteps;

        public string tag { get { return _tag; } }
        public string[] terrainLayers { get { return _terrainLayers; } }
        public AudioClip[] walkFootsteps { get {  return _walkFootsteps; } }
        public AudioClip[] runFootsteps { get { return _runFootsteps; } }
    }
}

