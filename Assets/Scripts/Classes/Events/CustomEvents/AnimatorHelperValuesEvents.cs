using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimatorHelperValuesEvents : CustomEventObject {
    [Tooltip("This is the AnimatorHelper to modify")]
    public AnimatorHelper animatorHelper = null;
    [Tooltip("This is the state name of the animator to hook into")]
    public string animationStateName = "";
    [Tooltip("These are the events that will be added for the OnStateEnter event")]
    public List<CustomEventsManager> onStateEnterManagers = null;
    public bool clearOnStateEnter = false;
    [Tooltip("These are the events that will be added for the OnStateExit event")]
    public List<CustomEventsManager> onStateExitManagers = null;
    public bool clearOnStateExit = false;
    [Tooltip("These are the events that will be added for the OnStateUpdate event")]
    public List<CustomEventsManager> onStateUpdateManagers = null;
    public bool clearOnStateUpdate = false;
    [Tooltip("These are the events that will be added for the OnStateMove event")]
    public List<CustomEventsManager> onStateMoveManagers = null;
    public bool clearOnStateMove = false;
    [Tooltip("These are the events that will be added for the OnStateIK event")]
    public List<CustomEventsManager> onStateIKManagers = null;
    public bool clearOnStateIK = false;
    [Tooltip("These are the events that will be added to the custom OnAnimationFinish event")]
    public List<CustomEventsManager> onAnimationFinishManagers = null;
    public bool clearOnAnimationFinish = false;
    
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
            this.gameObject.LogComponentError("animatorHelper", this.GetType());
        }
        
        if(animationStateName == "") {
            this.gameObject.LogComponentError("animationStateName", this.GetType());
        }
    }
    
    public override void ExecuteLogic() {
        SetAnimatorHelperValues();
    }
    
    public void SetAnimatorHelperValues() {
    
        if(onStateEnterManagers != null) {
            foreach(CustomEventsManager customEventsManager in onStateEnterManagers) {
                animatorHelper.AddOnStateEnter(animationStateName, customEventsManager);
            }
        }
        
        if(onStateExitManagers != null) {
            foreach(CustomEventsManager customEventsManager in onStateExitManagers) {
                animatorHelper.AddOnStateExit(animationStateName, customEventsManager);
            }
        }
        
        if(onStateUpdateManagers != null) {
            foreach(CustomEventsManager customEventsManager in onStateUpdateManagers) {
                animatorHelper.AddOnStateUpdate(animationStateName, customEventsManager);
            }
        }
        
        if(onStateMoveManagers != null) {
            foreach(CustomEventsManager customEventsManager in onStateMoveManagers) {
                animatorHelper.AddOnStateMove(animationStateName, customEventsManager);
            }
        }
        
        if(onStateIKManagers != null) {
            foreach(CustomEventsManager customEventsManager in onStateIKManagers) {
                animatorHelper.AddOnStateIK(animationStateName, customEventsManager);
            }
        }
        
        if(onAnimationFinishManagers != null) {
            foreach(CustomEventsManager customEventsManager in onAnimationFinishManagers) {
                animatorHelper.AddOnAnimationFinish(animationStateName, customEventsManager);
            }
        }
    }
}
