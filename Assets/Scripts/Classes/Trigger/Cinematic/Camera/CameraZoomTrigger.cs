using UnityEngine;
using System.Collections;

public class CameraZoomTrigger : EventTrigger {
    public GoTween currentTween = null;
    public Camera cameraToZoom = null;
    public float zoomDuration = 1f;
    public float startZoomSize = 5f;
    public float endZoomSize = 8f;
    
    public CustomEventsManager onZoomStartFinish = null;
    public CustomEventsManager onZoomEndFinish = null;
    
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
        
        if(cameraToZoom == null) {
            Debug.LogError("The 'cameraToZoom' is null, and must be set to a reference for the object: " + this.gameObject.name);
        }
    }
    
    public override void Entered(GameObject gameObjectEntering) {
        FireEnterEvents();
        StartZoom();
    }
    
    public override void Stay(GameObject gameObjectStaying) {
        FireStayEvents();
    }
    
    public override void Exited(GameObject gameObjectExiting) {
        FireExitEvents();
        EndZoom();
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
    
    public void StartZoom() {
        if(currentTween != null) {
            currentTween.destroy();
            currentTween = null;
        }
        
        currentTween = Go.to(cameraToZoom,
                             zoomDuration,
                             new GoTweenConfig().floatProp("orthographicSize", endZoomSize).setEaseType(GoEaseType.Linear)
        .onComplete(complete => {
            FireOnZoomStartFinish();
        }));
    }
    
    public void EndZoom() {
        if(currentTween != null) {
            currentTween.destroy();
            currentTween = null;
        }
        currentTween = Go.to(cameraToZoom,
                             zoomDuration,
                             new GoTweenConfig().floatProp("orthographicSize", startZoomSize).setEaseType(GoEaseType.Linear)
        .onComplete(complete => {
            FireOnZoomEndFinish();
        }));
    }
    
    public void FireOnZoomStartFinish() {
        if(onZoomStartFinish != null) {
            onZoomStartFinish.Execute();
        }
    }
    
    public void FireOnZoomEndFinish() {
        if(onZoomEndFinish != null) {
            onZoomEndFinish.Execute();
        }
    }
}