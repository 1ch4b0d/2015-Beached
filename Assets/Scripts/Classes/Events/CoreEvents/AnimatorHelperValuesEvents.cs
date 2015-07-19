using UnityEngine;
using System.Collections;

public class AnimatorHelperValuesEvents : CustomEventObject {
    [Tooltip("This is the AnimatorHelper to modify")]
    public AnimatorHelper animatorHelper = null;
    [Tooltip("This is the state name of the animator to hook into")]
    public string animationStateName = "";
    [Tooltip("These are the events that will be added for the OnStateEnter event")]
    public CustomEventsManager onStateEnterManager = null;
    [Tooltip("These are the events that will be added for the OnStateExit event")]
    public CustomEventsManager onStateExitManager = null;
    [Tooltip("These are the events that will be added for the OnStateUpdate event")]
    public CustomEventsManager onStateUpdateManager = null;
    [Tooltip("These are the events that will be added for the OnStateMove event")]
    public CustomEventsManager onStateMoveManager = null;
    [Tooltip("These are the events that will be added for the OnStateIK event")]
    public CustomEventsManager onStateIKManager = null;
    [Tooltip("These are the events that will be added to the custom OnAnimationFinish event")]
    public CustomEventsManager onAnimationFinishManager = null;
    
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
            Debug.LogError("The 'animatorHelper' property must be declared. Please fix this for: " + this.gameObject.name);
        }
        
        if(animationStateName == "") {
            Debug.LogError("The 'animationStateName' property must be declared. Please fix this for: " + this.gameObject.name);
        }
    }
    
    public override void ExecuteLogic() {
        SetAnimatorHelperValues();
    }
    
    public void SetAnimatorHelperValues() {
        if(onStateEnterManager != null) {
            foreach(CustomEvent customEvent in onStateEnterManager.GetEvents()) {
                animatorHelper.AddOnStateEnter(animationStateName, customEvent.GetEvent(), customEvent.ShouldLoop());
            }
        }
        
        if(onStateExitManager != null) {
            foreach(CustomEvent customEvent in onStateExitManager.GetEvents()) {
                animatorHelper.AddOnStateExit(animationStateName, customEvent.GetEvent(), customEvent.ShouldLoop());
            }
        }
        
        if(onStateUpdateManager != null) {
            foreach(CustomEvent customEvent in onStateUpdateManager.GetEvents()) {
                animatorHelper.AddOnStateUpdate(animationStateName, customEvent.GetEvent(), customEvent.ShouldLoop());
            }
        }
        
        if(onStateMoveManager != null) {
            foreach(CustomEvent customEvent in onStateMoveManager.GetEvents()) {
                animatorHelper.AddOnStateMove(animationStateName, customEvent.GetEvent(), customEvent.ShouldLoop());
            }
        }
        
        if(onStateIKManager != null) {
            foreach(CustomEvent customEvent in onStateIKManager.GetEvents()) {
                animatorHelper.AddOnStateIK(animationStateName, customEvent.GetEvent(), customEvent.ShouldLoop());
            }
        }
        
        if(onAnimationFinishManager != null) {
            foreach(CustomEvent customEvent in onAnimationFinishManager.GetEvents()) {
                animatorHelper.AddOnAnimationFinish(animationStateName, customEvent.GetEvent(), customEvent.ShouldLoop());
            }
        }
    }
}
