using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockPuzzle : MonoBehaviour
{
    private ClockHandRotation smallHand, BigHand;
    private ClockHandRotation currentHand;
    public List<int> WatchElements = new List<int> {1,2,3,4,5,6,7,8};
    public List<int> LitElements = new List<int>();
    public List<int> PossibleBigHandPositions = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
    public List<int> PossibleSmallHandPositions = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
    private void Start()
    {
        smallHand = transform.GetChild(0).transform.GetChild(0).GetComponent<ClockHandRotation>();
        BigHand = transform.GetChild(0).transform.GetChild(1).GetComponent<ClockHandRotation>();

        currentHand = BigHand;
        GenerateCode();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            currentHand.Rotate("clockwise");
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            currentHand.Rotate("counterclockwise");
        }
       else if(Input.GetKeyDown(KeyCode.Space))
        {
            if(currentHand == BigHand)
            {
                currentHand = smallHand;
            }else
            {
                currentHand = BigHand;
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            Resolve();
        }
    }

    private void Resolve()
    {
        //bool isSolved = false;
        bool isSmallHandCorrect = false;
        bool isBigHandCorrect = false;
        foreach (int number in PossibleBigHandPositions)
        {
            if (smallHand.currentlyLookingAt == number)
            {
                isSmallHandCorrect = true;
            }

        }
        foreach (int number in PossibleBigHandPositions)
        {
            if (BigHand.currentlyLookingAt == number)
            {
                isBigHandCorrect = true;
            }

        }
        if(isBigHandCorrect && isSmallHandCorrect)
        {
            //isSolved = true;
            Debug.Log("CORRECT ANSWER");
            //First Round Completed
        }
        else{
            Debug.Log("WRONG ANSWER");
            //Restart Puzzle
        }
    }

    private void GenerateCode()
    {
        bool isSolvable = false;
        litSymbols();
        isSolvable = createCode();
        while (isSolvable == false)
        {
            LitElements = new List<int>();
            WatchElements = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            litSymbols();     
            isSolvable = createCode();
        }

        Debug.Log("----------------------POSSIBLE BIGHAND POSITIONS -------------------------");
        foreach (int number in PossibleBigHandPositions)
        {
            Debug.Log(number);

        }
        Debug.Log("----------------------POSSIBLE SMALLHAND POSITIONS-------------------------");
        foreach (int number in PossibleSmallHandPositions)
        {
            Debug.Log(number);
        }
    }


    private bool createCode()
    {
        // LIT ELEMENTS DISCARD CONDITIONS
        foreach (int number in LitElements)
        {
            if (number == 1)
            {
                PossibleBigHandPositions.Remove(3);
                PossibleSmallHandPositions.Remove(6);
            }
            else if (number == 2)
            {
                PossibleBigHandPositions.Remove(7);
                PossibleSmallHandPositions.Remove(2);
            }
            else if (number == 3)
            {
                PossibleBigHandPositions.Remove(6);
                PossibleSmallHandPositions.Remove(5);
            }
            else if (number == 4)
            {
                PossibleBigHandPositions.Remove(2);
                PossibleSmallHandPositions.Remove(1);
            }
            else if (number == 5)
            {
                PossibleBigHandPositions.Remove(5);
                PossibleSmallHandPositions.Remove(4);
            }
            else if (number == 6)
            {
                PossibleBigHandPositions.Remove(1);
                PossibleSmallHandPositions.Remove(7);
            }
            else if (number == 7)
            {
                PossibleBigHandPositions.Remove(8);
                PossibleSmallHandPositions.Remove(8);
            }
            else if (number == 8)
            {
                PossibleBigHandPositions.Remove(4);
                PossibleSmallHandPositions.Remove(3);
            }
        }

        // UNLIT ELEMENTS DISCARD CONDITIONS
        foreach (int number in WatchElements)
        {
            if (number == 1)
            {
                PossibleSmallHandPositions.Remove(7);
                PossibleBigHandPositions.Remove(1);
            }
            else if (number == 2)
            {
                PossibleSmallHandPositions.Remove(1);
                PossibleBigHandPositions.Remove(8);
            }
            else if (number == 3)
            {
                PossibleSmallHandPositions.Remove(8);
                PossibleBigHandPositions.Remove(7);
            }
            else if (number == 4)
            {
                PossibleSmallHandPositions.Remove(3);
                PossibleBigHandPositions.Remove(6);
            }
            else if (number == 5)
            {
                PossibleSmallHandPositions.Remove(2);
                PossibleBigHandPositions.Remove(4);
            }
            else if (number == 6)
            {
                PossibleSmallHandPositions.Remove(5);
                PossibleBigHandPositions.Remove(2);
            }
            else if (number == 7)
            {
                PossibleSmallHandPositions.Remove(6);
                PossibleBigHandPositions.Remove(3);
            }
            else if (number == 8)
            {
                PossibleSmallHandPositions.Remove(4);
                PossibleBigHandPositions.Remove(5);
            }
        }
        bool isSolvable = true;
        if (PossibleSmallHandPositions.Count<1|| PossibleBigHandPositions.Count < 1)
        {
            isSolvable = false;
        }
        return isSolvable;
    }
    private void litSymbols()
    {
        int randomIndex;
        int litNumber;

        for (int i = 0; i < 4; i++)
        {
            randomIndex = Random.Range(0, WatchElements.Count - 1);
            litNumber = WatchElements[randomIndex];
            WatchElements.RemoveAt(randomIndex);
            LitElements.Add(litNumber);
        }
        Debug.Log("----------------------LIT ELEMENTS-------------------------");
        foreach (int number in LitElements)
        {
            Debug.Log(number);

        }
        Debug.Log("----------------------UNLIT ELEMENTS-------------------------");
        foreach (int number in WatchElements)
        {
            Debug.Log(number);
        }
    }


}