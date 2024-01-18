using Base;
using UnityEngine;

public class PickableItem : BaseInteractable
{
    // Variables referentes al controlador del jugador
    [Header("Player settings")]
    [SerializeField] public Transform PickupTarget;
    [Space]
    public Rigidbody CurrentObject = null;
    public bool IsPlaced = false;
    private int count;


    private void Start()
    {
        PickupTarget = GameObject.Find("PickupPoint").transform;

    }
    protected override void OnActivate()
    {

        if (CurrentObject)
        {
            CurrentObject.useGravity = true;
            CurrentObject = null;
        }
        else if (!CurrentObject)
        {
            Debug.Log("pickup");
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
            //CurrentObject.useGravity = true;
            //CurrentObject = null;
            //enabled = false;
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