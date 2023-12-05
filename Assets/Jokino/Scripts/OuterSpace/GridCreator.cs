using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    public int gridWidth = 6;
    public int gridHeight = 15;
    //public float hexagonSize = 50.0f; // Adjust the size based on your canvas units
    public GameObject container;
    public GameObject hexagonPrefab; // Assign your hexagon prefab in the Inspector

    private void Start()
    {
        GenerateHexagonGrid();
    }

    private void GenerateHexagonGrid()
    {
        float xPos;
        float yPos;
        for (int row = 0; row < gridHeight; row++)
        {
            for (int col = 0; col < gridWidth; col++)
            {
                 xPos = col  * 0.145f;
                 yPos = row*   0.095f;

                if (row % 2 == 0)
                {
                    if (row == 0)
                    {
                        xPos = col * 0.145f;
                        yPos = row * 0.095f;
                    }
                    else
                    {
                        xPos = col * 0.145f;
                        yPos = (row * 0.095f)-(0.095f*(row/2));
                    }
                  
                   
                }
                else
                {
                    xPos = (col * 0.145f)+ (0.145f/2);
                    yPos = row * (0.095f/2);
                }

                Vector3 hexagonPosition = new Vector3(xPos, yPos, 0);

                // Create your hexagon prefab
                GameObject hexagon = Instantiate(hexagonPrefab, transform);
                hexagon.GetComponent<RectTransform>().sizeDelta = new Vector2(0.1f, 0.1f);
                GridSlot currentHexagon = hexagon.GetComponent<GridSlot>();

                // Set the hexagon's parent to the canvas
                hexagon.transform.SetParent(container.transform, false);
                currentHexagon.row = row;
                currentHexagon.rowSize = gridWidth;
                currentHexagon.col = col;
                currentHexagon.colSize = gridHeight;

                currentHexagon.CheckIfIsOnBorder(); 
                currentHexagon.CalculateNeighboringPositions();
                
                hexagon.name = "hex: " + (row+1) + "/" + (col+1);           
                // Access RectTransform of the hexagon to position it in canvas space
                RectTransform hexagonRectTransform = hexagon.GetComponent<RectTransform>();
                hexagonRectTransform.anchoredPosition3D = hexagonPosition;
            }
        }

        foreach (Transform child in container.transform)
        {
           GridSlot script = child.GetComponent<GridSlot>();
            script.createNeighborList();
        }
    }
 
}
