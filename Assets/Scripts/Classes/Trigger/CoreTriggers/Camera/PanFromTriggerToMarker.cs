using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// TODO: This seems pretty dumb. I'd rather remove this all together and configure
//      an external script to track if the cinmeatic completed. This seems like...
//      really really dumb with all the event logic hooked up now.
public class PanFromTriggerToMarker : EventTrigger {
    public GoTween currentTween = null;
    public GameObject cameraToPan = null;
    public float panDuration = 1f;
    public float delay = 0f;
    public Transform markerPosition = null;
    public bool panIn = true;
    public GoEaseType panInEasingType = GoEaseType.Linear;
    public bool panOut = true;
    public GoEaseType panOutEasingType = GoEaseType.Linear;
    
    // Events
    public List<CustomEventsManager> onPanInStartEvents = null;
    public List<CustomEventsManager> onPanInFinishEvents = null;
    public List<CustomEventsManager> onPanOutStartEvents = null;
    public List<CustomEventsManager> onPanOutFinishEvents = null;
    
    // // Use this for initialization
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
        if(cameraToPan == null) {
            Debug.LogError(this.gameObject.name + " needs its 'cameraToPan' reference to be set in the 'PanFromTriggerToMarker' Script");
        }
    }
    
    public override void Entered(GameObject gameObjectEntering) {
        base.Entered(gameObjectEntering);
        if(panIn) {
            PanIn();
        }
    }
    
    public override void Exited(GameObject gameObjectExiting) {
        base.Exited(gameObjectExiting);
        if(panOut) {
            PanOut();
        }
    }
    
    public void PanIn() {
        FirePanInOnStartEvents();
        if(currentTween != null) {
            currentTween.destroy();
            currentTween = null;
        }
        
        Vector3 targetPosition = markerPosition.position;
        // targetPosition += CameraManager.Instance.CameraFollow().GetFollowOffsetVector3();
        targetPosition.z = cameraToPan.transform.position.z;
        
        // Debug.Log("Panning~~~");
        //On Tween Finish
        currentTween = Go.to(cameraToPan.transform,
                             panDuration,
                             new GoTweenConfig().position(targetPosition).setEaseType(panInEasingType)
        .onComplete(complete => {
            // Debug.Log("Completed~~~");
            FirePanInOnFinishEvents();
        }));
    }
    
    public void PanOut() {
        if(currentTween != null) {
            currentTween.destroy();
            currentTween = null;
            FirePanOutOnStartEvents();
            FirePanOutOnFinishEvents();
        }
    }
    
    public virtual void FirePanInOnStartEvents() {
        if(onPanInFinishEvents != null) {
            foreach(CustomEventsManager customEventsManager in onPanInStartEvents) {
                customEventsManager.Execute();
            }
        }
    }
    
    public virtual void FirePanInOnFinishEvents() {
        if(onPanInFinishEvents != null) {
            foreach(CustomEventsManager customEventsManager in onPanInFinishEvents) {
                customEventsManager.Execute();
            }
        }
    }
    
    public virtual void FirePanOutOnStartEvents() {
        if(onPanInFinishEvents != null) {
            foreach(CustomEventsManager customEventsManager in onPanOutStartEvents) {
                customEventsManager.Execute();
            }
        }
    }
    
    public virtual void FirePanOutOnFinishEvents() {
        if(onPanOutFinishEvents != null) {
            foreach(CustomEventsManager customEventsManager in onPanOutFinishEvents) {
                customEventsManager.Execute();
            }
        }
    }
}