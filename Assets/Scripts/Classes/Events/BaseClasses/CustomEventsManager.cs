using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

/// This is a custom event wrapper for unity in order to extend this class so
/// that custom logic can be executed via the "CustomEventsManager" being hooked
/// into the logic of the actual gameobject's logic.
///
/// In this example there is a suggested idiomatic approach that the developer
/// builds the events that fire for each event group on one single object
///
/// Example:
/// Suggested hierarchy for objects using this pattern
/// RootGameObject
/// |___CustomEventManagerGameObject
///     |___OnStart   - (Has "CustomEventsManager" and scripts that extend from "CustomTrigger")
///     |___OnExecute - (Has "CustomEventsManager" and scripts that extend from "CustomTrigger")
///     |___OnFinish  - (Has "CustomEventsManager" and scripts that extend from "CustomTrigger")
///
/// Each one of them has the "CustomEventsManager" attached to it. This is what is
/// managing each of the "CustomEventGameObject" scripts that are also attached
/// to the same GameObject. You can specify a separate game object instead so
/// that it can be decoupled, but the idiomatic suggestion is that they're all
/// on the same object
///
/// OnStart/OnExecute/OnFinish all have separate scripts attached that are
/// extended from the "CustomEventGameObject"
public class CustomEventsManager : MonoBehaviour {
    public CustomEvents events = null;
    public GameObject gameObjectWithEvents = null;
    
    protected virtual void Awake() {
        Initialize();
    }
    
    // Use this for initialization
    protected virtual void Start() {
    }
    
    // Update is called once per frame
    protected virtual void Update() {
    }
    
    protected virtual void Initialize() {
        events = new CustomEvents();
        
        if(gameObjectWithEvents == null) {
            gameObjectWithEvents = this.gameObject;
        }
        
        if(gameObjectWithEvents != null) {
            CustomEventObject[] customEventObjects = gameObjectWithEvents.GetComponents<CustomEventObject>();
            
            foreach(CustomEventObject customEventObject in customEventObjects) {
                // Debug.Log(customEventObject);
                if(this != customEventObject) {
                    events.Add(customEventObject.Execute, customEventObject.loop);
                }
            }
        }
    }
    
    public virtual void Execute() {
        // Debug.Log("Executing " + this.gameObject.name);
        events.Execute();
    }
}