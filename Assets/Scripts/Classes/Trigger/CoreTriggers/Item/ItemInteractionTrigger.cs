using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemInteractionTrigger : SpeechBubbleInteractionTrigger {
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
            itemGameObject = this.gameObject.GetFirstParent<Item>().gameObject;
            if(itemGameObject == null) {
                Debug.LogError("Could not find the " + this.gameObject.name + ": itemGameObject");
            }
        }
    }
    
    // TODO: Look into this speechbubble fade speed thing. It looks poorly named
    //       and seems... suspicious
    public override void Entered(GameObject gameObjectEntering) {
        // Debug.Log("Item Trigger Entered");
        CarryItem carryItem = gameObjectEntering.GetComponent<CarryItem>();
        if(carryItem != null) {
            if(carryItem.itemBeingCarried == null) {
                ShowSpeechBubble(speechBubbleFadeSpeed, speechBubbleFadeSpeed);
            }
        }
        else {
            ShowSpeechBubble(speechBubbleFadeSpeed, speechBubbleFadeSpeed);
        }
    }
    
    // TODO: Look into this speechbubble fade speed thing. It looks poorly named
    //       and seems... suspicious
    public override void Exited(GameObject gameObjectExiting) {
        // Debug.Log("Item Trigger Exited");
        CarryItem carryItem = gameObjectExiting.GetComponent<CarryItem>();
        if(carryItem != null) {
            if(carryItem.itemBeingCarried == null) {
                HideSpeechBubble(speechBubbleFadeSpeed, speechBubbleFadeSpeed);
            }
        }
        else {
            HideSpeechBubble(speechBubbleFadeSpeed, speechBubbleFadeSpeed);
        }
    }
    
    // TODO: Look into this speechbubble fade speed thing. It looks poorly named
    //       and seems... suspicious
    public override void ExecuteLogic(GameObject gameObjectExecuting) {
        // Debug.Log("Item Triggered Interaction");
        CarryItem carryItem = gameObjectExecuting.GetComponent<CarryItem>();
        if(itemGameObject != null
            && carryItem != null
            && carryItem.itemBeingCarried == null) {
            // Debug.Log("give this player an item!!!");
            carryItem.PickUpItem(itemGameObject);
            HideSpeechBubble(speechBubbleFadeSpeed, speechBubbleFadeSpeed);
        }
    }
    
    public override void ShowSpeechBubble(float horizontalScaleDuration, float verticalScaleDuration) {
        base.ShowSpeechBubble(horizontalScaleDuration, verticalScaleDuration);
    }
    
    public override void HideSpeechBubble(float horizontalScaleDuration, float verticalScaleDuration) {
        base.HideSpeechBubble(horizontalScaleDuration, verticalScaleDuration);
    }
}
