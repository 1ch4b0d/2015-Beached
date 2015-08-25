using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

// TODO: You ain't gonna need it (YAGNI) - make it so external classes can and an event to this (I think this might be a bad idea.)
public class CustomEventsManager : MonoBehaviour {
    /// <value>These are the custom events configured on the game object 'gameObjectWithEvents'.</value>
    private List<CustomEventObject> customEventObjects = null;
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
            customEventObjects = new List<CustomEventObject>(gameObjectWithEvents.GetComponents<CustomEventObject>());
        }
    }
    
    public virtual List<CustomEvent> GetEvents() {
        CustomEvents events = new CustomEvents();
        
        foreach(CustomEventObject customEventObject in customEventObjects) {
            if(customEventObject.enabled) {
                events.AddEvent(customEventObject.Execute, customEventObject.loop);
            }
        }
        
        return events.GetEvents();
    }
    
    public virtual void Execute() {
        // Debug.Log(this.gameObject.name + " Executed.");
        foreach(CustomEventObject customEventObject in customEventObjects) {
            if(customEventObject.enabled) {
                customEventObject.Execute();
            }
        }
    }
}