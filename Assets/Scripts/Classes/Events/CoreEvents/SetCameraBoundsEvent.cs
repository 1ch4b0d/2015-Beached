using UnityEngine;
using System.Collections;

public class SetCameraBoundsEvent : CustomEventObject {
    public Transform cameraLeftBound = null;
    public Transform cameraRightBound = null;
    public Transform cameraBottomBound = null;
    public Transform cameraTopBound = null;
    
    // protected override void Awake() {
    //     base.Awake();
    // }
    
    // // Use this for initialization
    // protected override void Start() {
    //     base.Start();
    // }
    
    // // Update is called once per frame
    // protected override void Update() {
    //     base.Update();
    // }
    
    protected override void Initialize() {
        base.Initialize();
        
        if(cameraLeftBound == null
            && cameraRightBound == null
            && cameraBottomBound == null
            && cameraTopBound == null) {
            Debug.LogError(this.gameObject.name + " needs at least one transform reference to be set in the 'SetCameraBoundsEvent' Script");
        }
    }
    
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