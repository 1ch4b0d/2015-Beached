using UnityEngine;
using System.Collections;

public class CustomCharacterController : MonoBehaviour {
    // public bool movingUp = false;
    public bool facingRight = false;
    // public bool movingDown = false;
    public bool isMoving = false;
    public bool isJumping = false;
    
    public bool isPaused = false;
    
    public Animator animator = null;
    
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
            UpdateAnimator();
        }
    }
    
    // Update is called once per frame
    protected virtual void LateUpdate() {
    }
    
    public virtual void Pause() {
        isPaused = true;
    }
    
    public virtual void Unpause() {
        isPaused = false;
    }
    
    public bool IsFacingLeft() {
        return !facingRight;
    }
    public bool IsFacingRight() {
        return facingRight;
    }
    
    public bool IsMovingLeft() {
        return (isMoving && !facingRight);
    }
    public bool IsMovingRight() {
        return (isMoving && facingRight);
    }
    
    public bool IsJumping() {
        return isJumping;
    }
    
    public void Reset() {
        isMoving = false;
        isJumping = false;
    }
    
    public void PerformLogic() {
        PerformInputLogic();
    }
    
    protected void PerformInputLogic() {
        // Horizontal movement (Left/Right)
        // Right
        if(SimpleInputManager.Instance.GetAxis("Horizontal") > 0) {
            isMoving = true;
            facingRight = true;
        }
        // Left
        else if(SimpleInputManager.Instance.GetAxis("Horizontal") < 0) {
            isMoving = true;
            facingRight = false;
        }
        else {
            isMoving = false;
        }
        
        // Jumping
        if(SimpleInputManager.Instance.GetButtonDown("Jump")) {
            isJumping = true;
        }
        if(SimpleInputManager.Instance.GetButtonUp("Jump")) {
            isJumping = false;
        }
        
        // // Vertical Movement (Up/Down)
        // // Up
        // if(SimpleInputManager.Instance.GetKeyDown(KeyCode.W)) {
        //     movingUp = true;
        // }
        // if(SimpleInputManager.Instance.GetKeyUp(KeyCode.W)) {
        //     movingUp = false;
        // }
        // // Down
        // if(SimpleInputManager.Instance.GetKeyDown(KeyCode.S)) {
        //     movingDown = true;
        // }
        // if(SimpleInputManager.Instance.GetKeyUp(KeyCode.S)) {
        //     movingDown = false;
        // }
    }
    
    protected virtual void UpdateAnimator() {
        if(animator != null) {
            animator.SetBool("isMoving", isMoving);
            animator.SetBool("facingRight", facingRight);
            animator.SetBool("isJumping", isJumping);
        }
    }
}
