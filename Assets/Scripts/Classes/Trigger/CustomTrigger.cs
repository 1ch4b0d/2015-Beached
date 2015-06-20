using UnityEngine;
using System.Collections;

//TODO: REMOVE NGUI TRIGGER FUNCTIONALITY OR GIVE THIS A
//      NAMESPACE AND RENAME IT TO "TRIGGER", BECAUSE THE
//      NAME CUSTOM TRIGGER IS DUMB
public class CustomTrigger : MonoBehaviour {
    bool loop = false;
    int currentIteration = 0;
    
    // Use this for initialization
    void Start() {
    }
    
    // Update is called once per frame
    void Update() {
    }
    
    public virtual void Entered(GameObject gameObjectEntering) {
        // By default the trigger is executed on enter, but in other classes
        // you can override this method in order to determine where you would
        // actually like to execute it
        Execute(gameObjectEntering);
    }
    
    public virtual void Exited(GameObject gameObjectExiting) {
    }
    
    public virtual void Execute(GameObject gameObjectExecuting) {
        // Perform only if it's the first iteration, or it should loop
        if(currentIteration < 1
            || loop) {
            currentIteration++;
        }
    }
}