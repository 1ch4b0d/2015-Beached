using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This is an interface to provide a hook in for the unity StateMachineBehavior logic from 
/// the GameObject that actually has the animator attached
///
/// Attach this to an object that has an Animator script attached.
/// The very same object's animator should the "ShellStateMachineBehaviour" script
/// attached to each state that you want to support custom events firing on.
/// </summary>
public class AnimatorHelper : MonoBehaviour {
	/// <value>The onStateEnter is a dictionary of CustomEvents that map to an animation state's name.</value>
    protected Dictionary<string, CustomEvents> onStateEnter = new Dictionary<string, CustomEvents>();
	/// <value>The onStateUpdate is a dictionary of CustomEvents that map to an animation state's name.</value>
    protected Dictionary<string, CustomEvents> onStateUpdate = new Dictionary<string, CustomEvents>();
	/// <value>The onStateExit is a dictionary of CustomEvents that map to an animation state's name.</value>
    protected Dictionary<string, CustomEvents> onStateExit = new Dictionary<string, CustomEvents>();
	/// <value>The onStateMove is a dictionary of CustomEvents that map to an animation state's name.</value>
    protected Dictionary<string, CustomEvents> onStateMove = new Dictionary<string, CustomEvents>();
	/// <value>The onStateIK is a dictionary of CustomEvents that map to an animation state's name.</value>
    protected Dictionary<string, CustomEvents> onStateIK = new Dictionary<string, CustomEvents>();
    //--------------------------------------------------------------------------
	/// <value>The onAnimationFinish is a dictionary of CustomEvents that map to an animation state's name.</value>
    public Dictionary<string, CustomEvents> onAnimationFinish = new Dictionary<string, CustomEvents>();
	/// <value>The destroyOnFinish is a dictionary of bools that map to an animation state's name. If true on completion of the animationState's animation it will be destroyed.</value>
    public Dictionary<string, bool> destroyOnFinish = new Dictionary<string, bool>();
    
    // Use this for initialization
    void Start() {
    }
    
    // Update is called once per frame
    void Update() {
    }
    //--------------------------------------------------------------------------
    // Simple Retrieval
    //--------------------------------------------------------------------------
    public Dictionary<string, CustomEvents> GetOnStateEnterDictionary() {
        return onStateEnter;
    }
    public Dictionary<string, CustomEvents> GetOnStateUpdateDictionary() {
        return onStateUpdate;
    }
    public Dictionary<string, CustomEvents> GetOnStateExitDictionary() {
        return onStateExit;
    }
    public Dictionary<string, CustomEvents> GetOnStateMoveDictionary() {
        return onStateMove;
    }
    public Dictionary<string, CustomEvents> GetOnStateIKDictionary() {
        return onStateIK;
    }
    public Dictionary<string, CustomEvents> GetOnAnimationFinishDictionary() {
        return onAnimationFinish;
    }
    
    //--------------------------------------------------------------------------
    // Custom Animation Methods
    //--------------------------------------------------------------------------
    public void SetDestroyOnFinish(string animationState, bool newDestroyOnFinish) {
        destroyOnFinish[animationState] = newDestroyOnFinish;
    }
    public bool GetDestroyOnFinish(Dictionary<string, bool> destroyOnFinishDictionary, string animationState) {
        bool destroyAnimationState = false;
        destroyOnFinishDictionary.TryGetValue(animationState, out destroyAnimationState);
		return destroyAnimationState;
    }
	// Convenience method
    public bool GetDestroyOnFinish(string animationState) {
		return GetDestroyOnFinish(destroyOnFinish, animationState);
	}
    //----------------------------
	/// <summary>
	/// This takes a dictionary and validates that the value, which should be a CustomEvents object, is stored 
	/// for the key (stateName) exists. If it doesn't creates the CustomEvents object there
	/// </summary>
	public void ValidateCustomEvents(Dictionary<string, CustomEvents> statesDictionary, string stateName) {
        CustomEvents stateBeingValidated = null;
        statesDictionary.TryGetValue(stateName, out stateBeingValidated);
		if(stateBeingValidated == null) {
			statesDictionary[stateName] = CustomEvents.Create();
		}
	}
    //----------------------------
    public void AddOnAnimationFinish(string stateName, System.Action eventFunction, bool loop = false) {
		ValidateCustomEvents(onAnimationFinish, stateName);
        onAnimationFinish[stateName].AddEvent(eventFunction, loop);
    }
    public CustomEvents GetOnAnimationFinish(string stateName) {
        CustomEvents returnEvent = null;
        onAnimationFinish.TryGetValue(stateName, out returnEvent);
        return returnEvent;
    }
    //--------------------------------------------------------------------------
    // Default Unity State Machine Hook Ins
    //--------------------------------------------------------------------------
    public void AddOnStateEnter(string stateName, System.Action eventFunction, bool loop = false) {
		ValidateCustomEvents(onStateEnter, stateName);
        onStateEnter[stateName].AddEvent(eventFunction, loop);
    }
    public CustomEvents GetOnStateEnter(string stateName) {
        CustomEvents returnEvent = null;
        onStateEnter.TryGetValue(stateName, out returnEvent);
        return returnEvent;
    }
    //----------------------------
    public void AddOnStateUpdate(string stateName, System.Action eventFunction, bool loop = false) {
		ValidateCustomEvents(onStateUpdate, stateName);
        onStateUpdate[stateName].AddEvent(eventFunction, loop);
    }
    public CustomEvents GetOnStateUpdate(string stateName) {
        CustomEvents returnEvent = null;
        onStateUpdate.TryGetValue(stateName, out returnEvent);
        return returnEvent;
    }
    //----------------------------
    public void AddOnStateExit(string stateName, System.Action eventFunction, bool loop = false) {
		ValidateCustomEvents(onStateExit, stateName);
        onStateExit[stateName].AddEvent(eventFunction, loop);
    }
    public CustomEvents GetOnStateExit(string stateName) {
        CustomEvents returnEvent = null;
        onStateExit.TryGetValue(stateName, out returnEvent);
        return returnEvent;
    }
    //----------------------------
    public void AddOnStateMove(string stateName, System.Action eventFunction, bool loop = false) {
		ValidateCustomEvents(onStateMove, stateName);
        onStateMove[stateName].AddEvent(eventFunction, loop);
    }
    public CustomEvents GetOnStateMove(string stateName) {
        CustomEvents returnEvent = null;
        onStateMove.TryGetValue(stateName, out returnEvent);
        return returnEvent;
    }
    //----------------------------
    public void AddOnStateIK(string stateName, System.Action eventFunction, bool loop = false) {
		ValidateCustomEvents(onStateIK, stateName);
        onStateIK[stateName].AddEvent(eventFunction, loop);
    }
    public CustomEvents GetOnStateIK(string stateName) {
        CustomEvents returnEvent = null;
        onStateIK.TryGetValue(stateName, out returnEvent);
        return returnEvent;
    }
}
