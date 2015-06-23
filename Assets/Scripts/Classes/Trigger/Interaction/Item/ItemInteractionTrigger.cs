using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemInteractionTrigger : InteractionTrigger {
    public GameObject itemGameObject = null;
    void Awake() {
    }
    
    // Use this for initialization
    void Start() {
        Initialize();
    }
    
    // Update is called once per frame
    void Update() {
    }
    
    public override void Initialize() {
        if(itemGameObject == null) {
            Debug.LogError("Could not find the " + this.gameObject.name + ": itemGameObject");
        }
    }
    
    public override void Entered(GameObject gameObjectEntering) {
        Debug.Log("Item Trigger Entered");
        // if(speechBubble != null) {
        //     speechBubble.Show(0.25f);
        // }
    }
    
    public override void Exited(GameObject gameObjectExiting) {
        Debug.Log("Item Trigger Exited");
        // if(speechBubble != null) {
        //     speechBubble.Hide(0.25f);
        // }
    }
    
    // public override void Execute(GameObject gameObjectToExecute) {
    // Perform only if it's the first iteration, or it should loop
    // if(currentIteration < 1
    //     || loop) {
    //     ExecuteLogic(gameObjectToExecute);
    //     // This isn't used in the execute logic, this is iterated in the ExecuteLogic function
    //     // currentIteration++;
    
    //     if(loop == false) {
    //         this.GetComponent<Collider2D>().enabled = false;
    //     }
    // }
    // }
    
    public override void ExecuteLogic(GameObject gameObjectExecuting) {
        // Debug.Log("Item Triggered Interaction");
        
        CarryItem carryItem = gameObjectExecuting.GetComponent<CarryItem>();
        if(carryItem != null
            && carryItem.itemBeingCarried == null) {
            // Debug.Log("give this player an item!!!");
            carryItem.PickUpItem(itemGameObject);
        }
    }
}
