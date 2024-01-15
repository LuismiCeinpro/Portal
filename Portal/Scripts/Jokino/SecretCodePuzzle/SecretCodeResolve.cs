using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretCodeResolve : MonoBehaviour
{
    public List<CodeLineScript> Lines = new List<CodeLineScript>();
    void Start()
    {
        // Step 5: Subscribe to the event in another script
        CodeLineScript.OnFinish += MyEventHandlerMethod;
        Lines[0].enabled = true;
    }

    private void MyEventHandlerMethod(bool correct)
    {
        if (correct)
        {
            Debug.Log("incorrect code");
        }
        else
        {
            Debug.Log("YouRs DId iT*");
        }
    }

    void OnDestroy()
    {
        // Step 7: Unsubscribe when the subscriber is destroyed to prevent memory leaks
        CodeLineScript.OnFinish -= MyEventHandlerMethod;
    }



}
