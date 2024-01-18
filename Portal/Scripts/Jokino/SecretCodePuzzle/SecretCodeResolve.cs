using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretCodeResolve : MonoBehaviour
{
    public List<CodeLineScript> Lines = new List<CodeLineScript>(); 
    private int lineNumber=0;
    void Start()
    {
        // Step 5: Subscribe to the event in another script
        CodeLineScript.OnFinish += MyEventHandlerMethod;
        Lines[lineNumber].enabled = true;
    }

    private void MyEventHandlerMethod(bool correct)
    {
        if (!correct)
        {
            Debug.Log("YouRs DId iT*");
     
        }
        else
        {
            int count = 0;
            foreach (PedestalStatus status in Lines[lineNumber].gameObject.transform)
            {
                Lines[lineNumber+1].gameObject.transform.GetChild(count).GetComponent<PedestalStatus>().CorrectGem=status.CorrectGem;
 
            }
            lineNumber++;
            Lines[lineNumber].enabled = true;
            
            Debug.Log("incorrect code");

        }
    }

    void OnDestroy()
    {
        // Step 7: Unsubscribe when the subscriber is destroyed to prevent memory leaks
        CodeLineScript.OnFinish -= MyEventHandlerMethod;
    }



}
