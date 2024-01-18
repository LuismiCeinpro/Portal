using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    //Variables Puerta y Posición Inicial de esta, "Activación" sirve para ejecutar el movmiento de la puerta. Introducir el objeto con este script en "Torch", variable Door
    public GameObject door;
    private Vector3 StartPoint;
    public bool Activation = false;
    void Start()
    {
        StartPoint.x = door.transform.position.x;
    }
    private void Update()
    {
        if (door.transform.position.x < StartPoint.x + 1.14 && Activation)
        {
            door.transform.position = new Vector3(door.transform.position.x + 0.01f, door.transform.position.y, door.transform.position.z);
            if (door.transform.position.x >= StartPoint.x + 1.14)
            {
                Activation = false;
            }
        }
    }
}
