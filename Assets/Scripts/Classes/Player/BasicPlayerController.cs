using UnityEngine;
using System.Collections;

public class BasicPlayerController : MonoBehaviour {
    public Vector2 movementSpeed = new Vector2(5f, 5f);
    
    public bool movingUp = false;
    public bool movingRight = false;
    public bool movingDown = false;
    public bool movingLeft = false;
    public bool actionButtonPressed = false;
    
    public bool disableController = false;
    
    // Use this for initialization
    void Start() {
    }
    
    // Update is called once per frame
    void Update() {
        // PerformLogic();
    }
    void LateUpdate() {
        actionButtonPressed = false;
    }
    
    public void Reset() {
        movingUp = false;
        movingRight = false;
        movingDown = false;
        movingLeft = false;
        actionButtonPressed = false;
    }
    
    public bool IsActionButtonPressed() {
        return actionButtonPressed;
    }
    
    public void PerformLogic() {
        PerformInputLogic();
        PerformMovementLogic();
    }
    
    protected void PerformInputLogic() {
        if(!disableController) {
            // Up
            if(Input.GetKeyDown(KeyCode.W)) {
                movingUp = true;
            }
            if(Input.GetKeyUp(KeyCode.W)) {
                movingUp = false;
            }
            
            // Right
            if(Input.GetKeyDown(KeyCode.D)) {
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
            }
            if(Input.GetKeyUp(KeyCode.A)) {
                movingLeft = false;
            }
        }
        
        // Action
        if(Input.GetKeyDown(KeyCode.J)) {
            actionButtonPressed = true;
        }
        if(Input.GetKeyUp(KeyCode.J)) {
            actionButtonPressed = false;
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
    
    protected void PerformMovementLogic() {
        Vector3 movementOffset = Vector3.zero;
        movementOffset = CalculateMovementOffset();
        movementOffset.z = 0f;
        movementOffset = movementOffset * Time.deltaTime;
        this.transform.position += movementOffset;
    }
}