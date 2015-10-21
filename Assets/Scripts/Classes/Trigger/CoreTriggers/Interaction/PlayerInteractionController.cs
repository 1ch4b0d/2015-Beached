using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InControl;

public class PlayerInteractionController : InteractionController {
    public CustomPlayerActions customPlayerActions;
    
    // Use this for initialization
    protected override void Awake() {
        base.Awake();
    }
    
    // Use this for initialization
    protected override void Start() {
        base.Start();
    }
    
    // Update is called once per frame
    protected override void Update() {
        base.Update();
    }
    
    protected override void LateUpdate() {
        base.LateUpdate();
    }
    
    protected override void Initialize() {
        base.Initialize();
        if(customPlayerActions == null) {
            this.gameObject.LogComponentError("customPlayerActions", this.GetType());
        }
    }
    
    protected override void PerformInputLogic() {
        // customPlayerActions.GetCustomPlayerActionSet().Interact.WasPressed
        if(customPlayerActions.GetCustomPlayerActionSet().Interact.WasPressed) {
            interactionButtonPressed = true;
        }
        if(!customPlayerActions.GetCustomPlayerActionSet().Interact.WasPressed) {
            interactionButtonPressed = false;
        }
    }
    
    public override void OnTriggerEnter2D(Collider2D collider) {
        if(!isInteracting) {
            InteractionTrigger interactionTrigger = collider.gameObject.GetComponent<InteractionTrigger>();
            if(interactionTrigger != null) {
                AddTrigger(interactionTrigger);
            }
        }
    }
    
    // public void OnTrigger2D(Collider2D collider) {
    // }
    
    public override void OnTriggerExit2D(Collider2D collider) {
        // Debug.Log("isInteracting: " + isInteracting);
        // if(!isInteracting) {
        InteractionTrigger interactionTrigger = collider.gameObject.GetComponent<InteractionTrigger>();
        if(interactionTrigger != null) {
            // && interactionTrigger != triggerBeingInteractedWith) {
            RemoveTrigger(interactionTrigger);
        }
        // }
    }
    
    
    public override void StartInteraction() {
        base.StartInteraction();
    }
    
    public override void FinishInteraction() {
        isInteracting = false;
        SpeechBubbleInteractionTrigger speechBubbleInteractionTrigger = triggerBeingInteractedWith.GetComponent<SpeechBubbleInteractionTrigger>();
        if(speechBubbleInteractionTrigger != null) {
            Debug.Log("Was interacting with speech bubble.");
            if(!triggers.Contains(speechBubbleInteractionTrigger)) {
                Debug.Log("don't contain it bruh");
                speechBubbleInteractionTrigger.HideSpeechBubble(speechBubbleInteractionTrigger.horizontalScaleInDuration, speechBubbleInteractionTrigger.verticalScaleInDuration);
            }
        }
        triggerBeingInteractedWith = null;
    }
}