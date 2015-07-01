using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractionController : MonoBehaviour {
    public List<InteractionTrigger> triggers = new List<InteractionTrigger>();
    public bool interactionButtonPressed = false;
    
    // Use this for initialization
    void Start() {
    }
    
    // Update is called once per frame
    void Update() {
        PerformLogic();
    }
    void LateUpdate() {
        interactionButtonPressed = false;
    }
    
    public InteractionTrigger GetNewestTrigger() {
        if(triggers != null
            && triggers.Count > 0) {
            return triggers[triggers.Count - 1];
        }
        else {
            return null;
        }
    }
    
    public void Reset() {
        interactionButtonPressed = false;
    }
    
    public bool IsActionButtonPressed() {
        return interactionButtonPressed;
    }
    
    public void PerformLogic() {
        PerformInputLogic();
    }
    
    protected void PerformInputLogic() {
        // Action
        if(Input.GetKeyDown(KeyCode.J)) {
            interactionButtonPressed = true;
        }
        if(Input.GetKeyUp(KeyCode.J)) {
            interactionButtonPressed = false;
        }
    }
    
    public void AddTrigger(InteractionTrigger trigger) {
        if(!triggers.Contains(trigger)) {
            triggers.Add(trigger);
        }
    }
    
    public void RemoveTrigger(InteractionTrigger trigger) {
        triggers.Remove(trigger);
    }
    
    public void OnTriggerEnter2D(Collider2D collider) {
        InteractionTrigger interactionTrigger = collider.gameObject.GetComponent<InteractionTrigger>();
        if(interactionTrigger != null) {
            AddTrigger(interactionTrigger);
        }
    }
    
    // public void OnTrigger2D(Collider2D collider) {
    // }
    
    public void OnTriggerExit2D(Collider2D collider) {
        InteractionTrigger interactionTrigger = collider.gameObject.GetComponent<InteractionTrigger>();
        if(interactionTrigger) {
            RemoveTrigger(interactionTrigger);
        }
    }
}