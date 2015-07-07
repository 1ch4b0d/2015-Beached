using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
    public InteractionController interactionController = null;
    
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
    
    public Rigidbody2D GetRigidbody() {
        return this.gameObject.GetComponent<Rigidbody2D>();
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
    
    public void ZeroOutVelocity() {
        GetRigidbody().velocity = Vector2.zero;
    }
    
    public InteractionController GetInteractionController() {
        return interactionController;
    }
    
    protected void PerformLogic() {
        PerformInteractionCheck();
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
            //--------------------------------------------------------
            // This removes the item from the interaction controller in
            // order to guarantee that the player doesn't retain access to the
            // item after having dropped or thrown it
            //--------------------------------------------------------
            // - This should really be compartmentalized into the carryItemScript
            //      somehow, but I'm not even remotely sure how to hook this up
            //      In the future figure out how to smooth this out, but in the
            //      meantime let it reside in the player script
            //--------------------------------------------------------
            if(carryItem.itemBeingCarried != null) {
                foreach(Collider2D trigger in carryItem.itemBeingCarried.GetComponent<Item>().triggers) {
                    InteractionTrigger interactionTrigger = trigger.gameObject.GetComponent<InteractionTrigger>();
                    if(interactionTrigger != null) {
                        interactionController.RemoveTrigger(interactionTrigger);
                    }
                }
                carryItem.ThrowItem(dropVelocity);
            }
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
            //--------------------------------------------------------
            // This removes the item from the interaction controller in
            // order to guarantee that the player doesn't retain access to the
            // item after having dropped or thrown it
            //--------------------------------------------------------
            // - This should really be compartmentalized into the carryItemScript
            //      somehow, but I'm not even remotely sure how to hook this up
            //      In the future figure out how to smooth this out, but in the
            //      meantime let it reside in the player script
            //--------------------------------------------------------
            if(carryItem.itemBeingCarried != null) {
                foreach(Collider2D trigger in carryItem.itemBeingCarried.GetComponent<Item>().triggers) {
                    InteractionTrigger interactionTrigger = trigger.gameObject.GetComponent<InteractionTrigger>();
                    if(interactionTrigger != null) {
                        interactionController.RemoveTrigger(interactionTrigger);
                    }
                }
                carryItem.DropItem(dropVelocity);
            }
        }
    }
    
    public void PerformCinematicTriggerCheck(Collider2D collider) {
    }
    
    public void ToggleAcrocatic(GameObject acrocaticGameObject, bool enabled)  {
        Acrocatic.Player acrocaticPlayer = acrocaticGameObject.GetComponent<Acrocatic.Player>();
        if(acrocaticPlayer) {
            acrocaticPlayer.enabled = enabled;
        }
        Acrocatic.PlayerRun acrocaticPlayerRun = acrocaticGameObject.GetComponent<Acrocatic.PlayerRun>();
        if(acrocaticPlayerRun) {
            acrocaticPlayerRun.enabled = enabled;
        }
        Acrocatic.PlayerJump acrocaticPlayerJump = acrocaticGameObject.GetComponent<Acrocatic.PlayerJump>();
        if(acrocaticPlayerJump) {
            acrocaticPlayerJump.enabled = enabled;
        }
        Acrocatic.PlayerWall acrocaticPlayerWall = acrocaticGameObject.GetComponent<Acrocatic.PlayerWall>();
        if(acrocaticPlayerWall) {
            acrocaticPlayerWall.enabled = enabled;
        }
        Acrocatic.PlayerDash acrocaticPlayerDash = acrocaticGameObject.GetComponent<Acrocatic.PlayerDash>();
        if(acrocaticPlayerDash) {
            acrocaticPlayerDash.enabled = enabled;
        }
        Acrocatic.PlayerHitbox acrocaticPlayerHitbox = acrocaticGameObject.GetComponent<Acrocatic.PlayerHitbox>();
        if(acrocaticPlayerHitbox) {
            acrocaticPlayerHitbox.enabled = enabled;
        }
        Acrocatic.PlayerCrouch acrocaticPlayerCrouch = acrocaticGameObject.GetComponent<Acrocatic.PlayerCrouch>();
        if(acrocaticPlayerCrouch) {
            acrocaticPlayerCrouch.enabled = enabled;
        }
        Acrocatic.PlayerPlatform acrocaticPlayerPlatform = acrocaticGameObject.GetComponent<Acrocatic.PlayerPlatform>();
        if(acrocaticPlayerPlatform) {
            acrocaticPlayerPlatform.enabled = enabled;
        }
        Acrocatic.PlayerLadder acrocaticPlayerLadder = acrocaticGameObject.GetComponent<Acrocatic.PlayerLadder>();
        if(acrocaticPlayerLadder) {
            acrocaticPlayerLadder.enabled = enabled;
        }
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
