using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleReset : MonoBehaviour
{

    public GameObject CompleteCode;
    void Start()
    {
        SecretCodeResolve.OnReset += eventHandler;
        
    }

    void ListGameObjectsWithXComponent<T>() where T : Component
    {
        T[] components = FindObjectsOfType<T>();

        foreach (T component in components)
        {
            GameObject obj = component.gameObject;
            if (obj.GetComponent<GemStatus>().IsPlaced==true)
            {
                Destroy(obj);
            }
           
        }
    }

    private void eventHandler(GameObject current)
    {
        ListGameObjectsWithXComponent<GemStatus>();
        Instantiate(CompleteCode, current.transform.position, Quaternion.identity);
        Destroy(current);
    }

    }
