using UnityEngine;
using System.Collections;

public class SetCameraBoundsTrigger : CustomTrigger {
    public Transform cameraLeftBound = null;
    public Transform cameraRightBound = null;
    public Transform cameraBottomBound = null;
    public Transform cameraTopBound = null;
    
    // Use this for initialization
    protected override void Start() {
    }
    
    // Update is called once per frame
    protected override void Update() {
    }
    
    public override void ExecuteLogic(GameObject gameObjectExecuting) {
        SetWorldBounds();
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