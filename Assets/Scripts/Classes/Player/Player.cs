using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
    // public InteractionTrigger currentInteractionTrigger = null;
    public InteractionController interactionController = null;
    
    // public PlayerController playerController = null;
    public Acrocatic.Player playerController = null;
    public Rigidbody2DSnapshot rigidbody2DSnapshot = null;
    
    public CarryItem carryItem = null;
    
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
            interactionController = this.gameObject.GetComponent<InteractionController>();
            if(interactionController == null) {
                Debug.LogWarning("Could not find the " + this.gameObject.name + ": interactionController");
            }
        }
        if(carryItem == null) {
            carryItem = this.gameObject.GetComponent<CarryItem>();
            if(carryItem == null) {
                Debug.LogError(" The component 'carryItem' has not been assigned. Please assign it before using this script.");
            }
        }
        if(playerController == null) {
            playerController = this.gameObject.GetComponent<Acrocatic.Player>();
            if(playerController == null) {
                Debug.LogError("Could not find the " + gameObject.name + ": playerController");
            }
        }
        if(rigidbody2DSnapshot == null) {
            rigidbody2DSnapshot = new Rigidbody2DSnapshot();
            if(rigidbody2DSnapshot == null) {
                Debug.LogError("Could not find the " + gameObject.name + ": rigidbody2DSnapshot");
            }
        }
    }
    
    public bool IsMoving(Rigidbody2D rigidbody) {
        if(rigidbody != null
            && (rigidbody.velocity.x > 0
                || rigidbody.velocity.y > 0)) {
            return true;
        }
        else {
            return false;
        }
    }
    
    public InteractionController GetInteractionController() {
        return null;
    }
    
    protected void PerformLogic() {
        // if(playerController != null) {
        //     playerController.PerformLogic();
        // }
        //-----------------------------------
        interactionController.PerformLogic();
        PerformInteractionCheck();
    }
    
    public void OnTriggerEnter2D(Collider2D collider) {
        CustomTrigger customTrigger = collider.gameObject.GetComponent<CustomTrigger>();
        InteractionTrigger interactionTrigger = collider.gameObject.GetComponent<InteractionTrigger>();
        if(customTrigger != null) {
            customTrigger.Entered(this.gameObject);
        }
        if(interactionTrigger != null) {
            interactionController.AddTrigger(interactionTrigger);
        }
    }
    
    // public void OnTrigger2D(Collider2D collider) {
    // }
    
    public void OnTriggerExit2D(Collider2D collider) {
        CustomTrigger customTrigger = collider.gameObject.GetComponent<CustomTrigger>();
        InteractionTrigger interactionTrigger = collider.gameObject.GetComponent<InteractionTrigger>();
        if(customTrigger != null) {
            customTrigger.Exited(this.gameObject);
        }
        if(interactionTrigger) {
            interactionController.RemoveTrigger(interactionTrigger);
        }
        //--------------
        // CustomTrigger currentInteractionTrigger = interactionController.GetNewestTrigger();
        // if(currentInteractionTrigger != null) {
        //     // if the object being exited is the same as the one assigned
        //     if(collider.gameObject.GetInstanceID() == currentInteractionTrigger.gameObject.GetInstanceID()) {
        //         currentInteractionTrigger = null;
        //     }
        // }
    }
    
    public void PerformInteractionCheck() {
        if(interactionController.IsActionButtonPressed()) {
            InteractionTrigger currentInteractionTrigger = interactionController.GetNewestTrigger();
            if(currentInteractionTrigger != null) {
                if(carryItem.IsCarryingItem()) {
                    PerformCarryItemReleaseCheck();
                }
                else {
                    currentInteractionTrigger.Interact(this.gameObject);
                }
            }
        }
    }
    
    // TODO: For real refactor this method, it's such a mess.
    public void PerformCarryItemReleaseCheck() {
        Rigidbody2D playerRigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        if(IsMoving(playerRigidbody)) {
            Vector3 dropVelocity = Vector3.zero;
            if(playerController != null) {
                if(playerController.facingRight) {
                    // dropVelocity = new Vector3(1, 1, 0);
                    dropVelocity = playerRigidbody.velocity * 2;
                    if(dropVelocity.x == 0) {
                        dropVelocity.x = 1f;
                    }
                    if(dropVelocity.y == 0) {
                        dropVelocity.y = 1f;
                    }
                }
                else {
                    // dropVelocity = new Vector3(-1, 1, 0);
                    dropVelocity = playerRigidbody.velocity * 2;
                    if(dropVelocity.x == 0) {
                        dropVelocity.x = -1f;
                    }
                    if(dropVelocity.y == 0) {
                        dropVelocity.y = 1f;
                    }
                }
            }
            carryItem.ThrowItem(dropVelocity);
        }
        else {
            Vector3 dropVelocity = Vector3.zero;
            if(playerController != null) {
                if(playerController.facingRight) {
                    dropVelocity = new Vector3(1, 1, 0);
                }
                else {
                    dropVelocity = new Vector3(-1, 1, 0);
                }
            }
            carryItem.DropItem(dropVelocity);
        }
    }
    
    public void PerformCinematicTriggerCheck(Collider2D collider) {
    }
    
    public void ToggleAcrocatic(GameObject acrocaticGameObject, bool enabled)  {
        acrocaticGameObject.GetComponent<Acrocatic.Player>().enabled = enabled;
        acrocaticGameObject.GetComponent<Acrocatic.PlayerRun>().enabled = enabled;
        acrocaticGameObject.GetComponent<Acrocatic.PlayerJump>().enabled = enabled;
        acrocaticGameObject.GetComponent<Acrocatic.PlayerWall>().enabled = enabled;
        acrocaticGameObject.GetComponent<Acrocatic.PlayerDash>().enabled = enabled;
        acrocaticGameObject.GetComponent<Acrocatic.PlayerHitbox>().enabled = enabled;
        acrocaticGameObject.GetComponent<Acrocatic.PlayerCrouch>().enabled = enabled;
        acrocaticGameObject.GetComponent<Acrocatic.PlayerPlatform>().enabled = enabled;
        acrocaticGameObject.GetComponent<Acrocatic.PlayerLadder>().enabled = enabled;
    }
    
    public void UpdateAnimator() {
    }
    
    public void Pause() {
        rigidbody2DSnapshot.Capture(this.gameObject);
        ToggleAcrocatic(this.gameObject, false);
        interactionController.Reset();
    }
    
    public void Unpause() {
        rigidbody2DSnapshot.Restore(this.gameObject);
        ToggleAcrocatic(this.gameObject, true);
        interactionController.Reset();
    }
    
    public void StartInteraction() {
        Pause();
    }
    
    public void StartGameplayState() {
        Unpause();
    }
}
