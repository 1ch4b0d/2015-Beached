using UnityEngine;
using System.Collections;

//TODO: REMOVE NGUI TRIGGER FUNCTIONALITY OR GIVE THIS A
//      NAMESPACE AND RENAME IT TO "TRIGGER", BECAUSE THE
//      NAME CUSTOM TRIGGER IS DUMB
public class CustomTrigger : MonoBehaviour {
    public bool loop = true;
    public int currentIteration = 0;
    
    public CustomEventsManager onFinishEvents = null;
    
    // Use this for initialization
    void Start() {
    }
    
    // Update is called once per frame
    void Update() {
    }
    
    public virtual void Initialize() {
    }
    
    public virtual void Entered(GameObject gameObjectEntering) {
        // Debug.Log(this.gameObject.name + " entered was triggered.");
        
        // By default the trigger is executed on enter, but in other classes
        // you can override this method in order to determine where you would
        // actually like to execute it
        Execute(gameObjectEntering);
    }
    
    public virtual void Exited(GameObject gameObjectExiting) {
    }
    
    public virtual void Execute(GameObject gameObjectToExecute) {
        // Perform only if it's the first iteration, or it should loop
        if(currentIteration < 1
            || loop) {
            ExecuteLogic(gameObjectToExecute);
            currentIteration++;
            
            if(!loop) {
                Collider2D attachedCollider = this.GetComponent<Collider2D>();
                if(attachedCollider != null) {
                    attachedCollider.enabled = false;
                }
            }
        }
    }
    
    public virtual void ExecuteLogic(GameObject gameObjectExecuting) {
    }
    
    public virtual void FireFinishEvents() {
        if(onFinishEvents != null) {
            onFinishEvents.Execute();
        }
    }
}