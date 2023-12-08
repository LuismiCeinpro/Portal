// HitObjectScript.cs

using UnityEngine;

public class LaserReceiver : MonoBehaviour
{
    public bool IsCurrentlyHit = false;
    public void LaserHitFunction()
    {
        IsCurrentlyHit = true;
        GameObject originalGameObject = gameObject;
        GameObject parentGameObject = transform.parent.gameObject;
        parentGameObject.SendMessage("LaserHit", originalGameObject.name, SendMessageOptions.DontRequireReceiver);
    }
    public void LaserHitStopFunction()
    {
        IsCurrentlyHit = false;
    }
}