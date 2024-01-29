using Gameplay;
using Portal;
using UnityEngine;

public class PickableItem : InteractableObject
{
    // Variables referentes al controlador del jugador
    [Header("Player settings")]
    [SerializeField] public Transform PickupTarget;
    [Space]
    public Rigidbody CurrentObject = null;
    public bool IsPlaced = false;
    private int count;
    

    protected override void OnActivate()
    {
        if (!PickupTarget) PickupTarget = GameObject.Find("PickupPoint").transform;
        if (CurrentObject)
        {
            CurrentObject.useGravity = true;
            CurrentObject = null;
            GameManager.instance.IsObjectPickedUp = false;
        }
        else if (!CurrentObject && !GameManager.instance.IsObjectPickedUp)
        {
            Debug.Log("TEST");
            Debug.Log("pickup");
            CurrentObject = transform.GetComponent<Rigidbody>();
            CurrentObject.useGravity = false;
            GameManager.instance.IsObjectPickedUp = true;
            enabled = true;
        }
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