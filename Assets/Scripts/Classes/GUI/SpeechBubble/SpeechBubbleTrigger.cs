using UnityEngine;
using System.Collections;

public class SpeechBubbleTrigger : InteractionTrigger {
    public SpeechBubble speechBubble = null;
    
    // Use this for initialization
    void Start() {
        Initialize();
    }
    
    // Update is called once per frame
    void Update() {
    }
    
    // public void OnTriggerEnter2D(Collider2D collider) {
    // }
    // public void OnTrigger2D(Collider2D collider) {
    // }
    // public void OnTriggerExit2D(Collider2D collider) {
    // }
    
    public void Initialize() {
    }
    
    public override void Entered() {
        // Debug.Log("SpeechBubble Trigger Entered");
        if(speechBubble != null) {
            speechBubble.Show(0.25f);
        }
    }
    
    public override void Exited() {
        // Debug.Log("SpeechBubble Trigger Exited");
        if(speechBubble != null) {
            speechBubble.Hide(0.25f);
        }
    }
    
    public override void Interact() {
        Debug.Log("SpeechBubble Triggered Interaction");
    }
}