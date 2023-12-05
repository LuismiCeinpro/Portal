using UnityEngine;

// Nombre de espacio para gestionar las clases referentes
// al control de los portales
namespace Portal
{
    // Clase que controla el comportamiento de los portales
    public class Manager : MonoBehaviour
    {
        // Variable que contiene el activador del portal
        private Origin _origin;
        // Variable que contiene el destino del portal
        private Target _target;
        
        // Método que establece el origen del portal
        public void SetOrigin(Origin origin)
        {
            // Guardamos el origen del portal
            _origin = origin;
        }

        // Método para establecer el destino del portal
        public void SetTarget(Target target)
        {
            // Guardamos el destion del portal
            _target = target;
        }

        // Método que nos permite teletransportarnos al destino
        public void Teleport()
        {
            // Evitamos teletransportarnos si no hay destino
            if (!_target) return;
            // Obtenemos la distancia entre el jugador y el plano del portal
            Vector3 portalToPlayer = _origin.player.transform.position - _origin.portalOrigin.transform.position;
            // Utilizamos el método Dot para saber si estamos accediendo desde detrás del portal o desde deltante
            float dotProduct = Vector3.Dot(_origin.portalOrigin.transform.up, portalToPlayer);
            // Si el valor es negativo, se entiende que lo estamos atravesando por detrás
            if (dotProduct < 0) return;
            // Obtenemos la diferencia de rotación entre el portal de origen y de destino
            float rotationDifference = -Quaternion.Angle(_origin.portalOrigin.transform.rotation, _target.transform.rotation);
            // Desactivamos el control del jugador para poder cambiar su posición y rotación
            _origin.player.enabled = false;
            // Actualizamos la rotación del jugador
            _origin.player.transform.Rotate(Vector3.up, rotationDifference);
            // Obtenemos la compensación de la posición con respecto al centro del portal
            Vector3 positionOffset = Quaternion.Euler(0, rotationDifference, 0) * portalToPlayer;
            // Actualizamos la posición del jugador en el portal de destino
            _origin.player.transform.position = _target.transform.position + positionOffset;
            // Reactivamos el jugador
            _origin.player.enabled = true;
        }

        private void Update()
        {
            // Si no hay un destino, no hacemos nada
            if (!_target) return;
            // Si hay un destino, obtenemos la distancia entre el portal de origen y el jugador
            Vector3 playerOffsetFromPortal = _origin.playerCamera.transform.position - _origin.portalOrigin.position;
            // Actualizamos la posición de la cámara del portal de destino añadiendo la distancia existente
            // entre el jugador y el portal de origen al lugar en el que el jugador debe aparecer
            _target.camera.transform.position = _target.transform.position + playerOffsetFromPortal;
            // Obtenemos la diferencia angular entre el portal de origen y el de destino
            float angularDifferenceBetweenPortalRotations = 
                Quaternion.Angle(_target.transform.rotation, _origin.portalOrigin.transform.rotation);
            Quaternion portalRotationDifference = 
                Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);
            // Creamos una dirección a la que tiene que apuntar la cámara de destino en el caso de que los
            // portales no estén orientados en la misma dirección
            Vector3 targetCameraDirection = portalRotationDifference * _origin.playerCamera.forward;
            // Aplicamos la nueva rotación a la cámara
            _target.camera.transform.rotation = Quaternion.LookRotation(targetCameraDirection, Vector3.up);
        }
    }
}
