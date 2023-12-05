using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;


public class DraggableItem : MonoBehaviour, IDragHandler, IBeginDragHandler
{
     Image image;
    [HideInInspector] public Transform parentAfterDrag;
    public GameObject cursor;
    private void Awake()
    {
        image = transform.GetComponent<Image>();
    }

    public void OnEndDrag()
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
        StopAllCoroutines();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Draggin'");
        Vector3 pos = cursor.GetComponent<RectTransform>().position;
        //pos.z = 0;
        transform.position = pos;

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var canvas = transform.parent;
        if (!canvas.GetComponent<Canvas>())
        {
            return;
        }
           

        parentAfterDrag = transform.parent;
        //transform.SetParent(transform.root);
        //transform.SetAsLastSibling();
        //image.raycastTarget = false;
        Debug.Log("dragBegin");
    }
}
