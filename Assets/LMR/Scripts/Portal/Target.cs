using UnityEngine;

namespace Portal
{
    // Clase que define el destino de un portal
    // Tiene que ser colocada en el objeto que define el centro
    // del destino del portal
    public class Target : MonoBehaviour
    {
        // Variable que indica la c�mara que graba el portal
        [SerializeField] private Camera _camera;
        // Variable que indica el material en el que se imprimir�
        // lo que graba la c�mara
        [SerializeField] private Material _material;
        // Propiedad que nos permite acceder a la c�mara que graba
        // el portal
        public new Camera camera { get { return _camera; } }

        private void Start()
        {
            // Creamos una nueva textura que ir� en la c�mara, acorde a resoluci�n de la pantalla
            camera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
            // Asociamos la textura al material
            _material.mainTexture = camera.targetTexture;
            // Indicamos al controlador de portales que establezca el destino
            GameManager.instance.portalManager.SetTarget(this);
        }
    }
}