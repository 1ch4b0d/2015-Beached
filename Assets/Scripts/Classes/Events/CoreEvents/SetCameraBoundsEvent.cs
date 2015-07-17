using UnityEngine;
using System.Collections;

public class SetCameraBoundsEvent : CustomEventsManager {
    public Transform cameraLeftBound = null;
    public Transform cameraRightBound = null;
    public Transform cameraBottomBound = null;
    public Transform cameraTopBound = null;
    
    // public override void Awake() {
    //     base.Awake();
    // }
    
    // // Use this for initialization
    // public override void Start() {
    //     base.Start();
    // }
    
    // // Update is called once per frame
    // public override void Update() {
    //     base.Update();
    // }
    
    public override void Execute() {
        SetWorldBounds();
    }
    
    public void SetWorldBounds() {
        CameraWorldBoundLock mainCameraWorldBoundLock = CameraManager.Instance.GetCameraWorldBoundLock();
        
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