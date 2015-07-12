using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemInteractionTrigger : InteractionTrigger {
    public GameObject itemGameObject = null;
    // public SpeechBubble speechBubble = null;
    public float speechBubbleFadeSpeed = 0.25f;
    
    protected override void Awake() {
    }
    
    // Use this for initialization
    protected override void Start() {
        Initialize();
    }
    
    // Update is called once per frame
    protected override void Update() {
    }
    
    protected override void Initialize() {
        if(itemGameObject == null) {
            itemGameObject = Utility.GetFirstParentOfType<Item>(this.gameObject).gameObject;
            if(itemGameObject == null) {
                Debug.LogError("Could not find the " + this.gameObject.name + ": itemGameObject");
            }
        }
    }
    
    public override void Entered(GameObject gameObjectEntering) {
        // Debug.Log("Item Trigger Entered");
        CarryItem carryItem = gameObjectEntering.GetComponent<CarryItem>();
        if(carryItem != null) {
            if(carryItem.itemBeingCarried == null) {
                ShowSpeechBubble(speechBubbleFadeSpeed);
            }
        }
        else {
            ShowSpeechBubble(speechBubbleFadeSpeed);
        }
    }
    
    public override void Exited(GameObject gameObjectExiting) {
        // Debug.Log("Item Trigger Exited");
        CarryItem carryItem = gameObjectExiting.GetComponent<CarryItem>();
        if(carryItem != null) {
            if(carryItem.itemBeingCarried == null) {
                HideSpeechBubble(speechBubbleFadeSpeed);
            }
        }
        else {
            HideSpeechBubble(speechBubbleFadeSpeed);
        }
    }
    
    public override void ExecuteLogic(GameObject gameObjectExecuting) {
        // Debug.Log("Item Triggered Interaction");
        CarryItem carryItem = gameObjectExecuting.GetComponent<CarryItem>();
        if(itemGameObject != null
            && carryItem != null
            && carryItem.itemBeingCarried == null) {
            // Debug.Log("give this player an item!!!");
            carryItem.PickUpItem(itemGameObject);
            HideSpeechBubble(speechBubbleFadeSpeed);
        }
    }
}
