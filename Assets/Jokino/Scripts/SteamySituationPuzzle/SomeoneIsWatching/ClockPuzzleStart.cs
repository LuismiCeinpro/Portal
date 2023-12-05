using Base;
using UnityEngine;

public class ClockPuzzleStart : BaseInteractable
{

    // Variables referentes al controlador del jugador
    [Header("Player settings")]
    [SerializeField] private GameObject CharacterController;
    [Space]
    [Header("Camera Info")]
    [SerializeField] private GameObject MainCamera;
    [SerializeField] private GameObject PuzzleCamera;

    protected override void OnActivate()
    {
        CharacterController.SetActive(false);
        PuzzleCamera.SetActive(true);
        transform.GetChild(0).GetComponent<ClockPuzzle>().enabled = true;
    }
}
