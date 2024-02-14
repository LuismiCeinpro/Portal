using System.Collections.Generic;
using UnityEngine;

// Clase que detectará los elementos del entorno para poder interactuar con ellos
public class PlayerInteractableDetector : MonoBehaviour
{
    [SerializeField] private Animator _crosshairAnimator;
    // Variable que indica la capa de físicas con la que se interactúa
    [SerializeField] private LayerMask _interactionLayer;
    // Variable que indica el origen desde el que se buscará
    [SerializeField] private Transform _raycastOrigin;
    // Variable que indica la distancia a la que se buscará
    [SerializeField] private float _raycastDistance;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (_raycastOrigin == null) _raycastOrigin = Camera.main.transform;
        _crosshairAnimator.gameObject.SetActive(GameManager.instance.interactionType == GameManager.InteractionType.Crosshair);
    }

    private void Update()
    {
        // Variable que almacenará el resultado del rayo
        RaycastHit hitInfo;
        if (Physics.SphereCast(_raycastOrigin.position, 0.2f, _raycastOrigin.forward, out hitInfo, _raycastDistance, _interactionLayer))
        {
            _crosshairAnimator.SetBool("Focus", true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                Gameplay.InteractableObject interactableObject;
                if (hitInfo.collider.TryGetComponent(out interactableObject)) interactableObject.Activate();
            }
        }
        else
        {
            _crosshairAnimator.SetBool("Focus", false);
        }
    }
}
