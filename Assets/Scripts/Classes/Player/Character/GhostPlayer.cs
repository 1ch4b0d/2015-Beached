﻿using UnityEngine;
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
        if(!IsPaused()) {
            base.Update();
            PerformFlipCheck();
        }
    }
    
    protected override void PerformLogic() {
        Vector3 movementOffset = characterMovement.CalculateMovementOffset(characterController);
        characterMovement.PerformMovementLogic(this.gameObject, movementOffset);
    }
    
    protected void PerformFlipCheck() {
        if((characterController.IsFacingLeft()
            && this.gameObject.transform.localScale.x > 0)
            ||
            (characterController.IsFacingRight()
             && this.gameObject.transform.localScale.x < 0)) {
            this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x * -1,
                                                               this.gameObject.transform.localScale.y,
                                                               this.gameObject.transform.localScale.z);
        }
    }
    
    protected override void Initialize() {
        base.Initialize();
        if(characterController == null) {
            characterController = this.gameObject.GetComponent<CustomCharacterController>();
            if(characterController == null) {
                this.gameObject.LogComponentError("characterController", this.GetType());
            }
        }
        
        if(characterMovement == null) {
            characterMovement = this.gameObject.GetComponent<CharacterMovement>();
            if(characterMovement == null) {
                this.gameObject.LogComponentError("characterMovement", this.GetType());
            }
        }
    }
    
    public override void Pause() {
        characterController.Pause();
    }
    
    public override void Unpause() {
        characterController.Unpause();
    }
}
