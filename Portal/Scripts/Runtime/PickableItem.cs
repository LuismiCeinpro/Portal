using Gameplay;
using Portal;
using UnityEngine;
using UnityEngine.Events;

public class PickableItem : InteractableObject
{
    // Variables referentes al controlador del jugador
    [Header("Player settings")]
    [SerializeField] public Transform PickupTarget;
    [Space]
    [SerializeField] private UnityEvent<PickableItem> _onRelease;
    [Space]
    [SerializeField] private UnityEvent<PickableItem> _onTake;
    public Rigidbody CurrentObject = null;
    public bool IsPlaced = false;

    protected override void OnActivate()
    {
        if (!PickupTarget) PickupTarget = GameObject.Find("PickupPoint").transform;
        if (CurrentObject) Release(true);
        else if (!CurrentObject && !GameManager.instance.IsObjectPickedUp) Take(true);
    }

    public void Release(bool fireOnRealeaseEvent)
    {
        CurrentObject.useGravity = true;
        CurrentObject = null;
        GameManager.instance.IsObjectPickedUp = false;
        _onRelease?.Invoke(this);
    }

    public void Take(bool fireOnTakeEvent)
    {
        CurrentObject = transform.GetComponent<Rigidbody>();
        CurrentObject.useGravity = false;
        GameManager.instance.IsObjectPickedUp = true;
        enabled = true;
        _onTake?.Invoke(this);
    }

    void Update()
    {
        if (CurrentObject)
        {
            if (IsPlaced)
            {
                GameManager.instance.IsObjectPickedUp = false;
                CurrentObject = null;
                return;
            }
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