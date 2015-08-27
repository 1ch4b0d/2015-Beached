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
        ClearAnimatorHelperEvents();
        SetAnimatorHelperValues();
    }
    
    public void ClearAnimatorHelperEvents() {
        if(clearOnStateEnter) {
            List<CustomEventsManager> onStateEnterEventsManager = animatorHelper.GetOnStateEnter(animationStateName);
            if(onStateEnterEventsManager != null) {
                onStateEnterEventsManager.Clear();
            }
        }
        if(clearOnStateExit) {
            List<CustomEventsManager> onStateExitEventsManager = animatorHelper.GetOnStateExit(animationStateName);
            if(onStateExitEventsManager != null) {
                onStateExitEventsManager.Clear();
            }
        }
        if(clearOnStateUpdate) {
            List<CustomEventsManager> onStateUpdateEventsManager = animatorHelper.GetOnStateUpdate(animationStateName);
            if(onStateUpdateEventsManager != null) {
                onStateUpdateEventsManager.Clear();
            }
        }
        if(clearOnStateMove) {
            List<CustomEventsManager> onStateMoveEventsManager = animatorHelper.GetOnStateMove(animationStateName);
            if(onStateMoveEventsManager != null) {
                onStateMoveEventsManager.Clear();
            }
        }
        if(clearOnStateIK) {
            List<CustomEventsManager> onStateIKEventsManager = animatorHelper.GetOnStateIK(animationStateName);
            if(onStateIKEventsManager != null) {
                onStateIKEventsManager.Clear();
            }
        }
        if(clearOnAnimationFinish) {
            List<CustomEventsManager> onAnimationFinishEventsManager = animatorHelper.GetOnAnimationFinish(animationStateName);
            if(onAnimationFinishEventsManager != null) {
                onAnimationFinishEventsManager.Clear();
            }
        }
    }
    
    public void SetAnimatorHelperValues() {
        if(onStateEnterManagers != null) {
            // Debug.Log(this.gameObject.transform.GetFullPath() + " setting " + onStateEnterManagers.Count + " event managers for onStateEnter.");
            foreach(CustomEventsManager customEventsManager in onStateEnterManagers) {
                animatorHelper.AddOnStateEnter(animationStateName, customEventsManager);
            }
        }
        
        if(onStateExitManagers != null) {
            // Debug.Log(this.gameObject.transform.GetFullPath() + " setting " + onStateIKManagers.Count + " event managers for onStateExit.");
            foreach(CustomEventsManager customEventsManager in onStateExitManagers) {
                animatorHelper.AddOnStateExit(animationStateName, customEventsManager);
            }
        }
        
        if(onStateUpdateManagers != null) {
            // Debug.Log(this.gameObject.transform.GetFullPath() + " setting " + onStateUpdateManagers.Count + " event managers for onStateUpdate.");
            foreach(CustomEventsManager customEventsManager in onStateUpdateManagers) {
                animatorHelper.AddOnStateUpdate(animationStateName, customEventsManager);
            }
        }
        
        if(onStateMoveManagers != null) {
            // Debug.Log(this.gameObject.transform.GetFullPath() + " setting " + onStateMoveManagers.Count + " event managers for onStateMove.");
            foreach(CustomEventsManager customEventsManager in onStateMoveManagers) {
                animatorHelper.AddOnStateMove(animationStateName, customEventsManager);
            }
        }
        
        if(onStateIKManagers != null) {
            // Debug.Log(this.gameObject.transform.GetFullPath() + " setting " + onStateIKManagers.Count + " event managers for onStateIK.");
            foreach(CustomEventsManager customEventsManager in onStateIKManagers) {
                animatorHelper.AddOnStateIK(animationStateName, customEventsManager);
            }
        }
        
        if(onAnimationFinishManagers != null) {
            // Debug.Log(this.gameObject.transform.GetFullPath() + " setting " + onAnimationFinishManagers.Count + " event managers for onAnimationFinish.");
            foreach(CustomEventsManager customEventsManager in onAnimationFinishManagers) {
                animatorHelper.AddOnAnimationFinish(animationStateName, customEventsManager);
            }
        }
    }
}
