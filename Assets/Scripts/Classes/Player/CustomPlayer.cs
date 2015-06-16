using UnityEngine;
using System.Collections;

public class CustomPlayer : MonoBehaviour {
    public InteractionTrigger currentInteractionTrigger = null;
    public BasicPlayerController playerController = null;
    
    // Use this for initialization
    void Start() {
    }
    
    // Update is called once per frame
    void Update() {
        PerformLogic();
    }
    
    protected void PerformLogic() {
        playerController.PerformLogic();
        if(playerController.IsActionButtonPressed()) {
            if(currentInteractionTrigger != null) {
                currentInteractionTrigger.Interact(this.gameObject);
            }
        }
    }
    
    public void OnTriggerEnter2D(Collider2D collider) {
        currentInteractionTrigger = collider.gameObject.GetComponent<InteractionTrigger>();
        if(currentInteractionTrigger != null) {
            currentInteractionTrigger.Entered(this.gameObject);
        }
    }
    
    public void OnTrigger2D(Collider2D collider) {
    }
    
    public void OnTriggerExit2D(Collider2D collider) {
        if(currentInteractionTrigger != null) {
            // if the object being exited is the same as the one assigned
            if(collider.gameObject.GetInstanceID() == currentInteractionTrigger.gameObject.GetInstanceID()) {
                currentInteractionTrigger.Exited(this.gameObject);
                currentInteractionTrigger = null;
            }
        }
    }
    
    public void UpdateAnimator() {
    }
    
    public void StartInteraction() {
        playerController.disableController = true;
        playerController.Reset();
    }
    
    public void StartGameplayState() {
        playerController.disableController = false;
        playerController.Reset();
    }
    
    public void FinishInteractionState() {
        playerController.disableController = false;
    }
}
