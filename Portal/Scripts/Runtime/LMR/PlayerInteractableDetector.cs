using System.Collections.Generic;
using UnityEngine;

// Clase que detectar� los elementos del entorno para poder interactuar con ellos
public class PlayerInteractableDetector : MonoBehaviour
{
    // Variable que indica la capa de f�sicas con la que se interact�a
    [SerializeField] private LayerMask _interactionLayer;
    // Variable que indica el origen desde el que se buscar�
    [SerializeField] private Transform _raycastOrigin;
    // Variable que indica la distancia a la que se buscar�
    [SerializeField] private float _raycastDistance;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (_raycastOrigin == null) _raycastOrigin = Camera.main.transform;
    }

    private void Update()
    {
        // Variable que almacenar� el resultado del rayo
        RaycastHit hitInfo;
        // Comprobamos si pulsamos la tecla e y lanzamos el rayo de tipo esfera para poder
        // abarcar elementos m�s peque�os
        if (Input.GetKeyDown(KeyCode.E) && 
            Physics.SphereCast(_raycastOrigin.position, 0.2f, _raycastOrigin.forward, 
            out hitInfo, _raycastDistance, _interactionLayer))
        {
            // Si el rayo encuentra un objeto dentro de la capa de f�sicas
            // Intentaremos recuperarlo y activarlo
            Gameplay.InteractableObject interactableObject;
            if (hitInfo.collider.TryGetComponent(out interactableObject))
                interactableObject.Activate();
        }
    }
}
