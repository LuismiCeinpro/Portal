using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "new player footsteps", menuName = "Portal/Player footsteps", order = 1)]
    public class PlayerFootstepsScriptableObject : ScriptableObject
    {
        [SerializeField] private string _tag;
        [SerializeField] private AudioClip[] _footsteps;
    }
}

