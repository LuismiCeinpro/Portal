// Importamos nuestro nombre de espacio para clases base
using Base;
using System.Collections;
using UnityEngine;
// Necesario para saber cuándo una nueva escena se ha cargado
using UnityEngine.SceneManagement;

// Nombre de espacio para gestionar las clases referentes
// al control de los portales
namespace Portal
{
    // Clase que indica el origen de un portal
    public class Origin : BaseInteractable
    {
        // Variables referentes al controlador del jugador
        [Header("Player settings")]
        [SerializeField] private CharacterController _player;
        // Propiedades de solo lectura accesibles desde el controlador de portales
        public CharacterController player { get { return GameManager.instance.player.controller; } }
        public Transform playerCamera { get { return GameManager.instance.player.camera.transform; } }
        // Variables referentes a la configuración del portal
        [Header("Portal settings")]
        // Variable que indica el centro del portal
        [SerializeField] private Transform _portalOrigin;
        // Variable que indica la escena a cargar con el portal
        [SerializeField] private string _portalScene;
        // Propiedades de solo lectura accesibles desde el controlador de portales
        public Transform portalOrigin { get { return _portalOrigin; } }
        // Variable que indica si se ha de cargar 
        private bool _hasToLoadPortal;

        // Sobrescribimos el método que se lanza al activar el objeto
        protected override void OnActivate()
        {
            // Indicamos al controlador de juego que vamos a abrir un portal
            GameManager.instance.portalManager.SetOrigin(this);
            // Indicamos al manejador de escenas que vamos a controlar
            // cuando se ha cargado una escena
            SceneManager.sceneLoaded += OnSceneLoaded;
            // Indicamos que se tiene que cargar el portal
            _hasToLoadPortal = true;
        }

        // Método que se ejecuta cuando se ha cargado la escena
        private void OnSceneLoaded(Scene scene, LoadSceneMode loadMode)
        {
            // Ponemos la escena nueva como escena activa
            SceneManager.SetActiveScene(scene);
            // Indicamos que no tenemos que recibir notificación
            // cuando se cargue una nueva escena
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        // Corrutina que carga una escena de manera asíncrona
        private IEnumerator LoadScene()
        {
            // Indicamos que la nueva escena se cargue de modo asíncrono,
            // añadiéndose a la escena actual
            yield return SceneManager.LoadSceneAsync(_portalScene, LoadSceneMode.Additive);
        }

        private void Update()
        {
            // Verificamos si tenemos que cargar un portal
            if (!_hasToLoadPortal) return;
            // Para no poner cargas en cola, indicamos que no hay que cargar
            // ningún portal
            _hasToLoadPortal = false;
            // Lanzamos la corrutina de carga de la escena
            StartCoroutine(LoadScene());
        }
    }
}