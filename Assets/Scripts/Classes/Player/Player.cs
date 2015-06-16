using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public InteractionTrigger currentInteractionTrigger = null;
    public InteractionController interactionController = null;
    public PlayerController playerController = null;
    
    void Awake() {
        Initialize();
    }
    
    // Use this for initialization
    void Start() {
    }
    
    // Update is called once per frame
    void Update() {
        PerformLogic();
    }
    
    protected void Initialize() {
        if(interactionController == null) {
            Debug.LogError("Could not find the " + gameObject.name + ": interactionController");
        }
        if(playerController == null) {
            Debug.LogError("Could not find the " + gameObject.name + ": playerController");
        }
    }
    
    protected void PerformLogic() {
        playerController.PerformLogic();
        interactionController.PerformLogic();
        if(interactionController.IsActionButtonPressed()) {
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
        interactionController.disableController = true;
        interactionController.Reset();
    }
    
    public void StartGameplayState() {
        interactionController.disableController = false;
        interactionController.Reset();
    }
    
    public void FinishInteractionState() {
        interactionController.disableController = false;
    }
}
