using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomEventObject : MonoBehaviour {
    public bool loop = false;
    public int currentIteration = 0;
    
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
        foreach(CustomEventsManager customEventsManager in onStartEvents) {
            if(customEventsManager == null) {
                Debug.LogError(this.transform.GetFullPath() + " has a NULL CustomEventsManager declared for the onStartEvents.");
            }
        }
        foreach(CustomEventsManager customEventsManager in onExecuteEvents) {
            if(customEventsManager == null) {
                Debug.LogError(this.transform.GetFullPath() + " has a NULL CustomEventsManager declared for the onExecuteEvents.");
            }
        }
        foreach(CustomEventsManager customEventsManager in onFinishEvents) {
            if(customEventsManager == null) {
                Debug.LogError(this.transform.GetFullPath() + " has a NULL CustomEventsManager declared for the onFinishEvents.");
            }
        }
    }
    
    
    public virtual void Execute() {
        // Debug.Log(this.gameObject.name + " Executed.");
        if(currentIteration < 1
            || loop) {
            currentIteration++;
            FireStartEvents();
            ExecuteLogic();
            FireFinishEvents();
        }
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
