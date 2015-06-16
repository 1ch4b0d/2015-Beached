using UnityEngine;
using System.Collections;

public class InteractionController : MonoBehaviour {
    public bool interactionButtonPressed = false;
    
    public bool disableController = false;
    
    // Use this for initialization
    void Start() {
    }
    
    // Update is called once per frame
    void Update() {
        // PerformLogic();
    }
    void LateUpdate() {
        interactionButtonPressed = false;
    }
    
    public void Reset() {
        interactionButtonPressed = false;
    }
    
    public bool IsActionButtonPressed() {
        return interactionButtonPressed;
    }
    
    public void PerformLogic() {
        PerformInputLogic();
    }
    
    protected void PerformInputLogic() {
        // Action
        if(Input.GetKeyDown(KeyCode.J)) {
            interactionButtonPressed = true;
        }
        if(Input.GetKeyUp(KeyCode.J)) {
            interactionButtonPressed = false;
        }
    }
}