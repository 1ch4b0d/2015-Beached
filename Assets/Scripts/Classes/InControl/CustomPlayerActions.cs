using UnityEngine;
using System.Collections;
using InControl;

public class CustomPlayerActions : MonoBehaviour {
    static CustomPlayerActionSet characterActions;
    
    protected void Awake() {
        Initialize();
    }
    
    protected void Start() {
    }
    
    protected void Update() {
        DebugInfo();
    }
    
    protected void LateUpdate() {
    }
    
    protected static void Initialize() {
        characterActions = new CustomPlayerActionSet();
        //---------------------------------------------
        // Left
        characterActions.Left.AddDefaultBinding(Key.A);
        characterActions.Left.AddDefaultBinding(Key.LeftArrow);
        characterActions.Left.AddDefaultBinding(InputControlType.DPadLeft);
        characterActions.Left.AddDefaultBinding(InputControlType.LeftStickLeft);
        
        // Right
        characterActions.Right.AddDefaultBinding(Key.D);
        characterActions.Right.AddDefaultBinding(Key.RightArrow);
        characterActions.Right.AddDefaultBinding(InputControlType.DPadRight);
        characterActions.Right.AddDefaultBinding(InputControlType.LeftStickRight);
        
        // Interact
        characterActions.Interact.AddDefaultBinding(Key.J);
        characterActions.Interact.AddDefaultBinding(InputControlType.Action1);
        
        // Jump
        characterActions.Jump.AddDefaultBinding(Key.Space);
        characterActions.Jump.AddDefaultBinding(InputControlType.Action2);
        
        // Dash
        // characterActions.Dash.AddDefaultBinding(Key.J);
        // characterActions.Dash.AddDefaultBinding(InputControlType.Action2);
    }
    
    public CustomPlayerActionSet GetCustomPlayerActionSet() {
        return characterActions;
    }
    
    protected void DebugInfo() {
        // Left Was Pressed
        // Debug.Log("-----------------------------------------------------");
        // Debug.Log("Left");
        // Debug.Log("-----------------------------------------------------");
        // DebugAction(characterActions.Left);
        // Debug.Log("-----------------------------------------------------");
        // Debug.Log("Right");
        // Debug.Log("-----------------------------------------------------");
        // DebugAction(characterActions.Right);
        // Debug.Log("-----------------------------------------------------");
        // Debug.Log("Move");
        // Debug.Log("-----------------------------------------------------");
        // DebugOneAxisAction(characterActions.Move);
        // Debug.Log("-----------------------------------------------------");
        // Debug.Log("Jump");
        // Debug.Log("-----------------------------------------------------");
        // DebugAction(characterActions.Jump);
    }
    
    protected void DebugAction(PlayerAction playerAction) {
        Debug.Log("HasChanged: " + playerAction.HasChanged);
        Debug.Log("IsPressed: " + playerAction.IsPressed);
        Debug.Log("LastState: " + playerAction.LastState);
        Debug.Log("LastValue: " + playerAction.LastValue);
        Debug.Log("LowerDeadZone: " + playerAction.LowerDeadZone);
        Debug.Log("Raw Value: " + playerAction.RawValue);
        Debug.Log("Sensitivity: " + playerAction.Sensitivity);
        Debug.Log("State: " + playerAction.State);
        Debug.Log("StateThreshold: " + playerAction.StateThreshold);
        Debug.Log("WasPressed: " + playerAction.WasPressed);
        Debug.Log("WasReleased: " + playerAction.WasReleased);
        Debug.Log("WasRepeated: " + playerAction.WasRepeated);
        Debug.Log("Value: " + playerAction.Value);
    }
    
    protected void DebugOneAxisAction(PlayerOneAxisAction playerOneAxisAction) {
        Debug.Log("HasChanged: " + playerOneAxisAction.HasChanged);
        Debug.Log("IsPressed: " + playerOneAxisAction.IsPressed);
        Debug.Log("LastState: " + playerOneAxisAction.LastState);
        Debug.Log("LastValue: " + playerOneAxisAction.LastValue);
        Debug.Log("LowerDeadZone: " + playerOneAxisAction.LowerDeadZone);
        Debug.Log("Raw Value: " + playerOneAxisAction.RawValue);
        Debug.Log("Sensitivity: " + playerOneAxisAction.Sensitivity);
        Debug.Log("State: " + playerOneAxisAction.State);
        Debug.Log("StateThreshold: " + playerOneAxisAction.StateThreshold);
        Debug.Log("WasPressed: " + playerOneAxisAction.WasPressed);
        Debug.Log("WasReleased: " + playerOneAxisAction.WasReleased);
        Debug.Log("WasRepeated: " + playerOneAxisAction.WasRepeated);
        Debug.Log("Value: " + playerOneAxisAction.Value);
    }
}