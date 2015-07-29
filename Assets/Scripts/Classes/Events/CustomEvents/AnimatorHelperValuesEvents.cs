using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimatorHelperValuesEvents : CustomEventObject {
    [Tooltip("This is the AnimatorHelper to modify")]
    public AnimatorHelper animatorHelper = null;
    [Tooltip("This is the state name of the animator to hook into")]
    public string animationStateName = "";
    [Tooltip("These are the events that will be added for the OnStateEnter event")]
    public List<CustomEventsManager> onStateEnterManager = null;
    [Tooltip("These are the events that will be added for the OnStateExit event")]
    public List<CustomEventsManager> onStateExitManager = null;
    [Tooltip("These are the events that will be added for the OnStateUpdate event")]
    public List<CustomEventsManager> onStateUpdateManager = null;
    [Tooltip("These are the events that will be added for the OnStateMove event")]
    public List<CustomEventsManager> onStateMoveManager = null;
    [Tooltip("These are the events that will be added for the OnStateIK event")]
    public List<CustomEventsManager> onStateIKManager = null;
    [Tooltip("These are the events that will be added to the custom OnAnimationFinish event")]
    public List<CustomEventsManager> onAnimationFinishManager = null;
    
    // // Use this for initialization
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
        if(animatorHelper == null) {
            Debug.LogError(this.gameObject.name + " needs its 'animatorHelper' reference to be set in the 'AnimatorHelperValuesEvents' Script");
        }
        
        if(animationStateName == "") {
            Debug.LogError(this.gameObject.name + " needs its 'animationStateName' reference to be set in the 'AnimatorHelperValuesEvents' Script");
        }
    }
    
    public override void ExecuteLogic() {
        SetAnimatorHelperValues();
    }
    
    public void SetAnimatorHelperValues() {
        if(onStateEnterManager != null) {
            foreach(CustomEventsManager customEventsManager in onStateEnterManager) {
                foreach(CustomEvent customEvent in customEventsManager.GetEvents()) {
                    animatorHelper.AddOnStateEnter(animationStateName, customEvent.GetEvent(), customEvent.ShouldLoop());
                }
            }
        }
        
        if(onStateExitManager != null) {
            foreach(CustomEventsManager customEventsManager in onStateExitManager) {
                foreach(CustomEvent customEvent in customEventsManager.GetEvents()) {
                    animatorHelper.AddOnStateExit(animationStateName, customEvent.GetEvent(), customEvent.ShouldLoop());
                }
            }
        }
        
        if(onStateUpdateManager != null) {
            foreach(CustomEventsManager customEventsManager in onStateUpdateManager) {
                foreach(CustomEvent customEvent in customEventsManager.GetEvents()) {
                    animatorHelper.AddOnStateUpdate(animationStateName, customEvent.GetEvent(), customEvent.ShouldLoop());
                }
            }
        }
        
        if(onStateMoveManager != null) {
            foreach(CustomEventsManager customEventManager in onStateMoveManager) {
                foreach(CustomEvent customEvent in customEventManager.GetEvents()) {
                    animatorHelper.AddOnStateMove(animationStateName, customEvent.GetEvent(), customEvent.ShouldLoop());
                }
            }
        }
        
        if(onStateIKManager != null) {
            foreach(CustomEventsManager customEventsManager in onStateIKManager) {
                foreach(CustomEvent customEvent in customEventsManager.GetEvents()) {
                    animatorHelper.AddOnStateIK(animationStateName, customEvent.GetEvent(), customEvent.ShouldLoop());
                }
            }
        }
        
        if(onAnimationFinishManager != null) {
            foreach(CustomEventsManager customEventsManager in onAnimationFinishManager) {
                foreach(CustomEvent customEvent in customEventsManager.GetEvents()) {
                    animatorHelper.AddOnAnimationFinish(animationStateName, customEvent.GetEvent(), customEvent.ShouldLoop());
                }
            }
        }
    }
}
