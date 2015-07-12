using UnityEngine;
using System.Collections;

public class CameraFollowTrigger : CustomTrigger {
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
    
    // public override void Execute(GameObject gameObjectToExecute) {
    //     // Perform only if it's the first iteration, or it should loop
    //     if(currentIteration < 1
    //         || loop) {
    //         ExecuteLogic(gameObjectToExecute);
    //         currentIteration++;
    //         if(!loop) {
    //             Collider2D attachedCollider = this.GetComponent<Collider2D>();
    //             if(attachedCollider != null) {
    //                 attachedCollider.enabled = false;
    //             }
    //         }
    //     }
    // }
    
    // public override void ExecuteLogic(GameObject gameObjectExecuting) {
    // }
}