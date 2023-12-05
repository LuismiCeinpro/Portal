using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockHandRotation : MonoBehaviour
{
    private float currentRotartion = 0;
    public int currentlyLookingAt  = 1;
    public List<GameObject> WatchElements = new List<GameObject>{};
    public void Rotate(string direction)
    {
        if (direction.ToLower() == "clockwise")
        {
            transform.Rotate(0, -45, 0);
            currentRotartion -= 45;
           
            if (currentlyLookingAt == 8) {
                currentlyLookingAt = 1; 
            }
            else
            {
                currentlyLookingAt += 1; 
            }
        }
        else
        {
            transform.Rotate(0, 45, 0);
            currentRotartion += 45;
            if (currentlyLookingAt == 1)
            {
                currentlyLookingAt = 8;
            }
            else
            {
                currentlyLookingAt -= 1;
            }
        }
        
    }
    
}
