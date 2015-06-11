using UnityEngine;
using System.Collections;

public class BasicPlayerController : MonoBehaviour {
    public InteractionTrigger interactionTrigger = null;
    
    public Vector2 movementSpeed = new Vector2(5f, 5f);
    
    public bool movingUp = false;
    public bool movingRight = false;
    public bool movingDown = false;
    public bool movingLeft = false;
    public bool actionButtonPressed = false;
    
    // Use this for initialization
    void Start() {
    }
    
    // Update is called once per frame
    void Update() {
        PerformLogic();
    }
    
    public void PerformLogic() {
        PerformInputLogic();
        
        Vector3 movementOffset = Vector3.zero;
        movementOffset = CalculateMovementOffset();
        movementOffset.z = 0f;
        movementOffset = movementOffset * Time.deltaTime;
        this.transform.position += movementOffset;
        
        if(actionButtonPressed) {
            PerformActionLogic();
        }
        
        // this is reset everytime at the end of the logic, same goes for jumping
        actionButtonPressed = false;
    }
    
    public void OnTriggerEnter2D(Collider2D collider) {
        InteractionTrigger colliderInteractionTrigger = collider.gameObject.GetComponent<InteractionTrigger>();
        if(colliderInteractionTrigger != null) {
            interactionTrigger = colliderInteractionTrigger;
            interactionTrigger.Entered();
        }
    }
    
    public void OnTrigger2D(Collider2D collider) {
    }
    
    public void OnTriggerExit2D(Collider2D collider) {
        if(interactionTrigger != null) {
            // if the object being exited is the same as the one assigned
            if(collider.gameObject.GetInstanceID() == interactionTrigger.gameObject.GetInstanceID()) {
                interactionTrigger.Exited();
                interactionTrigger = null;
            }
        }
    }
    
    protected void PerformInputLogic() {
        // Up
        if(Input.GetKeyDown(KeyCode.W)) {
            movingUp = true;
        }
        if(Input.GetKeyUp(KeyCode.W)) {
            movingUp = false;
        }
        
        // Right
        if(Input.GetKeyDown(KeyCode.D)) {
            movingLeft = false;
            movingRight = true;
        }
        if(Input.GetKeyUp(KeyCode.D)) {
            movingRight = false;
        }
        
        // Down
        if(Input.GetKeyDown(KeyCode.S)) {
            movingDown = true;
        }
        if(Input.GetKeyUp(KeyCode.S)) {
            movingDown = false;
        }
        
        // Left
        if(Input.GetKeyDown(KeyCode.A)) {
            movingLeft = true;
            movingRight = false;
        }
        if(Input.GetKeyUp(KeyCode.A)) {
            movingLeft = false;
        }
        
        // Action
        if(Input.GetKeyDown(KeyCode.Space)) {
            actionButtonPressed = true;
        }
    }
    
    protected Vector3 CalculateMovementOffset() {
        Vector3 movementOffset = Vector3.zero;
        
        // Top
        if(movingUp) {
            movementOffset.y += movementSpeed.y;
        }
        // Right
        if(movingRight) {
            movementOffset.x += movementSpeed.x;
        }
        // Down
        if(movingDown) {
            movementOffset.y -= movementSpeed.y;
        }
        // Left
        if(movingLeft) {
            movementOffset.x -= movementSpeed.x;
        }
        
        movementOffset.z = 0f;
        
        return movementOffset;
    }
    
    protected void UpdateAnimator() {
    }
    
    protected void PerformActionLogic() {
        Debug.Log("Performing Action");
        if(interactionTrigger != null) {
            interactionTrigger.Interact();
        }
    }
}