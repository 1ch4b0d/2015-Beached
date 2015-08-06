using UnityEngine;
using System.Collections;

/// <summary>
/// This is a class that uses movement not based on Unity's
/// physics system. Instead it transforms an object over a
/// distance by calculating and moving each step forward.
/// </summary>
public class CharacterMovement : MonoBehaviour {
    public Vector3 movementSpeed = new Vector3(5, 5);
    private bool disableZ = true;
    
    // Use this for initialization
    protected virtual void Awake() {
        Initialize();
    }
    
    // Use this for initialization
    protected virtual void Start() {
    }
    
    // Update is called once per frame
    protected virtual void Update() {
    }
    
    protected virtual void Initialize() {
    }
    
    public virtual Vector3 CalculateMovementOffset(CustomCharacterController characterController) {
        Vector3 movementOffset = Vector3.zero;
        
        // Right
        if(characterController.movingRight) {
            movementOffset.x += movementSpeed.x;
        }
        // Left
        if(characterController.movingLeft) {
            movementOffset.x -= movementSpeed.x;
        }
        
        // Jumping
        if(characterController.isJumping) {
            movementOffset.y += movementSpeed.y;
        }
        
        // // Top
        // if(movingUp) {
        //     movementOffset.y += movementSpeed.y;
        // }
        // // Down
        // if(movingDown) {
        //     movementOffset.y -= movementSpeed.y;
        // }
        
        if(disableZ) {
            movementOffset.z = 0f;
        }
        
        return movementOffset;
    }
    
    public virtual void PerformMovementLogic(GameObject gameObjecToMove, Vector3 movementOffset) {
        movementOffset = movementOffset * Time.deltaTime;
        gameObjecToMove.transform.position += movementOffset;
    }
}
