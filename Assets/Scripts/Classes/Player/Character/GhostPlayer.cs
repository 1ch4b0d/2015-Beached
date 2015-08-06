using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GhostPlayer : BasePlayer {
    public CustomCharacterController characterController = null;
    public CharacterMovement characterMovement = null;
    
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
    
    protected override void PerformLogic() {
        Vector3 movementOffset = characterMovement.CalculateMovementOffset(characterController);
        characterMovement.PerformMovementLogic(this.gameObject, movementOffset);
    }
    
    protected override void Initialize() {
        base.Initialize();
        if(characterController == null) {
            characterController = this.gameObject.GetComponent<CustomCharacterController>();
            if(characterController == null) {
                Debug.LogError(this.gameObject.name + " needs to set its characterController in order to be used.");
            }
        }
        
        if(characterMovement == null) {
            characterMovement = this.gameObject.GetComponent<CharacterMovement>();
            if(characterMovement == null) {
                Debug.LogError(this.gameObject.name + " needs to set its characterMovement in order to be used.");
            }
        }
    }
}
