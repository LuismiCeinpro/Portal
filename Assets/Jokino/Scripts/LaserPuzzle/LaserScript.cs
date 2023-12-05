using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//[RequireComponent(typeof(LineRenderer))]
public class LaserScript : MonoBehaviour
{
    [Header("Settings")]
    public LayerMask layerMask;
    public float defaultLength = 50;
    public int numOfRefelection = 1;

    private LineRenderer _lineRenderer;
    private Camera _camera;
    private RaycastHit hit;
    //private bool isLaser = false;


    private bool operational = false;
    private Ray ray;
    private Vector3 direction;
    private GameObject hitObject;
    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _camera = Camera.main;

    }


    void LaserHit()
    {
        operational = true;
    }
    // Update is called once per frame
    void Update()
    {   
        if (operational)
        {
            _lineRenderer.enabled = true;
            Reflectlaser();
        }
        else
        {
            _lineRenderer.enabled = false;
        }
        operational = false;
       

    }
    void Reflectlaser()
    {

        ray = new Ray(transform.position, transform.forward);

        _lineRenderer.positionCount = 1;
        _lineRenderer.SetPosition(0, transform.position);

        //Does the ray collide with any objects?

        float remainingLength = defaultLength;

        for (int i = 0; i < numOfRefelection; i++)
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, remainingLength, layerMask))
            {
                if( hitObject && hitObject.name != hit.collider.gameObject.name)
                {
                    hitObject.SendMessage("LaserHitStopFunction", SendMessageOptions.DontRequireReceiver);
                }
                else
                {
                    hitObject = hit.collider.gameObject;
                    hitObject.SendMessage("LaserHitFunction", SendMessageOptions.DontRequireReceiver);

                    _lineRenderer.positionCount += 1;
                    _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, hit.point);

                    remainingLength -= Vector3.Distance(ray.origin, hit.point);

                    ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
                }
     
            }
            else
            {
                _lineRenderer.positionCount += 1;
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, ray.origin + (ray.direction * remainingLength));
            }
        }

    }

    void NormalLaser()
    {
        _lineRenderer.SetPosition(0, transform.position);
        if (Physics.Raycast(transform.position, transform.forward, out hit, defaultLength, layerMask))
        {
            _lineRenderer.SetPosition(1, transform.position);
        }
        else
        {
            _lineRenderer.SetPosition(1, transform.position + (transform.forward * defaultLength));
        }
    }

}


