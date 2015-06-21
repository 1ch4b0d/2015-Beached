using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public CinematicTrigger currentCinematicTrigger = null;
    
    public InteractionTrigger currentInteractionTrigger = null;
    public InteractionController interactionController = null;
    
    public PlayerController playerController = null;
    public Rigidbody2DSnapshot rigidbody2DSnapshot = null;
    
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
        // if(playerController == null) {
        // Debug.LogError("Could not find the " + gameObject.name + ": playerController");
        // }
    }
    
    protected void PerformLogic() {
        if(playerController != null) {
            playerController.PerformLogic();
        }
        //-----------------------------------
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
        //--------------
        // currentCinematicTrigger = collider.gameObject.GetComponent<CinematicTrigger>();
        // if(currentCinematicTrigger != null) {
        //     currentCinematicTrigger.Entered(this.gameObject);
        // }
    }
    
    // public void OnTrigger2D(Collider2D collider) {
    // }
    
    public void OnTriggerExit2D(Collider2D collider) {
        if(currentInteractionTrigger != null) {
            // if the object being exited is the same as the one assigned
            if(collider.gameObject.GetInstanceID() == currentInteractionTrigger.gameObject.GetInstanceID()) {
                currentInteractionTrigger.Exited(this.gameObject);
                currentInteractionTrigger = null;
            }
        }
        //--------------
        // if(currentCinematicTrigger != null) {
        //     // if the object being exited is the same as the one assigned
        //     if(collider.gameObject.GetInstanceID() == currentCinematicTrigger.gameObject.GetInstanceID()) {
        //         currentCinematicTrigger.Exited(this.gameObject);
        //         currentCinematicTrigger = null;
        //     }
        // }
    }
    
    public void PerformCinematicTrigerCheck(Collider2D collider) {
    
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
        if(rigidbody2DSnapshot == null) {
            rigidbody2DSnapshot = new Rigidbody2DSnapshot();
        }
        rigidbody2DSnapshot.Pause(this.gameObject);
        ToggleAcrocatic(this.gameObject, false);
        interactionController.Reset();
    }
    
    public void Unpause() {
        rigidbody2DSnapshot.Unpause(this.gameObject);
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
