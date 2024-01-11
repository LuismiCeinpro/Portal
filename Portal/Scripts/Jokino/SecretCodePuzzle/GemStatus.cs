using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemStatus : MonoBehaviour
{
    public bool IsPlaced, IsCorrectlyPlaced, IsSomewhereElse = false;

    public void Resolve()
    {
        if (IsCorrectlyPlaced == false && IsSomewhereElse == false)
        {
            Destroy(transform.gameObject);
        }
        if (IsCorrectlyPlaced)
        {
            transform.gameObject.GetComponent<Light>().intensity = 10;
        }
    }

}
