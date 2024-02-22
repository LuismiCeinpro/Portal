using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using PlasticGui;

public class WorldUICanvas : MonoBehaviour
{
    [SerializeField] private UnityEvent<GridSlot> onGridSlotClick;
    private GraphicRaycaster _raycaster;
    private PointerEventData _pointerEventData;
    private EventSystem _eventSystem;
    [SerializeField] private RectTransform _cursor;
    private GameObject _selectedGameObject;
    [SerializeField] private UnityEvent onHover;
    [SerializeField] private UnityEvent onLeave;

    void Start()
    {
        Application.targetFrameRate = 60;
        // Fetch the Raycaster from the GameObject (the Canvas)
        _raycaster = GetComponent<GraphicRaycaster>();
        // Fetch the Event System from the Scene
        _eventSystem = GetComponent<EventSystem>();
    }

    void Update()
    {

        // Set up the new Pointer Event
        _pointerEventData = new PointerEventData(_eventSystem);
        // Set the Pointer Event Position to that of the mouse position
        _pointerEventData.position = Input.mousePosition;

        // Create a list of Raycast Results
        List<RaycastResult> res = new List<RaycastResult>();

        // Raycast using the Graphics Raycaster and mouse click position
        _raycaster.Raycast(_pointerEventData, res);
        if (res.Count > 0)
        {

            _cursor.position = Vector3.MoveTowards(_cursor.position, res[0].worldPosition, Time.deltaTime * 10f);
            Vector3 cursorPos = _cursor.localPosition;
            cursorPos.z = 0;
            _cursor.localPosition = cursorPos;
        }
        // Set up the new Pointer Event
        _pointerEventData = new PointerEventData(_eventSystem);
        // Set the Pointer Event Position to that of the mouse position
        _pointerEventData.position = Input.mousePosition;

        // Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        // Raycast using the Graphics Raycaster and mouse click position
        _raycaster.Raycast(_pointerEventData, results);

        if (results.Count == 0 && _selectedGameObject)
        {
            EventTrigger trigger = _selectedGameObject.GetComponent<EventTrigger>();
            if (trigger)
            {
                foreach (EventTrigger.Entry entry in trigger.triggers)
                {
                    if (entry.eventID == EventTriggerType.PointerExit)
                    {
                        entry.callback.Invoke(new PointerEventData(_eventSystem));
                        _selectedGameObject = null;
                        return;
                    }
                }
            }
        }

        // For every result returned, output the name of the GameObject on the Canvas hit by the Ray
        foreach (RaycastResult result in results)
        {
            if (_selectedGameObject == null)
            {
                EventTrigger trigger = result.gameObject.GetComponent<EventTrigger>();
                if (trigger)
                {
                    foreach (EventTrigger.Entry entry in trigger.triggers)
                    {
                        if (entry.eventID == EventTriggerType.PointerEnter)
                        {
                            entry.callback.Invoke(new PointerEventData(_eventSystem));
                            _selectedGameObject = result.gameObject;
                            return;
                        }
                    }
                }
            }
        }
        // Check for mouse click
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Set up the new Pointer Event
            _pointerEventData = new PointerEventData(_eventSystem);
            // Set the Pointer Event Position to that of the mouse position
            _pointerEventData.position = Input.mousePosition;

            // Create a list of Raycast Results
            results = new List<RaycastResult>();

            // Raycast using the Graphics Raycaster and mouse click position
            _raycaster.Raycast(_pointerEventData, results);

            // For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.name.ToLower() != "screen"&&result.gameObject.name.ToLower() !="cursor")
                {
                    // Update the position of the cursor to match the hit position
                    Button button = null;
                    if (result.gameObject.TryGetComponent(out button)) button.onClick.Invoke();
                    Debug.Log("Hit " + result.gameObject.name);
                    int c = 1;
                    if (result.gameObject.CompareTag("HexagonTile"))
                    {
                        GridSlot slot = result.gameObject.GetComponent<GridSlot>();
                        onGridSlotClick.Invoke(slot);
                        foreach (GridSlot tile in slot.adjacentTiles)
                        {
                            Debug.Log("ADJACENT TILE " + c + " :" + tile.name);
                            c++;
                        }
                        c = 1;
                        c = 1;
                        
                    }
                   
                    if (result.gameObject.tag.ToLower() == "draggable")
                    {
                        DraggableItem draggableItemScript = result.gameObject.GetComponent<DraggableItem>();
                        //draggableItemScript.enabled = true;
                        //draggableItemScript.OnBeginDrag(cursor);
                    }else if(result.gameObject.tag.ToLower() == "hexagontile"){
                        //createNeighboringHexagonList(result);
                    }
                        
                }
            }
        }
    }
}