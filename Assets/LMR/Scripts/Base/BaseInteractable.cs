using UnityEngine;
// Necesario para el manejo de eventos
using UnityEngine.Events;

// Nombre de espacio para todas las clases bases
namespace Base
{
    // Clase principal para cualquier tipo de objeto con el que el
    // jugador puede interactuar
    public abstract class BaseInteractable : MonoBehaviour
    {
        // Evento que se lanza cuando el jugador interactúa con
        // el objeto
        [SerializeField] private UnityEvent<BaseInteractable> _onActivate;

        // Método que activa el objeto que tendrán que implementar
        // todos los tipos de objetos
        public void Activate()
        {
            // Lanzamos el evento al activar el objeto
            _onActivate?.Invoke(this);
            // Lanzamos el método que se podrá controlar en las clase
            // hijas de BaseInteractable
            OnActivate();
        }

        // Método abstracto que tendrán que implementar todas las clases
        protected abstract void OnActivate();
    }
}