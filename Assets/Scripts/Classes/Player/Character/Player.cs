using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : BasePlayer {
    public List<Collider2D> colliders = null;
    public Dictionary<Collider2D, Collider2DSnapshot> collider2DSnapshots = null;
    
    public InteractionController interactionController = null;
    
    public Acrocatic.Player playerController = null;
    
    public Rigidbody2DSnapshot rigidbody2DSnapshot = null;
    
    public CarryItem carryItem = null;
    
    protected override void Awake() {
        Initialize();
    }
    
    // Use this for initialization
    protected override void Start() {
    }
    
    // Update is called once per frame
    protected override void Update() {
        PerformLogic();
    }
    
    protected override void Initialize() {
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
        
        rigidbody2DSnapshot = new Rigidbody2DSnapshot();
        //--------------------------
        colliders = GenerateColliderCache();
    }
    
    public bool IsDead() {
        return playerController.IsDead();
    }
    public void SetIsDead(bool newIsDead) {
        playerController.SetIsDead(newIsDead);
    }
    
    public Rigidbody2D GetRigidbody() {
        return this.gameObject.GetComponent<Rigidbody2D>();
    }
    
    public bool IsMovingVertically(Rigidbody2D rigidbody) {
        return (rigidbody != null
                && (rigidbody.velocity.y < 0 || rigidbody.velocity.y > 0)) ? true : false;
    }
    public bool IsMovingHorizontally(Rigidbody2D rigidbody) {
        return (rigidbody != null
                && (rigidbody.velocity.x < 0 || rigidbody.velocity.x > 0)) ? true : false;
    }
    
    public bool IsMoving(Rigidbody2D rigidbody) {
        return (IsMovingHorizontally(rigidbody) || IsMovingVertically(rigidbody));
    }
    
    public void ZeroOutVelocity() {
        GetRigidbody().velocity = Vector2.zero;
    }
    
    public InteractionController GetInteractionController() {
        return interactionController;
    }
    
    protected override void PerformLogic() {
        PerformInteractionCheck();
    }
    
    public void PerformInteractionCheck() {
        if(interactionController.IsActionButtonPressed()) {
            // Debug.Log("Action button pressed");
            InteractionTrigger currentInteractionTrigger = interactionController.GetNewestTrigger();
            if(currentInteractionTrigger != null) {
                // Debug.Log("currentInteractionTrigger is not null");
                if(carryItem.IsCarryingItem()) {
                    // Debug.Log("Carrying any item");
                    PerformCarryItemReleaseCheck();
                }
                else {
                    // Debug.Log("Interacting!");
                    currentInteractionTrigger.Interact(this.gameObject);
                }
            }
        }
    }
    
    // TODO: For real refactor this method, it's such a mess.
    public void PerformCarryItemReleaseCheck() {
        Rigidbody2D playerRigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        if(IsMovingHorizontally(playerRigidbody)) {
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
    
    public void ToggleController(GameObject acrocaticGameObject, bool enabled)  {
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
    
    private void ToggleColliders(List<Collider2D> colliders, bool isEnabled) {
        foreach(Collider2D collider2D in colliders) {
            collider2D.enabled = isEnabled;
        }
    }
    
    private List<Collider2D> GenerateColliderCache() {
        return new List<Collider2D>(this.gameObject.GetComponentsInChildren<Collider2D>());
    }
    private void CaptureCollider2DSnapshots(List<Collider2D> colliders, Dictionary<Collider2D, Collider2DSnapshot> colliderCache) {
        colliderCache.Clear();
        foreach(Collider2D collider2D in colliders) {
            Collider2DSnapshot snapshot = new Collider2DSnapshot();
            snapshot.Capture(collider2D);
            colliderCache[collider2D] = snapshot;
        }
    }
    
    public void UpdateAnimator() {
    }
    
    public override void Pause() {
        if(!isPaused) {
            // Debug.Log("Player paused");
            
            Rigidbody2D rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
            rigidbody2DSnapshot.Capture(rigidbody2D);
            rigidbody2D.isKinematic = true;
            //------
            // ToggleColliders(colliders, false);
            //------
            ToggleController(this.gameObject, false);
            //------
            interactionController.Reset();
            
            isPaused = true;
        }
    }
    
    public override void Unpause() {
        if(isPaused) {
            // Debug.Log("Player unpaused");
            
            Rigidbody2D rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
            rigidbody2DSnapshot.Restore(rigidbody2D);
            //------
            // ToggleColliders(colliders, true);
            //------
            ToggleController(this.gameObject, true);
            //------
            interactionController.Reset();
            
            isPaused = false;
        }
    }
    
    public void StartInteraction() {
        Pause();
    }
    
    public void StartGameplayState() {
        Unpause();
    }
}
