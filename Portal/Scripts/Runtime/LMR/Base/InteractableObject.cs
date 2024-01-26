using Gameplay;
using System.Collections.Generic;
using UnityEngine;
// Necesario para el manejo de eventos
using UnityEngine.Events;

// Nombre de espacio para todas las clases bases
namespace Gameplay
{
    // Clase principal para cualquier tipo de objeto con el que el
    // jugador puede interactuar
    public class InteractableObject : MonoBehaviour
    {
        [SerializeField] private List<InventoryItemScriptableObject> _itemsToRequest;
        [Space]
        // Evento que se lanza cuando el jugador interact�a con
        // el objeto
        [SerializeField] private UnityEvent<InteractableObject> _onActivate;
        [Space]
        [SerializeField] private UnityEvent<InteractableObject> _onCorrectItemSelected;
        [Space]
        [SerializeField] private UnityEvent<InteractableObject> _onIncorrectItemSelected;

        public List<InventoryItemScriptableObject> itemsToRequest { get { return _itemsToRequest; } }

        // M�todo que activa el objeto que tendr�n que implementar
        // todos los tipos de objetos
        public void Activate()
        {
            // Lanzamos el evento al activar el objeto
            _onActivate?.Invoke(this);
            // Lanzamos el m�todo que se podr� controlar en las clase
            // hijas de BaseInteractable
            OnActivate();
        }

        // M�todo que opcionalmente pueden implementar todas las clases
        protected virtual void OnActivate() { }

        public virtual void OnCorrectItemSelected() 
        { 
            _onCorrectItemSelected.Invoke(this);
        }

        public virtual void OnIncorrectItemSelected() 
        { 
            _onIncorrectItemSelected.Invoke(this);
        }
    }
}