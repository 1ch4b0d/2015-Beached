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
    /// <value>The onStateEnter is a dictionary of List<CustomEventsManager> that map to an animation state's name.</value>
    protected Dictionary<string, List<CustomEventsManager>> onStateEnter = new Dictionary<string, List<CustomEventsManager>>();
    /// <value>The onStateUpdate is a dictionary of List<CustomEventsManager> that map to an animation state's name.</value>
    protected Dictionary<string, List<CustomEventsManager>> onStateUpdate = new Dictionary<string, List<CustomEventsManager>>();
    /// <value>The onStateExit is a dictionary of List<CustomEventsManager> that map to an animation state's name.</value>
    protected Dictionary<string, List<CustomEventsManager>> onStateExit = new Dictionary<string, List<CustomEventsManager>>();
    /// <value>The onStateMove is a dictionary of List<CustomEventsManager> that map to an animation state's name.</value>
    protected Dictionary<string, List<CustomEventsManager>> onStateMove = new Dictionary<string, List<CustomEventsManager>>();
    /// <value>The onStateIK is a dictionary of List<CustomEventsManager> that map to an animation state's name.</value>
    protected Dictionary<string, List<CustomEventsManager>> onStateIK = new Dictionary<string, List<CustomEventsManager>>();
    //--------------------------------------------------------------------------
    /// <value>The onAnimationFinish is a dictionary of List<CustomEventsManager> that map to an animation state's name.</value>
    public Dictionary<string, List<CustomEventsManager>> onAnimationFinish = new Dictionary<string, List<CustomEventsManager>>();
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
    public Dictionary<string, List<CustomEventsManager>> GetOnStateEnterDictionary() {
        return onStateEnter;
    }
    public Dictionary<string, List<CustomEventsManager>> GetOnStateUpdateDictionary() {
        return onStateUpdate;
    }
    public Dictionary<string, List<CustomEventsManager>> GetOnStateExitDictionary() {
        return onStateExit;
    }
    public Dictionary<string, List<CustomEventsManager>> GetOnStateMoveDictionary() {
        return onStateMove;
    }
    public Dictionary<string, List<CustomEventsManager>> GetOnStateIKDictionary() {
        return onStateIK;
    }
    public Dictionary<string, List<CustomEventsManager>> GetOnAnimationFinishDictionary() {
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
    public void ValidateCustomEvents(Dictionary<string, List<CustomEventsManager>> statesDictionary, string stateName) {
        List<CustomEventsManager> stateBeingValidated = null;
        statesDictionary.TryGetValue(stateName, out stateBeingValidated);
        if(stateBeingValidated == null) {
            statesDictionary[stateName] = new List<CustomEventsManager>();
        }
    }
    //----------------------------
    public void AddOnAnimationFinish(string stateName, CustomEventsManager eventsManager) {
        ValidateCustomEvents(onAnimationFinish, stateName);
        onAnimationFinish[stateName].Add(eventsManager);
    }
    public List<CustomEventsManager> GetOnAnimationFinish(string stateName) {
        List<CustomEventsManager> returnEventManager = null;
        onAnimationFinish.TryGetValue(stateName, out returnEventManager);
        return returnEventManager;
    }
    //--------------------------------------------------------------------------
    // Default Unity State Machine Hook Ins
    //--------------------------------------------------------------------------
    public void AddOnStateEnter(string stateName, CustomEventsManager eventsManager) {
        ValidateCustomEvents(onStateEnter, stateName);
        onStateEnter[stateName].Add(eventsManager);
    }
    public List<CustomEventsManager> GetOnStateEnter(string stateName) {
        List<CustomEventsManager> returnEventManager = null;
        onStateEnter.TryGetValue(stateName, out returnEventManager);
        return returnEventManager;
    }
    //----------------------------
    public void AddOnStateUpdate(string stateName, CustomEventsManager eventsManager) {
        ValidateCustomEvents(onStateUpdate, stateName);
        onStateUpdate[stateName].Add(eventsManager);
    }
    public List<CustomEventsManager> GetOnStateUpdate(string stateName) {
        List<CustomEventsManager> returnEventManager = null;
        onStateUpdate.TryGetValue(stateName, out returnEventManager);
        return returnEventManager;
    }
    //----------------------------
    public void AddOnStateExit(string stateName, CustomEventsManager eventsManager) {
        ValidateCustomEvents(onStateExit, stateName);
        onStateExit[stateName].Add(eventsManager);
    }
    public List<CustomEventsManager> GetOnStateExit(string stateName) {
        List<CustomEventsManager> returnEventManager = null;
        onStateExit.TryGetValue(stateName, out returnEventManager);
        return returnEventManager;
    }
    //----------------------------
    public void AddOnStateMove(string stateName, CustomEventsManager eventsManager) {
        ValidateCustomEvents(onStateMove, stateName);
        onStateMove[stateName].Add(eventsManager);
    }
    public List<CustomEventsManager> GetOnStateMove(string stateName) {
        List<CustomEventsManager> returnEventManager = null;
        onStateMove.TryGetValue(stateName, out returnEventManager);
        return returnEventManager;
    }
    //----------------------------
    public void AddOnStateIK(string stateName, CustomEventsManager eventsManager) {
        ValidateCustomEvents(onStateIK, stateName);
        onStateIK[stateName].Add(eventsManager);
    }
    public List<CustomEventsManager> GetOnStateIK(string stateName) {
        List<CustomEventsManager> returnEventManager = null;
        onStateIK.TryGetValue(stateName, out returnEventManager);
        return returnEventManager;
    }
}
