using UnityEngine;
using System.Collections;

public class DetonatorPrimerTrigger : CustomTrigger {
    DetonatorPrimer detonatorPrimer = null;
    
    // Use this for initialization
    void Start() {
        if(detonatorPrimer == null) {
            detonatorPrimer = Utility.GetFirstParentOfType<DetonatorPrimer>(this.gameObject);
            if(detonatorPrimer == null) {
                Debug.Log("The DetonatorPrimerTrigger of the gameObject: " + this.gameObject.name + " is null. Fix it.");
            }
        }
    }
    
    // Update is called once per frame
    void Update() {
    
    }
    
    public override void Entered(GameObject gameObjectEntering) {
        base.Entered(gameObjectEntering);
    }
    
    public override void Exited(GameObject gameObjectExiting) {
        Dynamite dynamite = gameObjectExiting.GetComponent<Dynamite>();
        if(dynamite != null) {
            // Debug.Log("Dynamite is exiting the detonator primer");
            detonatorPrimer.isPrimed = false;
        }
    }
    
    public override void Execute(GameObject gameObjectToExecute) {
        base.Execute(gameObjectToExecute);
    }
    
    public override void ExecuteLogic(GameObject gameObjectExecuting) {
        Dynamite dynamite = gameObjectExecuting.GetComponent<Dynamite>();
        if(dynamite != null) {
            // Debug.Log("Dynamite is in the detonator primer");
            detonatorPrimer.isPrimed = true;
        }
    }
}
