using System.Collections.Generic;
using UnityEngine;

public class CodeLineScript : MonoBehaviour
{
    private List<GemStatus> PlacedGems = new List<GemStatus>();
    public List<GemSnap> Pedestals = new List<GemSnap>();
    public List<string> CorrectGems = new List<string>();
    private int CodeLength = 4;
    private bool IsCodeCorrect;

    public delegate void EventHandler(bool correct);

    // Declare the event of type MyEventHandler
    public static event EventHandler OnFinish;

    private void Start()
    {
     foreach(GemSnap pedestal in Pedestals)
        {
            Debug.Log("test");
            pedestal.gameObject.SetActive(true);
            CorrectGems.Add(pedestal.CorrectGem);    
        }
    }

    private void OnEnable()
    {
        PlacedGems = new List<GemStatus>();
    }

    public void AddGem(GemStatus gem)
    {
        PlacedGems.Add(gem);
        if (gem.GetComponent<GemStatus>().IsCorrectlyPlaced)
        {
            int loopRemoveCorrectGem = 0;
            int removeCorrectGemIndex = -1;
            foreach (string g in CorrectGems)
            {
                if (g == gem.tag)
                {
                    removeCorrectGemIndex = loopRemoveCorrectGem;
                }
                loopRemoveCorrectGem++;
            }
            if (removeCorrectGemIndex > -1)
            {
                CorrectGems.RemoveAt(removeCorrectGemIndex);
            }
          
        }
        if (PlacedGems.Count == CodeLength)
        {

            IsCodeCorrect = true;
            foreach (GemStatus placedGem in PlacedGems)
            {
                if (!placedGem.IsCorrectlyPlaced)
                {
                    IsCodeCorrect = false;
                    int index = CorrectGems.IndexOf(placedGem.tag);
                    if (index > -1)
                    {
                        CorrectGems.RemoveAt(index);
                        placedGem.IsSomewhereElse = true;
                    }
                }
                   
            }
            foreach (GemStatus g in PlacedGems)
            {
                g.Resolve();
            }

            if (IsCodeCorrect)
            {
                // create win win function, yay
                Debug.Log("CORRECT COMBINATION");
                if (OnFinish != null)
                {
                    // Raise the event by invoking all the subscribers
                    OnFinish(true);
                }

            }
            else
            {
                // Ohno you lost function, :(
                Debug.Log("INCORRECT COMBINATION");
                if (OnFinish != null)
                {
                    // Raise the event by invoking all the subscribers
                    OnFinish(false);
                }
            }
        }
      
    }
}
