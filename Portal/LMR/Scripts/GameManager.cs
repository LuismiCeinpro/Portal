// Importamos el nombre de espacio de los portales
using Gameplay;
using Portal;
using System.Collections;
using Testing;
using UnityEngine;
using UnityEngine.SceneManagement;

// Clase que controla el juego
public class GameManager : MonoBehaviour
{
    [SerializeField] public string _testScene;
    [SerializeField] private Transform _playerContainer;
    [SerializeField] private Player _player;
    // Variable que contiene el controlador de portales
    [SerializeField] private Manager _portalManager;
    // Propiedad que permite acceder al controlador de portales
    public Manager portalManager { get { return _portalManager; } }
    public Player player { get { return _player; } }
    // Propiedad que permite acceder al GameManager
    public static GameManager instance { get; private set; }
    public InteractionType interactionType { get; private set; }

    // Inicialización de la clase
    private void Awake()
    {
        // Comprobamos que no exista una instancia previa
        if (instance == null)
        {
            // Indicamos que la instancia estática es la instancia
            // actual
            instance = this;
            // Indicamos que el objeto no se destruya con el cambio de escenas
            DontDestroyOnLoad(gameObject);
            if (!string.IsNullOrEmpty(_testScene))
            {
                SceneManager.LoadScene(_testScene);
            }
        }
        // Si existe una instancia previa, destruímos la actual
        else Destroy(gameObject);
    }

    public void SetInteractionType(InteractionType type)
    {
        interactionType = type;
    }
    
    public IEnumerator DetachPlayer(TestingStartPosition testing)
    {
        _playerContainer.position = testing.transform.position;
        _playerContainer.rotation = testing.transform.rotation;
        while (_playerContainer.childCount > 0)
        {
            _playerContainer.GetChild(0).SetParent(null);
            yield return null;
        }
    }

    public enum InteractionType
    {
        Outline,
        Crosshair,
        Both,
        None
    }
}
