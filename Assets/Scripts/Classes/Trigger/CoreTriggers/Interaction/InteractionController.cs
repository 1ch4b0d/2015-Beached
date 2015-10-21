using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InControl;

public class InteractionController : MonoBehaviour {
    public List<InteractionTrigger> triggers = new List<InteractionTrigger>();
    public InteractionTrigger triggerBeingInteractedWith = null;
    public bool interactionButtonPressed = false;
    public bool isInteracting = false;
    public bool isPaused = false;
    
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
    
    public bool IsInteracting() {
        return isInteracting;
    }
    
    public InteractionTrigger GetTriggerBeingInteractedWith() {
        return triggerBeingInteractedWith;
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
    
    public virtual void OnTriggerEnter2D(Collider2D collider) {
        InteractionTrigger interactionTrigger = collider.gameObject.GetComponent<InteractionTrigger>();
        if(interactionTrigger != null) {
            AddTrigger(interactionTrigger);
        }
    }
    
    // public void OnTrigger2D(Collider2D collider) {
    // }
    
    public virtual void OnTriggerExit2D(Collider2D collider) {
        InteractionTrigger interactionTrigger = collider.gameObject.GetComponent<InteractionTrigger>();
        if(interactionTrigger) {
            RemoveTrigger(interactionTrigger);
        }
    }
    
    public virtual void StartInteraction() {
        interactionButtonPressed = false;
        isInteracting = true;
        triggerBeingInteractedWith = GetNewestTrigger();
    }
    
    public virtual void FinishInteraction() {
        isInteracting = false;
        triggerBeingInteractedWith = null;
    }
    
    public void Pause() {
        isPaused = true;
    }
    
    public void Unpause() {
        isPaused = false;
    }
}