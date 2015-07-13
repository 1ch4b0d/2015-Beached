using UnityEngine;
using System.Collections;

public class CameraFollowTrigger : EventTrigger {
    public GoTween currentTween = null;
    public CameraFollow cameraFollow = null;
    public float zoomDuration = 1f;
    public float startZoomSize = 5f;
    public float endZoomSize = 8f;
    
    // Use this for initialization
    // protected override void Awake() {
    //     base.Awake();
    // }
    // // Use this for initialization
    // protected override void Start() {
    // }
    // // Update is called once per frame
    // protected override void Update() {
    // }
    
    protected override void Initialize() {
        base.Initialize();
        
        if(cameraFollow == null) {
            Debug.LogError("The 'cameraFollow' is null, and must be set to a reference for the object: " + this.gameObject.name);
        }
    }
    
    public override void Entered(GameObject gameObjectEntering) {
        FireEnterEvents();
    }
    
    public override void Stay(GameObject gameObjectStaying) {
        FireStayEvents();
    }
    
    public override void Exited(GameObject gameObjectExiting) {
        FireExitEvents();
    }
}