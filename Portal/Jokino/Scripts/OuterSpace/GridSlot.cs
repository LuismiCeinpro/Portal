using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class GridSlot : MonoBehaviour, IDropHandler
{
    public int row;
    public int rowSize;
    public int col;
    public int colSize;
    public bool IsOnBorder = false;
    public List<int> adjacentTileChildPositions = new List<int>();
    public List<GameObject> adjacentTiles = new List<GameObject>();
    public GameObject HexGrid;
    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
            draggableItem.parentAfterDrag = transform;
        }
    }

    public void CalculateNeighboringPositions()
    {
        int SiblingIndex = transform.GetSiblingIndex();
        
        if ((row+1)%2==0&& IsOnBorder == false) //For Tiles with Even Row number
        {
            adjacentTileChildPositions.Add(SiblingIndex + (rowSize*2)); // Top Tile
            adjacentTileChildPositions.Add(SiblingIndex + (rowSize+1));  // Right-Top Tile
            adjacentTileChildPositions.Add(SiblingIndex - (rowSize-1));  // Right-Bottom Tile
            adjacentTileChildPositions.Add(SiblingIndex - (rowSize * 2)); // Bottom Tile        
            adjacentTileChildPositions.Add(SiblingIndex - (rowSize));  // Left-Bottom Tile       
            adjacentTileChildPositions.Add(SiblingIndex + (rowSize));  // Left-Top Tile
        }
        else if((row + 1) % 2 != 0 && IsOnBorder == false)//For Tiles with Odd Row number
        {
            adjacentTileChildPositions.Add(SiblingIndex + (rowSize * 2)); // Top Tile
            adjacentTileChildPositions.Add(SiblingIndex + (rowSize));  // Top-Right Tile
            adjacentTileChildPositions.Add(SiblingIndex - (rowSize));  // Bottom-Right Tile
            adjacentTileChildPositions.Add(SiblingIndex - (rowSize* 2)); // Bottom Tile        
            adjacentTileChildPositions.Add(SiblingIndex - (rowSize+1));  // Bottom-Left Tile       
            adjacentTileChildPositions.Add(SiblingIndex + (rowSize-1));  // Top-Left Tile
        }

       
    }

    public void CheckIfIsOnBorder()
    {
        if (row + 1 <= 2)
        {
            IsOnBorder = true;
            transform.GetComponent<Image>().color = new Color32(0, 0, 0, 255);
        }
        else if ((row + 1) % 2 == 0&& (col+1)==rowSize)
        {
            IsOnBorder = true;
            transform.GetComponent<Image>().color = new Color32(0, 0, 0, 255);
        }
        else if ((row + 1) >= (colSize - 1))
        {
            IsOnBorder = true;
            transform.GetComponent<Image>().color = new Color32(0, 0, 0, 255);

        } else if((row + 1) % 2 != 0 && (col+1) == 1)
        {
            IsOnBorder = true;
            transform.GetComponent<Image>().color = new Color32(0, 0, 0, 255);
        }
    }
    public void createNeighborList()
    {
        HexGrid = transform.parent.gameObject;
        foreach (int position in adjacentTileChildPositions)
        {
            if (position > 0 && position < rowSize * colSize - 1&& (HexGrid.transform.GetChild(position).transform.GetComponent<GridSlot>().IsOnBorder==false))
            {
                adjacentTiles.Add(HexGrid.transform.GetChild(position).transform.gameObject);
            }
        }
    }
}


