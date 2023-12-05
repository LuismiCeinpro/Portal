using UnityEngine;

namespace Portal
{
    // Clase que nos permite teletransportar al jugador
    public class Teleport : MonoBehaviour
    {
        // Método para detectar cuándo el jugador entra en el portal
        private void OnTriggerEnter(Collider other)
        {
            // Comprobamos si es el jugador en el que ha entrado en el portal
            // para teletransportarlo
            if (other.CompareTag("Player")) GameManager.instance.portalManager.Teleport();
        }
    }
}


