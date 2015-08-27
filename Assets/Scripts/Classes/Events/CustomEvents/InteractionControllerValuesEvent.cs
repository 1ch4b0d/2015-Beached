using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractionControllerValuesEvent : CustomEventObject {
    public InteractionController interactionController = null;
    public List<InteractionTrigger> triggersToAdd = null;
    public List<InteractionTrigger> triggersToRemove = null;
    
    // // Use this for initialization
    // protected overrid void Awake() {
    // base.Awake();
    // }
    
    // // Use this for initialization
    // protected overrid void Start() {
    // base.Start();
    // }
    
    // // Update is called once per frame
    // protected overrid void Update() {
    // base.Update();
    // }
    
    protected override void Initialize() {
        base.Initialize();
        if(interactionController == null) {
            this.gameObject.LogComponentError("interactionController", this.GetType());
        }
        
        if((triggersToAdd == null || triggersToAdd.Count == 0)
            && (triggersToRemove == null || triggersToRemove.Count == 0)) {
            this.gameObject.LogComponentError("triggersToAdd", this.GetType());
        }
        else {
            if(triggersToAdd == null) {
                triggersToAdd = new List<InteractionTrigger>();
            }
            foreach(InteractionTrigger interactionTrigger in triggersToAdd) {
                if(interactionTrigger == null) {
                    Debug.LogError(this.transform.GetFullPath() + " has a NULL element in its triggersToRemove property of the InteractionControllerValuesEvent script.");
                }
            }
            
            if(triggersToRemove == null) {
                triggersToRemove = new List<InteractionTrigger>();
            }
            foreach(InteractionTrigger interactionTrigger in triggersToRemove) {
                if(interactionTrigger == null) {
                    Debug.LogError(this.transform.GetFullPath() + " has a NULL element in its triggersToRemove property of the InteractionControllerValuesEvent script.");
                }
            }
        }
    }
    
    public override void ExecuteLogic() {
        foreach(InteractionTrigger triggerToAdd in triggersToAdd) {
            interactionController.AddTrigger(triggerToAdd);
        }
        
        foreach(InteractionTrigger triggerToRemove in triggersToRemove) {
            interactionController.RemoveTrigger(triggerToRemove);
        }
    }
}