using UnityEngine;
using System.Collections;

//TODO: REMOVE NGUI TRIGGER FUNCTIONALITY OR GIVE THIS A
//      NAMESPACE AND RENAME IT TO "TRIGGER", BECAUSE THE
//      NAME CUSTOM TRIGGER IS DUMB
public class EventTrigger : CustomTrigger {
    //-------------------------------------------------
    // Executes these as custom hooks to be determined by the trigger
    //-------------------------------------------------
    public CustomEventsManager onStartEvents = null;
    public CustomEventsManager onExecuteEvents = null;
    public CustomEventsManager onFinishEvents = null;
    
    // Use this for initialization
    protected override void Start() {
    }
    
    // Update is called once per frame
    protected override void Update() {
    }
    
    protected override void Initialize() {
    }
    
    public override void Entered(GameObject gameObjectEntering) {
        // Debug.Log(this.gameObject.name + " entered was triggered.");
        
        // By default the trigger is executed on enter, but in other classes
        // you can override this method in order to determine where you would
        // actually like to execute it
        FireEnterEvents();
    }
    
    public override void Exited(GameObject gameObjectExiting) {
        FireExitEvents();
    }
    
    public override void Execute(GameObject gameObjectToExecute) {
        // Perform only if it's the first iteration, or it should loop
        if(currentIteration < 1
            || loop) {
            ExecuteLogic(gameObjectToExecute);
            FireExecuteEvents();
            currentIteration++;
            
            if(!loop) {
                Collider2D attachedCollider = this.GetComponent<Collider2D>();
                if(attachedCollider != null) {
                    attachedCollider.enabled = false;
                }
            }
        }
    }
    
    public override void ExecuteLogic(GameObject gameObjectExecuting) {
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