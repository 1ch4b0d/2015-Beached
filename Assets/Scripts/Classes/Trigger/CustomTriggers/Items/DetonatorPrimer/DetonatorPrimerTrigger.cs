using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DetonatorPrimerTrigger : CustomTrigger {
    // should really be private
    public DetonatorPrimer detonatorPrimer = null;
    public List<Dynamite> dynamites = null;
    
    // Use this for initialization
    protected override void Start() {
        Initialize();
    }
    
    // Update is called once per frame
    protected override void Update() {
    }
    
    protected override void Initialize() {
        if(dynamites == null) {
            dynamites = new List<Dynamite>();
        }
        
        if(detonatorPrimer == null) {
            detonatorPrimer = this.gameObject.GetFirstParentOfType<DetonatorPrimer>();
            if(detonatorPrimer == null) {
                Debug.Log("The DetonatorPrimerTrigger of the gameObject: " + this.gameObject.name + " is null. Fix it.");
            }
        }
    }
    
    public override void Entered(GameObject gameObjectEntering) {
        Dynamite dynamite = gameObjectEntering.GetComponent<Dynamite>();
        if(dynamite != null) {
            detonatorPrimer.AddPrimerComponent(gameObjectEntering);
            base.Entered(gameObjectEntering);
        }
    }
    
    public override void Exited(GameObject gameObjectExiting) {
        Dynamite dynamite = gameObjectExiting.GetComponent<Dynamite>();
        if(dynamite != null) {
            detonatorPrimer.RemovePrimerComponent(gameObjectExiting);
            // Debug.Log("Dynamite is exiting the detonator primer");
        }
        base.Exited(gameObjectExiting);
    }
    
    public override void Execute(GameObject gameObjectToExecute) {
        base.Execute(gameObjectToExecute);
    }
    
    // public override void ExecuteLogic(GameObject gameObjectExecuting) {
    //     base.ExecuteLogic(gameObjectExecuting);
    // }
}