using UnityEngine;
using System.Collections;

public class CustomEventObject : MonoBehaviour {
    public bool loop = true;
    public int currentIteration = 0;
    
    public CustomEventsManager onStartEvents = null;
    public CustomEventsManager onExecuteEvents = null;
    public CustomEventsManager onFinishEvents = null;
    
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
        if(currentIteration < 1
            || loop) {
            ExecuteLogic();
            currentIteration++;
        }
    }
    
    public virtual void ExecuteLogic() {
    }
    
    public virtual void FireStartEvents() {
        if(onStartEvents != null) {
            onStartEvents.Execute();
        }
    }
    
    public virtual void FireExecuteEvents() {
        if(onExecuteEvents != null) {
            onExecuteEvents.Execute();
        }
    }
    
    public virtual void FireFinishEvents() {
        if(onFinishEvents != null) {
            onFinishEvents.Execute();
        }
    }
}
