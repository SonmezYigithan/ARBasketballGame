using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TapToPlaceScripts : MonoBehaviour
{
    ARRaycastManager raycastManager;
    GameObject spawnedObject;

    [SerializeField]
    GameObject PlacedPrefab;
    // detect a plane 
    //if there is a plane we can add objects

    static List<ARRaycastHit> HitList = new List<ARRaycastHit>();

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if(Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }

    void Update()
    {
        if(!TryGetTouchPosition(out Vector2 touchPosition))
        {
            return;
        }

        if (raycastManager.Raycast(touchPosition, HitList, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = HitList[0].pose;

            if(spawnedObject == null)
            {
                //if there is no object placed
                spawnedObject = Instantiate(PlacedPrefab, hitPose.position, hitPose.rotation);
            }
            else
            {
                //if there is alreaday an object spawned
                spawnedObject.transform.position = hitPose.position;
                spawnedObject.transform.rotation = hitPose.rotation;
            }
        }
    }
}
