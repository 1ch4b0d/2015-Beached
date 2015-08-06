using UnityEngine;
using System.Collections;

public class CustomCharacterController : MonoBehaviour {
    // public bool movingUp = false;
    public bool movingRight = false;
    // public bool movingDown = false;
    public bool movingLeft = false;
    public bool isJumping = false;
    
    public bool isPaused = false;
    
    // Use this for initialization
    protected virtual void Awake() {
    }
    
    // Use this for initialization
    protected virtual void Start() {
    }
    
    // Update is called once per frame
    protected virtual void Update() {
        if(!isPaused) {
            PerformLogic();
        }
    }
    
    // Update is called once per frame
    protected virtual void LateUpdate() {
    }
    
    public void Reset() {
        // movingUp = false;
        movingRight = false;
        // movingDown = false;
        movingLeft = false;
        isJumping = false;
    }
    
    public void PerformLogic() {
        PerformInputLogic();
    }
    
    protected void PerformInputLogic() {
        // Horizontal movement (Left/Right)
        // Right
        if(InputManager.Instance.GetAxis("Horizontal") > 0) {
            movingRight = true;
            movingLeft = false;
        }
        // Left
        else if(InputManager.Instance.GetAxis("Horizontal") < 0) {
            movingRight = false;
            movingLeft = true;
        }
        else {
            movingRight = false;
            movingLeft = false;
        }
        
        // Jumping
        if(InputManager.Instance.GetButtonDown("Jump")) {
            isJumping = true;
        }
        if(InputManager.Instance.GetButtonUp("Jump")) {
            isJumping = false;
        }
        
        // // Vertical Movement (Up/Down)
        // // Up
        // if(InputManager.Instance.GetKeyDown(KeyCode.W)) {
        //     movingUp = true;
        // }
        // if(InputManager.Instance.GetKeyUp(KeyCode.W)) {
        //     movingUp = false;
        // }
        // // Down
        // if(InputManager.Instance.GetKeyDown(KeyCode.S)) {
        //     movingDown = true;
        // }
        // if(InputManager.Instance.GetKeyUp(KeyCode.S)) {
        //     movingDown = false;
        // }
    }
}
