using UnityEngine;
using System.Collections;

//TODO: REMOVE NGUI TRIGGER FUNCTIONALITY OR GIVE THIS A
//      NAMESPACE AND RENAME IT TO "TRIGGER", BECAUSE THE
//      NAME CUSTOM TRIGGER IS DUMB
public class CustomTrigger : MonoBehaviour {
    public bool loop = true;
    public int currentIteration = 0;
    public CustomEventsManager onStart = null;
    public CustomEventsManager onExecute = null;
    public CustomEventsManager onFinish = null;
    
    // Use this for initialization
    void Start() {
    }
    
    // Update is called once per frame
    void Update() {
    }
    
    public virtual void Initialize() {
    }
    
    public virtual void Entered(GameObject gameObjectEntering) {
        // By default the trigger is executed on enter, but in other classes
        // you can override this method in order to determine where you would
        // actually like to execute it
        if(onStart != null) {
            onStart.Execute();
        }
        Execute(gameObjectEntering);
    }
    
    public virtual void Exited(GameObject gameObjectExiting) {
    }
    
    public virtual void Execute(GameObject gameObjectToExecute) {
        // Perform only if it's the first iteration, or it should loop
        if(currentIteration < 1
            || loop) {
            ExecuteLogic(gameObjectToExecute);
            if(onFinish != null) {
                onFinish.Execute();
            }
            currentIteration++;
            
            if(loop == false) {
                Collider2D attachedCollider = this.GetComponent<Collider2D>();
                if(attachedCollider != null) {
                    attachedCollider.enabled = false;
                }
            }
        }
    }
    
    public virtual void ExecuteLogic() {
        // This is really poor form. Like really bad form.
        // One day when the opportunity arises investigate events and how to not
        // design them so poorly like this current implementation
        // https://msdn.microsoft.com/en-us/library/vstudio/ms229011.aspx
        if(onFinish != null) {
            onFinish.Execute();
        }
        ExecuteLogic(null);
    }
    
    public virtual void ExecuteLogic(GameObject gameObjectExecuting) {
    }
}