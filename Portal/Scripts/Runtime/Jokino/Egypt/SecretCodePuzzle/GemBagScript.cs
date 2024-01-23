using Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBagScript : BaseInteractable
{
    // Variables referentes al controlador del jugador
    [Header("Player settings")]
    [SerializeField] private Transform PickupTarget;
    [Space]

    public string color;
    public GameObject gemPrefab;

    private void Start()
    {
        PickupTarget = GameObject.Find("PickupPoint").transform;

    }
    protected override void OnActivate()
    {
        if (!GameManager.instance.IsObjectPickedUp)
        {
            GameObject pr = Instantiate(gemPrefab, PickupTarget.transform.position, Quaternion.identity);
            PickableItem currentGem = pr.GetComponent<PickableItem>();
            Rigidbody currentGemRigidbody = pr.GetComponent<Rigidbody>();
            currentGem.PickupTarget = PickupTarget;
            currentGem.CurrentObject = currentGemRigidbody;
            currentGemRigidbody.useGravity = false;
            GameManager.instance.IsObjectPickedUp = true;
        }

    }
}