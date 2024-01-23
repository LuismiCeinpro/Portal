using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public string listenToTag = "Comp";
    public bool IsFull = false;
    public string CorrectItem;
    private GameObject father;
    private int Count = 0;
    public Door Door;

    public delegate void EventHandler(string current);

    public static event EventHandler OnComplete;

    public void Awake()
    {
        father = transform.parent.gameObject;
        Debug.Log(transform.parent.name + " " + " " + CorrectItem);
    }
    private void Place(Rigidbody rb, GameObject Item)
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.rotation = Quaternion.identity;
        Item.transform.position = transform.position;
        Item.layer = LayerMask.NameToLayer("Default");
        Item.GetComponent<BoxCollider>().enabled = false;
        Item.GetComponent<Rigidbody>().useGravity = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        //La forma correcta: El script del objeto tenga una variable que detecte si esta conectada
        //Comprobar tag de objeto
        GameObject Item = other.gameObject;
        string ItemName = Item.name;
        Debug.Log("OnTrigger");

        //Comprobar si el objeto a snappear es el correcto en base a su name y el valor de count (0 palos, 1 bandeja, 2 leña)
        if (!IsFull)
        {
            if (Item.CompareTag("Torch"))
            {
                Rigidbody rb = other.GetComponent<Rigidbody>();
                if (ItemName == "Legs" && Count == 0)
                {
                    Place(rb, Item);
                    Count++;
                    Debug.Log("Patas colocadas");
                    if (Item.GetComponent<PickableItem>())
                    {
                        Item.GetComponent<PickableItem>().IsPlaced = true;
                    }
                }
                else if (ItemName == "Plate" && Count == 1)
                {
                    Count++;
                    Place(rb, Item);
                    Debug.Log("Bandeja puesta");
                    if (Item.GetComponent<PickableItem>())
                    {
                        Item.GetComponent<PickableItem>().IsPlaced = true;
                    }
                }
                else if (ItemName == "Wood" && Count == 2)
                {
                    Place(rb, Item);
                    IsFull = true;
                    Debug.Log("Leña colocada");
                    Door.Activation = true;
                    if (Item.GetComponent<PickableItem>())
                    {
                        Item.GetComponent<PickableItem>().IsPlaced = true;
                    }
                    OnComplete("");
                }
            }
        }
    }
}