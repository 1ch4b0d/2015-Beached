using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InControl;

public class InteractionController : MonoBehaviour {
    public List<InteractionTrigger> triggers = new List<InteractionTrigger>();
    public bool interactionButtonPressed = false;
    
    // Use this for initialization
    protected virtual void Awake() {
        Initialize();
    }
    // Use this for initialization
    protected virtual void Start() {
    }
    
    // Update is called once per frame
    protected virtual void Update() {
        PerformLogic();
    }
    protected virtual void LateUpdate() {
        Reset();
    }
    
    protected virtual void Initialize() {
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
    
    protected virtual void PerformInputLogic() {
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