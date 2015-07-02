using UnityEngine;
using System.Collections;

public class PanAndSetCameraBounds : PanFromTriggerToMarker {
    public Transform cameraLeftBound = null;
    public Transform cameraRightBound = null;
    public Transform cameraBottomBound = null;
    public Transform cameraTopBound = null;
    // Use this for initialization
    void Start() {
    }
    
    // Update is called once per frame
    void Update() {
    }
    
    public override void OnPanStart() {
    }
    
    public override void OnPanFinish() {
        SetWorldBounds();
        // mainCameraWorldBoundLock.minimumPosition = new Vector2(triggerTransform.position.x, mainCameraWorldBoundLock.minimumPosition.y);
        // mainCameraWorldBoundLock.maximumPosition = new Vector2(rightWorldBorderXPosition, mainCameraWorldBoundLock.maximumPosition.y);
    }
    
    public void SetWorldBounds() {
        CameraWorldBoundLock mainCameraWorldBoundLock = CameraManager.Instance.CameraWorldBoundLock();
        
        Vector2 newMinimumPosition = mainCameraWorldBoundLock.minimumPosition;
        Vector2 newMaximumPosition = mainCameraWorldBoundLock.maximumPosition;
        
        if(cameraLeftBound != null) {
            newMinimumPosition.x = cameraLeftBound.position.x;
        }
        if(cameraRightBound != null) {
            newMaximumPosition.x = cameraRightBound.position.x;
        }
        if(cameraBottomBound != null) {
            newMinimumPosition.y = cameraBottomBound.position.y;
        }
        if(cameraTopBound != null) {
            newMinimumPosition.y = cameraTopBound.position.y;
        }
        
        mainCameraWorldBoundLock.minimumPosition = newMinimumPosition;
        mainCameraWorldBoundLock.maximumPosition = newMaximumPosition;
    }
}