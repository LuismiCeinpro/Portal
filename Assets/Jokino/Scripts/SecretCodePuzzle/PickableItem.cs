using Base;
using UnityEngine;

public class PickableItem : BaseInteractable
{

    // Variables referentes al controlador del jugador
    [Header("Player settings")]
    [SerializeField] public Transform PickupTarget;
    [Space]
    public Rigidbody CurrentObject;
    public bool IsPlaced = false;

    protected override void OnActivate()
    {
        Debug.Log("test");
        if (CurrentObject)
        {
            CurrentObject.useGravity = true;
            CurrentObject = null;
            return;
        }
        else
        {
            CurrentObject = transform.GetComponent<Rigidbody>();
            CurrentObject.useGravity = false;
            enabled = true;
        }

    }
    void Update()
    {
        if (CurrentObject)
        {
                if (IsPlaced)
                {
                    CurrentObject = null;
                    return;
                }   
          
        }
        if (Input.GetKeyDown(KeyCode.E) && CurrentObject)
        {
            CurrentObject.useGravity = true;
            CurrentObject = null;
            enabled = false;
            return;
        }
    }

    void FixedUpdate()
    {
        if (CurrentObject)
        {
            Vector3 DirectionToPoint = PickupTarget.position - CurrentObject.position;
            float DistanceToPoint = DirectionToPoint.magnitude;

            CurrentObject.velocity = DirectionToPoint * 12f * DistanceToPoint;
        }
    }
}