using UnityEngine;
using System.Collections;

public class CharacterRigidbody2DMovement : CharacterMovement {
    // Use this for initialization
    protected override void Awake() {
        base.Awake();
    }
    
    // Use this for initialization
    protected override void Start() {
        base.Start();
    }
    
    // Update is called once per frame
    protected override void Update() {
        base.Update();
    }
    
    protected override void Initialize() {
        base.Initialize();
    }
    
    public override Vector3 CalculateMovementOffset(CustomCharacterController characterController) {
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
        
        
        movementOffset.z = 0f;
        
        return movementOffset;
    }
    
    public override void PerformMovementLogic(GameObject gameObjecToMove, Vector3 movementOffset) {
        movementOffset.z = 0f;
        gameObjecToMove.GetComponent<Rigidbody2D>().AddForce(movementOffset);
    }
}
