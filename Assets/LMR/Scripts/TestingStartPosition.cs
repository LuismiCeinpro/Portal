using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Testing
{
    public class TestingStartPosition : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(GameManager.instance.DetachPlayer(this));
        }
    }
}


