using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    //Variables Puerta y Posición Inicial de esta, "Activación" sirve para ejecutar el movmiento de la puerta. Introducir el objeto con este script en "Torch", variable Door
    public GameObject door = null;
    private Vector3 StartPoint;
    public bool Activation = false;
    public bool IsFlipped = false;
    public float movementSpeed = 0.01f;
    public float goalPosition = 1.14f;

    void Start()
    {
        if (!door)
        {
            door = transform.gameObject;
            
        }
        StartPoint.x = door.transform.position.x;
    }
    private void Update()
    {
        if (Activation)
        {
            if (!IsFlipped)
            {
                door.transform.position = new Vector3(door.transform.position.x - movementSpeed, door.transform.position.y, door.transform.position.z);
            }
            else
            {
                door.transform.position = new Vector3(door.transform.position.x + movementSpeed, door.transform.position.y, door.transform.position.z);
            }
            
      
        }
        if (door.transform.position.x >= StartPoint.x + goalPosition || door.transform.position.x <= StartPoint.x - goalPosition)
        {
            Activation = false;
        }
    }
}
