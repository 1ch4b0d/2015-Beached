using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomEventObject : MonoBehaviour {
    public bool loop = false;
    
    public List<CustomEventsManager> onStartEvents = null;
    public List<CustomEventsManager> onExecuteEvents = null;
    public List<CustomEventsManager> onFinishEvents = null;
    
    // Use this for initialization
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
    }
    
    public virtual void Execute() {
        // Perform only if it's the first iteration, or it should loop
        FireStartEvents();
        ExecuteLogic();
        FireFinishEvents();
    }
    
    public virtual void ExecuteLogic() {
        FireExecuteEvents();
    }
    
    public virtual void FireStartEvents() {
        if(onStartEvents != null
            && onStartEvents.Count > 0) {
            foreach(CustomEventsManager customEventsManager in onStartEvents) {
                customEventsManager.Execute();
            }
        }
    }
    
    public virtual void FireExecuteEvents() {
        if(onExecuteEvents != null) {
            foreach(CustomEventsManager customEventsManager in onExecuteEvents) {
                customEventsManager.Execute();
            }
        }
    }
    
    public virtual void FireFinishEvents() {
        if(onFinishEvents != null) {
            foreach(CustomEventsManager customEventsManager in onFinishEvents) {
                customEventsManager.Execute();
            }
        }
    }
}
