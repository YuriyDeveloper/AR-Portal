using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class AnotherRealityRePlace : MonoBehaviour
{ 
    [SerializeField] private GameObject _anotherReality;

    private ARRaycastManager _aRRaycastManager;
    private static List<ARRaycastHit> _hits = new List<ARRaycastHit>();
    private Vector2 _touchPosition;

   
    private void Awake()
    {
        _aRRaycastManager = GetComponent<ARRaycastManager>();
        _anotherReality.SetActive(false);
    }

  
    private void Update()
    {
        DetectTouch();
    }


    private void DetectTouch()
    {
        if (Input.touchCount>0)
        {
            _touchPosition = Input.GetTouch(0).position;
            if (_aRRaycastManager.Raycast(_touchPosition, _hits, TrackableType.PlaneWithinPolygon))
            {
                var hitPose = _hits[0].pose;
                _anotherReality.SetActive(true);
                _anotherReality.transform.position = hitPose.position;
                LookAtPlayer(_anotherReality.transform);
            }
        }
    }

    private void LookAtPlayer(Transform scene)
    {
        var LookDirection = Camera.main.transform.position - scene.position;
        LookDirection.y = 0;
        scene.rotation = Quaternion.LookRotation(-LookDirection);
    }
}
