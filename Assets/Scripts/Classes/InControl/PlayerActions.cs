using UnityEngine;
using InControl;

public class PlayerActions : PlayerActionSet {
    public PlayerAction Left;
    public PlayerAction Right;
    public PlayerAction Jump;
    public PlayerAction Interact;
    public PlayerOneAxisAction Move;
    
    public PlayerActions() {
        Left = CreatePlayerAction("Move Left");
        Right = CreatePlayerAction("Move Right");
        Jump = CreatePlayerAction("Jump");
        Interact = CreatePlayerAction("Interact");
        Move = CreateOneAxisPlayerAction(Left, Right);
    }
}