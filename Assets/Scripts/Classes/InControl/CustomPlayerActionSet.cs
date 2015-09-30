using UnityEngine;
using System.Collections;
using InControl;

public class CustomPlayerActionSet : PlayerActionSet {
    public PlayerAction Left;
    public PlayerAction Right;
    public PlayerAction Jump;
    // public PlayerAction Dash;
    public PlayerAction Interact;
    
    public PlayerOneAxisAction Move;
    
    public CustomPlayerActionSet() {
        Left = CreatePlayerAction("Move Left");
        Right = CreatePlayerAction("Move Right");
        Jump = CreatePlayerAction("Jump");
        // Dash = CreatePlayerAction("Dash");
        Interact = CreatePlayerAction("Interact");
        Move = CreateOneAxisPlayerAction(Left, Right);
    }
}

