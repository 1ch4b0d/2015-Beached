using UnityEngine;
using System.Collections;
using InControl;

public class CustomPlayerActions : MonoBehaviour {
    static CustomPlayerActionSet characterActions;
    
    public static CustomPlayerActionSet Instance {
        get {
            if(characterActions == null) {
                Initialize();
            }
            return characterActions;
        }
    }
    
    protected void Awake() {
        if(characterActions == null) {
            Initialize();
        }
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
    
    protected void DebugInfo() {
        // Left Was Pressed
        Debug.Log("-----------------------------------------------------");
        Debug.Log("Left");
        Debug.Log("-----------------------------------------------------");
        DebugAction(characterActions.Left);
        // Debug.Log("-----------------------------------------------------");
        // Debug.Log("Right");
        // Debug.Log("-----------------------------------------------------");
        // DebugAction(characterActions.Right);
        // Debug.Log("-----------------------------------------------------");
        // Debug.Log("Jump");
        // Debug.Log("-----------------------------------------------------");
        // DebugAction(characterActions.Jump);
    }
    
    protected void DebugAction(PlayerAction playerAction) {
        Debug.Log("WasPressed: " + characterActions.Left.WasPressed);
        Debug.Log("IsPressed: " + characterActions.Left.IsPressed);
        Debug.Log("WasReleased: " + characterActions.Left.WasReleased);
        Debug.Log("HasChanged: " + characterActions.Left.HasChanged);
        Debug.Log("State: " + characterActions.Left.State);
        Debug.Log("Value: " + characterActions.Left.Value);
        Debug.Log("LastState: " + characterActions.Left.LastState);
        Debug.Log("LastValue: " + characterActions.Left.LastValue);
    }
}