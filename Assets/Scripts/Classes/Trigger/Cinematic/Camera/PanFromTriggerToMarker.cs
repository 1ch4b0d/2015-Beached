using UnityEngine;
using System.Collections;

public class PanFromTriggerToMarker : EventTrigger {
    public GoTween currentTween = null;
    public GameObject cameraToPan = null;
    public float panDuration = 1f;
    public float delay = 0f;
    public Transform markerPosition = null;
    public bool cameraFollowOnFinished = false;
    public GoEaseType panInEasingType = GoEaseType.Linear;
    public GoEaseType panOutEasingType = GoEaseType.Linear;
    
    // Events
    public CustomEventsManager onPanInFinishEvents = null;
    public CustomEventsManager onPanOutFinishEvents = null;
    
    // Use this for initialization
    protected override void Start() {
    }
    
    // Update is called once per frame
    protected override void Update() {
    }
    
    protected override void Initialize() {
        if(cameraToPan == null) {
            Debug.LogError("The 'cameraToPan' must be set for the 'PanFromTriggerToMarker' component of: " + this.gameObject.name);
        }
    }
    
    public override void Entered(GameObject gameObjectEntering) {
        base.Entered(gameObjectEntering);
        PanIn();
    }
    
    public override void Exited(GameObject gameObjectExiting) {
        base.Exited(gameObjectExiting);
        PanOut();
    }
    
    public override void ExecuteLogic(GameObject gameObjectExecuting) {
        // gameObjectExecuting.transform.position = markerPosition.position;
        
        // Player player = gameObjectExecuting.GetComponent<Player>();
        // player.ToggleAcrocatic(gameObjectExecuting, false);
        // player.ZeroOutVelocity();
        
        // Target Position including the camera's follow offset so that it doesn't jutter
        // Vector3 targetPosition = markerPosition.position;
        // targetPosition += CameraManager.Instance.CameraFollow().GetFollowOffsetVector3();
        // targetPosition.z = CameraManager.Instance.GetMainCamera().transform.position.z;
        
        // Debug.Log("Panning~~~");
        //On Tween Finish
        // Go.to(CameraManager.Instance.GetMainCamera().transform,
        // panDuration,
        // new GoTweenConfig().position(targetPosition).setEaseType(GoEaseType.BackIn)
        // .onComplete(complete => {
        // Debug.Log("Completed~~~");
        // CameraFollow mainCameraFollow = CameraManager.Instance.CameraFollow();
        
        // player.ToggleAcrocatic(gameObjectExecuting, true);
        
        // mainCameraFollow.enabled = cameraFollowOnFinished;
        
        // FireFinishEvents();
        // }));
    }
    
    public void PanIn() {
        if(currentTween != null) {
            currentTween.destroy();
            currentTween = null;
        }
        
        Vector3 targetPosition = markerPosition.position;
        // targetPosition += CameraManager.Instance.CameraFollow().GetFollowOffsetVector3();
        targetPosition.z = cameraToPan.transform.position.z;
        
        Debug.Log("Panning~~~");
        //On Tween Finish
        currentTween = Go.to(cameraToPan.transform,
                             panDuration,
                             new GoTweenConfig().position(targetPosition).setEaseType(panInEasingType)
        .onComplete(complete => {
            Debug.Log("Completed~~~");
            // player.ToggleAcrocatic(gameObjectExecuting, true);
            // mainCameraFollow.enabled = cameraFollowOnFinished;
            FireFinishEvents();
        }));
    }
    
    public void PanOut() {
        if(currentTween != null) {
            currentTween.destroy();
            currentTween = null;
        }
    }
    public virtual void FirePanInOnFinishEvents() {
        if(onPanInFinishEvents != null) {
            onPanInFinishEvents.Execute();
        }
    }
    
    public virtual void FirePanOutOnFinishEvents() {
        if(onPanOutFinishEvents != null) {
            onPanOutFinishEvents.Execute();
            Debug.Log("Firing exit events");
        }
    }
}