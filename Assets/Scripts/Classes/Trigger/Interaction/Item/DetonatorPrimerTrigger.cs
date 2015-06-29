using UnityEngine;
using System.Collections;

public class DetonatorPrimerTrigger : CustomTrigger {

    // Use this for initialization
    void Start() {
    
    }
    
    // Update is called once per frame
    void Update() {
    
    }
    
    public override void Entered(GameObject gameObjectEntering) {
        Debug.Log("Entered");
        // By default the trigger is executed on enter, but in other classes
        // you can override this method in order to determine where you would
        // actually like to execute it
        Execute(gameObjectEntering);
    }
    
    public override void Exited(GameObject gameObjectExiting) {
    }
    
    public override void Execute(GameObject gameObjectToExecute) {
        // Perform only if it's the first iteration, or it should loop
        if(currentIteration < 1
            || loop) {
            ExecuteLogic(gameObjectToExecute);
            currentIteration++;
            
            if(loop == false) {
                this.GetComponent<Collider2D>().enabled = false;
            }
        }
    }
    
    public override void ExecuteLogic(GameObject gameObjectExecuting) {
        Debug.Log("executing");
    }
}
