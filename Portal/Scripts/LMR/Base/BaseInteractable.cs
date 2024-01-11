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
        // Evento que se lanza cuando el jugador interact�a con
        // el objeto
        [SerializeField] private UnityEvent<BaseInteractable> _onActivate;

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

        // M�todo abstracto que tendr�n que implementar todas las clases
        protected abstract void OnActivate();
    }
}