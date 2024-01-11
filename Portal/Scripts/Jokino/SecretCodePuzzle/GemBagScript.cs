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

    protected override void OnActivate()
    {
        GameObject pr = Instantiate(gemPrefab, PickupTarget.transform.position, Quaternion.identity);
        PickableItem currentGem = pr.GetComponent<PickableItem>();
        Rigidbody currentGemRigidbody = pr.GetComponent<Rigidbody>();
        currentGem.PickupTarget = PickupTarget;
        currentGem.CurrentObject = currentGemRigidbody;
        currentGemRigidbody.useGravity = false;
    }
}