using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InControl;

public class PlayerInteractionController : InteractionController {
    public CustomPlayerActions customPlayerActions;
    
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
    
    protected override void LateUpdate() {
        base.LateUpdate();
    }
    
    protected override void Initialize() {
        base.Initialize();
        if(customPlayerActions == null) {
            this.gameObject.LogComponentError("customPlayerActions", this.GetType());
        }
    }
    
    protected override void PerformInputLogic() {
        // customPlayerActions.GetCustomPlayerActionSet().Interact.WasPressed
        if(customPlayerActions.GetCustomPlayerActionSet().Interact.WasPressed) {
            interactionButtonPressed = true;
        }
        if(!customPlayerActions.GetCustomPlayerActionSet().Interact.WasPressed) {
            interactionButtonPressed = false;
        }
    }
}