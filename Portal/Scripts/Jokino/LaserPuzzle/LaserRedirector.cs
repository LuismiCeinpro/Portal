using UnityEngine;

public class LaserRedirector : MonoBehaviour
{
    private GameObject originalGameObject;
    private GameObject LaserExit;
    private GameObject LaserExitCaster;
    private GameObject LaserEntry;
    private GameObject LaserEntryCaster;


    // Child Order:
    // South:0
    // SouthWest:1
    // SouthEast:2
    // Est:3
    // East:4
    // North:5
    // NorthWest:6
    // NorthEast:7
    void Start()
    {
        originalGameObject = gameObject;

        LaserEntry = originalGameObject.transform.GetChild(1).gameObject;
        LaserEntryCaster = LaserEntry.transform.GetChild(0).gameObject;
    }

   public void LaserHit(string receiver)
    {

        if (receiver == "South")
        {
            LaserExit = originalGameObject.transform.GetChild(6).gameObject;
            LaserExitCaster = LaserExit.transform.GetChild(0).gameObject;
            LaserExitCaster.SendMessage("LaserHit", SendMessageOptions.DontRequireReceiver);
            //LaserEntryCaster.SendMessage("LaserHitStop", SendMessageOptions.DontRequireReceiver);
        }
         if (receiver == "SouthWest")
        {
            LaserExit = originalGameObject.transform.GetChild(4).gameObject;
            LaserExitCaster = LaserExit.transform.GetChild(0).gameObject;

            LaserExitCaster.SendMessage("LaserHit", SendMessageOptions.DontRequireReceiver);
            //LaserExitCaster.SendMessage("LaserHitStop", SendMessageOptions.DontRequireReceiver);
        }
        if (receiver == "SouthEast")
        {
            LaserExit = originalGameObject.transform.GetChild(5).gameObject;
            LaserExitCaster = LaserExit.transform.GetChild(0).gameObject;

            LaserExitCaster.SendMessage("LaserHit", SendMessageOptions.DontRequireReceiver);
            //LaserExitCaster.SendMessage("LaserHitStop", SendMessageOptions.DontRequireReceiver);

        }
    }
}
